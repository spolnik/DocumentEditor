using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentEditorLibrary.DocumentPattern
{
    [Serializable]
    public class DocumentPattern
    {
        private const double DefaultPageWidth = 595.0;
        private const double DefaultPageHeight = 842.0;

        private const double MinPageWidth = 50.0;
        private const double MinPageHeight = 50.0;

        private double _pageWidth;
        private double _pageHeight;

        private readonly List<ContentPattern> _contents;

        internal DocumentPattern()
        {
            this._contents = new List<ContentPattern>();
            this._pageHeight = DefaultPageHeight;
            this._pageWidth = DefaultPageWidth;
        }

        public double PageWidth
        {
            get { return this._pageWidth; }
            set
            {
                this._pageWidth = value <= MinPageWidth ? MinPageWidth : value;
            }
        }

        public double PageHeight
        {
            get { return this._pageHeight; }
            set
            {
                this._pageHeight = value <= MinPageHeight ? MinPageHeight : value;
            }
        }

        public List<ContentPattern> Contents
        {
            get { return this._contents; }
        }

        public string Id
        {
            get; set;
        }
    }
}