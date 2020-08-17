using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace swindow
{
    public partial class SettingDialog : SDialog
    {
        private List<Setting> _settings;
        private ISettingControl _control;
        private int _select = -1;
        public SettingDialog()
        {
            InitializeComponent();
            InitializeSettings();
        }
        private void InitializeSettings()
        {
            _settings = Settings.GetSettings();

            foreach (var setting in _settings)
            {
                var item = new ListBoxItem
                {
                    Content = setting.Name
                };
                if (setting.Type != "Set")
                {
                    item.Padding = new Thickness(20, 0, 0, 0);
                }
                else
                {
                    item.IsEnabled = false;
                }
                settingsPanel.Items.Add(item);
            }
            settingsPanel.SelectedIndex = 1;
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var setting = _settings[_select];
            _control.GetSetting(setting);
            Settings.Apply();
        }
        public void AddSettingControl(UserControl control)
        {
            controlGrid.Children.Add(control);
        }
        private void SettingsPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _select = settingsPanel.SelectedIndex;
            var setting = _settings[_select];

            if (_control != null)
            {
                _control.SetZIndex(0);
            }

            foreach (var control in controlGrid.Children)
            {
                var sc = (ISettingControl)control;

                if (setting.Type == sc.Type)
                {
                    _control = sc;
                    break;
                }
            }
            _control.SetZIndex(1);
            _control.InitSetting(setting);
        }
    }
}
