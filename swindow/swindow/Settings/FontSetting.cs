using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace swindow
{
    public class FontSetting : Setting
    {
        public FontSetting()
        {
            Type = "Font";
        }
        public FontFamily Family { get; set; }
        public FontStyle Style { get; set; }
        public FontWeight Weight { get; set; }
        public double Size { get; set; }

        public override void Write(XElement element)
        {
            var font = new XElement(Key);

            font.Add(new XElement(Key + strFamily, Family.ToString()));
            font.Add(new XElement(Key + strStyle, Style.ToString()));
            font.Add(new XElement(Key + strWeight, Weight.ToString()));
            font.Add(new XElement(Key + strSize, Size.ToString()));

            element.Add(font);
        }
        public override void Write(ResourceDictionary dict)
        {
            dict[Key + strFamily] = Family;
            dict[Key + strStyle] = Style;
            dict[Key + strWeight] = Weight;
            dict[Key + strSize] = Size;
        }
        public override void Read(XElement element)
        {
            var font = element.Element(Key);

            string family = font.Element(Key + strFamily).Value;
            string style = font.Element(Key + strStyle).Value;
            string weight = font.Element(Key + strWeight).Value;
            string size = font.Element(Key + strSize).Value;

            var styleConvert = new FontStyleConverter();
            var weightConvert = new FontWeightConverter();

            Family = new FontFamily(family);
            Style = (FontStyle)styleConvert.ConvertFromString(style);
            Weight = (FontWeight)weightConvert.ConvertFromString(weight);
            Size = double.Parse(size);
        }
        public override void Read(ResourceDictionary dict)
        {
            Family = (FontFamily)dict[Key + strFamily];
            Style = (FontStyle)dict[Key + strStyle];
            Weight = (FontWeight)dict[Key + strWeight];
            Size = (double)dict[Key + strSize];
        }

        private static readonly string strFamily = "Family";
        private static readonly string strStyle = "Style";
        private static readonly string strWeight = "Weight";
        private static readonly string strSize = "Size";
    }

}
