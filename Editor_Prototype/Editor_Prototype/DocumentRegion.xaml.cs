using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Editor_Prototype.DocumentEditorWebService;

namespace Editor_Prototype
{
    /// <summary>
    /// Interaction logic for DocumentRegion.xaml
    /// </summary>
    public partial class DocumentRegion : UserControl, INotifyPropertyChanged
    {
        private string _regionContent;
        private ContentPattern _contentPattern;

        private TextRange _range;

        public event RoutedEventHandler Click;

        public string RegionContent
        {
            get
            {
                return this._regionContent;
            }
            set
            {
                this._regionContent = value;

                if (this._range == null)
                {
                    this._range = new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd);
                }

                this._range.Text = _regionContent;

                this.OnPropertyChanged("RegionContent");

            }
        }

        public ContentPattern ContentPattern
        {
            get { return this._contentPattern; }
            set
            {
                this._contentPattern = value;

                this.Width = this.ContentPattern.Width;
                this.Height = this.ContentPattern.Height;

                Canvas.SetTop(this, this.ContentPattern.Y);
                Canvas.SetLeft(this, this.ContentPattern.X);

                this.OnPropertyChanged("Width");
                this.OnPropertyChanged("Height");
                this.OnPropertyChanged("ContentPattern");
            }
        }

        public DocumentRegion()
        {
            this.Loaded += (s, e) =>
                               {
                                   _range = new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd) { Text = this.RegionContent };

                                   //this._regionContent = content;
                               };

            //this.Width = width;
            //this.Height = height;

            //Canvas.SetTop(this, y);
            //Canvas.SetLeft(this, x);

            InitializeComponent();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (Click != null)
            {
                Click(this, new RoutedEventArgs());
            }
        }


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private void RegionContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._range = new TextRange(rtbContent.Document.ContentStart, rtbContent.Document.ContentEnd);
            this._regionContent = _range.Text;
        }
    }
}
