using System;
using System.Windows;

namespace swindow
{
    public partial class HelpDialog : SDialog
    {
        public HelpDialog()
        {
            InitializeComponent();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            aboutBorder.Visibility = Visibility.Visible;
        }

        protected override void OnClosed(EventArgs e)
        {
            helpDoc.Document = null;
            aboutDoc.Document = null;
            base.OnClosed(e);
        }

        private void AboutClose_Click(object sender, RoutedEventArgs e)
        {
            aboutBorder.Visibility = Visibility.Collapsed;
        }
    }
}
