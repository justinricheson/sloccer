using Sloccer.Core;
using System.IO;
using System.Windows;
using System.Linq;

namespace Sloccer.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var testFile = new FileInfo("C:\\Users\\Justin\\Desktop\\roslyn\\Src\\Compilers\\CSharp\\Portable\\CSharpExtensions.cs");

            var a = new CSharpSlocAnalyser();
            var results = a.GetSlocFor(testFile);

            System.Diagnostics.Debug.WriteLine("Total: {0}", results.TotalLineCount);

            System.Diagnostics.Debug.WriteLine(string.Format("WhiteSpace: {0}",
                results.WhiteSpaceLines
                    .Except(results.CommentLines)
                    .Except(results.CompilerDirectiveLines)
                    .Except(results.OtherLines)
                    .ToLines()));

            var foo = results.WhiteSpaceLines
                .Except(results.CommentLines)
                .Except(results.CompilerDirectiveLines)
                .Except(results.OtherLines)
                .Where(l => l.Text != string.Empty)
                .ToList();

            //System.Diagnostics.Debug.WriteLine("Comments",
            //    results.CommentLines
            //        .Except(results.CompilerDirectiveLines)
            //        .Except(results.OtherLines));

            //System.Diagnostics.Debug.WriteLine("Compiler Directives",
            //    results.CompilerDirectiveLines
            //        .Except(results.CommentLines)
            //        .Except(results.OtherLines));

            //System.Diagnostics.Debug.WriteLine("Other", results.OtherLines);

            InitializeComponent();
        }
    }
}
