using System;
using System.Collections.Generic;
using System.Text;

namespace xBehave.Paster.Gherkin
{
    internal class Scenario : SyntaxGroupingNode
    {
        private readonly string _methodName;
        private readonly List<SyntaxNode> _lines = new List<SyntaxNode>();

        public Scenario(string rawLine)
        {
            _methodName = rawLine.RemoveQuotes()
                                 .ToMethodCase();
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendLine("[Scenario]");
            sb.AppendFormat(@"public void {0}(){1}{{{1}", _methodName, Environment.NewLine);
            foreach (var appender in _lines)
                appender.Append(sb);
            sb.AppendLine("}");
        }

        public void AddNode(Instruction node)
        {
            _lines.Add(node);
        }
    }
}