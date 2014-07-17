using System.Text;

namespace SiliconSharkLtd.Paster
{
    internal class NOPLine : IStringAppender
    {
        public void Append(StringBuilder sb)
        {
            //Do nothing
        }
    }
}