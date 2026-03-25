namespace Framework.Core.Utils.Extensions
{
    using System;
    using System.Linq;

    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;

    /// <summary>
    /// The test settings extensions.
    /// </summary>
    public static class TestSettingsExtensions
    {
        private static readonly string[][] LookUpList =
            {
                new[]
                    {
                        EnvironmentConstants.FeatureAccessControlsOn,
                        EnvironmentConstants.FeatureAccessControlsOff
                    },
                new[]
                    {
                        EnvironmentConstants.InfrastructureAccessControlsOn,
                        EnvironmentConstants
                            .InfrastructureAccessControlsOff
                    }
            };

        /// <summary>
        /// Adds the setting to the test settings cache.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value to append.
        /// </typeparam>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="settingKey">
        /// The setting key
        /// </param>
        /// <param name="updateOption">
        /// Specifies, how to set the values of test settings.
        /// </param>
        /// <param name="valuesToAppend">
        /// The values to append to the value of the setting key
        /// </param>
        /// <returns>
        /// The <see cref="TestSettings"/>.
        /// </returns>
        public static TestSettings AppendValues<TValue>(
            this TestSettings settings,
            string settingKey,
            SettingUpdateOption updateOption,
            params TValue[] valuesToAppend)
        {
            return settings.AppendValues(
                settingKey,
                TestSettingsExtensions.LookupSettingKey(settingKey),
                updateOption,
                valuesToAppend);
        }

        private static string[] LookupSettingKey(string settingName)
        {
            return settingName == null ? null : LookUpList.FirstOrDefault(arr => Array.IndexOf(arr, settingName) > -1);
        }
    }
}