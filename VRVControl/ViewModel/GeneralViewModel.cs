using NAudio.CoreAudioApi;
using System;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Input;

namespace VRVControl.ViewModel
{
    class GeneralViewModel : BindableBase
    {
        // NAudio device enumerator
        public MMDeviceEnumerator devEnum = new MMDeviceEnumerator();      

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
       
    }
}
