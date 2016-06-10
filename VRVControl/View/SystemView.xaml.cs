using System.Windows.Controls;
using VRVControl.ViewModel;

namespace VRVControl.View
{
    /// <summary>
    /// Interaction logic for SystemView.xaml
    /// </summary>
    public partial class SystemView : UserControl
    {
        public SystemView()
        {
            InitializeComponent();

            this.DataContext = new SystemViewModel();
        }
    }
}
