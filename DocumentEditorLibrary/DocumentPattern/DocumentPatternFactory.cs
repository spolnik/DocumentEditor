using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentEditorLibrary.DocumentPattern
{
    public class DocumentPatternFactory : IFactory<DocumentPattern>
    {
        private readonly Dictionary<string, DocumentPattern> _documentPatterns;

        private DocumentPatternFactory()
        {
            this._documentPatterns = new Dictionary<string, DocumentPattern>();
        }

        public static IFactory<DocumentPattern> Instance
        {
            get { return SingletonCreator.Inst; }
        }

        #region DocumentPatternFactory Members

        public DocumentPattern CreateNew()
        {
            return new DocumentPattern();
        }

        public void Save(DocumentPattern pattern)
        {
            if (this._documentPatterns.ContainsKey(pattern.Id))
                throw new ArgumentException("You must declare the unique id for the document pattern", "pattern");

            this._documentPatterns.Add(pattern.Id, pattern);
        }

        public DocumentPattern Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "Argument cannot be null");

            return this._documentPatterns.ContainsKey(id) ? this._documentPatterns[id] : null;
        }

        public List<DocumentPattern> GetAll()
        {
            return this._documentPatterns.Keys.Select(key => this._documentPatterns[key]).ToList();
        }

        public void ClearAll()
        {
            this._documentPatterns.Clear();
        }

        #endregion

        #region Nested type: SingletonCreator

        private static class SingletonCreator
        {
            internal static readonly DocumentPatternFactory Inst;

            static SingletonCreator()
            {
                Inst = new DocumentPatternFactory();
            }
        }

        #endregion
    }
}