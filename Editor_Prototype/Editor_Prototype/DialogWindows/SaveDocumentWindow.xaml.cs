using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Editor_Prototype.DocumentEditorWebService;

namespace Editor_Prototype.DialogWindows
{
    /// <summary>
    /// Interaction logic for SaveDocumentWindow.xaml
    /// </summary>
    public partial class SaveDocumentWindow : Window
    {
        private Document _documentToSave;
        private DocumentView _currentView;

        public SaveDocumentWindow(Document doc, DocumentView view)
        {
            this._documentToSave = doc;
            this._currentView = view;

            InitializeComponent();
        }

        private void SaveDocument_Click(object sender, RoutedEventArgs e)
        {
            var proxy = new DocumentEditorServiceSoapClient();

            this._documentToSave.Id = documentNameTxt.Text;

            foreach (DocumentRegion region in this._currentView.documentBoard.Children)
            {
                this._documentToSave = proxy.AddDocumentData(this._documentToSave, region.ContentPattern.Id, region.RegionContent);
            }

            proxy.SaveDocument(this._documentToSave);

            this.Close();
        }
    }
}
