namespace VRVControl.Model
{
    public class UserSettings
    {
        public UserSettings(string action, string keybind)
        {
            this.Action = action;
            this.Keybind = keybind;
        }

        public string Action { get; set; }
        public string Keybind { get; set; }
    }
}
