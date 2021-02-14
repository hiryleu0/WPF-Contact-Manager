using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://stackoverflow.com/questions/17967052/wpf-simple-binding-to-inotifypropertychanged-object
namespace WPF16
{
    [Serializable]
    public enum Gender { male, female}
    [Serializable]
    public class Person: INotifyPropertyChanged
    {
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        private string surname;
        public string Surname { get { return surname; } set { surname = value; OnPropertyChanged("Surname"); } }

        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }

        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged("Phone"); } }

        private Gender gender;
        public Gender Gender { get { return gender; } set { gender = value; OnPropertyChanged("Gender"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
