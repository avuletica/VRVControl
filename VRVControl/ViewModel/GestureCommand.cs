using System.Windows.Input;
using VRVControl.Model;

namespace VRVControl
{
    public class GestureCommand : RoutedUICommand
    {
        public GestureCommand(CommandDescription commandDescription)
        {
            this.CommandDescription = commandDescription;
        }

        public CommandDescription CommandDescription { get; set; }
    }
}
