using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SearchByRegex.Commands
{
    public class BaseCommand : ICommand
    {
        private Action methodToExecute;
        private Func<bool> canExecuteMethod;

        public BaseCommand(Action methodToExecute, Func<bool> canExecuteMethod)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region ICommand Interface

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecuteMethod != null)
                return canExecuteMethod();

            if (methodToExecute != null)
                return true;

            return false;
        }

        public void Execute(object parameter)
        {
            methodToExecute?.Invoke();
        }
    }
}
