using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.ViewModel
{
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Dictionary for property values
        /// </summary>
        private readonly Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        /// <summary>
        /// Eventhandler for property change events
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
                        InvokePropertyChanged(propertyName);
                    }
                }
                else
                {
                    _propertyValues[key] = value;
                    InvokePropertyChanged(propertyName);
                }
            }
            else
            {
                _propertyValues.Add(key, value);
                InvokePropertyChanged(propertyName);
            }

            callback?.Invoke();
        }

        /// <summary>
        /// Notify Property Changed Invocator
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void InvokePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
