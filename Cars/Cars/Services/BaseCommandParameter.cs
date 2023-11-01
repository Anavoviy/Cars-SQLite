using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Cars.Services
{
    public class BaseCommandParameter : ICommand
    {
        private Action<object> execute;
        private Func<bool> canExecute;

        public BaseCommandParameter(Action<object> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
