using System;
using System.Windows.Input;

namespace WPFTest
{
    public class DelegateCommand:ICommand
    {
        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc==null || CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        public Action ExecuteAction { get; set; }
        public Action<object> ObjectExecuteAction { get; set; }
        public Func<object,bool> CanExecuteFunc { get; set; }

        public DelegateCommand(Action executeMethod)
        {
            ExecuteAction = executeMethod;
        }

        public DelegateCommand(Action<object> objectExecuteMethod)
        {
            ObjectExecuteAction = objectExecuteMethod;
        }
    }
}