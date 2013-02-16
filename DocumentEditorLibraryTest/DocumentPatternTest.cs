using System;
using DocumentEditorLibrary.DocumentPattern;
using NUnit.Framework;

namespace DocumentEditorLibraryTest
{
    [TestFixture]
    public class DocumentPatternTest
    {
        internal const string UniqueId = "XXSSAA-223";
        private DocumentPattern _documentPattern;
        private ContentPattern _contentPattern;

        [SetUp]
        public void SetUp()
        {
            this._documentPattern = DocumentPatternFactory.Instance.CreateNew();
            this._documentPattern.Id = UniqueId;
            DocumentPatternFactory.Instance.Save(this._documentPattern);

            this._contentPattern = ContentPatternFactory.Instance.CreateNew();
            this._contentPattern.Id = ContentPatternTest.UniqueId;
        }

        [TearDown]
        public void TearDown()
        {
            DocumentPatternFactory.Instance.ClearAll();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExceptionWhenAddTwoTheSameIds()
        {
            DocumentPattern docPattern = DocumentPatternFactory.Instance.CreateNew();
            docPattern.Id = UniqueId;
            DocumentPatternFactory.Instance.Save(docPattern);
        }

        [Test]
        public void TestExistingPatternInFactory()
        {
            DocumentPattern documentPattern = DocumentPatternFactory.Instance.Get(UniqueId);
            Assert.AreSame(documentPattern, this._documentPattern);
        }
    }
}