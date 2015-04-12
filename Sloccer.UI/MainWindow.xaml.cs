using Sloccer.Core;
using System.Windows;

namespace Sloccer.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var a = new CSharpSlocAnalyser();
            a.GetSlocFor(null, SlocOptions.CountCompilerDirectives);

            InitializeComponent();
        }
    }
}
