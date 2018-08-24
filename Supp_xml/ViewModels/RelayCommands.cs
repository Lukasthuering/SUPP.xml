using System;
using System.Windows.Input;

namespace Supp_xml.ViewModels
{
    public class RelayCommands : ICommand
    {
        #region Declarations

        readonly Func<Boolean> _canExecute;
        readonly Action _execute;
        private DateTime startdate;

        #endregion

        #region Constructors

        public RelayCommands(Action execute) : this(execute, null) { }

        public RelayCommands(DateTime startdate)
        {
            this.startdate = startdate;
        }

        public RelayCommands(Action execute, Func<Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(Object parameter)
        {
            _execute();
        }

        #endregion
    }
}
