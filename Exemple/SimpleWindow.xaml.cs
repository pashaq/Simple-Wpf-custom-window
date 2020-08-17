using swindow;
using System.Windows;


namespace Exemple
{
    public partial class SimpleWindow : SWindow
    {
        public SimpleWindow()
        {
            InitializeComponent();

            Settings.AddSettingDict(Resources.MergedDictionaries[0]);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            if (SDialog.IsGoDialog) return;
            var setting = new SettingDialog
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            setting.Resources.MergedDictionaries.Add(Resources);
            setting.Show();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            if (SDialog.IsGoDialog) return;

            var help = new HelpDialog
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            help.Resources.MergedDictionaries.Add(Resources);
            help.Show();
        }
    }
}
