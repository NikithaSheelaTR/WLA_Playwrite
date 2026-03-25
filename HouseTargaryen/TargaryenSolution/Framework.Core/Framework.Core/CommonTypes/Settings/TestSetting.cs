namespace Framework.Core.CommonTypes.Settings
{
    using System;

    using Framework.Core.Utils;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// 
    /// </summary>
    public abstract class TestSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract bool IsSet();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract TestSettingKey GetKey();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestSetting<T> : TestSetting
    {

        #region Private Class Variable Declarations 

        private readonly TestSettingKey<T> _key;
        private bool _locked;
        private T _value;
        private bool _set = false;

        #endregion

        #region Class Constructors

        /// <summary>
        /// TestSetting constructor.
        /// </summary>
        /// <param name="key">The TestSettingKey associated with the TestSetting.</param>
        /// <param name="value">The value currently stored in the TestSetting.</param>
        /// <param name="locked">Whether or not the TestSetting value is locked.</param>
        public TestSetting(TestSettingKey<T> key, T value = default(T), bool locked = false)
        {
            this._key = key;
            this._locked = locked;
            if (value == null || value.Equals(key.GetDefaultValue()))
            {
                this._set = false;
                this._value = this.GetDefaultValue();
            }
            else
            {
                this._set = true;
                this._value = value;
            }
        }

        #endregion

        #region Getter Functions

        /// <summary>
        /// Returns the test TestSettingKey associated with the TestSetting.
        /// </summary>
        /// <returns>The test TestSettingKey associated with the TestSetting.</returns>
        public override TestSettingKey GetKey()
        {
            return this._key;
        }

        /// <summary>
        /// Returns the default value of the test TestSettingKey associated with the TestSetting.
        /// </summary>
        /// <returns>The default value of the test TestSettingKey associated with the TestSetting.</returns>
        public T GetDefaultValue()
        {
            T defaultValue = this._key.GetDefaultValue();
            return defaultValue;
        }

        /// <summary>
        /// Returns the value currently stored in the TestSetting.
        /// </summary>
        /// <returns>The value currently stored in the TestSetting.</returns>
        public T GetValue()
        {
            return this._value;
        }

        /// <summary>
        /// Returns whether or not the TestSetting's current value is locked or not.
        /// </summary>
        /// <returns>True if the TestSetting's current value is locked and false if not.</returns>
        public bool IsLocked()
        {
            return this._locked;
        }

        /// <summary>
        /// Returns whether the TestSetting has been set or if its current value is null.
        /// </summary>
        /// <returns>True if the TestSetting's current value isn't null and false if it is.</returns>
        public override bool IsSet()
        {
            return this._set;
        }

        #endregion

        #region Setter Functions 

        /// <summary>
        /// Sets the TestSetting to value to the default and resets the flag on whether it's set.
        /// If the TestSetting value is locked, this won't change anything.
        /// </summary>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting<T> ResetValue()
        {
            if (!this._locked)
            {
                this._value = this.GetDefaultValue();
                this._set = false;
            }
            return this;
        }

        /// <summary>
        /// Sets the TestSetting to the specified value.
        /// If the TestSetting value is locked, this won't change anything.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting<T> SetValue(T value)
        {
            if (!this._locked)
            {
                this._value = value;
                this._set = true;
            }
            return this;
        }

        /// <summary>
        /// Sets the value of the TestSetting to the specified String value.
        /// If the TestSetting value is locked, this won't change anything.
        /// This converts the String value to a value it can understand based on the TestSettingKey's value type.
        /// </summary>
        /// <param name="valueString">The value String being set.</param>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting<T> SetValueByString(string valueString)
        {

            T value = this.ConvertStringToValue(typeof (T), valueString);
            return this.SetValue(value);
        }

        /// <summary>
        /// Internal function used to convert a String to the specified value type.
        /// </summary>
        /// <param name="valueType">The Class of the value type that the String is being converted to.</param>
        /// <param name="valueString">The String value being converted.</param>
        /// <returns>An Object of the specified type corresponding to the specified String value.</returns>
        private T ConvertStringToValue(Type valueType, string valueString)
        {
            //TODO this still needs to be fixed, doing a quick fix for now just to get tags working in config file generation
            if (valueType.IsArray)
            {
                // Get the type of array
                Type innerType = valueType.GetElementType();

                // Split the value String into tokens
                string[] values;
                if (valueString.Contains(";"))
                {
                    values = valueString.Split(';');
                }
                else
                {
                    values = valueString.Split(',');
                }

                var returnValues = new string[values.Length];

                // Loop through the tokens are recursively call this function
                for (int i = 0; i < values.Length; i++)
                {
                    String singleValue = values[i].Trim();
                    returnValues[i] = singleValue;
                }

                try
                {
                    return (T) Convert.ChangeType(returnValues, typeof (T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }

            }
            if (valueType.IsInterface)
            {
                try
                {
                    T value = EnumUtils.GetEnumFromId<T>(valueType, null, valueString);
                    return value;
                }
                catch (ArgumentException)
                {
                    T value = EnumUtils.GetEnumFromString<T>(valueType, null, valueString);
                    return value;
                }
            }
            else if (typeof (string).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(valueString, valueType);
                return value;
            }
            else if (valueType.IsEnum)
            {
                try
                {
                    var value = EnumUtils.GetEnumFromId<T>(valueType, null, valueString);
                    return value;
                }
                catch (ArgumentException)
                {
                    var value = EnumUtils.GetEnumFromString<T>(valueType, null, valueString);
                    return value;
                }
            }
            else if (typeof (Boolean).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(Boolean.Parse(valueString), valueType);
                return value;
            }
            else if (typeof (Byte).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(Byte.Parse(valueString), valueType);
                return value;
            }
            else if (typeof (Char).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(valueString.ToCharArray()[0], valueType);
                return value;
            }
            else if (typeof (Int16).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(Int16.Parse(valueString), valueType);
                return value;
            }
            else if (typeof (Int32).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(Int32.Parse(valueString), valueType);
                return value;
            }
            else if (typeof (Double).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(Double.Parse(valueString), valueType);
                return value;
            }
            else if (typeof (long).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(long.Parse(valueString), valueType);
                return value;
            }
            else if (typeof (short).IsAssignableFrom(valueType))
            {
                var value = (T) Convert.ChangeType(short.Parse(valueString), valueType);
                return value;
            }
            else
            {
                try
                {
                    //adding this as a final check/attempt to get around some enum limitations on the .net side
                    T value = EnumUtils.GetEnumFromString<T>(valueType, null, valueString);
                    return value;
                }
                catch (Exception)
                {
                    throw new Exception("The value type: " + valueType + " cannot be set to a String value.");
                }
            }
        }

        /// <summary>
        /// Locks the TestSetting value so it cannot be changed.
        /// </summary>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting<T> LockValue()
        {
            return this.SetLocked(true);
        }

        /// <summary>
        /// Unlocks the TestSetting value so it can be changed again.
        /// </summary>
        /// <returns>The TestSetting object after it is modified.</returns>
        public TestSetting<T> UnlockValue()
        {
            return this.SetLocked(false);
        }

        /// <summary>
        /// Internal function used to set whether the TestSetting value can be changed or not.
        /// </summary>
        /// <param name="locked">Whether the TestSetting value can be changed or not.</param>
        /// <returns>The TestSetting object after it is modified.</returns>
        private TestSetting<T> SetLocked(bool locked)
        {
            this._locked = locked;
            return this;
        }

        #endregion

        #region Object Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(Object o)
        {
            Type currentType = this.GetType();
            Type otherType = o.GetType();

            bool result = currentType == otherType;
            if (!result)
            {
                return false;
            }

            TestSetting<T> testSetting1 = (TestSetting<T>) o;
            TestSetting<T> testSetting2 = (TestSetting<T>) this;

            T value1 = testSetting1.GetValue();
            T value2 = testSetting2.GetValue();
            bool valuesMatch = value1.Equals(value2);

            return valuesMatch;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.GetValue());
            result = HashCodeUtils.Hash(result, this.GetKey());
            result = HashCodeUtils.Hash(result, this.IsLocked());
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (typeof (T).IsArray)
            {
                return this._key + "=" + this._value.ToString();
            }
            else
            {
                return this._key + "=" + this._value;
            }
        }

        #endregion
    }
}
