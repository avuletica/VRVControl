using VRVControl.ViewModel;

namespace VRVControl
{
    class MainWindowViewModel : BindableBase
    {
        private GeneralViewModel _generalViewModel = new GeneralViewModel();
        private KeyboardViewModel _keyboardViewModel = new KeyboardViewModel();
        private SystemViewModel _systemViewModel = new SystemViewModel();
        private AboutViewModel _aboutViewModel = new AboutViewModel();

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
                case "keyboard":               
                    CurrentViewModel = _keyboardViewModel;
                    break;
                case "system":
                    CurrentViewModel = _systemViewModel;
                    break;
                case "about":
                    CurrentViewModel = _aboutViewModel;
                    break;                    
            }
        }
    }
}
