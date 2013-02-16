using System;
using DocumentEditorLibrary.Document;
using DocumentEditorLibrary.DocumentPattern;
using NUnit.Framework;

namespace DocumentEditorLibraryTest
{
    [TestFixture]
    public class DocumentTest
    {
        private const string SimpleData = "Simple tekst";
        private Document _document;
        private DocumentPattern _documentPattern;
        private ContentPattern _contentPattern;

        [SetUp]
        public void SetUp()
        {
            this._documentPattern = DocumentPatternFactory.Instance.CreateNew();
            this._documentPattern.Id = DocumentPatternTest.UniqueId;

            this._contentPattern = ContentPatternFactory.Instance.CreateNew();
            this._contentPattern.Id = ContentPatternTest.UniqueId;

            this._contentPattern.Height = ContentPatternTest.SampleHeight;
            this._contentPattern.Width = ContentPatternTest.SampleWidth;
            this._contentPattern.X = ContentPatternTest.SampleX;
            this._contentPattern.Y = ContentPatternTest.SampleY;
            this._documentPattern.Contents.Add((ContentPattern) this._contentPattern);

            this._document = DocumentFactory.Instance.CreateNew();
            this._document.InitContentsData((DocumentPattern) this._documentPattern);
        }

        [Test]
        public void Test()
        {
            this._document.AddData(ContentPatternTest.UniqueId, SimpleData);
            Assert.AreEqual(SimpleData, this._document.GetData(ContentPatternTest.UniqueId));
        }

        [Test]
        public void TestData()
        {
            
        }
    }
}