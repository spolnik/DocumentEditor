using System;
using System.Windows;
using Editor_Prototype.DocumentEditorWebService;
using Editor_Prototype.Util;

namespace Editor_Prototype.DialogWindows
{
    /// <summary>
    /// Interaction logic for AddRegionWindow.xaml
    /// </summary>
    public partial class AddRegionWindow : Window
    {
        public event EventHandler<RegionAddedEventArgs> RegionCreated;
        public bool CanAddRegionsToDocument { get; set;}

        public AddRegionWindow()
        {
            CanAddRegionsToDocument = ((MainWindow) Application.Current.MainWindow).CanAddRegionsToDocument;

            this.DataContext = this;

            InitializeComponent();
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                

                double d = 0.0;

                var region = new DocumentRegion
                                 {
                                     Uid = this.txtBoxId.Text,
                                     RegionContent = txtBoxId.Text
                                 };

                var proxy = new DocumentEditorServiceSoapClient();

                var cp = proxy.CreateContentPattern();
                
                cp.Id = txtBoxId.Text;
                cp.X = double.Parse(txtBoxPosX.Text);
                cp.Y = double.Parse(txtBoxosY.Text);
                cp.Width = double.Parse(txtBoxWidth.Text);
                cp.Height = double.Parse(txtBoxHeight.Text);

                proxy.SaveContentPattern(cp);

                region.ContentPattern = cp;

                if (RegionCreated != null)
                {
                    RegionCreated(this, new RegionAddedEventArgs(region));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
     
            this.Close();
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var proxy = new DocumentEditorWebService.DocumentEditorServiceSoapClient();

                ContentPattern cp = proxy.CreateContentPattern();

                cp.Id = txtBoxId.Text;
                cp.X = double.Parse(txtBoxPosX.Text);
                cp.Y = double.Parse(txtBoxosY.Text);
                cp.Width = double.Parse(txtBoxWidth.Text);
                cp.Height = double.Parse(txtBoxHeight.Text);

                proxy.SaveContentPattern(cp);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            this.Close();
        }
    }
}
