using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using VRVControl.Model;

namespace VRVControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();

            var commands = (Application.Current as App).Commands;

            foreach (var command in commands)
            {
                this.InputBindings.Add(createKeyBinding(command));
            }
        }

        private KeyBinding createKeyBinding(GestureCommand command)
        {
            var keyboardBinding = new KeyBinding();
            //commandDescription.ModifierKeys

            keyboardBinding.Command = command;

            Binding keyBinding = new Binding("Key");

            keyBinding.Source = command.CommandDescription;
            keyBinding.Mode = BindingMode.TwoWay; //Prominit u one-way
            keyBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(keyboardBinding, KeyBinding.KeyProperty, keyBinding);


            Binding modifiersBinding = new Binding("ModifierKeys");

            modifiersBinding.Source = command.CommandDescription;
            modifiersBinding.Mode = BindingMode.TwoWay; //Prominit u one-way
            modifiersBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(keyboardBinding, KeyBinding.ModifiersProperty, modifiersBinding);

            return keyboardBinding;
        }
    }
}
