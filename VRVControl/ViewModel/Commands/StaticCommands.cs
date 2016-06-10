using System.Windows.Input;

namespace VRVControl.ViewModel.Commands
{
    public static class StaticCommands
    {
        private static readonly RoutedUICommand increaseSoundCommand = new RoutedUICommand("description", "IncreaseSoundCommand", typeof(StaticCommands));

        public static RoutedUICommand IncreaseSoundCommand
        {
            get
            {
                return increaseSoundCommand;
            }
        }

        private static readonly RoutedUICommand decreaseSoundCommand = new RoutedUICommand("description", "DecreaseSoundCommand", typeof(StaticCommands));

        public static RoutedUICommand DecreaseSoundCommand
        {
            get
            {
                return decreaseSoundCommand;
            }
        }

        private static readonly RoutedUICommand muteSoundCommand = new RoutedUICommand("description", "MuteSoundCommand", typeof(StaticCommands));

        public static RoutedUICommand MuteSoundCommand
        {
            get
            {
                return muteSoundCommand;
            }
        }

        private static readonly RoutedUICommand enableVoiceControlCommand = new RoutedUICommand("description", "EnableVoiceControlCommand", typeof(StaticCommands));

        public static RoutedUICommand EnableVoiceControlCommand
        {
            get
            {
                return enableVoiceControlCommand;
            }
        }

        private static readonly RoutedUICommand disableVoiceControlCommand = new RoutedUICommand("description", "DisableVoiceControlCommand", typeof(StaticCommands));

        public static RoutedUICommand DisableVoiceControlCommand
        {
            get
            {
                return disableVoiceControlCommand;
            }
        }
    }
}
