namespace Framework.Core.CommonTypes.Settings
{
    using System;

    using Framework.Core.Utils;

    /// <summary>
    /// Abstract class needed to get around some of the generics/type casting issues not present in the Java version
    /// </summary>
    public abstract class TestSettingKey
    {
        /// <summary>
        /// The underlying Type of the key/value
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// Returns the name of the test setting key.
        /// </summary>
        public abstract string GetName();


        /// <summary>
        /// Returns the name of the group of setting keys the setting belongs to.
        /// </summary>
        public abstract string GetGroup();

    }


    /// <summary>
    /// A TestSettingKey is essentially an object used to retrieve a particular TestSetting from
    /// the Dictionary of TestSettings.
    /// </summary>
    /// <typeparam name="T">the Type of the value associated with a particular key</typeparam>
    public class TestSettingKey<T> : TestSettingKey
    {

        private readonly string _name;
        private readonly string _group;
        private readonly T _defaultValue;

        /// <summary>
        /// 
        /// </summary>
        public override Type Type
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="defaultValue"></param>
        public TestSettingKey(string name,  T defaultValue, string group = null)
        {
            this._name = name;
            this._group = group;
            this._defaultValue = defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public TestSettingKey(string name, string group)
        {
            this._name = name;
            this._group = group;
            this._defaultValue = default(T);
        }

        #region Getter Functions

        /// <summary>
        /// Returns the name of the test setting key.
        /// </summary>
        public override string GetName()
        {
            return this._name;
        }

        /// <summary>
        /// Returns the name of the group of setting keys the setting belongs to.
        /// </summary>
        public override string GetGroup()
        {
            return this._group;
        }

        /// <summary>
        /// Returns the default value that should be used for the test setting key if none are specified.
        /// </summary>
        public T GetDefaultValue()
        {
            return this._defaultValue;
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
            try
            {

                var testSettingKey1 = (TestSettingKey<T>) o;
                TestSettingKey<T> testSettingKey2 = this;

                string name1 = testSettingKey1.GetName();
                string name2 = testSettingKey2.GetName();

                if (name1 == null || name2 == null)
                {
                    return false;
                }

                if (!name1.Equals(name2))
                {
                    return false;
                }

                T defaultVal1 = testSettingKey1.GetDefaultValue();
                T defaultVal2 = testSettingKey2.GetDefaultValue();

                if (defaultVal1 == null)
                {
                    if (defaultVal2 == null)
                    {
                        return true;
                    }
                    return false;
                }

                return defaultVal1.Equals(defaultVal2);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var result = HashCodeUtils.Seed;
            result = HashCodeUtils.Hash(result, this.GetGroup());
            result = HashCodeUtils.Hash(result, this.GetName());
            result = HashCodeUtils.Hash(result, this.GetDefaultValue());
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return typeof (T) + " " + this._name;
        }

        #endregion
    }
}
