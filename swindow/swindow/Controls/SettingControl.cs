namespace swindow
{
    public interface ISettingControl
    {
        void InitSetting(Setting setting);
        void GetSetting(Setting setting);
        void SetZIndex(int index);
        string Type { get; set; }
    }
}
