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

            var a = new CSharpSlocAnalyser();
            var results = a.GetSlocFor(testFile);

            var whitespace = results.WhiteSpaceLines
                .ToLines();

            var comments = results.CommentLines
                .ToLines();

            var directives = results.CompilerDirectiveLines
                .ToLines();

            var braces = results.CurlyBraceLines
                .ToLines();

            var other = results.OtherLines
                .ToLines();

            var numClasses = results.NumberOfClasses;

            var numMethods = results.NumberOfMethods;

            InitializeComponent();
        }
    }
}
