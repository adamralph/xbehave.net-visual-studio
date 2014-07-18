using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{

    internal class ScenarioLine : IList<IStringAppender>, IStringAppender
    {
        private readonly string _textLine;
        private readonly IList<IStringAppender> _lines = new List<IStringAppender>();

        public ScenarioLine(string textLine)
        {
            _textLine = textLine.Substring(8);//Remove 'scenario'
            if (_textLine[0] == ':')
            {
                _textLine = _textLine.Substring(1);
            }
            _textLine = MethodCase(_textLine);
        }

        private static string MethodCase(string textLine)
        {
            var chars = textLine.ToCharArray();
            var methodNameChars = new List<char>();
            for (int index = 0; index < chars.Count(); index++)
            {
                if (chars[index] == ' ')
                {
                    index++;
                    methodNameChars.Add(Char.ToUpper(chars[index]));
                }
                else
                {
                    methodNameChars.Add(chars[index]);
                }
            }

            return new string(methodNameChars.ToArray());
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendLine("[Scenario]");
            sb.AppendFormat(@"public void {0}(){1}{{{1}",
                            _textLine,
                            Environment.NewLine);
            foreach (var appender in _lines)
            {
                appender.Append(sb);
            }
            sb.AppendLine("}");
        }

        public IEnumerator<IStringAppender> GetEnumerator()
        {
            return _lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _lines).GetEnumerator();
        }

        public void Add(IStringAppender item)
        {
            _lines.Add(item);
        }

        public void Clear()
        {
            _lines.Clear();
        }

        public bool Contains(IStringAppender item)
        {
            return _lines.Contains(item);
        }

        public void CopyTo(IStringAppender[] array,
                           int arrayIndex)
        {
            _lines.CopyTo(array,
                          arrayIndex);
        }

        public bool Remove(IStringAppender item)
        {
            return _lines.Remove(item);
        }

        public int Count
        {
            get { return _lines.Count; }
        }

        public bool IsReadOnly
        {
            get { return _lines.IsReadOnly; }
        }

        public int IndexOf(IStringAppender item)
        {
            return _lines.IndexOf(item);
        }

        public void Insert(int index,
                           IStringAppender item)
        {
            _lines.Insert(index,
                          item);
        }

        public void RemoveAt(int index)
        {
            _lines.RemoveAt(index);
        }

        public IStringAppender this[int index]
        {
            get { return _lines[index]; }
            set { _lines[index] = value; }
        }
    }
}