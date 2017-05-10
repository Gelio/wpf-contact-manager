using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Contact_Manager
{
    public class ContactToImageSourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Contact contact = (Contact)values[0];
            string customPicture = (string) values[1];
            if (contact == null)
                return null;

            if (!string.IsNullOrEmpty(customPicture) && File.Exists(customPicture))
                return new BitmapImage(new Uri(customPicture, UriKind.RelativeOrAbsolute));

            PersonSex sex = contact.Sex;
            return new BitmapImage(new Uri(sex == PersonSex.Male ? "man.png" : "woman.jpg", UriKind.RelativeOrAbsolute));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}