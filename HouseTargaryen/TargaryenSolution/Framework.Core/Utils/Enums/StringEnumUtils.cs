namespace Framework.Core.Utils.Enums
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides utility methods to retrieve values from string enumerations that use the <see cref="StringValueAttribute"/>.
    /// </summary>
    public class StringEnumUtils
    {
        /// <summary>
        /// Retrieves the string value of an enumeration.
        /// </summary>
        /// <param name="value">The enumeration from which the string value will be retrieved.</param>
        /// <returns>The string value of the provided enumeration.</returns>
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }


        /// <summary>
        /// Retrieves the enumeration value for a string.
        /// </summary>
        /// <typeparam name="T">The type of enumeration.</typeparam>
        /// <param name="value">The string to search for in the enumeration.</param>
        /// <returns>An enumeration value that corresponds with the provided string.</returns>
        /// <exception cref="ArgumentException">Thrown if an enumeration value cannot be found that corresponds with the provided string.</exception>
        public static T GetEnumValue<T>(string value)
        {
            Type type = typeof(T);
            FieldInfo[] fieldInfo = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo info in fieldInfo)
            {
                var attrs = info.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Any(attr => attr.Value.Equals(value)))
                {
                    return (T)info.GetValue(null);
                }
            }

            throw new ArgumentException("The string " + value + " does not parse into any value for this enum.");
        }
    }
}
