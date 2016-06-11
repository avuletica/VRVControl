using NAudio.CoreAudioApi;
using System;
using System.Windows.Threading;

namespace VRVControl.ViewModel
{
    class GeneralViewModel : BindableBase
    {
        // NAudio device enumerator
        public MMDeviceEnumerator devEnum = new MMDeviceEnumerator();

        public GeneralViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            Volume = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
        }          

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
