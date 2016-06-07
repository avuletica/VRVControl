using NAudio.CoreAudioApi;
using System;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Input;

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

        public App()
        {
            var incVolBinding = new CommandBinding(MyCommands.IncreaseSoundCommand, IncreaseSound, CanIncreaseSound);
            var decVolBinding = new CommandBinding(MyCommands.DecreaseSoundCommand, DecreaseSound, CanDecreaseSound);
            var muteSoundBinding = new CommandBinding(MyCommands.MuteSoundCommand, MuteSound, CanMuteSound);
            var enableVoiceControlBinding = new CommandBinding(MyCommands.EnableVoiceControlCommand, EnableVoiceControl,
                CanEnableVoiceControl);
            var disableVoiceControlBinding = new CommandBinding(MyCommands.DisableVoiceControlCommand, DisableVoiceControl,
                CanDisableVoiceControl);

            // Register CommandBinding for all windows.
            CommandManager.RegisterClassCommandBinding(typeof(Window), incVolBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), decVolBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), muteSoundBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), enableVoiceControlBinding);
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
