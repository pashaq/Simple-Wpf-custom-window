using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;

namespace swindow
{
    public class Settings
    {
        public static List<ResourceDictionary> _dicts = new List<ResourceDictionary>();
        public static void AddSettingDict(ResourceDictionary dict)
        {
            _dicts.Add(dict);
        }
        public static List<Setting> GetSettings()
        {
            var result = new List<Setting>();

            for (int i = List.Count - 1; i >= 0; i--)
            {
                var setting = List[i];

                if (setting.Type == "Set")
                {
                    var set = (SettingSet)setting;
                    result.Add(setting);
                    foreach (var s in set.Set)
                    {
                        result.Add(s);
                    }
                }
                else
                {
                    result.Add(setting);
                }
            }
            return result;
        }
        public static string Name { get; set; } = "settings";
        private static XElement _settings;
        public static List<Setting> List { get; } = new List<Setting>();

        public static Rect RestoreBounds
        {
            get
            {
                var rect = _settings.Element("RestoreBounds");

                if (rect != null)
                {
                    return Rect.Parse(rect.Value);
                }
                else
                {
                    return new Rect(100, 50, 900, 550);
                }
            }
            set
            {
                var rect = _settings.Element("RestoreBounds");

                if (rect == null)
                {
                    _settings.Add(new XElement("RestoreBounds", value.ToString()));
                }
                else
                {
                    rect.Value = value.ToString();
                }
            }
        }
        public static WindowState WindowState
        {
            get
            {
                var rect = _settings.Element("WindowState");

                if (rect != null)
                {
                    if (Enum.TryParse(rect.Value, out WindowState state))
                    {
                        return state;
                    }
                    else
                    {
                        rect.Value = WindowState.Normal.ToString();
                        return WindowState.Normal;
                    }
                }
                return WindowState.Normal;
            }
            set
            {
                var rect = _settings.Element("WindowState");

                if (rect == null)
                {
                    _settings.Add(new XElement("WindowState", value.ToString()));
                }
                else
                {
                    rect.Value = value.ToString();
                }
            }
        }

        public static void Load()
        {
            List.Clear();

            foreach (var dict in _dicts)
            {
                List.Add((Setting)dict["SettingKey"]);
            }
            try
            {
                _settings = XElement.Load(Name + ".xml");

                foreach (var setting in List)
                {
                    setting.Read(_settings);
                }
            }
            catch (Exception)
            {
                _settings = new XElement(Name);

                foreach (var setting in List)
                {
                    setting.Write(_settings);
                }
            }
        }
        public static void Apply()
        {
            _settings = new XElement(Name);

            int i = 0;
            foreach (var setting in List)
            {
                setting.Write(_dicts[i++]);
                setting.Write(_settings);
            }
        }
        public static void Save(Rect bounds, WindowState state)
        {
            _settings.Add(new XElement("RestoreBounds", bounds.ToString()));
            _settings.Add(new XElement("WindowState", state.ToString()));

            _settings.Save(Name + ".xml");
        }
    }
}
