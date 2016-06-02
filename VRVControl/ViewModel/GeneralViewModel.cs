using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace VRVControl.ViewModel
{
    class GeneralViewModel : BindableBase
    {
        public GeneralViewModel()
        {
            IncVol = new RelayCommand(() => IncVolExecute());
            DecVol = new RelayCommand(() => DecVolExecute());
            Mute = new RelayCommand(() => MuteExecute());
            EnableVoiceControl = new RelayCommand(() => EnableVoiceControlExecute());
            DisableVoiceControl = new RelayCommand(() => DisableVoiceControlExecute());
        }

        // initialize speech recognition 
        public SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        public Choices clist = new Choices();

        // NAudio device enumerator
        public MMDeviceEnumerator devEnum = new MMDeviceEnumerator();

        private bool _isEnableVoiceControlEnabled = true;

        public bool IsEnableVoiceControlEnabled
        {
            get
            {
                return _isEnableVoiceControlEnabled;
            }
            set
            {
                _isEnableVoiceControlEnabled = value;
                this.OnPropertyChanged("IsEnableVoiceControlEnabled");
            }
        }
        private bool _isDisableVoiceControlEnabled = false;

        public bool IsDisableVoiceControlEnabled
        {
            get
            {
                return _isDisableVoiceControlEnabled;
            }
            set
            {
                _isDisableVoiceControlEnabled = value;
                this.OnPropertyChanged("IsDisableVoiceControlEnabled");
            }
        }

        // Volume slider
        private float _volume;
        public float Volume
        {
            get { return this._volume; }
            set
            {
                this._volume = value;

                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value;

                this.OnPropertyChanged(nameof(this.Volume));
            }
        }       

        public ICommand IncVol { get; private set; }

        private void IncVolExecute()
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.VolumeStepUp();
        }

        public ICommand DecVol { get; private set; }

        private void DecVolExecute()
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            defaultDevice.AudioEndpointVolume.VolumeStepDown();
        }

        public ICommand Mute { get; private set; }

        private void MuteExecute()
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            var deviceState = defaultDevice.AudioEndpointVolume.Mute;

            if(deviceState)
            {
                defaultDevice.AudioEndpointVolume.Mute = false;
            }
            else
                defaultDevice.AudioEndpointVolume.Mute = true;
        }

        public ICommand EnableVoiceControl { get; private set; }

        private void EnableVoiceControlExecute()
        {
            IsEnableVoiceControlEnabled = false;
            IsDisableVoiceControlEnabled = true;

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
                    MuteExecute();
                    break;
                case "Close":
                    Application.Current.Shutdown();
                    break;
            }

        }

        public ICommand DisableVoiceControl { get; private set; }

        private void DisableVoiceControlExecute()
        {
            sre.RecognizeAsyncStop();
            IsEnableVoiceControlEnabled = true;
            IsDisableVoiceControlEnabled = false;
        }
    }
}
