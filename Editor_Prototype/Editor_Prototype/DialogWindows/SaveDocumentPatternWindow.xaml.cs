using System.Windows;
using Editor_Prototype.DocumentEditorWebService;

namespace Editor_Prototype.DialogWindows
{
    /// <summary>
    /// Interaction logic for SaveDocumentPatternWindow.xaml
    /// </summary>
    public partial class SaveDocumentPatternWindow : Window
    {
        private readonly DocumentPattern _documentToSave;

        public SaveDocumentPatternWindow(DocumentPattern dp)
        {
            this._documentToSave = dp;

            InitializeComponent();
        }

        private void SaveDocumentPattern_Click(object sender, RoutedEventArgs e)
        {
            var proxy = new DocumentEditorServiceSoapClient();

            this._documentToSave.Id = documentPatternNameTxt.Text;

            proxy.SaveDocumentPattern(this._documentToSave);

            this.Close();
        }
    }
}
