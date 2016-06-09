using System.Windows.Input;

namespace VRVControl.Model
{
    public class CommandDescription : BindableBase
    {
        public CommandDescription(CommandType type, string description, Key key, ModifierKeys modifierKeys)
        {
            this.Type = type;
            this.Description = description;
            this.Key = key;
            this.ModifierKeys = modifierKeys;
        }

        private CommandType _type;
        public CommandType Type
        {
            get { return this._type; }
            set
            {
                this._type = value;
                this.OnPropertyChanged(nameof(this.Type));
            }
        }

        private string _description;
        public string Description
        {
            get { return this._description; }
            set
            {
                this._description = value;
                this.OnPropertyChanged(nameof(this.Description));
            }
        }

        private Key _key;
        public Key Key
        {
            get { return this._key; }
            set
            {
                this._key = value;
                this.OnPropertyChanged(nameof(this.Key));
            }
        }

        private ModifierKeys _modifierKeys;
        public ModifierKeys ModifierKeys
        {
            get { return this._modifierKeys; }
            set
            {
                this._modifierKeys = value;
                this.OnPropertyChanged(nameof(this.ModifierKeys));
            }
        }
    }

    public enum CommandType
    {
        IncreaseVolume,
        DecreaseVolume,
        MuteSound,
        EnableVoiceControl,
        DisableVoiceControl
    }
}
