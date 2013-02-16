using System;
using Editor_Prototype.DocumentEditorWebService;

namespace Editor_Prototype.Util
{
    public class DocumentLoadedEventArgs : EventArgs
    {
        public Document Document { get; private set; }

        public DocumentLoadedEventArgs(Document document)
        {
            this.Document = document;
        }
    }
}