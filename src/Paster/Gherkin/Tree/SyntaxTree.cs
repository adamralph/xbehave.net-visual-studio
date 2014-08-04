using System.Collections.Generic;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class SyntaxTree
    {
        private readonly IList<IStringAppender> _nodes = new List<IStringAppender>();

        private StringAppenderCollection currentParent;

        public void AddParent(StringAppenderCollection parent)
        {
            _nodes.Add(parent);
            currentParent = parent;
        }

        public void AddNode(IStringAppender node)
        {
            if (currentParent == null)
                _nodes.Add(node);
            else
                currentParent.Add(node);
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