using System;
using System.Windows.Input;

namespace Sloccer.UI.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private Action _action;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
            }
        }
    }
}
