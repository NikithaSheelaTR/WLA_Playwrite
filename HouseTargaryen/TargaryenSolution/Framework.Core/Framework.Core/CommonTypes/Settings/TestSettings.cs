namespace Framework.Core.CommonTypes.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    using Framework.Core.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// 
    /// </summary>
    public class TestSettings : ITestSettings
    {
        #region TestSetting Dictionary Declaration 

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<TestSettingKey, TestSetting> Settings = new
         Dictionary<TestSettingKey, TestSetting>();

        #endregion

        #region Class Variable Declaration 

        /// <summary>
        /// 
        /// </summary>
        protected Type KeysClass;

        #endregion

        #region Class Constructors

        /// <summary>
        /// Default TestSettings constructor.
        /// </summary>
        public TestSettings()
        {
            this.KeysClass = this.InitKeys().GetType();
            this.InitSettings();
        }

        /// <summary>
        /// Copy TestSettings constructor.
        /// </summary>
        /// <param name="testSettings"></param>
        public TestSettings(TestSettings testSettings)
        {
            this.KeysClass = this.InitKeys().GetType();
            this.Settings = testSettings.Settings;
        }

        #endregion

        #region Initialize Functions 

        /// <summary>
        /// Initializes the TestSettingKeys for the TestSettings.
        /// IMPORTANT: This MUST be overridden.
        /// </summary>
        /// <returns>The newly-initialized TestSettingKeys for the TestSettings.</returns>
        public virtual TestSettingKeys InitKeys() 
        {
            var keys = new TestSettingKeys();
            keys.InitKeys();
            return keys;
        }

        /// <summary>
        /// Initializes the TestSettings, adding an object into the HashMap for each of the test TestSettingKeys.
        /// </summary>
        protected void InitSettings()
        {
            foreach (var testSettingKey in this.GetKeys())
            {                
                this.AddSetting(testSettingKey.Type, testSettingKey);
            }
        }

        /// <summary>
        /// Adds a TestSetting object into the TestSetting Dictionary.
        /// </summary>
        /// <param name="T"></param>
        /// <param name="key">The TestSettingKey of the TestSetting being added to the HashMap.</param>
        /// <returns>The newly-created TestSetting object.</returns>
        protected TestSetting AddSetting(Type T, TestSettingKey key)
        {
            MethodInfo method = this.GetType().GetMethod("AddSettingForceType")
                                    .MakeGenericMethod(T);
            var setting = (TestSetting)method.Invoke(this, new object[] { key });
            if (this.Settings.ContainsKey(key))
            {
                this.Settings.Remove(key);
            }
            this.Settings.Add(key, setting);

            return setting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public TestSetting AddSettingForceType<T>(TestSettingKey<T> key)
        {
            return new TestSetting<T>(key, key.GetDefaultValue());
        }

        #endregion

        #region Getter Functions 

        /// <summary>
        /// Returns the TestSettingKeys Class associated with these TestSettings.
        /// </summary>
        /// <returns>The TestSettingKeys Class associated with these TestSettings.</returns>
        public Type GetKeysClass()
        {
            return this.KeysClass;
        }

        /// <summary>
        /// Returns an array of TestSettingKey objects mapping to the TestSettings.
        /// </summary>
        /// <returns>An array of TestSettingKey objects mapping to the TestSettings.</returns>
        public TestSettingKey[] GetKeys()
        {
            var keysClass = this.GetKeysClass();
            var keys = this.GetKeys(keysClass);
            return keys;
        }

        /// <summary>
        /// Returns an array of TestSettingKey objects mapping to the TestSettings that match the specified group.
        /// </summary>
        /// <param name="keyGroup">The key group that the desired TestSettingKeys should belong to.</param>
        /// <returns>An array of TestSettingKey objects mapping to the TestSettings that match the specified group.</returns>
        public TestSettingKey[] GetKeys(string keyGroup)
        {
            var keysClass = this.GetKeysClass();
            var keys = this.GetKeys(keysClass, keyGroup);
            return keys;
        }

        /// <summary>
        /// Returns an array of TestSettingKey objects mapping to the specified Class of TestSettingKeys.
        /// </summary>
        /// <param name="keysClass">The TestSettingKeys Class that you want the TestSettingKey objects for.</param>
        /// <returns>An array of TestSettingKey objects mapping to the specified Class of TestSettingKeys.</returns>
        public TestSettingKey[] GetKeys(Type keysClass)
        {
            
            try
            {
                return ReflectionUtils.Invoke<TestSettingKey[]>(typeof (TestSettingKey[]), "GetKeys",
                    keysClass);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        /// <summary>
        /// Returns an array of TestSettingKey objects mapping to the specified Class of TestSettingKeys that match the specified group.
        /// </summary>
        /// <param name="keysClass"> The TestSettingKeys Class that you want the TestSettingKey objects for.</param>
        /// <param name="keyGroup">The key group that the desired TestSettingKeys should belong to.</param>
        /// <returns>An array of TestSettingKey objects mapping to the specified Class of TestSettingKeys that match the specified group.</returns>
        public TestSettingKey[] GetKeys(Type keysClass, string keyGroup)
        {

            try
            {
                return ReflectionUtils.Invoke<TestSettingKey[]>(typeof (TestSettingKey[]), "GetKeys",
                    keysClass, keyGroup);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        /// <summary>
        /// Returns a TestSettingKey corresponding to the specified name.
        /// </summary>
        /// <param name="keyName">The name of the desired TestSettingKey.</param>
        /// <returns> A TestSettingKey corresponding to the specified name.</returns>
        /// <exception cref="NullReferenceException">Thrown if the specified keyName is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified keyName doesn't match one of the TestSettingKeys.</exception>
        public TestSettingKey GetKey(string keyName)
        {
            // Check if the keyName argument is null
            if (keyName == null)
            {
                throw new NullReferenceException("The keyName argument cannot be null.");
            }

            // Loop through the keys and look for a match
            var testSettingKeys = this.GetKeys();
            foreach (var key in testSettingKeys)
            {
                if (key.GetName().Equals(keyName))
                {
                    return key;
                }
            }
            
            throw new ArgumentException("Cannot be parsed into a TestSettingKey: '" + keyName + "'.");
        }

        /// <summary>
        /// Returns an array of TestSetting objects corresponding to the specified Class of TestSettingKeys that also match the specified key group.
        /// </summary>
        /// <param name="keyGroup">The key group that the desired TestSettings should belong to.</param>
        /// <returns>An array of TestSetting corresponding to the specified TestSettingKeys class that also match the specified key group.</returns>
        public TestSetting[] GetSettings(string keyGroup = null)
        {
            var testSettingKeys = this.GetKeys(this.KeysClass, keyGroup);
            var returnArray = new TestSetting[testSettingKeys.Length];
            for (var i = 0; i < testSettingKeys.Length; i++)
            {
                MethodInfo method = this.GetType().GetMethod("GetSettingFromKey").MakeGenericMethod(testSettingKeys[i].Type);
                var testSetting = (TestSetting)method.Invoke(this, new object[] { testSettingKeys[i] });
                returnArray[i] = testSetting;
            }
            return returnArray;
        }

        /// <summary>
        /// Returns a TestSetting corresponding to the specified key setting name.
        /// </summary>
        /// <param name="keyName">The name of the KeySetting corresponding to the desired TestSetting.</param>
        /// <returns>A TestSetting corresponding to the specified key setting name.</returns>
        /// <exception cref="NullReferenceException">Thrown if the specified keyName is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the specified keyName doesn't match one of the TestSettings.</exception>
        public TestSetting GetSetting<T>(string keyName)
        {
            // Check if the keyName argument is null
            if (keyName == null)
            {
                throw new NullReferenceException("The keyName argument cannot be null.");
            }

            // Loop through the keys and look for a match
            var testSettingKeys = this.GetKeys();
            foreach (var t in testSettingKeys)
            {
                var key = t;
                if (key.GetName().Equals(keyName))
                {
                    var testSetting = this.GetSetting<T>(key);
                    return testSetting;
                }
            }
            throw new ArgumentException("Cannot be parsed into a TestSetting: '" + keyName + "'.");
        }

        /// <summary>
        /// Returns the TestSetting object corresponding to the specified TestSettingKey.
        /// </summary>
        /// <param name="key">The TestSettingKey corresponding to the desired TestSetting.</param>
        /// <returns>The TestSetting object corresponding to the specified TestSettingKey.</returns>
        public TestSetting<T> GetSetting<T>(TestSettingKey key)
        {
            if (this.Settings.ContainsKey(key))
            {
                var setting = this.Settings[key];
                return (TestSetting<T>) setting;
            }
            else
            {
                Console.Error.WriteLine("Unable to retrieve test setting for key - " + key.GetName());
                return null;
            }

        }
        /// <summary>
        /// Returns the TestSetting object corresponding to the specified TestSettingKey.
        /// </summary>
        /// <param name="key">The TestSettingKey corresponding to the desired TestSetting.</param>
        /// <returns>The TestSetting object corresponding to the specified TestSettingKey.</returns>
        public TestSetting<T> GetSettingFromKey<T>(TestSettingKey key)
        {
            var setting = this.Settings[key];
            return (TestSetting<T>)setting;
        }

        /// <summary>
        /// Returns the value of the TestSetting object corresponding to the specified TestSettingKey.
        /// </summary>
        /// <param name="key">The TestSettingKey corresponding to the TestSetting being set.</param>
        /// <returns>The value of the TestSetting object corresponding to the specified TestSettingKey.</returns>
        public T GetValue<T>(TestSettingKey key)
        {
            var setting = this.GetSetting<T>(key);
            var value = setting.GetValue();
            return value;
        }

        /// <summary>
        /// Returns whether or not the value of the TestSetting object corresponding to the specified TestSettingKey has been modified.
        /// Ultimately, this just checks to see if the value is null.
        /// </summary>
        /// <param name="key">The TestSettingKey corresponding to the TestSetting being set.</param>
        /// <returns>True if the TestSetting's value is currently not null and false if it is.</returns>
        public bool IsValueSet<T>(TestSettingKey<T> key)
        {
            var setting = this.GetSetting<T>(key);
            var isSet = setting.IsSet();
            return isSet;
        }

        #endregion

        #region Setter Functions

        /// <summary>
        /// Sets the value of the TestSetting corresponding to the specified test TestSettingKey to the specified value.
        /// If the TestSetting value is locked, this won't change anything.
        /// </summary>
        /// <param name="key">The test TestSettingKey corresponding to the TestSetting being modified.</param>
        /// <param name="value">The value being set.</param>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting SetValue<T>(TestSettingKey<T> key, T value)
        {
            var setting = this.GetSetting<T>(key);
            var newTestSetting = setting.SetValue(value);
            if (this.Settings.ContainsKey(key))
            {
                this.Settings.Remove(key);
            }
            this.Settings.Add(key, newTestSetting);
            return newTestSetting;
        }

        /// <summary>
        /// Sets the value of the TestSetting corresponding to the specified test TestSettingKey to the specified value if it's not already set.
        /// If the TestSetting value is locked, this won't change anything regardless of whether its set or not.
        /// </summary>
        /// <param name="key"> The test TestSettingKey corresponding to the TestSetting being modified.</param>
        /// <param name="value">The value being set.</param>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting SetValueIfNotSet<T>(TestSettingKey<T> key, T value)
        {
            var setting = this.GetSetting<T>(key);
            if (!setting.IsSet())
            {
                var newTestSetting = setting.SetValue(value);
                if (this.Settings.ContainsKey(key))
                {
                    this.Settings.Remove(key);
                }
                this.Settings.Add(key, newTestSetting);
                return newTestSetting;
            }
            return setting;
        }

        /// <summary>
        /// Sets the value of the TestSetting corresponding to the specified test TestSettingKey to the specified value String.
        /// If the TestSetting value is locked, this won't change anything.
        /// This converts the String value to a value it can understand based on the TestSettingKey's value type.
        /// </summary>
        /// <param name="key">The test TestSettingKey corresponding to the TestSetting being modified.</param>
        /// <param name="value">The String value being set.</param>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting<T> SetValueByString<T>(TestSettingKey<T> key, string value)
        {
            var setting = this.GetSetting<T>(key);
            var newTestSetting = setting.SetValueByString(value);
            if (this.Settings.ContainsKey(key))
            {
                this.Settings.Remove(key);
            }
            this.Settings.Add(key, newTestSetting);
            return newTestSetting;
        }

        /// <summary>
        /// Locks the value of the TestSetting corresponding to the specified test TestSettingKey so it cannot be modified.
        /// </summary>
        /// <param name="key">The test TestSettingKey corresponding to the TestSetting being locked.</param>
        /// <returns>The TestSetting object after it is locked.</returns>
        public TestSetting<T> LockValue<T>(TestSettingKey<T> key)
        {
            var setting = this.GetSetting<T>(key);
            var newTestSetting = setting.LockValue();
            if (this.Settings.ContainsKey(key))
            {
                this.Settings.Remove(key);
            }
            this.Settings.Add(key, newTestSetting);
            return newTestSetting;
        }

        /// <summary>
        /// Unlocks the value of the TestSetting corresponding to the specified test TestSettingKey so it can be modified.
        /// </summary>
        /// <param name="key">The test TestSettingKey corresponding to the TestSetting being unlocked.</param>
        /// <returns>The TestSetting object after it is unlocked.</returns>
        public TestSetting UnlockValue<T>(TestSettingKey<T> key)
        {
            var setting = this.GetSetting<T>(key);
            var newTestSetting = setting.UnlockValue();
            if (this.Settings.ContainsKey(key))
            {
                this.Settings.Remove(key);
            }
            this.Settings.Add(key, newTestSetting);
            return newTestSetting;
        }

        /// <summary>
        /// Resets the value of the TestSetting corresponding to the specified test TestSettingKey so it's back to an unmodified state.
        /// </summary>
        /// <param name="key">The test TestSettingKey corresponding to the TestSetting being reset.</param>
        /// <returns>The TestSetting object after it is reset.</returns>
        public TestSetting ResetValue<T>(TestSettingKey<T> key)
        {
            TestSetting<T> setting = this.GetSetting<T>(key);
            var newTestSetting = setting.ResetValue();
            if (this.Settings.ContainsKey(key))
            {
                this.Settings.Remove(key);
            }
            this.Settings.Add(key, newTestSetting);
            return newTestSetting;
        }


        /// <summary>
        /// Returns a copy of this TestSettings object for copying purposes.
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <returns>A copy of this TestSettings object for copying purposes.</returns>
        public TS CastTestSettings<TS>()
        {
            return (TS) Convert.ChangeType(this, typeof (TS));
        }

        #endregion

        #region Object Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("[" + this.GetType().Name + "] { ");
            var testSettings = this.Settings.Values;
            foreach (var testSetting in testSettings)
            {
                builder.Append(testSetting + ", ");
            }
            if (testSettings.Count > 0)
            {
                builder.Remove(builder.Length - 2, 2);
            }
            builder.Append(" }");
            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testContext"></param>
        public virtual void CheckOutUser(TestContext testContext)
        {
            Console.WriteLine("CheckOutUser not implemented in TestSettings.");
        }
    }
}
