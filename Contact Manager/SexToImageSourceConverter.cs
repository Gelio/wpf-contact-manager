using System;
using System.Globalization;
using System.Windows.Data;

namespace Contact_Manager
{
    public class SexToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            PersonSex sex = (PersonSex) value;
            if (sex == PersonSex.Male)
                return "man.png";
            else
                return "woman.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}