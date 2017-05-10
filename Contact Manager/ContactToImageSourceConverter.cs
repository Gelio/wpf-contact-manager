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
            if (contact == null)
                return null;

            if (!string.IsNullOrEmpty(contact.CustomPicture) && File.Exists(contact.CustomPicture))
                return new BitmapImage(new Uri(contact.CustomPicture, UriKind.RelativeOrAbsolute));

            PersonSex sex = contact.Sex;
            return new BitmapImage(new Uri(sex == PersonSex.Male ? "man.png" : "woman.jpg", UriKind.RelativeOrAbsolute));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}