using Microsoft.Win32;
using System;
using System.Reflection;
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

                if (isStartUpChecked)                     
                    key.SetValue(curAssembly.GetName().Name, curAssembly.Location);                
                else                
                    key.DeleteValue(curAssembly.GetName().Name);          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to set registry key!");
            }
        }


    }


}
