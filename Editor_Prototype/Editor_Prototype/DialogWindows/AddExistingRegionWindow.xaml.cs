using System;
using System.Collections.Generic;
using System.Windows;
using Editor_Prototype.DocumentEditorWebService;
using Editor_Prototype.Util;

namespace Editor_Prototype.DialogWindows
{
    public partial class AddExistingRegionWindow : Window
    {
        public event EventHandler<RegionAddedEventArgs> RegionAdded;

        public List<ContentPattern> ContentPatterns { get; private set; }

        public AddExistingRegionWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
                               {
                                   var proxy = new DocumentEditorServiceSoapClient();

                                   this.ContentPatterns = new List<ContentPattern>(proxy.GetAllContentPatterns());

                                   this.DataContext = this;
                               };
        }

        private void AddContentPattern_Click(object sender, RoutedEventArgs e)
        {
            var cp = (ContentPattern) contentPaternsLst.SelectedValue;

            var region = new DocumentRegion
                             {
                                 RegionContent = cp.Id
                             };

            region.ContentPattern = cp;

            if (RegionAdded != null)
            {
                RegionAdded(this, new RegionAddedEventArgs(region));
            }

            this.Close();
        }
    }
}
