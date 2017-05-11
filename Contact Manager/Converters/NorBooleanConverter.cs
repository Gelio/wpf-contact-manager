using System;
using System.Globalization;
using System.Windows.Data;

namespace Contact_Manager.Converters
{
    [ValueConversion(typeof(bool[]), typeof(bool))]
    public class NorBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
                if (value == null || (bool)value)
                    return false;

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}