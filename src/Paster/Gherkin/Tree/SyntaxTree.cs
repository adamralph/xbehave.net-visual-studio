using System.Collections.Generic;
using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal class SyntaxTree
    {
        private readonly IList<SyntaxGroupingNode> _nodes = new List<SyntaxGroupingNode>();

        public void Add(SyntaxGroupingNode group)
        {
            _nodes.Add(group);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var node in _nodes)
                node.Append(builder);

            return builder.ToString();
        }
    }
}