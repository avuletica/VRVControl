using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VRVControl.Model;

namespace VRVControl.ViewModel
{
    class KeyboardViewModel : BindableBase
    {
        public KeyboardViewModel()
        {
            this.Commands = (Application.Current as App).CommandDescriptions;
            this.SelectedCommand = this.Commands.First();
        }

        private CommandDescription selectedCommand;
        public CommandDescription SelectedCommand
        {
            get { return this.selectedCommand; }
            set
            {
                this.selectedCommand = value;
                this.OnPropertyChanged(nameof(this.SelectedCommand));
            }
        }

        public List<CommandDescription> Commands { get; private set; }
    }

}
