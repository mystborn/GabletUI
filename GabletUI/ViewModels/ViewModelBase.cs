using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GabletUI.ViewModels
{
    public class ViewModelBase : ReactiveObject, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>>? _errors = null;
        private object _errorLock = new object();
        private bool _hasErrors = false;

        public bool HasErrors
        {
            get => _hasErrors;
            set => this.RaiseAndSetIfChanged(ref _hasErrors, value);
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors?.TryGetValue(propertyName ?? "", out var list) ?? false)
                return list;

            return Array.Empty<string>();
        }

        protected void AddError(string propertyName, string error)
        {
            lock (_errorLock)
            {
                _errors ??= new Dictionary<string, List<string>>();

                if (!_errors.TryGetValue(propertyName, out var list))
                {
                    list = new List<string>();
                    _errors[propertyName] = list;
                }

                list.Add(error);

                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

                UpdateHasErrors();
            }
        }

        protected void RemoveError(string propertyName, string error)
        {
            lock (_errorLock)
            {
                _errors ??= new Dictionary<string, List<string>>();

                if (_errors.TryGetValue(propertyName, out var list))
                {
                    if (list.Remove(error))
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }

                UpdateHasErrors();
            }
        }

        protected void ClearErrors(string propertyName)
        {
            lock (_errorLock)
            {
                _errors ??= new Dictionary<string, List<string>>();

                if (_errors.TryGetValue(propertyName, out var list))
                {
                    var size = list.Count;
                    list.Clear();

                    if (size > 0)
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }

                UpdateHasErrors();
            }
        }

        protected void ClearAllErrors()
        {
            lock(_errorLock)
            {
                _errors?.Clear();
            }
        }

        protected void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            lock (_errorLock)
            {
                _errors ??= new Dictionary<string, List<string>>();
                var list = new List<string>(errors);

                if (!_errors.TryGetValue(propertyName, out var existingList))
                {
                    _errors[propertyName] = list;
                    if (list.Count > 0)
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }
                else
                {
                    if (!list.SequenceEqual(existingList))
                    {
                        _errors[propertyName] = list;
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                    }
                }

                UpdateHasErrors();
            }
        }

        public bool HasError(string propertyName)
        {
            lock (_errorLock)
            {
                _errors ??= new Dictionary<string, List<string>>();
                return _errors.TryGetValue(propertyName, out var list) && list.Count > 0;
            }
        }

        private void UpdateHasErrors()
        {
            HasErrors = _errors?.Values.Any(l => l.Count > 0) ?? false;
        }
    }
}
