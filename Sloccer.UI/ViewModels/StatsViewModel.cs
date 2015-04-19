using Sloccer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloccer.UI.ViewModels
{
    public class StatsViewModel : ViewModelBase
    {
        private int _total;
        private int _whiteSpace;
        private int _comments;
        private int _directives;
        private int _braces;
        private int _executable;
        private int _numClasses;
        private int _numMethods;

        public int Total
        {
            get { return _total; }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    NotifyChange("Total");
                }
            }
        }
        public int Whitespace
        {
            get { return _whiteSpace; }
            set
            {
                if (_whiteSpace != value)
                {
                    _whiteSpace = value;
                    NotifyChange("Whitespace");
                }
            }
        }
        public int Comments
        {
            get { return _comments; }
            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    NotifyChange("Comments");
                }
            }
        }
        public int Directives
        {
            get { return _directives; }
            set
            {
                if (_directives != value)
                {
                    _directives = value;
                    NotifyChange("Directives");
                }
            }
        }
        public int Braces
        {
            get { return _braces; }
            set
            {
                if (_braces != value)
                {
                    _braces = value;
                    NotifyChange("Braces");
                }
            }
        }
        public int Executable
        {
            get { return _executable; }
            set
            {
                if (_executable != value)
                {
                    _executable = value;
                    NotifyChange("Executable");
                }
            }
        }
        public int NumberOfClasses
        {
            get { return _numClasses; }
            set
            {
                if (_numClasses != value)
                {
                    _numClasses = value;
                    NotifyChange("NumberOfClasses");
                }
            }
        }
        public int NumberOfMethods
        {
            get { return _numMethods; }
            set
            {
                if (_numMethods != value)
                {
                    _numMethods = value;
                    NotifyChange("NumberOfMethods");
                }
            }
        }

        public void Fill(SlocResult slocResult)
        {
            Total = slocResult.TotalLineCount;
            Whitespace = slocResult.WhiteSpaceLines.Count;
            Comments = slocResult.CommentLines.Count;
            Directives = slocResult.CompilerDirectiveLines.Count;
            Braces = slocResult.CurlyBraceLines.Count;
            Executable = slocResult.OtherLines.Count;
            NumberOfClasses = slocResult.NumberOfClasses;
            NumberOfMethods = slocResult.NumberOfMethods;
        }

        public void Fill(IEnumerable<SlocResult> slocResults)
        {
            Total = slocResults.Sum(r => r.TotalLineCount);
            Whitespace = slocResults.Sum(r => r.WhiteSpaceLines.Count);
            Comments = slocResults.Sum(r => r.CommentLines.Count);
            Directives = slocResults.Sum(r => r.CompilerDirectiveLines.Count);
            Braces = slocResults.Sum(r => r.CurlyBraceLines.Count);
            Executable = slocResults.Sum(r => r.OtherLines.Count);
            NumberOfClasses = slocResults.Sum(r => r.NumberOfClasses);
            NumberOfMethods = slocResults.Sum(r => r.NumberOfMethods);
        }
    }
}
