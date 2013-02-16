using System;

namespace DocumentEditorLibrary.DocumentPattern
{
    [Serializable]
    public class ContentPattern
    {
        private double _height;
        private double _width;
        private double _x;
        private double _y;

        internal ContentPattern()
        {
            this._x = 0;
            this._y = 0;
            this._width = 0;
            this._height = 0;
        }

        public double X
        {
            get { return this._x; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid value", "value");
                this._x = value;
            }
        }

        public double Y
        {
            get { return this._y; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid value", "value");
                this._y = value;
            }
        }

        public double Width
        {
            get { return this._width; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid value", "value");
                this._width = value;
            }
        }

        public double Height
        {
            get { return this._height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid value", "value");
                this._height = value;
            }
        }

        public string Id
        {
            get; set;
        }
    }
}