using swindow;
using System.Windows.Controls;

namespace Exemple
{
    /// <summary>
    /// Логика взаимодействия для TextControl.xaml
    /// </summary>
    public partial class TextControl : UserControl
    {
        public TextControl()
        {
            InitializeComponent();

            Settings.AddSettingDict(Resources.MergedDictionaries[0]);
        }
    }
}
