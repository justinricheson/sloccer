using Sloccer.Core;
using System.IO;
using System.Windows;

namespace Sloccer.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var testFile = new FileInfo("C:\\Users\\Justin\\Desktop\\roslyn\\Src\\Compilers\\CSharp\\Portable\\CSharpExtensions.cs");
            var codeRetriever = new FileStringRetriver(testFile);

            var a = new CSharpSlocAnalyser();
            var results = a.GetSlocFor(codeRetriever);

            var whitespace = results.WhiteSpaceLines
                .ConvertToString();

            var comments = results.CommentLines
                .ConvertToString();

            var directives = results.CompilerDirectiveLines
                .ConvertToString();

            var braces = results.CurlyBraceLines
                .ConvertToString();

            var other = results.OtherLines
                .ConvertToString();

            var numClasses = results.NumberOfClasses;

            var numMethods = results.NumberOfMethods;

            InitializeComponent();
        }
    }
}
