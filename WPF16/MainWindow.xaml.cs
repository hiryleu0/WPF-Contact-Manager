
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.IO;
using Validation.Interface;
using System.Reflection;
using System.ComponentModel;

namespace WPF16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public IValidation NameValidation => NameCombo.SelectedItem as IValidation;
        public IValidation SurnmeValidation => SurnameCombo.SelectedItem as IValidation;
        public IValidation EmailValidation => EmailCombo.SelectedItem as IValidation;
        public IValidation PhoneValidation => PhoneCombo.SelectedItem as IValidation;

        private bool _locked = true;
        public bool Locked
        {
            get
            {
                return _locked;
            }
            set
            {
                _locked = value;
                OnPropertyChanged("Locked");
            }
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Person> People { get; set; }
        public Brush Brush { get {
                var c = new Color();
                c.R = 253;
                c.G = 207;
                c.B = 108;
                c.A = 255;
                return new SolidColorBrush(c);
                     }}
        public Validation.Interface.IValidation[] validations { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            People = new ObservableCollection<Person>();
            validations = CreatePlugin(@"..\..\..\Walidacja");
            DataContext = this;
        }

        //https://stackoverflow.com/questions/5751844/how-to-reference-a-dll-on-runtime?fbclid=IwAR0xFW0nWDhBQ2qEG1qacsnvxyXQ258k5IYZLe7ehekq6YHd17mY55N0XKw
        public IValidation[] CreatePlugin(string path)
        {
            List<IValidation> rules = new List<IValidation>();
            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                foreach (Type assemblyType in Assembly.LoadFrom(file).GetTypes())
                {
                    Type interfaceType = assemblyType.GetInterface(typeof(IValidation).FullName);

                    if (interfaceType != null)
                    {
                       rules.Add((IValidation)Activator.CreateInstance(assemblyType));
                    }
                }
            }

            return rules.ToArray();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var grid = new Grid();
            grid.Background = Brushes.Gray;
            grid.Opacity = 0.5;
            grid.IsEnabled = false;
            this.grid.Children.Add(grid);

            this.IsEnabled = false;
            new Window1(this).ShowDialog();
            this.IsEnabled = true;

            this.grid.Children.Remove(grid);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            People.Clear();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("This is a simple contact manager", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "xml file (*.xml)|*.xml";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
                    xs.Serialize(fs, People);
                    fs.Close();
                    System.Windows.MessageBox.Show("File saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
                System.Windows.MessageBox.Show("File could not be saved", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "xml file (*.xml)|*.xml";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.Open);
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
                    var collection = xs.Deserialize(fs) as ObservableCollection<Person>;
                    People.Clear();
                    foreach (var p in collection)
                        People.Add(p);
                    fs.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                System.Windows.MessageBox.Show("File could not be loaded", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Person p = list.SelectedItem as Person;
            People.Remove(p);
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Locked = !Locked;
        }
    }
}
