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
using Editor_Prototype.Util;

namespace Editor_Prototype.DialogWindows
{
    /// <summary>
    /// Interaction logic for OpenDocumentWindow.xaml
    /// </summary>
    public partial class OpenDocumentWindow : Window
    {
        public event EventHandler<DocumentLoadedEventArgs> DocumentLoaded;

        public List<Document> Documents { get; private set; }

        public OpenDocumentWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
                               {
                                   var proxy = new DocumentEditorServiceSoapClient();

                                   this.Documents = new List<Document>(proxy.GetAllDocuments());

                                   this.DataContext = this;
                               };
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            if (this.DocumentLoaded != null)
            {
                this.DocumentLoaded(this, new DocumentLoadedEventArgs(documentsLst.SelectedValue as Document));
            }

            this.Close();
        }
    }
}
