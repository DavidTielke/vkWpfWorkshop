using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfDemo.Annotations;

namespace WpfDemo
{
    public class Person : INotifyPropertyChanged
    {
        private string _nachname;
        private string _vorname;
        private bool _isMale;

        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (value == _vorname) return;
                _vorname = value;
                OnPropertyChanged();
            }
        }

        public string Nachname
        {
            get { return _nachname; }
            set
            {
                if (value == _nachname) return;
                _nachname = value;
                OnPropertyChanged();
            }
        }

        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                if (value == _isMale) return;
                _isMale = value;
                OnPropertyChanged();
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