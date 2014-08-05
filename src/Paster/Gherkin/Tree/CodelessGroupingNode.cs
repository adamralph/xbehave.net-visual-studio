using System.Collections.Generic;
using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal class CodelessGroupingNode : SyntaxGroupingNode
    {
        private readonly List<SyntaxNode> _nodes = new List<SyntaxNode>();

        public void Append(StringBuilder sb)
        {
            foreach (var node in _nodes)
                node.Append(sb);
        }

        public void AddNode(Instruction node)
        {
            _nodes.Add(node);
        }
    }
}