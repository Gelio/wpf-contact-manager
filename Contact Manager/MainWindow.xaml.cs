using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Contact_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> ContactsList { get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<string> AvailableSexList { get; set; } = new ObservableCollection<string>() { "Male", "Female" };

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            PopulateContacts(null, null);
        }

        private void PopulateContacts(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                ContactsList.Add(new Contact() { Name = "Jan" + i, Surname = "Kowalski" + i, PhoneNumber = "12345678" + i, Sex = i % 2 == 0 ? PersonSex.Male : PersonSex.Female });
            }
        }

        private void ImportContacts(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExportContacts(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
