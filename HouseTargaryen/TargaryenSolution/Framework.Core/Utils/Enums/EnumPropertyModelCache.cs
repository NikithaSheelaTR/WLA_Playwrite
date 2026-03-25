namespace Framework.Core.Utils.Enums
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The cache for enumeration to property model maps.
    /// </summary>
    public sealed class EnumPropertyModelCache
    {
        private static readonly Dictionary<long, object> RegisteredEnumMappers = new Dictionary<long, object>();

        /// <summary>
        /// Retrieves a property model for the specified enumeration value.
        /// </summary>
        /// <param name="enumValue">
        /// The enumeration value.
        /// </param>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <typeparam name="TPropertyModel">The type of a Property Model to map the enumeration to.</typeparam>
        /// <returns>The property model info describing the enumeration value.</returns>
        public static TPropertyModel GetEnumInfo<TEnum, TPropertyModel>(TEnum enumValue) where TEnum : struct
        {
            return EnumPropertyModelCache.GetMap<TEnum, TPropertyModel>()[enumValue];
        }

        /// <summary>
        /// Extracts property model info describing the members of the specified enumeration type.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info from JSON file name </param>
        /// <param name="sourceFolder">Source path for Enums which are stored in subfolders</param>
        /// <typeparam name="TEnum"> The type of the enumeration. </typeparam>
        /// <typeparam name="TPropertyModel"> The type of a Property Model to map the enumeration to. </typeparam>
        /// <returns> The property model info describing the members of the specified enumeration type </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Throws an exception if no Property Model can be detected for the specified enumeration type.
        /// </exception>
        public static EnumPropertyMapper<TEnum, TPropertyModel> GetMap<TEnum, TPropertyModel>(string additionalInfo = "", string sourceFolder = "") where TEnum : struct
        {
            EnumPropertyMapper<TEnum, TPropertyModel> propertyMapper;

            if (EnumPropertyModelCache.IsMapRegistered<TEnum, TPropertyModel>(additionalInfo, sourceFolder))
            {
                propertyMapper = EnumPropertyModelCache.GetMapInternal<TEnum, TPropertyModel>(additionalInfo, sourceFolder);
            }
            else
            {
                propertyMapper = new EnumPropertyMapper<TEnum, TPropertyModel>(additionalInfo, sourceFolder);
                EnumPropertyModelCache.RegisterMap(propertyMapper, additionalInfo, sourceFolder);
            }

            return propertyMapper;
        }

        /// <summary>
        /// Extracts property model info describing the members of the specified enumeration type.
        /// </summary>
        /// <param name="additionalInfo"> TThe additional Info from JSON file name </param>
        /// <param name="sourceFolder">Source path for Enums which are stored in subfolders</param>
        /// <typeparam name="TEnum"> The type of the enumeration. </typeparam>
        /// <typeparam name="TPropertyModel"> The type of a Property Model to map the enumeration to. </typeparam>
        /// <returns> The property model info describing the members of the specified enumeration type </returns>
        /// <exception cref="ArgumentOutOfRangeException"> Throws an exception if no Property Model can be detected for the specified enumeration type.
        /// </exception>
        private static EnumPropertyMapper<TEnum, TPropertyModel> GetMapInternal<TEnum, TPropertyModel>(string additionalInfo, string sourceFolder)
            where TEnum : struct
        {
            EnumPropertyMapper<TEnum, TPropertyModel> result;
            object mapper;

            if (!RegisteredEnumMappers.TryGetValue(EnumPropertyMapper<TEnum, TPropertyModel>.CachedMapCode(additionalInfo, sourceFolder), out mapper)
                || (result = mapper as EnumPropertyMapper<TEnum, TPropertyModel>) == null)
            {
                throw new InvalidCastException(
                    $"No valid {nameof(EnumPropertyMapper<TEnum, TPropertyModel>)} property model map is registered"
                    + $" for the {typeof(TEnum).Name} enumeration.");
            }

            return result;
        }

        /// <summary>
        /// Checks if the Enumeration to Property Model pair has ever been cached.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info from JSON file name </param>
        /// <param name="sourceFolder"> The path to the file with the enumeration to property model map. </param>
        /// <typeparam name="TEnum"> The type of the enumeration. </typeparam>
        /// <typeparam name="TPropertyModel"> The type of a Property Model to map the enumeration to. </typeparam>
        /// <returns> The <see cref="bool"/>. </returns>
        private static bool IsMapRegistered<TEnum, TPropertyModel>(string additionalInfo, string sourceFolder) where TEnum : struct
        {
            return RegisteredEnumMappers.ContainsKey(EnumPropertyMapper<TEnum, TPropertyModel>.CachedMapCode(additionalInfo, sourceFolder));
        }

        /// <summary>
        /// Registers an enumeration to property model map.
        /// </summary>
        /// <param name="propertyModelMap"> The enumeration to property model map. </param>
        /// <param name="sourceFolder"> The path to the file with the enumeration to property model map. </param>
        /// <param name="additionalInfo"> The additional Info from JSON file name</param>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <typeparam name="TPropertyModel">The type of a Property Model to map the enumeration to.</typeparam>
        private static void RegisterMap<TEnum, TPropertyModel>(
            EnumPropertyMapper<TEnum, TPropertyModel> propertyModelMap, string additionalInfo, string sourceFolder) where TEnum : struct
        {
            if (!EnumPropertyModelCache.IsMapRegistered<TEnum, TPropertyModel>(additionalInfo, sourceFolder))
            {
                RegisteredEnumMappers.Add(EnumPropertyMapper<TEnum, TPropertyModel>.CachedMapCode(additionalInfo, sourceFolder), propertyModelMap);
            }
        }
    }
}