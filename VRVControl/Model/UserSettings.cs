using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
