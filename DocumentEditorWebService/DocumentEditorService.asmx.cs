using System.Collections.Generic;
using System.Web.Services;
using DocumentEditorLibrary.Document;
using DocumentEditorLibrary.DocumentPattern;

namespace DocumentEditorWebService
{
    /// <summary>
    /// Summary description for DocumentService
    /// </summary>
    [WebService(Namespace = "http://documentservice.agh.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DocumentEditorService : WebService
    {
        #region DOCUMENT PATTERN

        [WebMethod]
        public DocumentPattern CreateDocumentPattern()
        {
            return DocumentPatternFactory.Instance.CreateNew();    
        }

        [WebMethod]
        public void ClearAllDocumentPatterns()
        {
            DocumentPatternFactory.Instance.ClearAll();
        }

        [WebMethod]
        public DocumentPattern GetDocumentPattern(string documentPatternId)
        {
            return DocumentPatternFactory.Instance.Get(documentPatternId);
        }

        [WebMethod]
        public void SaveDocumentPattern(DocumentPattern documentPattern)
        {
            DocumentPatternFactory.Instance.Save(documentPattern);
        }

        [WebMethod]
        public List<DocumentPattern> GetAllDocumentPatterns()
        {
            return DocumentPatternFactory.Instance.GetAll();
        }

        [WebMethod]
        public DocumentPattern AddContentToDocumentPattern(DocumentPattern docPattern, ContentPattern contentPattern)
        {
            docPattern.Contents.Add(contentPattern);
            return docPattern;
        }

        #endregion

        #region CONTENT PATTERN

        [WebMethod]
        public ContentPattern CreateContentPattern()
        {
            return ContentPatternFactory.Instance.CreateNew();
        }

        [WebMethod]
        public void ClearAllContentPattenrs()
        {
            ContentPatternFactory.Instance.ClearAll();
        }

        [WebMethod]
        public ContentPattern GetContentPattern(string contentId)
        {
            return ContentPatternFactory.Instance.Get(contentId);
        }

        [WebMethod]
        public void SaveContentPattern(ContentPattern contentPattern)
        {
            ContentPatternFactory.Instance.Save(contentPattern);
        }

        [WebMethod]
        public List<ContentPattern> GetAllContentPatterns()
        {
            return ContentPatternFactory.Instance.GetAll();
        }

        #endregion

        #region DOCUMENT

        [WebMethod]
        public Document CreateDocument()
        {
            return DocumentFactory.Instance.CreateNew();
        }

        [WebMethod]
        public void ClearAllDocuments()
        {
            DocumentFactory.Instance.ClearAll();
        }

        [WebMethod]
        public void GetDocument(string documentId)
        {
            DocumentFactory.Instance.Get(documentId);
        }

        [WebMethod]
        public void SaveDocument(Document document)
        {
            DocumentFactory.Instance.Save(document);
        }

        [WebMethod]
        public List<Document> GetAllDocuments()
        {
            return DocumentFactory.Instance.GetAll();
        }

        [WebMethod]
        public Document AddDocumentData(Document document, string contentId, string data)
        {
            document.AddData(contentId, data);

            return document;
        }

        [WebMethod]
        public string GetDocumentData(Document document, string contentId)
        {
            return document.GetData(contentId);
        }

        [WebMethod]
        public void ClearDocumentData(Document document)
        {
            document.ClearAllData();
        }

        [WebMethod]
        public Document InitDocumentContentsData(Document document, DocumentPattern documentPattern)
        {
            document.InitContentsData(documentPattern);

            return document;
        }

        #endregion
    }
}