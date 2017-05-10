using System.ComponentModel;
using System.Runtime.CompilerServices;
using Contact_Manager.Annotations;

namespace Contact_Manager
{
    public class Contact : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private PersonSex _sex;
        private string _phoneNumber;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }

        public PersonSex Sex
        {
            get { return _sex; }
            set
            {
                if (value == _sex) return;
                _sex = value;
                OnPropertyChanged(nameof(Sex));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value == _phoneNumber) return;
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}