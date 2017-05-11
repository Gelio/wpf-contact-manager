using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Serialization;
using Contact_Manager.Annotations;
using Microsoft.Win32;

namespace Contact_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Contact> _contactsList = new ObservableCollection<Contact>();
        private Visibility _overlayVisibility = Visibility.Collapsed;
        private string _contactsFilter;
        private ICollectionView _contactsListCollectionView;
        private Contact _selectedContact;

        public ObservableCollection<Contact> ContactsList
        {
            get { return _contactsList; }
            set
            {
                if (Equals(value, _contactsList)) return;
                _contactsList = value;
                OnPropertyChanged(nameof(ContactsList));
            }
        }

        public ICollectionView ContactsListCollectionView
        {
            get { return _contactsListCollectionView; }
            set
            {
                if (Equals(value, _contactsListCollectionView)) return;
                _contactsListCollectionView = value;
                OnPropertyChanged(nameof(ContactsListCollectionView));
            }
        }

        public string ContactsFilter
        {
            get { return _contactsFilter; }
            set
            {
                if (value == _contactsFilter) return;
                _contactsFilter = value;
                OnPropertyChanged(nameof(ContactsFilter));
            }
        }

        public Visibility OverlayVisibility
        {
            get { return _overlayVisibility; }
            set
            {
                if (value == _overlayVisibility) return;
                _overlayVisibility = value;
                OnPropertyChanged(nameof(OverlayVisibility));
            }
        }

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (Equals(value, _selectedContact)) return;
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        public ObservableCollection<string> AvailableSexList { get; set; } = new ObservableCollection<string>() { "Male", "Female" };

        public MainWindow()
        {
            InitializeComponent();
            CreateContactsListCollectionView();
            this.DataContext = this;
            PopulateContacts(null, null);
        }

        private void CreateContactsListCollectionView()
        {
            var contactsListView = new CollectionViewSource() { Source = ContactsList };
            ContactsListCollectionView = contactsListView.View;
            ContactsListCollectionView.Filter = ContactsFilterPredicate;
        }

        private bool ContactsFilterPredicate(object o)
        {
            Contact contact = o as Contact;
            if (contact == null)
                return false;

            if (string.IsNullOrEmpty(ContactsFilter))
                return true;

            IEnumerable<string> filterParts = ContactsFilter.Split(' ').Select(s => s.ToLower()).ToArray();
            IEnumerable<string> personData = new string[] { contact.Name, contact.Surname, contact.City, contact.PhoneNumber }.Select(s => s.ToLower()).ToArray();

            foreach (string filterPart in filterParts)
            {
                bool partMatches = false;
                foreach (string personDatum in personData)
                    if (!string.IsNullOrEmpty(personDatum) && personDatum.Contains(filterPart))
                    {
                        partMatches = true;
                        break;
                    }

                if (partMatches)
                    return true;
            }

            return false;
        }

        private void PopulateContacts(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                ContactsList.Add(new Contact()
                {
                    Name = "Jan" + i,
                    Surname = "Kowalski" + i,
                    PhoneNumber = "12345678" + i,
                    Sex = i % 2 == 0 ? PersonSex.Male : PersonSex.Female,
                    BirthDate = DateTime.Now,
                    City = "Chicago"
                });
            }
        }

        private void ImportContacts(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "XML file (*.xml)|*.xml",
                Title = "Please select an XML file"
            };

            bool? result = openDialog.ShowDialog(this);
            if (!result.Value)
                return;

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Contact>));
            ObservableCollection<Contact> newContacts = null;

            try
            {
                using (FileStream fileStream = File.Open(openDialog.FileName, FileMode.Open))
                    newContacts = (ObservableCollection<Contact>) serializer.Deserialize(fileStream);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Exception: " + error.Message, "Exception", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            if (newContacts == null)
                return;

            ContactsList = newContacts;
            CreateContactsListCollectionView();
        }

        private void ExportContacts(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog()
            {
                Filter = "XML file (*.xml)|*.xml"
            };

            bool? result = saveDialog.ShowDialog(this);
            if (!result.Value)
                return;

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Contact>));

            try
            {
                using (FileStream fileStream = File.Open(saveDialog.FileName, FileMode.Create))
                    serializer.Serialize(fileStream, ContactsList);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Exception: " + error.Message, "Exception", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangePersonPicture(object sender, MouseButtonEventArgs e)
        {
            Contact contact = ContactListView.SelectedItem as Contact;
            if (contact == null)
            {
                MessageBox.Show(this, "Please select a contact first", "Select a contact first", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            var openDialog = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png"
            };

            bool? result = openDialog.ShowDialog(this);
            if (!result.Value)
                return;

            contact.CustomPicture = openDialog.FileName;

        }

        private void RemoveSelectedContact(object sender, RoutedEventArgs e)
        {
            Contact contact = ContactListView.SelectedItem as Contact;
            if (contact == null)
                return;

            ContactsList.Remove(contact);
        }

        private void OpenNewContactDialog(object sender, RoutedEventArgs e)
        {
            AddItemWindow dialog = new AddItemWindow()
            {
                Owner = this
            };

            OverlayVisibility = Visibility.Visible;
            bool? result = dialog.ShowDialog();
            OverlayVisibility = Visibility.Collapsed;

            if (!result.HasValue || !result.Value)
                return;

            ContactsList.Add(dialog.NewContact);
        }

        private void RefreshFilter(object sender, KeyEventArgs e)
        {
            ContactsListCollectionView.Refresh();
        }
    }
}
