using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VRVControl.ViewModel
{
    public class SystemViewModel : BindableBase
    {

        public SystemViewModel()
        {
            LaunchAppOnStartup = new RelayCommand(() => LaunchAppOnStartupExecute());
        }

        private bool isStartUpChecked = false;

        public bool IsStartUpChecked
        {
            get
            {
                return isStartUpChecked;
            }
            set
            {
                isStartUpChecked = value;
                this.OnPropertyChanged(nameof(this.IsStartUpChecked));
            }
        }

        public ICommand LaunchAppOnStartup { get; private set; }

        private void LaunchAppOnStartupExecute()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to set registry key!");
            }
        }


    }


}
