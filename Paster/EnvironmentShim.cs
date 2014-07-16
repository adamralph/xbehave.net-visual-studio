using EnvDTE;
using EnvDTE80;

namespace SiliconSharkLtd.Paster
{
    internal class EnvironmentShim : IEnvironment
    {
        private readonly DTE2 _dte;

        public EnvironmentShim(DTE2 dte)
        {
            _dte = dte;
        }

        public void Paste(string codeLines)
        {
            var currentDocument = (TextDocument)_dte.ActiveDocument.Object("TextDocument");
            var startPoint = currentDocument.Selection.ActivePoint.CreateEditPoint();
            var endPoint = currentDocument.Selection.ActivePoint.CreateEditPoint();
            var ownUndoContext = false;
            if (!_dte.UndoContext.IsOpen)
            {
                ownUndoContext = true;
                _dte.UndoContext.Open("GherkinPaster");
            }

            currentDocument.Selection.Delete();

            endPoint.Insert(codeLines);

            startPoint.SmartFormat(endPoint);

            if (ownUndoContext)
            {
                _dte.UndoContext.Close();
            }
        }
    }
}