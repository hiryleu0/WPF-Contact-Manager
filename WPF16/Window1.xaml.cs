using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.ComponentModel.DataAnnotations;

namespace WPF16
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window,INotifyPropertyChanged
    {
        private string name;
        public string Namep { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        private string surname;
        public string Surnamep { get { return surname; } set { surname = value; OnPropertyChanged("Surname"); } }

        private string email;
        public string Emailp { get { return email; } set { email = value; OnPropertyChanged("Email"); } }

        private string phone;
        public string Phonep { get { return phone; } set { phone = value; OnPropertyChanged("Phone"); } }

        private Gender gender;
        public Gender Genderp { get { return gender; } set { gender = value; OnPropertyChanged("Gender"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindow Paren;
        public Window1(MainWindow Parent)
        {
            InitializeComponent();
            if(Parent.NameValidation!=null)
                NameBinding.ValidationRules.Add(Parent.NameValidation as ValidationRule);
            if (Parent.SurnmeValidation != null)
                SurnameBinding.ValidationRules.Add(Parent.SurnmeValidation as ValidationRule);
            if (Parent.EmailValidation != null)
                EmailBinding.ValidationRules.Add(Parent.EmailValidation as ValidationRule);
            if (Parent.PhoneValidation != null)
                PhoneBinding.ValidationRules.Add(Parent.PhoneValidation as ValidationRule);

            this.Paren = Parent;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Person p = new Person
            {
                Name = Name.Text,
                Surname = Surname.Text,
                Email = Email.Text,
                Phone = Phone.Text,
                Gender = Gender.SelectedIndex == 0 ? WPF16.Gender.male : WPF16.Gender.female
            };
            Paren.People.Add(p);
            Close();
        }

        private void Name_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
        }
    }
}
