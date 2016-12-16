using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfDemo.Annotations;
using WpfDemo.Commands;

namespace WpfDemo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Person _ausgewähltePerson;

        public MainWindow()
        {
            Personen = new ObservableCollection<Person>
            {
                new Person {Vorname = "Maximilian", Nachname = "Tielke", IsMale = true},
                new Person {Vorname = "Lena", Nachname = "Tielke", IsMale = false},
                new Person {Vorname = "David", Nachname = "Tielke", IsMale = true},
                new Person {Vorname = "Hasi", Nachname = "Tielke", IsMale = false}
            };
            RemovePersonCommand = new RemovePersonCommand(this);
            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<Person> Personen { get; set; }
        public ICommand RemovePersonCommand { get; set; }

        public Person AusgewähltePerson
        {
            get { return _ausgewähltePerson; }
            set
            {
                if (Equals(value, _ausgewähltePerson)) return;

                if (_ausgewähltePerson != null)
                {
                    _ausgewähltePerson.PropertyChanged -= AusgewähltePersonWurdeGeändert;
                }

                _ausgewähltePerson = value;

                if (value != null)
                {
                    _ausgewähltePerson.PropertyChanged += AusgewähltePersonWurdeGeändert;
                }

                OnPropertyChanged();
                OnPropertyChanged("KannGelöschtWerden");
                OnPropertyChanged("VornameZeichenVerbleibend");
            }
        }
        
        public int VornameZeichenVerbleibend
        {
            get
            {
                if (AusgewähltePerson == null || AusgewähltePerson.Vorname == null)
                {
                    return 0;
                }
                return 25 - AusgewähltePerson.Vorname.Length;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AusgewähltePersonWurdeGeändert(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Vorname")
            {
                OnPropertyChanged("VornameZeichenVerbleibend");
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            var person = new Person();
            Personen.Add(person);
            AusgewähltePerson = person;
        }

        private void ClearButtonClickHandler(object sender, RoutedEventArgs e)
        {
            Personen.Clear();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var del = PropertyChanged;
            if (del != null)
            {
                del(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}