using System.Windows.Forms;

namespace SiliconSharkLtd.Paster
{
    internal sealed class ClipboardShim : IClipboard
    {
        private static readonly ClipboardShim _instance = new ClipboardShim();

        static ClipboardShim()
        {}

        private ClipboardShim()
        {}

        public static ClipboardShim Instance
        {
            get { return _instance; }
        }

        public string GetText()
        {
            return Clipboard.GetText();
        }

        public bool ContainsText()
        {
            return Clipboard.ContainsText();
        }
    }
}