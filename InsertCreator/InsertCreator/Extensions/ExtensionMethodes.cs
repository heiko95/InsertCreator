using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Liedeinblendung.Extensions
{
    /// <summary>
    /// Includes extension methods
    /// </summary>
    public static class ExtensionMethodes
    {
        #region ObservableCollection extensions

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

        #endregion ObservableCollection extensions
    }
}