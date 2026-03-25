namespace Framework.Core.CommonTypes.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The settings collection extension.
    /// </summary>
    public static class SettingsCollectionExtension
    {
        /// <summary>
        /// Adds a key-value pair to the dictionary. If a dictionary is null, it will be created.
        ///  If a pair with the key already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="TKey">Type of key.</typeparam>
        /// <typeparam name="TValue">Type of value to add.</typeparam>
        /// <param name="settingsCollection">Target dictionary.</param>
        /// <param name="keyToAdd">Key to add.</param>
        /// <param name="valueToAdd">Value to add.</param>
        /// <param name="overwriteExisting">Overwrite the pair if the key already exists.</param>
        /// <returns> The <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> Append<TKey, TValue>(
            this Dictionary<TKey, TValue> settingsCollection,
            TKey keyToAdd,
            TValue valueToAdd,
            bool overwriteExisting)
        {
            if (settingsCollection == null)
            {
                settingsCollection = new Dictionary<TKey, TValue>();
            }

            if (keyToAdd != null)
            {
                if (settingsCollection.ContainsKey(keyToAdd))
                {
                    if (overwriteExisting)
                    {
                        settingsCollection[keyToAdd] = valueToAdd;
                    }
                    else if (typeof(TValue) == typeof(string))
                    {
                        settingsCollection[keyToAdd] =
                            (TValue)
                            Convert.ChangeType(
                                string.Join(",", settingsCollection[keyToAdd], valueToAdd),
                                typeof(TValue));
                    }
                }
                else
                {
                    settingsCollection.Add(keyToAdd, valueToAdd);
                }
            }

            return settingsCollection;
        }

        /// <summary>
        /// Creates key-value pairs based on the specified collection of keys in association with the value and adds them to the dictionary.
        /// If a dictionary is null, it will be created.
        /// If a pair with the key already exists, it will be overwritten.
        /// </summary>
        /// <param name="settingsCollection">Target dictionary.</param>
        /// <param name="value">The value to associate with each key-value pair.</param>
        /// <param name="keys">The keys to form the key-value pairs.</param>
        /// <typeparam name="TKey">Type of keys.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <returns>The <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> AppendRoutingSettings<TKey, TValue>(
            this Dictionary<TKey, TValue> settingsCollection,
            TValue value,
            params TKey[] keys)
        {
            if (settingsCollection == null)
            {
                settingsCollection = new Dictionary<TKey, TValue>();
            }

            if (keys == null || !keys.Any())
            {
                return settingsCollection;
            }

            foreach (TKey key in keys)
            {
                settingsCollection.Append(key, value, true);
            }

            return settingsCollection;
        }

        /// <summary>
        /// Looks through the target dictionary and removes key-value pairs for which the specified key-value predicate is true.
        /// If a dictionary is null, it will be created.
        /// </summary>
        /// <param name="settingsCollection">Target dictionary.</param>
        /// <param name="settingsSelector">The key-value predicate.</param>
        /// <typeparam name="TKey">Type of key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <returns>The <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public static Dictionary<TKey, TValue> RemoveRoutingSettings<TKey, TValue>(
            this Dictionary<TKey, TValue> settingsCollection,
            Func<TKey, TValue, bool> settingsSelector)
        {
            if (settingsCollection == null)
            {
                settingsCollection = new Dictionary<TKey, TValue>();
            }

            if (settingsSelector == null)
            {
                return settingsCollection;
            }

            foreach (TKey key in settingsCollection.Keys.ToArray())
            {
                if (settingsSelector(key, settingsCollection[key]))
                {
                    settingsCollection.Remove(key);
                }
            }

            return settingsCollection;
        }
    }
}