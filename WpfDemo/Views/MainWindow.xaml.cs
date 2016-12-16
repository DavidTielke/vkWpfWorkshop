using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfDemo.Annotations;
using WpfDemo.ViewModels;

namespace WpfDemo
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainWindowViewModel();
            ViewModel.ShowRemovePersonDialog += ShowRemovePersonDialog;

            DataContext = ViewModel;
        }

        private bool ShowRemovePersonDialog(Person arg)
        {
            var result = MessageBox.Show("Wollen Sie die Person " + arg.Vorname + " wirklich löschen?", "Person löschen",
                MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
    }
}