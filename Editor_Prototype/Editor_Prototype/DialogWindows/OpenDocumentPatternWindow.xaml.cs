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
    /// Interaction logic for OpenDocumentPatternWindow.xaml
    /// </summary>
    public partial class OpenDocumentPatternWindow : Window
    {
        public event EventHandler<DocumentPatternLoadedEventArgs> DocumentPatternLoaded;
        public event EventHandler<DocumentPatternLoadedEventArgs> NewDocumentCreated;

        public List<DocumentPattern> DocumentPatterns { get; private set; }

        public OpenDocumentPatternWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
                               {
                                   var proxy = new DocumentEditorServiceSoapClient();

                                   this.DocumentPatterns = new List<DocumentPattern>(proxy.GetAllDocumentPatterns());

                                   this.DataContext = this;
                               };
        }

        private void OpenDocumentPattern_Click(object sender, RoutedEventArgs e)
        {
            if (this.DocumentPatternLoaded != null)
            {
                this.DocumentPatternLoaded(this, new DocumentPatternLoadedEventArgs(documentPaternsLst.SelectedValue as DocumentPattern));
            }

            if (this.NewDocumentCreated != null)
            {
                this.NewDocumentCreated(this, new DocumentPatternLoadedEventArgs(documentPaternsLst.SelectedValue as DocumentPattern));
            }

            this.Close();
        }
    }
}
