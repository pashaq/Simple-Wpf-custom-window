using System.Windows;
using System.Xml.Linq;

namespace swindow
{
    public abstract class Setting
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public abstract void Write(XElement element);
        public abstract void Write(ResourceDictionary dict);
        public abstract void Read(XElement element);
        public abstract void Read(ResourceDictionary dict);
    }
}
