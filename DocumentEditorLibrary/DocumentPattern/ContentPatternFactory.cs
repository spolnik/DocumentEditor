using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentEditorLibrary.DocumentPattern
{
    public class ContentPatternFactory : IFactory<ContentPattern>
    {
        private readonly Dictionary<string, ContentPattern> _contentPatterns;

        private ContentPatternFactory()
        {
            this._contentPatterns = new Dictionary<string, ContentPattern>();
        }

        public static IFactory<ContentPattern> Instance
        {
            get { return SingletonCreator.Inst; }
        }

        #region IFactory<IContentPattern> Members

        public ContentPattern CreateNew()
        {
            return new ContentPattern();
        }

        public void Save(ContentPattern pattern)
        {
            if (this._contentPatterns.ContainsKey(pattern.Id))
                throw new ArgumentException("You must declare the unique id for document pattern", "pattern");

            this._contentPatterns.Add(pattern.Id, pattern);
        }

        public ContentPattern Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "Argument cannot be null");

            return this._contentPatterns.ContainsKey(id) ? this._contentPatterns[id] : null;
        }

        public List<ContentPattern> GetAll()
        {
            return this._contentPatterns.Keys.Select(key => this._contentPatterns[key]).ToList();
        }

        public void ClearAll()
        {
            this._contentPatterns.Clear();
        }

        #endregion

        #region Nested type: SingletonCreator

        private static class SingletonCreator
        {
            internal static readonly ContentPatternFactory Inst;

            static SingletonCreator()
            {
                Inst = new ContentPatternFactory();
            }
        }

        #endregion
    }
}