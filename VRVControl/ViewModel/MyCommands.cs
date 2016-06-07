using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VRVControl
{
    public static class MyCommands
    {
        private static readonly RoutedUICommand incSoundCommand = new RoutedUICommand("Increases system volume",
            "IncreaseSoundCommand", typeof(MyCommands));

        public static RoutedUICommand IncreaseSoundCommand
        {
            get
            {
                return incSoundCommand;
            }
        }

        private static readonly RoutedUICommand decSoundCommand = new RoutedUICommand("Decreases system volume",
            "DecreaseSoundCommand", typeof(MyCommands));

        public static RoutedUICommand DecreaseSoundCommand
        {
            get
            {
                return decSoundCommand;
            }
        }

        private static readonly RoutedUICommand muteSoundCommand = new RoutedUICommand("Mute system volume",
            "MuteSoundCommand", typeof(MyCommands));

        public static RoutedUICommand MuteSoundCommand
        {
            get
            {
                return muteSoundCommand;
            }
        }

        private static readonly RoutedUICommand enableVoiceControlCommand = new RoutedUICommand("Enables voice control",
            "enableVoiceControlCommand", typeof(MyCommands));

        public static RoutedUICommand EnableVoiceControlCommand
        {
            get
            {
                return enableVoiceControlCommand;
            }
        }

        private static readonly RoutedUICommand disableVoiceControlCommand = new RoutedUICommand("Disables voice control",
            "disableVoiceControlCommand", typeof(MyCommands));

        public static RoutedUICommand DisableVoiceControlCommand
        {
            get
            {
                return disableVoiceControlCommand;
            }
        }
    }
}
