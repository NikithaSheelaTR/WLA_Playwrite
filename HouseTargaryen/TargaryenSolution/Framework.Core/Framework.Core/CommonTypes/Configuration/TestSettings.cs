namespace Framework.Core.CommonTypes.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;

    /// <summary>
    /// The class that extracts test settings.
    /// </summary>
    public sealed class TestSettings : IObservable<SettingUpdateContext>
    {
        /// <summary>
        /// The default delimiter.
        /// </summary>
        public const string DefaultDelimiter = ",";

        /// <summary>
        /// The settings cache
        /// </summary>
        private readonly IDictionary<string, string> settingsCache;

        private readonly List<IObserver<SettingUpdateContext>> settingsObservers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestSettings"/> class.
        /// </summary>
        public TestSettings()
        {
            this.settingsCache = new Dictionary<string, string>();
            this.settingsObservers = new List<IObserver<SettingUpdateContext>>();
        }

        /// <summary>
        /// Adds the setting to the test settings cache.
        /// </summary>
        /// <param name="settingName">The name of the setting to add.</param>
        /// <param name="settingValue">The value of the setting.</param>
        /// <param name="updateOption">Specifies, how to set the values of test settings.</param>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        public TestSettings Append(string settingName, string settingValue, SettingUpdateOption updateOption)
        {
            return this.Append(new KeyValuePair<string, string>(settingName, settingValue), updateOption);
        }

        /// <summary>
        /// Adds the setting to the test settings cache.
        /// </summary>
        /// <param name="setting">The key-value pair of the setting name and value to add.</param>
        /// <param name="updateOption">Specifies, how to set the values of test settings.</param>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        public TestSettings Append(KeyValuePair<string, string> setting, SettingUpdateOption updateOption)
        {
            var context = new SettingUpdateContext { TestSetting = setting, UpdateOption = updateOption };

            if (this.AppendInternal(context))
            {
                this.settingsObservers.ForEach(o => o.OnNext(context));
            }

            return this;
        }

        /// <summary>
        /// Appends the values to the value of the specified setting, given the setting update mode. provided that the values to append are not contained within
        /// the values of the look-up setting keys.
        /// </summary>
        /// <param name="settingKey">The setting key whose value to append the provided values to.</param>
        /// <param name="lookupSettingKeys">The lookup setting keys to scan their values for occurrences of the values to append. For example, paired settings that cannot overlap in their values (FACs, IACs, SFs).</param>
        /// <param name="updateOption">Specifies, how to set the values of test settings.
        /// <see cref="SettingUpdateOption.UpdateIfUnset"/>: Only values that are not contained in the lookup setting key array are considered for appending.
        /// <see cref="SettingUpdateOption.Append"/>: Only values that are not contained in the target setting key's value array are considered. The valuesToAppend are removed from the arrays of values of the lookup setting keys.
        /// <see cref="SettingUpdateOption.Overwrite"/>: All the provided values are added to the target setting key's value array replacing its content. The valuesToAppend are removed from the arrays of values of the lookup setting keys.
        /// </param>
        /// <param name="valuesToAppend">The values to append to the value of the setting key.</param>
        /// <typeparam name="TValue">The type of the value to append.</typeparam>
        /// <returns>The <see cref="TestSettings"/>Specifies, how to set the values of test settings.</returns>
        public TestSettings AppendValues<TValue>(
            string settingKey,
            string[] lookupSettingKeys,
            SettingUpdateOption updateOption,
            params TValue[] valuesToAppend)
        {
            if (settingKey != null && valuesToAppend != null)
            {
                string[] values =
                    valuesToAppend.Select(v => v == null ? null : v.ToString())
                                  .Where(v => !string.IsNullOrEmpty(v))
                                  .ToArray();

                if (updateOption == SettingUpdateOption.UpdateIfUnset)
                {
                    List<string> examinedSettingKeys =
                        new List<string> { settingKey }.AppendUniqueValues(lookupSettingKeys);

                    foreach (
                        string valueToAdd in values.Where(v => examinedSettingKeys.All(s => !this.HasValue(s, v, true)))
                    )
                    {
                        this.Append(settingKey, valueToAdd, SettingUpdateOption.Append);
                    }
                }
                else
                {
                    this.RemoveSubvalues(lookupSettingKeys, values);

                    if (updateOption == SettingUpdateOption.Overwrite)
                    {
                        this.Append(settingKey, string.Join(DefaultDelimiter, values), SettingUpdateOption.Overwrite);
                    }
                    else
                    {
                        foreach (string valueToAdd in values.Where(v => !this.HasValue(settingKey, v, true)))
                        {
                            this.Append(settingKey, valueToAdd, SettingUpdateOption.Append);
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Clears the test settings cache.
        /// </summary>
        public void Clear()
        {
            this.settingsCache.Clear();
        }

        /// <summary>
        /// Gets the value of a test setting.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <returns>The value of the test setting.</returns>
        public string GetValue(string settingKey)
        {
            string result;

            this.settingsCache.TryGetValue(settingKey, out result);
            return result;
        }

        /// <summary>
        /// Gets the value of a test setting and converts it to desired.
        /// </summary>
        /// <typeparam name="T">The target reference type to convert the string setting value to.</typeparam>
        /// <param name="settingKey">The setting key.</param>
        /// <returns>The value of the test setting converted to type that is desired.</returns>
        public T GetValueAsRef<T>(string settingKey) where T : class, IConvertible
        {
            return this.GetValue(settingKey).ConvertToRefType<T>();
        }

        /// <summary>
        /// Gets the value of a test setting and converts it to <see cref="Nullable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The target value type to convert the string setting value to.</typeparam>
        /// <param name="settingKey">The setting key.</param>
        /// <returns>The value of the test setting converted to type that is desired.</returns>
        public T? GetValueAsStruct<T>(string settingKey) where T : struct, IConvertible
        {
            return this.GetValue(settingKey).ConvertToValueType<T>();
        }

        /// <summary>
        /// Gets an array of string values from the value of a test setting.
        /// The tokens are supposed to be separated by commas and/or semicolons.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <returns>The values of the test setting.</returns>
        public string[] GetValues(string settingKey)
        {
            string value = this.GetValue(settingKey);

            return value == null ? new string[0] : value.Split(',', ';');
        }

        /// <summary>
        /// Gets an array of desired values from the value of a test setting.
        /// The tokens are supposed to be separated by commas and/or semicolons.
        /// </summary>
        /// <typeparam name="T">The target value type to convert each parsed string token value to.</typeparam>
        /// <param name="settingKey">The setting key.</param>
        /// <returns>The array of desired values of the test setting.</returns>
        public T[] GetValuesAs<T>(string settingKey) where T : struct, IConvertible
        {
            var result = new List<T>();

            foreach (string value in this.GetValues(settingKey))
            {
                T? convertedValue = value.ConvertToValueType<T>();

                if (convertedValue.HasValue)
                {
                    result.Add(convertedValue.Value);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Checks if the test setting cache contains the value by the specified key.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <param name="valueToCheck">The value to check.</param>
        /// <param name="seekWithinValueArray">Specifies whether the occurrences of the value should be sought for in the test setting value array.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool HasValue(string settingKey, string valueToCheck, bool seekWithinValueArray)
        {
            return string.Equals(valueToCheck, this.GetValue(settingKey), StringComparison.InvariantCulture)
                   || (seekWithinValueArray
                       && this.GetValues(settingKey).Contains(valueToCheck, StringComparer.InvariantCulture));
        }

        /// <summary>
        /// Adds the values of the settings from environment variables to the test settings cache.
        /// </summary>
        /// <param name="updateOption">Specifies, how to set the values of test settings.</param>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        [SuppressMessage("ReSharper", "AccessToForEachVariableInClosure", Justification = "Suppression is alright here."
        )]
        public TestSettings InitFromEnvironment(SettingUpdateOption updateOption)
        {
            var settings = new Dictionary<string, string>();

            foreach (string key in this.settingsCache.Keys.ToArray())
            {
                string envValue = string.Empty;

                if (SafeMethodExecutor.Execute(() => envValue = Environment.GetEnvironmentVariable(key)).ResultType
                    == ExecutionResultType.Success && envValue != null)
                {
                    settings.Add(key, envValue);
                }
            }

            return this.InitFromPairs(settings, updateOption);
        }

        /// <summary>
        /// Adds the settings from a file to the test settings cache.
        /// </summary>
        /// <param name="configFile">The configuration file with settings.</param>
        /// <param name="updateOption">Specifies, how to set the values of test settings.</param>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        public TestSettings InitFromFile(string configFile, SettingUpdateOption updateOption)
        {
            TestSettings result;
            var reader = new TestSettingsReader();

            try
            {
                reader.ReadSettings(configFile);
                result = this.InitFromPairs(reader.Settings, updateOption);
            }
            catch (FileNotFoundException)
            {
                result = this;
            }

            return result;
        }

        /// <summary>
        /// Adds the settings from the list of key-value pairs to the test settings cache.
        /// </summary>
        /// <param name="pairs">The pairs to add.</param>
        /// <param name="updateOption">Specifies, how to set the values of test settings.</param>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        public TestSettings InitFromPairs(
            IEnumerable<KeyValuePair<string, string>> pairs,
            SettingUpdateOption updateOption)
        {
            if (pairs != null)
            {
                foreach (KeyValuePair<string, string> pair in pairs.Where(p => p.Key != null))
                {
                    this.Append(pair, updateOption);
                }
            }

            return this;
        }

        /// <summary>
        /// Initializes the settings cache from the specified test settings object.
        /// </summary>
        public TestSettings UpdateFromTestProperties(IDictionary properties)
        {
            var pairs = new List<KeyValuePair<string, string>>();
            foreach (var k in properties.Keys)
            {
                if (settingsCache.ContainsKey(k.ToString()))
                {
                    pairs.Add(new KeyValuePair<string, string>(k.ToString(), properties[k].ToString()));
                }
            }
            return this.InitFromPairs(pairs, SettingUpdateOption.Append);
        }

        /// <summary>
        /// Initializes the settings cache from the specified test settings object.
        /// </summary>
        /// <param name="settings">The settings object to copy key-value pairs from.</param>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        public TestSettings InitFromSettings(TestSettings settings)
        {
            this.Clear();

            if (settings != null)
            {
                this.InitFromPairs(settings.settingsCache, SettingUpdateOption.Overwrite);
            }

            return this;
        }

        /// <summary>
        /// Removes the specified values from the delimiter-separated list of values stored by each specified setting key.
        /// </summary>
        /// <param name="settingKeys">The setting keys to look their values through.</param>
        /// <param name="subvaluesToRemove">The values to remove from the array of delimiter-separated values.</param>
        /// <typeparam name="TValue">The type of the value to remove.</typeparam>
        /// <returns>The <see cref="TestSettings"/>.</returns>
        [SuppressMessage("ReSharper", "AccessToForEachVariableInClosure", Justification = "Suppression is alright there"
        )]
        public TestSettings RemoveSubvalues<TValue>(string[] settingKeys, params TValue[] subvaluesToRemove)
        {
            if (settingKeys != null && subvaluesToRemove != null)
            {
                foreach (
                    string subvalueToRemove in
                    subvaluesToRemove.Where(v => v != null)
                                     .Select(v => v.ToString())
                                     .Where(v => !string.IsNullOrEmpty(v)))
                {
                    foreach (string settingKey in settingKeys.Where(k => this.HasValue(k, subvalueToRemove, true)))
                    {
                        string oldValue = this.settingsCache[settingKey];
                        string newValue = string.Join(
                            DefaultDelimiter,
                            this.GetValues(settingKey)
                                .Where(k => !k.Equals(subvalueToRemove, StringComparison.InvariantCulture)));

                        if (oldValue != newValue)
                        {
                            var context = new SettingUpdateContext
                                              {
                                                  TestSetting =
                                                      new KeyValuePair<string, string>(
                                                          settingKey,
                                                          newValue),
                                                  UpdateOption = SettingUpdateOption.Overwrite
                                              };
                            this.settingsCache[settingKey] = newValue;
                            this.settingsObservers.ForEach(o => o.OnNext(context));
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Subscribes the test settings observer to receive change notifications.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>The <see cref="IDisposable"/>.</returns>
        public IDisposable Subscribe(IObserver<SettingUpdateContext> observer)
        {
            if (!this.settingsObservers.Contains(observer))
            {
                this.settingsObservers.Add(observer);
            }

            return new Unsubscriber<SettingUpdateContext>(this.settingsObservers, observer);
        }

        private bool AppendInternal(SettingUpdateContext settingUpdateContext)
        {
            bool result = false;

            if (settingUpdateContext.TestSetting.Key != null)
            {
                if (this.settingsCache.ContainsKey(settingUpdateContext.TestSetting.Key))
                {
                    string val = this.settingsCache[settingUpdateContext.TestSetting.Key];

                    if (string.IsNullOrEmpty(val) || settingUpdateContext.UpdateOption == SettingUpdateOption.Overwrite)
                    {
                        this.settingsCache[settingUpdateContext.TestSetting.Key] =
                            settingUpdateContext.TestSetting.Value;
                        result = true;
                    }
                    else if (settingUpdateContext.UpdateOption == SettingUpdateOption.Append
                             && !string.IsNullOrEmpty(settingUpdateContext.TestSetting.Value))
                    {
                        this.settingsCache[settingUpdateContext.TestSetting.Key] = (string.IsNullOrEmpty(val)
                                                                                        ? string.Empty
                                                                                        : val + DefaultDelimiter)
                                                                                   + settingUpdateContext.TestSetting
                                                                                                         .Value;
                        result = true;
                    }
                }
                else
                {
                    this.settingsCache.Add(settingUpdateContext.TestSetting);
                    result = true;
                }
            }

            return result;
        }
    }
}