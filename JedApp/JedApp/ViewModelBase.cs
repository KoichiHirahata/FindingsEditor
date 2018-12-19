using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace JedApp
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<string, string> _ErrorMessages = new Dictionary<string, string>();

        string IDataErrorInfo.Error
        {
            get { return (_ErrorMessages.Count > 0) ? "Has Error" : null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (_ErrorMessages.ContainsKey(columnName))
                    return _ErrorMessages[columnName];
                else
                    return null;
            }
        }

        protected void SetError(string propertyName, string errorMessage)
        {
            _ErrorMessages[propertyName] = errorMessage;
        }

        protected void ClearErrror(string propertyName)
        {
            if (_ErrorMessages.ContainsKey(propertyName))
                _ErrorMessages.Remove(propertyName);
        }

        private class _DelegateCommand : ICommand
        {
            private Action<object> _Command;
            private Func<object, bool> _CanExecute;

            public _DelegateCommand(Action<object> command, Func<object, bool> canExecute = null)
            {
                _Command = command ?? throw new ArgumentNullException();
                _CanExecute = canExecute;
            }

            void ICommand.Execute(object parameter)
            {
                _Command(parameter);
            }

            bool ICommand.CanExecute(object parameter)
            {
                if (_CanExecute != null)
                    return _CanExecute(parameter);
                else
                    return true;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }

        protected ICommand CreateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            return new _DelegateCommand(command, canExecute);
        }
    }
}
