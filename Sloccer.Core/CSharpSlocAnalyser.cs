using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharpExtensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.Text;

namespace Sloccer.Core
{
    public class CSharpSlocAnalyser : ISlocAnalyser
    {
        public long GetSlocFor(IEnumerable<FileInfo> files, SlocOptions options)
        {
            var tree = CSharpSyntaxTree.ParseText(
@"using /* Blah */
    System;
// Blah
public class MyClass
{
    public void MyMethod()
    {
        var blah = ""abc"";
    }
}");

            var root = tree.GetRoot();
            var walker = new CustomWalker();
            walker.Visit(root);
            var lineMap = walker.LineMap;

            foreach(var line in lineMap.OrderBy(l => l.Key))
            {
                System.Diagnostics.Debug.WriteLine("Line {0}", line.Key);
                foreach(var b in line.Value)
                {
                    if(b is SyntaxToken)
                    {
                        var token = (SyntaxToken)b;
                        System.Diagnostics.Debug.WriteLine("\t{0} - '{1}'", token.CSharpKind(), token.Text);
                    }
                    else
                    {
                        var trivia = (SyntaxTrivia)b;
                        System.Diagnostics.Debug.WriteLine("\t{0} - '{1}'", trivia.CSharpKind(), trivia);
                    }
                }
            }

            return 1;
        }

        public class CustomWalker : CSharpSyntaxWalker
        {
            public Dictionary<int, List<object>> LineMap { get; }

            public CustomWalker() : base(SyntaxWalkerDepth.StructuredTrivia)
            {
                LineMap = new Dictionary<int, List<object>>();
            }

            public override void VisitToken(SyntaxToken token)
            {
                var parent = token.SyntaxTree.GetRoot();

                AddLine(token, token.Span.Start, parent);

                base.VisitToken(token);
            }

            public override void VisitTrivia(SyntaxTrivia trivia)
            {
                var parent = trivia.SyntaxTree.GetRoot();

                AddLine(trivia, trivia.Span.Start, parent);

                base.VisitTrivia(trivia);
            }

            private void AddLine(object tokenOrTrivia, int position, SyntaxNode parent)
            {
                var text = parent.GetText();
                var line = text.Lines.GetLineFromPosition(position).LineNumber;

                if (!LineMap.ContainsKey(line))
                {
                    LineMap.Add(line, new List<object>());
                }

                LineMap[line].Add(tokenOrTrivia);
            }
        }
    }
}

// Simple newline count
// Comment filter
// Whitespace filter

// Semantic Analysis
// How many classes
// How many methods
// How many blocks