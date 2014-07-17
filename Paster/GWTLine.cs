using System;
using System.Text;

namespace SiliconSharkLtd.Paster
{
    internal class GWTLine : IStringAppender
    {
        private readonly string _textLine;
        private readonly Identifier _gherkinType;

        public GWTLine(string textLine,
                       Identifier gherkinType)
        {
            _textLine = textLine;
            _gherkinType = gherkinType;
        }

        public void Append(StringBuilder sb)
        {
            sb.AppendFormat(@"""{0}"".{1}(() => {{}});{2}",
                            _textLine,
                            _gherkinType,
                            Environment.NewLine);
        }
    }
}