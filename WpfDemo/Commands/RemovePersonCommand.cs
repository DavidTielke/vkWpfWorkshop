using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDemo.Commands
{
    public class RemovePersonCommand : ICommand
    {
        private readonly MainWindow _mainWindow;

        public RemovePersonCommand(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public bool CanExecute(object parameter)
        {
            return _mainWindow.AusgewähltePerson != null;
        }

        public void Execute(object parameter)
        {
            _mainWindow.Personen.Remove(_mainWindow.AusgewähltePerson);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
