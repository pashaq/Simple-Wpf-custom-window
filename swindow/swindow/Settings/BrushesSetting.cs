using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace swindow
{
    public class BrushesSetting : Setting
    {
        public BrushesSetting()
        {
            Type = "Brushes";
        }
        public SolidColorBrush Background { get; set; }
        public SolidColorBrush MiBackground { get; set; }
        public SolidColorBrush HiBackground { get; set; }
        public SolidColorBrush BorderBrush { get; set; }
        public SolidColorBrush MiBorderBrush { get; set; }
        public SolidColorBrush HiBorderBrush { get; set; }
        public SolidColorBrush Foreground { get; set; }
        public SolidColorBrush MiForeground { get; set; }
        public SolidColorBrush HiForeground { get; set; }
        public SolidColorBrush RedBrush { get; set; }

        public string BrushesName { get; set; } = "Unknown";

        public override void Write(XElement element)
        {
            var brushes = new XElement(Key);

            brushes.Add(new XElement(strName, BrushesName));

            brushes.Add(new XElement(strBackground, Background.ToString()));
            brushes.Add(new XElement(strMiBackground, MiBackground.ToString()));
            brushes.Add(new XElement(strHiBackground, HiBackground.ToString()));
            brushes.Add(new XElement(strBorderBrush, BorderBrush.ToString()));
            brushes.Add(new XElement(strMiBorderBrush, MiBorderBrush.ToString()));
            brushes.Add(new XElement(strHiBorderBrush, HiBorderBrush.ToString()));
            brushes.Add(new XElement(strForeground, Foreground.ToString()));
            brushes.Add(new XElement(strMiForeground, MiForeground.ToString()));
            brushes.Add(new XElement(strHiForeground, HiForeground.ToString()));
            brushes.Add(new XElement(strRedBrush, RedBrush.ToString()));

            element.Add(brushes);
        }
        public override void Write(ResourceDictionary dict)
        {
            dict[strName] = BrushesName;

            dict[strBackground] = Background;
            dict[strMiBackground] = MiBackground;
            dict[strHiBackground] = HiBackground;
            dict[strBorderBrush] = BorderBrush;
            dict[strMiBorderBrush] = MiBorderBrush;
            dict[strHiBorderBrush] = HiBorderBrush;
            dict[strForeground] = Foreground;
            dict[strMiForeground] = MiForeground;
            dict[strHiForeground] = HiForeground;
            dict[strRedBrush] = RedBrush;
        }
        public override void Read(XElement element)
        {
            var brushes = element.Element(Key);

            BrushesName = brushes.Element(strName).Value;

            var brushConvert = new BrushConverter();

            string brush0 = brushes.Element(strBackground).Value;
            string brush1 = brushes.Element(strMiBackground).Value;
            string brush2 = brushes.Element(strHiBackground).Value;
            string brush3 = brushes.Element(strBorderBrush).Value;
            string brush4 = brushes.Element(strMiBorderBrush).Value;
            string brush5 = brushes.Element(strHiBorderBrush).Value;
            string brush6 = brushes.Element(strForeground).Value;
            string brush7 = brushes.Element(strMiForeground).Value;
            string brush8 = brushes.Element(strHiForeground).Value;
            string brush9 = brushes.Element(strRedBrush).Value;

            Background = (SolidColorBrush)brushConvert.ConvertFromString(brush0);
            MiBackground = (SolidColorBrush)brushConvert.ConvertFromString(brush1);
            HiBackground = (SolidColorBrush)brushConvert.ConvertFromString(brush2);
            BorderBrush = (SolidColorBrush)brushConvert.ConvertFromString(brush3);
            MiBorderBrush = (SolidColorBrush)brushConvert.ConvertFromString(brush4);
            HiBorderBrush = (SolidColorBrush)brushConvert.ConvertFromString(brush5);
            Foreground = (SolidColorBrush)brushConvert.ConvertFromString(brush6);
            MiForeground = (SolidColorBrush)brushConvert.ConvertFromString(brush7);
            HiForeground = (SolidColorBrush)brushConvert.ConvertFromString(brush8);
            RedBrush = (SolidColorBrush)brushConvert.ConvertFromString(brush9);
        }
        public override void Read(ResourceDictionary dict)
        {
            BrushesName = (string)dict[strName];

            Background = (SolidColorBrush)dict[strBackground];
            MiBackground = (SolidColorBrush)dict[strMiBackground];
            HiBackground = (SolidColorBrush)dict[strHiBackground];
            BorderBrush = (SolidColorBrush)dict[strBorderBrush];
            MiBorderBrush = (SolidColorBrush)dict[strMiBorderBrush];
            HiBorderBrush = (SolidColorBrush)dict[strHiBorderBrush];
            Foreground = (SolidColorBrush)dict[strForeground];
            MiForeground = (SolidColorBrush)dict[strMiForeground];
            HiForeground = (SolidColorBrush)dict[strHiForeground];
            RedBrush = (SolidColorBrush)dict[strRedBrush];
        }

        private static readonly string strName = "BrushesName";
        private static readonly string strBackground = "Background";
        private static readonly string strMiBackground = "MiBackground";
        private static readonly string strHiBackground = "HiBackground";
        private static readonly string strBorderBrush = "BorderBrush";
        private static readonly string strMiBorderBrush = "MiBorderBrush";
        private static readonly string strHiBorderBrush = "HiBorderBrush";
        private static readonly string strForeground = "Foreground";
        private static readonly string strMiForeground = "MiForeground";
        private static readonly string strHiForeground = "HiForeground";
        private static readonly string strRedBrush = "RedBrush";
    }
}
