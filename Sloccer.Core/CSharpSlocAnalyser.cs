using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharpExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sloccer.Core
{
    public class CSharpSlocAnalyser
    {
        public SlocResult GetSlocFor(IStringRetriever codeStringRetriever)
        {
            var code = codeStringRetriever.GetString();
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            var tokenWalker = new TokenAndTriviaWalker(root);
            tokenWalker.Visit(root);

            var methodWalker = new ClassAndMethodWalker();
            methodWalker.Visit(root);

            var lines = tokenWalker.LineMap;

            return new SlocResult
            {
                TotalLineCount = code.ToLines().Count,
                WhiteSpaceLines = lines.Where(l => IsWhiteSpace(l)).ToList(),
                CommentLines = lines.Where(l => IsComment(l)).ToList(),
                CompilerDirectiveLines = lines.Where(l => IsCompilerDirective(l)).ToList(),
                CurlyBraceLines = lines.Where(l => IsCurlyBraceLine(l)).ToList(),
                OtherLines = lines.Where(l => IsOtherLine(l)).ToList(),
                NumberOfClasses = methodWalker.NumberOfClasses,
                NumberOfMethods = methodWalker.NumberOfMethods
            };
        }

        private bool IsComment(Line line)
        {
            if (line.TokensAndTrivia.OfType<SyntaxToken>()
                .Count(t => t.CSharpKind() == SyntaxKind.XmlComment) > 0)
            {
                return true;
            }
            else
            {
                var trivia = line.TokensAndTrivia
                    .OfType<SyntaxTrivia>()
                    .Where(l => IsComment(l))
                    .ToList();
                if (trivia.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsComment(SyntaxTrivia trivia)
        {
            return trivia.CSharpKind() == SyntaxKind.SingleLineCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.SingleLineDocumentationCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.DocumentationCommentExteriorTrivia
                || trivia.CSharpKind() == SyntaxKind.MultiLineCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.MultiLineDocumentationCommentTrivia
                || trivia.CSharpKind() == SyntaxKind.XmlComment;
        }

        private bool IsWhiteSpace(Line line)
        {
            if (line.TokensAndTrivia.OfType<SyntaxToken>().Count() == 0)
            {
                var trivia = line.TokensAndTrivia.OfType<SyntaxTrivia>().ToList();
                if (trivia.Count(t => IsWhiteSpace(t)) == trivia.Count)
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsWhiteSpace(SyntaxTrivia trivia)
        {
            return trivia.CSharpKind() == SyntaxKind.WhitespaceTrivia
                || trivia.CSharpKind() == SyntaxKind.EndOfLineTrivia;
        }

        private bool IsCurlyBraceLine(Line line)
        {
            var tokens = line.TokensAndTrivia
                .OfType<SyntaxToken>()
                .ToList();

            return tokens.Count == 1 &&
                (tokens.First().CSharpKind() == SyntaxKind.OpenBraceToken
              || tokens.First().CSharpKind() == SyntaxKind.CloseBraceToken);
        }
        private bool IsCompilerDirective(Line line)
        {
            return line.TokensAndTrivia
                .OfType<SyntaxTrivia>()
                .Count(t => t.IsDirective) > 0;
        }
        private bool IsOtherLine(Line line)
        {
            return !IsComment(line)
                && !IsWhiteSpace(line)
                && !IsCompilerDirective(line)
                && !IsCurlyBraceLine(line);
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