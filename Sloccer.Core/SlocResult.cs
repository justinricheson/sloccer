using System.Collections.Generic;

namespace Sloccer.Core
{
    public class SlocResult
    {
        public int TotalLineCount { get; set; }
        public List<Line> WhiteSpaceLines { get; set; }
        public List<Line> CommentLines { get; set; }
        public List<Line> CompilerDirectiveLines { get; set; }
        public List<Line> CurlyBraceLines { get; set; }
        public List<Line> OtherLines { get; set; }
        public int NumberOfClasses { get; set; }
        public int NumberOfMethods { get; set; }

        public SlocResult()
        {
            WhiteSpaceLines = new List<Line>();
            CommentLines = new List<Line>();
            CompilerDirectiveLines = new List<Line>();
            CurlyBraceLines = new List<Line>();
            OtherLines = new List<Line>();
        }
    }
}
