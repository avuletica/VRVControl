using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace VRVControl.ViewModel.Converters
{
    public class GestureConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var keybind in values)
            {
                keybind.ToString();
            }
            return values;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] tokens = value.ToString().Split('+');
            var a = tokens[0];
            var b = tokens[1];

            // Remove whitespace
            a = a.Replace(" ", String.Empty);
            b = b.Replace(" ", String.Empty);  
          
            object key =  (Key)Enum.Parse(typeof(Key), b, true); 
            object modifier =  (ModifierKeys)Enum.Parse(typeof(ModifierKeys), a, true);

            return new[] { modifier, key }; 
        }
    }
}
