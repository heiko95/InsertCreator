using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Private Fields

        /// <summary>
        /// Dictionary for property values
        /// </summary>
        private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        #endregion Private Fields

        #region Public Events

        /// <summary>
        /// Eventhandler for property change events
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Protected Methods

        /// <summary>
        /// Method to get a property value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected TValue GetValue<TValue>(TValue defaultValue = default(TValue), [CallerMemberName] string propertyName = null)
        {
            string key = $"{typeof(TValue)}::{propertyName}";
            return _propertyValues.ContainsKey(key) ? (TValue)_propertyValues[key] : defaultValue;
        }

        /// <summary>
        /// Notify Property Changed Invocator
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method to set a property value and trigger the propertyChanged event
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="callback"></param>
        /// <param name="propertyName"></param>
        protected void SetValue<TValue>(TValue value, Action callback = null, [CallerMemberName] string propertyName = null)
        {
            string key = $"{typeof(TValue)}::{propertyName}";
            if (_propertyValues.ContainsKey(key))
            {
                if (_propertyValues[key] != null && _propertyValues[key].Equals(value))
                    return;

                IComparable comparableValue = value as IComparable;
                IComparable comparableDictionary = _propertyValues[key] as IComparable;
                if (comparableValue != null && comparableDictionary != null)
                {
                    if (!Equals(comparableValue, comparableDictionary))
                    {
                        _propertyValues[key] = value;
                        OnPropertyChanged(propertyName);
                    }
                }
                else
                {
                    _propertyValues[key] = value;
                    OnPropertyChanged(propertyName);
                }
            }
            else
            {
                _propertyValues.Add(key, value);
                OnPropertyChanged(propertyName);
            }

            callback?.Invoke();
        }

        #endregion Protected Methods
    }
}