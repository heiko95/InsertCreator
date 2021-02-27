using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HgSoftware.InsertCreator.Extensions
{
    /// <summary>
    /// Includes extension methods
    /// </summary>
    public static class ExtensionMethodes
    {
        #region Public Methods

        /// <summary>
        /// Add Range method for the type ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ObservableCollection<T> lista, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                lista.Add(item);
            }
        }

        /// <summary>
        /// Shortens the string at the given position and adds "..." at end
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string LimitString(this string value, int length)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > length)
                return $"{ value.Substring(0, length - 4)} ...";
            return value;
        }

        #endregion Public Methods
    }
}