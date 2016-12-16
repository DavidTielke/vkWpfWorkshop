﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfDemo.Annotations;

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
            RemovePersonCommand = new RelayCommand(ExecuteRemovePerson, CanExecuteRemovePerson);
            CreatePersonCommand = new RelayCommand(ExecuteCreatePerson);
            ClearPersonsCommand = new RelayCommand(ExecuteClearPersons, CanExecuteClearPersons);
            InitializeComponent();

            DataContext = this;
        }

        public ICommand ClearPersonsCommand { get; set; }

        public ICommand CreatePersonCommand { get; set; }

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

        private bool CanExecuteClearPersons(object arg)
        {
            return Personen.Count > 0;
        }

        private void ExecuteClearPersons(object obj)
        {
            Personen.Clear();
        }

        private void ExecuteCreatePerson(object obj)
        {
            var person = new Person();
            Personen.Add(person);
            AusgewähltePerson = person;
        }

        private bool CanExecuteRemovePerson(object arg)
        {
            return AusgewähltePerson != null;
        }

        private void ExecuteRemovePerson(object obj)
        {
            Personen.Remove(AusgewähltePerson);
        }

        private void AusgewähltePersonWurdeGeändert(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Vorname")
            {
                OnPropertyChanged("VornameZeichenVerbleibend");
            }
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