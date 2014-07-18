using xBehave.Paster.System;

namespace Paster.Specs.Fakes
{
    internal class ClipboardShim : IClipboard
    {
        private readonly string _text;

        public ClipboardShim(string text)
        {
            _text = text;
        }

        public string GetText()
        {
            return _text;
        }

        public bool ContainsText()
        {
            return !string.IsNullOrEmpty(_text);
        }
    }
}