using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;

namespace swindow
{
    public class SettingList : List<Setting>
    {
        public string Key { get; set; }
    }
    public class SettingSet : Setting
    {
        public List<Setting> Set { get; set; }
        public SettingSet()
        {
            Type = "Set";
        }
        public override void Write(XElement element)
        {
            var set = new XElement(Key);

            foreach (var setting in Set)
            {
                setting.Write(set);
            }

            element.Add(set);
        }
        public override void Write(ResourceDictionary dict)
        {
            foreach (var setting in Set)
            {
                setting.Write(dict);
            }
        }
        public override void Read(XElement element)
        {
            var set = element.Element(Key);

            foreach (var setting in Set)
            {
                setting.Read(set);
            }
        }
        public override void Read(ResourceDictionary dict)
        {
            foreach (var setting in Set)
            {
                setting.Read(dict);
            }
        }
    }
}
