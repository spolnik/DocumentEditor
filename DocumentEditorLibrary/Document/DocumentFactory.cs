using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentEditorLibrary.Document
{
    public class DocumentFactory : IFactory<Document>
    {
        private readonly Dictionary<string, Document> _documents;

        private DocumentFactory()
        {
            this._documents = new Dictionary<string, Document>();
        }

        public static IFactory<Document> Instance
        {
            get { return SingletonCreator.Inst; }
        }

        #region IFactory<Document> Members

        public Document CreateNew()
        {
            return new Document();
        }

        public void Save(Document pattern)
        {
            if (this._documents.ContainsKey(pattern.Id))
                throw new ArgumentException("You must declare the unique id for document pattern", "pattern");

            this._documents.Add(pattern.Id, pattern);
        }

        public Document Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "Argument cannot be null");

            return this._documents.ContainsKey(id) ? this._documents[id] : null;
        }

        public List<Document> GetAll()
        {
            return this._documents.Keys.Select(key => this._documents[key]).ToList();
        }

        public void ClearAll()
        {
            this._documents.Clear();
        }

        #endregion

        public Document CreateDocument()
        {
            return new Document();
        }

        #region Nested type: SingletonCreator

        private static class SingletonCreator
        {
            internal static readonly IFactory<Document> Inst;

            static SingletonCreator()
            {
                Inst = new DocumentFactory();
            }
        }

        #endregion
    }
}