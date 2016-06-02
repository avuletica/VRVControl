using VRVControl.ViewModel;

namespace VRVControl
{
    class MainWindowViewModel : BindableBase
    {
        private GeneralViewModel _generalViewModel = new GeneralViewModel();
        private SettingsViewModel _settingsViewModel = new SettingsViewModel();

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            CurrentViewModel = _generalViewModel;
        }

        private BindableBase _CurrentViewModel;      

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {

            switch (destination)
            {
                case "general":
                    CurrentViewModel = _generalViewModel;
                    break;
                case "settings":
                default:
                    CurrentViewModel = _settingsViewModel;
                    break;
            }
        }
    }
}
