using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WpfDemo.Annotations;

namespace WpfDemo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Person _ausgewähltePerson;
        private readonly bool _kannGelöschtWerden;

        public ObservableCollection<Person> Personen { get; set; }

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

        private void AusgewähltePersonWurdeGeändert(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Vorname")
            {
                OnPropertyChanged("VornameZeichenVerbleibend");
            }
        }

        public bool KannGelöschtWerden => AusgewähltePerson != null;

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

        public MainWindow()
        {
            Personen = new ObservableCollection<Person>
            {
                new Person {Vorname = "Maximilian", Nachname="Tielke", IsMale = true},
                new Person {Vorname = "Lena", Nachname="Tielke", IsMale = false},
                new Person {Vorname = "David", Nachname="Tielke", IsMale = true},
                new Person {Vorname = "Hasi", Nachname="Tielke", IsMale = false},
            };
            InitializeComponent();

            DataContext = this;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var del = PropertyChanged;
            if (del != null)
            {
                del(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RemoveButtonHandler(object sender, RoutedEventArgs e)
        {
            Personen.Remove(AusgewähltePerson);
        }
    }
}
