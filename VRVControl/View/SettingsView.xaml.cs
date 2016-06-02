using System.Windows.Controls;
using VRVControl.ViewModel;

namespace VRVControl.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            this.DataContext = new SettingsViewModel();
        }
               
    }
}
