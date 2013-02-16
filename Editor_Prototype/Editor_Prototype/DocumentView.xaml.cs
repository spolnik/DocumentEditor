using System.Windows.Controls;
using Editor_Prototype.DocumentEditorWebService;

namespace Editor_Prototype
{
    /// <summary>
    /// Interaction logic for DocumentView.xaml
    /// </summary>
    public partial class DocumentView : UserControl
    {
        public DocumentPattern DocumentPattern { get; set; }

        public Canvas DocumentBoard
        {
            get { return this.documentBoard; }
        }

        public DocumentView(DocumentPattern documentPattern)
        {
            this.DocumentPattern = documentPattern;

            InitializeComponent();
        }
    }
}
