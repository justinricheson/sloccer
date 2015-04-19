using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharpExtensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System;

namespace Sloccer.Core
{
    public class CSharpSlocAnalyser : ISlocAnalyser
    {
        public SlocResult GetSlocFor(FileInfo file)
        {
            var simpleLineCount = 0;
            var fileSt = new StringBuilder();
            using (var rdr = new StreamReader(file.FullName))
            {
                while (rdr.Peek() > -1)
                {
                    simpleLineCount++;
                    fileSt.AppendLine(rdr.ReadLine());
                }
            }

            var tree = CSharpSyntaxTree.ParseText(fileSt.ToString());
            var root = tree.GetRoot();

            var walker = new TokenAndTriviaWalker();
            walker.Visit(root);

            var lineMap = walker.LineMap;

            return new SlocResult
            {
                TotalLineCount = simpleLineCount,
                WhiteSpaceLines = GetWhitespaceLines(lineMap),
                CommentLines = GetCommentLines(lineMap),
                CompilerDirectiveLines = GetCompilerDirectiveLines(lineMap),
                OtherLines = GetOtherLines(lineMap)
            };
        }

        private List<Line> GetWhitespaceLines(List<Line> lines)
        {
            var result = new List<Line>();

            foreach (var line in lines)
            {
                var trivia = line.TokensAndTrivia
                    .OfType<SyntaxTrivia>()
                    .Where(l => IsWhiteSpace(l))
                    .ToList();
                if (trivia.Count > 0)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        private List<Line> GetCommentLines(List<Line> lines)
        {
            var result = new List<Line>();

            foreach (var line in lines)
            {
                var trivia = line.TokensAndTrivia
                    .OfType<SyntaxTrivia>()
                    .Where(l => IsComment(l))
                    .ToList();
                if (trivia.Count > 0)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        private List<Line> GetCompilerDirectiveLines(List<Line> lines)
        {
            var result = new List<Line>();

            foreach (var line in lines)
            {
                var trivia = line.TokensAndTrivia
                    .OfType<SyntaxTrivia>()
                    .Where(l => l.IsDirective)
                    .ToList();
                if (trivia.Count > 0)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        private List<Line> GetOtherLines(List<Line> lines)
        {
            var result = new List<Line>();

            foreach (var line in lines)
            {
                var tokens = line.TokensAndTrivia
                    .OfType<SyntaxToken>()
                    .ToList();
                if (tokens.Count > 0)
                {
                    result.Add(line);
                }
                else
                {
                    var trivia = line.TokensAndTrivia
                        .OfType<SyntaxTrivia>()
                        .Where(l => !l.IsDirective && !IsComment(l) && !IsWhiteSpace(l))
                        .ToList();
                    if (trivia.Count > 0)
                    {
                        result.Add(line);
                    }
                }
            }

            return result;
        }

        private bool IsComment(SyntaxTrivia trivia)
        {
            return trivia.CSharpKind() == SyntaxKind.SingleLineCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.SingleLineDocumentationCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.DocumentationCommentExteriorTrivia
                || trivia.CSharpKind() == SyntaxKind.MultiLineCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.MultiLineDocumentationCommentTrivia;
        }

        private bool IsWhiteSpace(SyntaxTrivia trivia)
        {
            return trivia.CSharpKind() == SyntaxKind.WhitespaceTrivia
                || trivia.CSharpKind() == SyntaxKind.EndOfLineTrivia;
        }

        private static void PrintMap(Dictionary<int, List<object>> lineMap)
        {
            foreach (var line in lineMap.OrderBy(l => l.Key))
            {
                System.Diagnostics.Debug.WriteLine("Line {0}", line.Key);
                foreach (var b in line.Value)
                {
                    if (b is SyntaxToken)
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