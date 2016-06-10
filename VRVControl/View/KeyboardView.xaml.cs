using System.Windows.Controls;
using VRVControl.ViewModel;

namespace VRVControl.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class KeyboardView : UserControl
    {
        public KeyboardView()
        {
            InitializeComponent();

            this.DataContext = new KeyboardViewModel();
        }
               
    }
}
