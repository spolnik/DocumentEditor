using System;
using DocumentEditorLibrary.DocumentPattern;
using NUnit.Framework;

namespace DocumentEditorLibraryTest
{
    [TestFixture]
    public class ContentPatternTest
    {
        internal const int SampleWidth = 10;
        internal const int InvalidX = 20;
        internal const int SampleX = 5;

        internal const int SampleHeight = 10;
        internal const int InvalidY = 20;
        internal const int SampleY = 5;
        internal const string UniqueId = "xasaww221";

        private ContentPattern _contentPattern;

        [SetUp]
        public void SetUp()
        {
            this._contentPattern = ContentPatternFactory.Instance.CreateNew();
            this._contentPattern.Id = UniqueId;
        }

        [Test]
        public void TestDefaultValues()
        {
            Assert.AreEqual(0, this._contentPattern.Height);
            Assert.AreEqual(0, this._contentPattern.Width);
            Assert.AreEqual(0, this._contentPattern.X);
            Assert.AreEqual(0, this._contentPattern.Y);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSettingInvalidXValue()
        {
            this._contentPattern.Width = SampleWidth;
            this._contentPattern.X = InvalidX;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSettingInvalidYValue()
        {
            this._contentPattern.Height = SampleHeight;
            this._contentPattern.Y = InvalidY;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSettingInvalidWidthValue()
        {
            this._contentPattern.Width = 0;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSettingInvalidHeightValue()
        {
            this._contentPattern.Height = 0;
        }

        [Test]
        public void TestValidValues()
        {
            this._contentPattern.Height = SampleHeight;
            this._contentPattern.Width = SampleWidth;
            this._contentPattern.X = SampleX;
            this._contentPattern.Y = SampleY;

            Assert.AreEqual(SampleHeight, this._contentPattern.Height);
            Assert.AreEqual(SampleWidth, this._contentPattern.Width);
            Assert.AreEqual(SampleX, this._contentPattern.X);
            Assert.AreEqual(SampleY, this._contentPattern.Y);
        }
    }
}