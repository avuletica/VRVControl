using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Input;
using VRVControl.Model;

namespace VRVControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // initialize speech recognition 
        public SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        public Choices clist = new Choices();

        // NAudio device enumerator
        public MMDeviceEnumerator devEnum = new MMDeviceEnumerator();

        public List<CommandDescription> CommandDescriptions = new List<CommandDescription>();
        public List<GestureCommand> Commands = new List<GestureCommand>();

        public App()
        {
            CommandDescriptions.Add(new CommandDescription(CommandType.IncreaseVolume, "Increase Volume", Key.Up, ModifierKeys.Control));
            CommandDescriptions.Add(new CommandDescription(CommandType.DecreaseVolume, "Decrease Volume", Key.Down, ModifierKeys.Control));
            CommandDescriptions.Add(new CommandDescription(CommandType.MuteSound, "Mute sound", Key.M, ModifierKeys.Control));
            CommandDescriptions.Add(new CommandDescription(CommandType.EnableVoiceControl, "Enables voice control", Key.E, ModifierKeys.Control));
            CommandDescriptions.Add(new CommandDescription(CommandType.DisableVoiceControl, "Disables voice control", Key.D, ModifierKeys.Control));

            foreach (var commandDescription in this.CommandDescriptions)
            {
                var command = new GestureCommand(commandDescription);
                this.Commands.Add(command);

                switch (commandDescription.Type)
                {
                    case CommandType.IncreaseVolume:
                        RegisterCommand(command, IncreaseSound, CanIncreaseSound);
                        break;
                    case CommandType.DecreaseVolume:
                        RegisterCommand(command, DecreaseSound, CanDecreaseSound);
                        break;
                    case CommandType.MuteSound:
                        RegisterCommand(command, MuteSound, CanMuteSound);
                        break;
                    case CommandType.EnableVoiceControl:
                        RegisterCommand(command, EnableVoiceControl, CanEnableVoiceControl);
                        break;
                    case CommandType.DisableVoiceControl:
                        RegisterCommand(command, DisableVoiceControl, CanDisableVoiceControl);
                        break;
                    default:
                        break;
                }           
            }        
        }

        private void RegisterCommand(GestureCommand command, ExecutedRoutedEventHandler commandFunction,CanExecuteRoutedEventHandler CanDoCommand)
        {
            CommandManager.RegisterClassCommandBinding
            (
                typeof(Window),
                new CommandBinding(command, commandFunction, CanDoCommand)
            );
        }

        private void IncreaseSound(object sender, ExecutedRoutedEventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.VolumeStepUp();
        }

        private void CanIncreaseSound(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DecreaseSound(object sender, ExecutedRoutedEventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.VolumeStepDown();
        }

        private void CanDecreaseSound(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CanEnableVoiceControl(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EnableVoiceControl(object sender, ExecutedRoutedEventArgs e)
        {
            clist.Add(new string[] { "Increase sound", "Decrease sound", "Mute", "Close" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Speech not recognized!");
            }
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            switch (e.Result.Text.ToString())
            {
                case "Increase sound":
                    defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar += (float)0.10;
                    break;
                case "Decrease sound":
                    defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar -= (float)0.10;
                    break;
                case "Mute":
                    MuteSound(null, null);
                    break;
                case "Close":
                    Current.Shutdown();
                    break;
            }
        }

        private void CanMuteSound(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MuteSound(object sender, ExecutedRoutedEventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            var deviceState = defaultDevice.AudioEndpointVolume.Mute;

            if (deviceState)
            {
                defaultDevice.AudioEndpointVolume.Mute = false;
            }
            else
                defaultDevice.AudioEndpointVolume.Mute = true;
        }

        private void CanDisableVoiceControl(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DisableVoiceControl(object sender, ExecutedRoutedEventArgs e)
        {
            sre.RecognizeAsyncStop();
        }

    }
}
