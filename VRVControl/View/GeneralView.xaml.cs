using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio.CoreAudioApi;
using System.Speech.Recognition;

namespace VRVControl.View
{
    /// <summary>
    /// Interaction logic for GeneralView.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        // NAudio
        MMDeviceEnumerator devEnum = new MMDeviceEnumerator();

        // initialize speech recognition 
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();

        public GeneralView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(GeneralView_Loaded);
        }

        private void GeneralView_Loaded(object sender, RoutedEventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            slider.Value = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
        }

        private void Mute()
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            if (defaultDevice.AudioEndpointVolume.Mute)
                defaultDevice.AudioEndpointVolume.Mute = false;
            else
                defaultDevice.AudioEndpointVolume.Mute = true;
        }

        private void VolDown()
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.VolumeStepDown();
            slider.Value = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
        }

        private void VolUp()
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.VolumeStepUp();
            slider.Value = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
        }

        void bntIncVol(object sender, RoutedEventArgs e)
        {
            VolUp();
        }

        void bntDecVol(object sender, RoutedEventArgs e)
        {
            VolDown();
        }

        private void btnMute(object sender, RoutedEventArgs e)
        {
            Mute();
        }              

        private void btnStartVoiceControl_Click(object sender, RoutedEventArgs e)
        {
            btnStartVoiceControl.IsEnabled = false;
            btnStopVoiceControl.IsEnabled = true;
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

                MessageBox.Show(ex.Message, "Error");
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
                    Mute();
                    break;
                case "Close":
                    Application.Current.Shutdown();
                    break;
            }

        }

        private void btnStopVoiceControl_Click(object sender, RoutedEventArgs e)
        {
            sre.RecognizeAsyncStop();
            btnStartVoiceControl.IsEnabled = true;
            btnStopVoiceControl.IsEnabled = false;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (float)slider.Value;
        }
    }
}
