using System;
using System.Collections.Generic;
using System.Linq;
using DocumentEditorLibrary.DocumentPattern;

namespace DocumentEditorLibrary.Document
{
    [Serializable]
    public class Document
    {
        private List<DocumentData> _documentData;

        public Document()
        {
            this._documentData = new List<DocumentData>();
        }

        public void AddData(string contentId, string data)
        {
            foreach (DocumentData documentData in
                this._documentData.Where(documentData => documentData.ContentId.Equals(contentId)))
            {
                documentData.Data = data;
            }
        }

        public string GetData(string contentId)
        {
            foreach (DocumentData documentData in
                this._documentData.Where(documentData => documentData.ContentId.Equals(contentId)))
            {
                return documentData.Data;
            }

            return string.Empty;
        }

        public void ClearAllData()
        {
            foreach (DocumentData documentData in this._documentData)
                documentData.Data = string.Empty;
        }

        public void InitContentsData(DocumentPattern.DocumentPattern documentPattern)
        {
            this._documentData = new List<DocumentData>();
            this.DocumentPatternId = documentPattern.Id;

            foreach (ContentPattern content in documentPattern.Contents)
                this._documentData.Add(new DocumentData {ContentId = content.Id, Data = string.Empty});
        }

        public string Id
        {
            get; set;
        }

        public string DocumentPatternId
        {
            get; set;
        }

        public List<DocumentData> DocumentData
        {
            get { return this._documentData; }
        }
    }
}