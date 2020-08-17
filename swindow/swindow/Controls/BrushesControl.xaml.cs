using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace swindow
{
    /// <summary>
    /// Логика взаимодействия для BrushesControl.xaml
    /// </summary>
    public partial class BrushesControl : UserControl, ISettingControl
    {

        public string Type { get; set; } = "Brushes";
        private IEnumerable<ResourceDictionary> _dicts;
        public BrushesControl()
        {
            InitializeComponent();
            InitControl();
        }
        private void InitControl()
        {
            _dicts = Resources.MergedDictionaries[0].MergedDictionaries;
            Resources.MergedDictionaries.RemoveAt(0);

            VirtualizingPanel.SetScrollUnit(brushList, ScrollUnit.Pixel);

            foreach (var dict in _dicts)
            {
                var item = new ListBoxItem
                {
                    Resources = dict,
                    Content = (string)dict["BrushesName"]
                };

                brushList.Items.Add(item);
            }
        }
        public void SetZIndex(int index)
        {
            Panel.SetZIndex(this, index);
        }
        public void InitSetting(Setting setting)
        {
            var brush = (BrushesSetting)setting;
            foreach (var item in brushList.Items)
            {
                var i = (ListBoxItem)item;
                if ((string)i.Content == brush.BrushesName)
                {
                    i.IsSelected = true;
                    brushList.ScrollIntoView(i);
                    break;
                }
            }
        }
        public void GetSetting(Setting setting)
        {
            string name = (string)(brushList.SelectedItem as ContentControl).Content;

            foreach (var dict in _dicts)
            {
                if ((string)dict["BrushesName"] == name)
                {
                    setting.Read(dict);
                    break;
                }
            }

        }


    }
}
