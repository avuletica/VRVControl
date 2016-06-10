using System.Windows;
using System.Windows.Controls;
using NAudio.CoreAudioApi;
using System.Speech.Recognition;
using VRVControl.ViewModel;

namespace VRVControl.View
{
    /// <summary>
    /// Interaction logic for GeneralView.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        // NAudio
        public MMDeviceEnumerator devEnum = new MMDeviceEnumerator();

        // initialize speech recognition 
        public SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        public Choices clist = new Choices();

        public GeneralView()
        {
            InitializeComponent();
            this.DataContext = new GeneralViewModel();
            this.Loaded += new RoutedEventHandler(GeneralView_Loaded);            
        }

        private void GeneralView_Loaded(object sender, RoutedEventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            volumeSlider.Value = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
        }

        private void btnStartVoiceControl_Click(object sender, RoutedEventArgs e)
        {
            btnStartVoiceControl.IsEnabled = false;
            btnStopVoiceControl.IsEnabled = true;
        }

        private void btnStopVoiceControl_Click(object sender, RoutedEventArgs e)
        {
            btnStartVoiceControl.IsEnabled = true;
            btnStopVoiceControl.IsEnabled = false;
        }        
    }
}
