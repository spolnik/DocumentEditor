using System;

namespace DocumentEditorLibrary.Document
{
    [Serializable]
    public class DocumentData
    {
        public string ContentId
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}