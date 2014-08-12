using System.Collections.Generic;
using System.Text;

namespace xBehave.Paster.System
{
    internal static class StringBuilderExtensions
    {
        internal static StringBuilder AppendLines(this StringBuilder source, IEnumerable<string> lines)
        {
            foreach (var line in lines)
                source.AppendLine(line);
            return source;
        }
    }
}