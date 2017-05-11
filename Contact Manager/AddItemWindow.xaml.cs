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
using System.Windows.Shapes;

namespace Contact_Manager
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public ObservableCollection<string> AvailableSexList { get; set; } = new ObservableCollection<string>() { "Male", "Female" };
        public Contact NewContact { get; set; }

        public AddItemWindow()
        {
            InitializeComponent();
            NewContact = new Contact()
            {
                BirthDate = DateTime.Now
            };
            this.DataContext = this;
        }

        private void CancelWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void AddNewPerson(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
