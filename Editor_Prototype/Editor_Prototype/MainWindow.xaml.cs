using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Editor_Prototype.DialogWindows;
using Editor_Prototype.DocumentEditorWebService;
using Editor_Prototype.Util;

namespace Editor_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _canAddRegionsToDocument;

        private DocumentView _activeDocument;

        private Document _currentlyEditedDocument;

        private AddRegionWindow _addRegionWindow;

        private ListView _modeMenu;
        public ListView ModeMenu
        {
            get
            {
                return this._modeMenu;
            }
            set
            {
                this._modeMenu = value;
                OnPropertyChanged("ModeMenu");
            }
        }

        public bool CanAddRegionsToDocument
        {
            get { return this._canAddRegionsToDocument; }
            set
            {
                this._canAddRegionsToDocument = value;
                OnPropertyChanged("CanAddRegionsToDocument");
            }
        }

        //private List<Window> windows = new List<Window>();

        public MainWindow()
        {
            this._canAddRegionsToDocument = false;

            this.DataContext = this;

            this.Loaded += MainWindow_Loaded;

            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Menu.Content = this.FindResource(EditorModeCmbBx.SelectedValue.ToString());
        }

        private void AddRegion_Click(object sender, RoutedEventArgs e)
        {
            _addRegionWindow = new AddRegionWindow();

            _addRegionWindow.RegionCreated += this.InsertRegion;

            _addRegionWindow.ShowDialog();
        }

        private void InsertRegion(object sender, RegionAddedEventArgs e)
        {
            e.Region.Click += Region_Clicked;

            if (this._activeDocument == null) return;

            var proxy = new DocumentEditorServiceSoapClient();

            this._activeDocument.DocumentPattern = proxy.AddContentToDocumentPattern(this._activeDocument.DocumentPattern, e.Region.ContentPattern);

            this._activeDocument.DocumentBoard.Children.Add(e.Region);
        }

        private void Region_Clicked(object sender, RoutedEventArgs e)
        {
            this.propGrid.Instance = ((DocumentRegion) sender).ContentPattern;
        }

        private void EditionModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                switch ((DocumentEditorMode) Enum.Parse(typeof(DocumentEditorMode), ((ComboBox)sender).SelectedValue.ToString()))
                {
                    case DocumentEditorMode.PatternCreation:
                        //this.ModeMenu = (ListView) this.FindResource("PatternModeOptions");
                        this.Menu.Content = this.FindResource("PatternCreation");
                        break;
                    case DocumentEditorMode.DocumentEdition:
                        //this.ModeMenu = (ListView) this.FindResource("EditionModeOptions");
                        this.Menu.Content = this.FindResource("DocumentEdition");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("sender");
                }
            }
        }

        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    foreach (Window window in windows)
        //    {
        //        window.Close();
        //    }

        //    base.OnClosed(e);
        //}

        //internal void RemoveWindow(Window w)
        //{
        //    this.windows.Remove(w);
        //}

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private void NewDocumentPattern_Click(object sender, RoutedEventArgs e)
        {
            this.CanAddRegionsToDocument = true;

            var proxy = new DocumentEditorServiceSoapClient();

            var dv = new DocumentView(proxy.CreateDocumentPattern());
            
            this._activeDocument = dv;

            this.documentContainer.Content = dv;
        }

        private void OpenDocumentPattern_Click(object sender, RoutedEventArgs e)
        {
            var win = new OpenDocumentPatternWindow();

            win.DocumentPatternLoaded += DocumentPatternLoaded;

            win.ShowDialog();
        }

        private void DocumentPatternLoaded(object sender, DocumentPatternLoadedEventArgs e)
        {
            this.CanAddRegionsToDocument = true;

            this._activeDocument = new DocumentView(e.DocumentPattern);

            this.documentContainer.Content = this._activeDocument;

            this.LoadRegions(this._activeDocument);
        }

        private void LoadRegions(DocumentView activeDocument)
        {
            foreach (ContentPattern contentPattern in activeDocument.DocumentPattern.Contents)
            {
                var region = new DocumentRegion
                                 {
                                     RegionContent = contentPattern.Id
                                 };

                region.ContentPattern = contentPattern;

                activeDocument.documentBoard.Children.Add(region);
            }
        }

        private void AddExistingRegion_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddExistingRegionWindow();

            win.RegionAdded += this.InsertRegion;

            win.ShowDialog();
        }

        private void SaveDocumentPattern_Click(object sender, RoutedEventArgs e)
        {
            var win = new SaveDocumentPatternWindow(this._activeDocument.DocumentPattern);

            win.ShowDialog();
        }

        private void NewDocument_Click(object sender, RoutedEventArgs e)
        {
            var win = new OpenDocumentPatternWindow();

            win.DocumentPatternLoaded += DocumentPatternLoaded;

            var proxy = new DocumentEditorServiceSoapClient();

            this._currentlyEditedDocument = proxy.CreateDocument();


            win.NewDocumentCreated += (s, ev) =>
            {
                this._currentlyEditedDocument = proxy.InitDocumentContentsData(
                    this._currentlyEditedDocument,
                    ev.DocumentPattern);
                this._currentlyEditedDocument.DocumentPatternId = ev.DocumentPattern.Id;
            };

            win.ShowDialog();
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            var win = new OpenDocumentWindow();

            win.DocumentLoaded += DocumentLoaded;

            win.ShowDialog();
        }

        private void DocumentLoaded(object sender, DocumentLoadedEventArgs e)
        {
            var proxy = new DocumentEditorServiceSoapClient();

            Document doc = e.Document;

            this._activeDocument = new DocumentView(proxy.GetDocumentPattern(doc.DocumentPatternId));

            this.LoadRegions(_activeDocument);

            foreach (DocumentRegion region in _activeDocument.DocumentBoard.Children)
            {
                region.RegionContent = proxy.GetDocumentData(doc, region.ContentPattern.Id);
            }

            this.documentContainer.Content = this._activeDocument;

            this._currentlyEditedDocument = doc;
        }

        private void SaveDocument_Click(object sender, RoutedEventArgs e)
        {
            if (_currentlyEditedDocument != null)
            {
                var win = new SaveDocumentWindow(this._currentlyEditedDocument, _activeDocument);

                win.ShowDialog();
            }
        }
    }
}
