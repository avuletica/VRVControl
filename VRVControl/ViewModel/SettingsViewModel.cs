using System.Collections.ObjectModel;
using System.Linq;
using VRVControl.Model;

namespace VRVControl.ViewModel
{
    class SettingsViewModel : BindableBase
    {
        public SettingsViewModel()
        {
            this.UserSettingsList = new ObservableCollection<UserSettings>()
            {
                new UserSettings("Volume up", "Shift + Up"),
                new UserSettings("Volume down", "Shift + Down"),
                new UserSettings("Mute", "Shift + M"),
                new UserSettings("Enable Voice Control", "Ctrl + E"),
                new UserSettings("Desable Voice Control", "Ctrl + D"),
            };
            this.SelectedAction = this.UserSettingsList.First();
        }

        private UserSettings selectedItem;
        public UserSettings SelectedAction
        {
            get { return this.selectedItem; }
            set
            {
                this.selectedItem = value;
                this.OnPropertyChanged(nameof(this.SelectedAction));
            }
        }

        public ObservableCollection<UserSettings> UserSettingsList { get; private set; }
    }
    
}
