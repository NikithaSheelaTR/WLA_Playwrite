namespace Framework.Core.DataModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// The class represents routing settings.
    /// </summary>
    public class RoutingSettingsInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoutingSettingsInfo"/> class.
        /// </summary>
        public RoutingSettingsInfo()
        {
            this.RoutingDropdownSettings = new Dictionary<RoutingSettingDropdown, RoutingSettingDropdownOption>();
            this.RoutingTextboxSettings = new Dictionary<RoutingSettingTextbox, string>();
            this.SupportedFeatureSettings = new Dictionary<SupportedFeatures, bool>();
            this.FeatureAccessControls = new Dictionary<FeatureAccessControl, FeatureSelectionOption>();
            this.RoutingUrlSettings = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the FAC settings
        /// </summary>
        public Dictionary<FeatureAccessControl, FeatureSelectionOption> FeatureAccessControls { get; set; }

        /// <summary>
        /// Gets or sets the routing dropdown settings.
        /// </summary>
        public Dictionary<RoutingSettingDropdown, RoutingSettingDropdownOption> RoutingDropdownSettings { get; set; }

        /// <summary>
        /// Gets or sets the routing textbox settings.
        /// </summary>
        public Dictionary<RoutingSettingTextbox, string> RoutingTextboxSettings { get; set; }

        /// <summary>
        /// Gets or sets the routing URL settings.
        /// </summary>
        public Dictionary<string, string> RoutingUrlSettings { get; set; }

        /// <summary>
        /// Gets or sets the supported feature settings.
        /// </summary>
        public Dictionary<SupportedFeatures, bool> SupportedFeatureSettings { get; set; }

        /// <summary>
        /// Converts the <see cref="RoutingSettingsInfo"/> to string.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(
                RoutingSettingsInfo.PrintInfoFor("Feature access controls:\t", this.FeatureAccessControls));
            builder.AppendLine(RoutingSettingsInfo.PrintInfoFor("Supported features:\t", this.SupportedFeatureSettings));
            builder.AppendLine(RoutingSettingsInfo.PrintInfoFor("Text-box settings:\t", this.RoutingTextboxSettings));
            builder.AppendLine(RoutingSettingsInfo.PrintInfoFor("Drop-down settings:\t", this.RoutingDropdownSettings));
            return builder.ToString();
        }

        /// <summary>
        /// Returns Form data dictionary
        /// </summary>
        /// <returns><seealso cref="Dictionary{TKey,TValue}"/></returns>
        public Dictionary<string, string> ToFormDataDictionary()
        {
            var resultDictionary = new Dictionary<string, string>();

            this.AddInfoToDictionary(this.FeatureAccessControls, ref resultDictionary);
            this.AddInfoToDictionary(this.RoutingDropdownSettings, ref resultDictionary);
            this.AddInfoToDictionary(this.RoutingTextboxSettings, ref resultDictionary);
            this.AddInfoToDictionary(this.SupportedFeatureSettings, ref resultDictionary);

            return resultDictionary;
        }

        /// <summary>
        /// Append given enum property map to dictionary[id,value]
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictToSeacrh"></param>
        /// <param name="dictToAdd"></param>
        private void AddInfoToDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictToSeacrh, ref Dictionary<string, string> dictToAdd) where TKey : struct
        {
            var mapper = EnumPropertyModelCache.GetMap<TKey, RoutingConfigModel>();
            foreach (KeyValuePair<TKey, RoutingConfigModel> settings in mapper)
            {
                if (dictToSeacrh.ContainsKey(settings.Key))
                {
                    dictToAdd.Add(settings.Value.Id, dictToSeacrh.First(x => x.Key.Equals(settings.Key)).Value.ToString().ToUpper());
                }
            }
        }

        private static string PrintInfoFor<TKey, TValue>(string message, IDictionary<TKey, TValue> dict)
        {
            string result = message;

            if (dict.Any())
            {
                result += string.Join(", ", dict.Select(p => p.Key + "=" + p.Value));
            }

            return result;
        }
    }
}