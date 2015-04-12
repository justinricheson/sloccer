using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;

namespace Sloccer.Core
{
    public class CSharpSlocAnalyser : ISlocAnalyser
    {
        public long GetSlocFor(IEnumerable<FileInfo> files, SlocOptions options)
        {
            var tree = CSharpSyntaxTree.ParseText(
                @"public class MyClass
                {
                    public void MyMethod()
                    {
                    }
                }");

            return 1;
        }
    }
}
