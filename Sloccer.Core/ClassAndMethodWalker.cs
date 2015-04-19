using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sloccer.Core
{
    public class ClassAndMethodWalker : CSharpSyntaxWalker
    {
        public int NumberOfClasses { get; private set; }
        public int NumberOfMethods { get; private set; }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            NumberOfClasses++;
            base.VisitClassDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            NumberOfMethods++;
            base.VisitMethodDeclaration(node);
        }
    }
}
