using System.Text;
using xBehave.Paster.System;

namespace xBehave.Paster.Gherkin
{
    internal class NOPLine : IStringAppender
    {
        public void Append(StringBuilder sb)
        {
            //Do nothing
        }
    }
}