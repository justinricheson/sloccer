using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;

namespace Sloccer.Core
{
    public class TokenAndTriviaWalker : CSharpSyntaxWalker
    {
        private Dictionary<int, Line> _lineMap;
        public List<Line> LineMap
        {
            get { return _lineMap.Values.OrderBy(l => l.LineNumber).ToList(); }
        }

        public TokenAndTriviaWalker() : base(SyntaxWalkerDepth.StructuredTrivia)
        {
            _lineMap = new Dictionary<int, Line>();
        }

        public override void VisitToken(SyntaxToken token)
        {
            AddLine(token, token.Span.Start);

            base.VisitToken(token);
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            AddLine(trivia, trivia.Span.Start);

            base.VisitTrivia(trivia);
        }

        private SourceText _rootText;
        private void AddLine(object tokenOrTrivia, int position)
        {
            if (_rootText == null)
            {
                if (tokenOrTrivia is SyntaxToken)
                {
                    _rootText = ((SyntaxToken)tokenOrTrivia).SyntaxTree.GetRoot().GetText();
                }
                else
                {
                    _rootText = ((SyntaxTrivia)tokenOrTrivia).SyntaxTree.GetRoot().GetText();
                }
            }

            var line = _rootText.Lines.GetLineFromPosition(position).LineNumber;

            if (!_lineMap.ContainsKey(line))
            {
                _lineMap.Add(line, new Line(line, _rootText.Lines[line].ToString()));
            }

            _lineMap[line].TokensAndTrivia.Add(tokenOrTrivia);
        }
    }
}
