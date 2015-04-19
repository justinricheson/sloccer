using System.Collections.Generic;

namespace Sloccer.Core
{
    public class Line
    {
        public int LineNumber { get; set; }
        public string Text { get; set; }
        public List<object> TokensAndTrivia { get; set; }

        public Line(int lineNumber, string text)
        {
            LineNumber = lineNumber;
            Text = text;
            TokensAndTrivia = new List<object>();
        }
    }
}
