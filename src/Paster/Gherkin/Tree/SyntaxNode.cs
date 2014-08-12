using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal interface SyntaxNode
    {
        void Append(StringBuilder sb, Substitution[] substitutions);
    }
}