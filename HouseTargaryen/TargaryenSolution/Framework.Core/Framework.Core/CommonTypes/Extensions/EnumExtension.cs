namespace Framework.Core.CommonTypes.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Enumeration extensions.
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
        /// Return the text value stored in the corresponding JSON file
        /// </summary>
        /// <typeparam name="TEnum"> The enumeration type </typeparam>
        /// <param name="enumeration"> the enumeration to get the string value from </param>
        /// <returns> the text value </returns>
        public static string GetEnumTextValue<TEnum>(this TEnum enumeration) where TEnum : struct
        {
            return EnumPropertyModelCache.GetEnumInfo<TEnum, BaseTextModel>(enumeration).Text;
        }

        /// <summary>
        /// The method looks through the values of specified property of the enumeration property model that is associated with the enumeration type 
        /// and returns the enumeration member which has the matched property model value.
        /// </summary>
        /// <param name="value">
        /// The value of the property model to search for. 
        /// </param>
        /// <param name="valueRetriever">
        /// The procedure to retrieve the value from the property model for comparison.
        /// </param>
        /// <param name="additionalInfo">The additional name which is placed between the enum name and Info word</param>
        /// <param name="sourceFolder">Source path for Enums which are stored in subfolders</param>
        /// <typeparam name="TEnum">
        /// The enumeration type
        /// </typeparam>
        /// <typeparam name="TPropertyModel">
        /// The type of the enumeration property model that is associated with the enumeration and its properties are looked through for match.
        /// </typeparam>
        /// <returns>
        /// The enumeration member which has the matched property model value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No property member retrieval procedure has been specified.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception not exactly one property model match was found.
        /// </exception>
        public static TEnum GetEnumValueByPropertyModel<TEnum, TPropertyModel>(
            this string value,
            Func<TPropertyModel, string> valueRetriever,
            string additionalInfo = "",
            string sourceFolder = "")
            where TEnum : struct
        {
            if (valueRetriever == null)
            {
                throw new ArgumentNullException(
                    nameof(valueRetriever),
                    "The operation to retrieve " + typeof(TEnum).Name + " from property model value is undefined");
            }

            KeyValuePair<TEnum, TPropertyModel>[] suitablePairs =
                EnumPropertyModelCache.GetMap<TEnum, TPropertyModel>(additionalInfo, sourceFolder)
                                      .Where(
                                          enumPropertyPair =>
                                              string.Equals(
                                                  valueRetriever(enumPropertyPair.Value),
                                                  value,
                                                  StringComparison.InvariantCultureIgnoreCase))
                                      .ToArray();

            if (!suitablePairs.Any())
            {
                throw new ArgumentException(
                    $"The string '{value}' does not parse into any value for {typeof(TEnum).Name} enumeration");
            }

            if (suitablePairs.Length == 1)
            {
                return suitablePairs[0].Key;
            }

            string enumValues = string.Join(", ", suitablePairs.Select(pair => pair.Key));
            throw new InvalidOperationException(
                $"The string '{value}' corresponds to several {typeof(TEnum).Name} enumeration values: {enumValues}");
        }

        /// <summary>
        /// The method looks through the values of Text property of the enumeration property model that is associated with the enumeration type 
        /// and returns the enumeration member which has the matched property model value.
        /// </summary>
        /// <param name="value"> The value of the property model to search for. </param>
        /// <param name="additionalInfo"> The additional Info. </param>
        /// <param name="sourceFolder"> The source Folder. </param>
        /// <typeparam name="TEnum"> The enumeration type </typeparam>
        /// <returns>
        /// The enumeration member which has the matched property model value.
        /// </returns>
        public static TEnum GetEnumValueByText<TEnum>(this string value, string additionalInfo = "", string sourceFolder = "") where TEnum : struct
        {
            return value.GetEnumValueByPropertyModel<TEnum, BaseTextModel>(model => model.Text, additionalInfo, sourceFolder);
        }

        /// <summary>
        /// Add a value to flags enumeration variable (uses bitwise or-equal operator to set the bit)
        /// </summary>
        /// <param name="en">
        /// The original flagged enumeration variable
        /// </param>
        /// <param name="flag">
        /// The flag to set.
        /// </param>
        /// <typeparam name="T"> The type of an enumeration labelled with the Flags attribute.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Enum"/>.
        /// </returns>
        /// <exception cref="TypeAccessException"> Supports only <see cref="Enum"/>
        /// </exception>
        public static T SetFlag<T>(this T en, T flag) where T : struct, IConvertible
        {
            return en.OperationWithFlag(flag, (baseEnum, newflag) => en = (T)((dynamic)baseEnum | (dynamic)newflag));
        }

        /// <summary>
        /// Add values to flags enumeration variable (uses bitwise or-equal operator to set the bit)
        /// </summary>
        /// <param name="en">
        /// The original flagged enumeration variable
        /// </param>
        /// <param name="flags">
        /// The set of flags to set.
        /// </param>
        /// <typeparam name="T"> The type of an enumeration labelled with the Flags attribute.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Enum"/>.
        /// </returns>
        /// <exception cref="TypeAccessException"> Supports only <see cref="Enum"/>
        /// </exception>
        public static T SetFlags<T>(this T en, params T[] flags) where T : struct, IConvertible
        {
            return en.OperationWithFlags((baseEnum, newflag) => en = baseEnum.SetFlag(newflag), flags);
        }

        /// <summary>
        /// Remove a value from flags enumeration variable (uses bitwise and-with-negation operator to clear the bit)
        /// </summary>
        /// <param name="en">
        /// The original flagged enumeration variable
        /// </param>
        /// <param name="flag">
        /// The flag to remove.
        /// </param>
        /// <typeparam name="T"> The type of an enumeration labelled with the Flags attribute.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Enum"/>.
        /// </returns>
        /// <exception cref="TypeAccessException"> Supports only <see cref="Enum"/>
        /// </exception>
        public static T UnsetFlag<T>(this T en, T flag) where T : struct, IConvertible
        {
            return en.OperationWithFlag(flag, (baseEnum, newflag) => en = (T)((dynamic)baseEnum & ~(dynamic)newflag));
        }

        /// <summary>
        /// Remove values from flags enumeration variable (uses bitwise and-with-negation operator to clear the bit)
        /// </summary>
        /// <param name="en">
        /// The original flagged enumeration variable
        /// </param>
        /// <param name="flags">
        /// The set of flags to remove.
        /// </param>
        /// <typeparam name="T"> The type of an enumeration labelled with the Flags attribute.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Enum"/>.
        /// </returns>
        /// <exception cref="TypeAccessException"> Supports only <see cref="Enum"/>
        /// </exception>
        public static T UnsetFlags<T>(this T en, params T[] flags) where T : struct, IConvertible
        {
            return en.OperationWithFlags((baseEnum, newflag) => en = baseEnum.UnsetFlag(newflag), flags);
        }

        /// <summary>
        /// Encapsulate logic for checking type of basic variable and perform required action
        /// </summary>
        /// <param name="en">
        /// The original flagged enumeration variable
        /// </param>
        /// <param name="flag">
        /// The flag for action with it.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <typeparam name="T"> The type of an enumeration labelled with the Flags attribute.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Enum"/>.
        /// </returns>
        /// <exception cref="TypeAccessException"> Supports only <see cref="Enum"/>
        /// </exception>
        private static T OperationWithFlag<T>(this T en, T flag, Func<T, T, T> operation)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new TypeAccessException("This method only supports the  Enum value type.");
            }

            T result = operation(en, flag);
            return result;
        }

        /// <summary>
        /// Encapsulate logic for safety perform required action on the flags
        /// </summary>
        /// <param name="en">
        /// The original flagged enumeration variable
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <typeparam name="T"> The type of an enumeration labelled with the Flags attribute.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Enum"/>.
        /// </returns>
        /// <exception cref="TypeAccessException"> Supports only <see cref="Enum"/>
        /// </exception>
        private static T OperationWithFlags<T>(this T en, Func<T, T, T> operation, params T[] flags)
            where T : struct, IConvertible
        {
            if (flags == null || flags.Length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(flags));
            }

            foreach (T flag in flags)
            {
                en = operation(en, flag);
            }

            return en;
        }
    }
}