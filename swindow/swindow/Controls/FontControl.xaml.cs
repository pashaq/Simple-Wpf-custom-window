using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace swindow
{
    public partial class FontControl : UserControl, ISettingControl
    {
        public string Type { get; set; } = "Font";
        public FontControl()
        {
            InitializeComponent();

            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                familyListBox.Items.Add(fontFamily.Source);
            }
        }
        public void SetZIndex(int index)
        {
            Panel.SetZIndex(this, index);
        }
        private bool _bInit = false;
        public void GetSetting(Setting setting)
        {
            var font = (FontSetting)setting;

            font.Family = new FontFamily((string)familyListBox.SelectedItem);
            font.Style = (FontStyle)styleComboBox.SelectedItem;
            font.Weight = (FontWeight)weightComboBox.SelectedItem;
            font.Size = (double)sizeSlider.Value;
        }
        public void InitSetting(Setting setting)
        {
            _bInit = true;

            var font = (FontSetting)setting;

            FontFamilyInfo(font.Family, out HashSet<FontStyle> styles,
                out HashSet<FontWeight> weights);

            weightComboBox.ItemsSource = weights;
            styleComboBox.ItemsSource = styles;

            familyListBox.SelectedItem = font.Family.Source;
            familyListBox.ScrollIntoView(font.Family.Source);
            weightComboBox.SelectedItem = font.Weight;
            styleComboBox.SelectedItem = font.Style;
            sizeSlider.Value = font.Size; ;

            fontTextBox.FontFamily = font.Family;
            fontTextBox.FontStyle = font.Style;
            fontTextBox.FontWeight = font.Weight;
            fontTextBox.FontSize = font.Size;
        }
        private void FontFamilyChanged(FontFamily family)
        {
            FontFamilyInfo(family, out HashSet<FontStyle> styles, out HashSet<FontWeight> weights);

            weightComboBox.ItemsSource = weights;
            styleComboBox.ItemsSource = styles;

            weightComboBox.SelectedIndex = 0;
            styleComboBox.SelectedIndex = 0;

            fontTextBox.FontFamily = family;
            fontTextBox.FontStyle = (FontStyle)styleComboBox.SelectedItem;
            fontTextBox.FontWeight = (FontWeight)weightComboBox.SelectedItem;
        }
        private void FamilyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_bInit)
            {
                _bInit = false;
                return;
            }
            FontFamilyChanged(new FontFamily((string)familyListBox.SelectedItem));
        }
        private void WeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weightComboBox.SelectedItem == null) return;
            fontTextBox.FontWeight = (FontWeight)weightComboBox.SelectedItem;
        }
        private void StyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (styleComboBox.SelectedItem == null) return;
            fontTextBox.FontStyle = (FontStyle)styleComboBox.SelectedItem;
        }
        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            fontSizeTextBlock.Text = string.Format("{0,2:f}", sizeSlider.Value);
            fontTextBox.FontSize = sizeSlider.Value;
        }
        public static void FontFamilyInfo(FontFamily family, out HashSet<FontStyle> styles,
            out HashSet<FontWeight> weights)
        {
            var faces = family.FamilyTypefaces;
            styles = new HashSet<FontStyle>();
            weights = new HashSet<FontWeight>();

            foreach (var face in faces)
            {
                styles.Add(face.Style);
                weights.Add(face.Weight);
            }
        }
    }
}