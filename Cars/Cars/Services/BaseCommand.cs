using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Cars.Services
{
    public class BaseCommand : ICommand
    {
        private Action execute;
        private Func<bool> canExecute;

        public BaseCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter = null)
        {
            return canExecute == null ? true : canExecute.Invoke();
        }

        public async void Execute(object parameter = null)
        {
            execute.Invoke();
        }
    }
}
