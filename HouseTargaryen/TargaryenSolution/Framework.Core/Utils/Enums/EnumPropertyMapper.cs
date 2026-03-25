namespace Framework.Core.Utils.Enums
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    using Framework.Core.CommonTypes.Constants;

    /// <summary>
    /// The class to represent a mapping of an enumeration to a property model.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <typeparam name="TPropertyModel">The type of a Property Model to map the enumeration to.</typeparam>
    public class EnumPropertyMapper<TEnum, TPropertyModel> : IReadOnlyCollection<KeyValuePair<TEnum, TPropertyModel>>
        where TEnum : struct
    {
        private readonly Dictionary<TEnum, TPropertyModel> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumPropertyMapper{TEnum, TPropertyModel}"/> class.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info from JSON file name</param>
        /// <param name="sourceFolder">The path to the file with the enumeration to property model map.</param>
        public EnumPropertyMapper(string additionalInfo, string sourceFolder)
        {
            string enumName = typeof(TEnum).Name;

            if (string.IsNullOrEmpty(sourceFolder))
            {
                sourceFolder = PathsToSourceFiles.JsonsForEnumPropertyMaps;
            }

            string sourceFile = $"{sourceFolder}/{enumName}{additionalInfo}Info.json";

            try
            {
                this.map = ObjectSerializer.DeserializeJsonToObject<Dictionary<TEnum, TPropertyModel>>(
                    File.ReadAllText(sourceFile));
            }
            catch (FileNotFoundException e)
            {
                throw new InvalidOperationException(
                    $"No file that contains a property model map for {enumName} exists at the specified path: {sourceFile}",
                    e);
            }
        }

        /// <summary>
        /// Gets the count of mapped pairs.
        /// </summary>
        public int Count => this.map.Count;

        /// <summary>
        /// Returns the property model describing the specified enumeration value.
        /// </summary>
        /// <param name="index">The enumeration member to retrieve the property model for.</param>
        /// <returns>The property model describing the specified enumeration value.</returns>
        public TPropertyModel this[TEnum index]
            => this.map.ContainsKey(index) ? this.map[index] : default(TPropertyModel);

        /// <summary>
        /// Gets the cached map code that uniquely represents a pair of the enumeration type mapped to the property model type.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info from JSON file name </param>
        /// <param name="sourceFolder"> The path to the file with the enumeration to property model map. </param>
        /// <returns> Hash Code </returns>
        public static long CachedMapCode(string additionalInfo, string sourceFolder)
        {
            return ((long)typeof(TEnum).GetHashCode() << (sizeof(int) * 8))
                                           ^ typeof(TPropertyModel).GetHashCode()
                                           ^ additionalInfo.GetHashCode() 
                                           ^ sourceFolder.GetHashCode();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<KeyValuePair<TEnum, TPropertyModel>> GetEnumerator()
        {
            return ((IReadOnlyCollection<KeyValuePair<TEnum, TPropertyModel>>)this.map).GetEnumerator();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IReadOnlyCollection<KeyValuePair<TEnum, TPropertyModel>>)this.map).GetEnumerator();
        }
    }
}