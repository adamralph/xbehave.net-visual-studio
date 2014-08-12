using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal interface SyntaxGroupingNode
    {
        void Append(StringBuilder sb);
        void AddNode(Instruction node);
        void AddExample(string[] exampleNames);
        void AddData(string[] variableValues);
    }
}