using System;
using Editor_Prototype.DocumentEditorWebService;

namespace Editor_Prototype.Util
{
    public class DocumentPatternLoadedEventArgs : EventArgs
    {
        public DocumentPattern DocumentPattern { get; private set; }

        public DocumentPatternLoadedEventArgs(DocumentPattern documentPattern)
        {
            this.DocumentPattern = documentPattern;
        }
    }
}