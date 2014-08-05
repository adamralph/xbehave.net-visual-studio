using System;
using System.Collections.Generic;
using System.Linq;

namespace xBehave.Paster.Gherkin
{
    internal static class RawLineManipulations
    {
        internal static string EscapeDoubleQuotes(this string rawLine)
        {
            return rawLine.Replace("\"", "\\\"");
        }

        internal static string RemoveScenarioTag(this string rawLine)
        {
            var index = rawLine.IndexOf("scenario:", StringComparison.OrdinalIgnoreCase);
            return rawLine.Substring(index + 9);
        }

        internal static string RemoveQuotes(this string rawLine)
        {
            return rawLine.Replace(@"""", "");
        }

        internal static string ToMethodCase(this string rawLine)
        {
            var chars = rawLine.ToCharArray();
            var methodNameChars = new List<char>();
            for (int index = 0; index < chars.Count(); index++)
            {
                if (chars[index] == ' ')
                {
                    index++;
                    methodNameChars.Add(Char.ToUpper(chars[index]));
                }
                else
                    methodNameChars.Add(chars[index]);
            }

            return new string(methodNameChars.ToArray());
        }
    }
}