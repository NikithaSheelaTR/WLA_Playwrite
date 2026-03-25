namespace Framework.Core.Utils.Enums
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Converts the enumeration value to an array of bytes based on the underlying type.
        /// </summary>
        /// <param name="value">The value of an enumeration variable.</param>
        /// <returns>An array of bytes representing the enumeration value based on the underlying type.</returns>
        public static byte[] GetBytes(this Enum value)
        {
            Type underlyingType = Enum.GetUnderlyingType(value.GetType());

            if (underlyingType == typeof(sbyte) || underlyingType == typeof(byte) || underlyingType == typeof(bool))
            {
                return new[] { Convert.ToByte(value) };
            }

            if (underlyingType == typeof(short) || underlyingType == typeof(ushort) || underlyingType == typeof(char))
            {
                return BitConverter.GetBytes(Convert.ToUInt16(value));
            }

            if (underlyingType == typeof(long) || underlyingType == typeof(ulong))
            {
                return BitConverter.GetBytes(Convert.ToUInt64(value));
            }

            return BitConverter.GetBytes(Convert.ToUInt32(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            MemberInfo mi = typeof(EnumAttribute).IsAssignableFrom(typeof(T))
                ? value.GetType().GetField(value.ToString())
                : value.GetType().GetMember(value.ToString())[0];
            T[] attributes = (T[])mi.GetCustomAttributes(typeof(T), false);

            return attributes.Length > 0 ? attributes[0] : null;
        }

        /// <summary>
        /// Gets the string value of an enum that uses the StringValue attribute.
        /// </summary>
        /// <param name="value">The enum to get the string value of</param>
        /// <returns>String value of the enum passed in</returns>
        public static string GetStringValue<TAttribute>(this Enum value) where TAttribute : Attribute, IEnumAttributeBase
        {
            string output = null;
            var attr = EnumExtension.GetAttribute<TAttribute>(value);

            if (attr != null)
            {
                output = attr.GetValue();
            }

            return output;
        }

        /// <summary>
        /// Return the value stored in the string value attribute 
        /// </summary>
        /// <param name="enumeration">enum to get the string value from</param>
        /// <returns>string value</returns>
        public static string GetStringValue(this Enum enumeration)
        {
            var attribute = EnumExtension.GetAttribute<StringValueAttribute>(enumeration);
            return attribute == null ? string.Empty : attribute.Value;
        }

        /// <summary>
        /// Given a string return the enum value that matches it
        /// </summary>
        /// <typeparam name="TEnum">the Enum type</typeparam>
        /// <typeparam name="TAttribute">Type of the attribute we're looking for</typeparam>
        /// <param name="value">the string to search for</param>
        /// <returns>the enum representation of that string</returns>
        public static TEnum GetEnumValue<TEnum, TAttribute>(this string value) where TAttribute : IEnumAttributeBase
        {
            Type type = typeof(TEnum);
            FieldInfo[] fieldInfo = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo info in fieldInfo)
            {
                var attrs = info.GetCustomAttributes(typeof(TAttribute), false);

                foreach (TAttribute attr in attrs)
                {
                    if (attr.GetValue().Equals(value))
                    {
                        return (TEnum)info.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("The string " + value + " does not parse into any value for this enum.");
        }
    }
}