namespace Framework.Core.CommonTypes.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// The test setting update context.
    /// </summary>
    public struct SettingUpdateContext
    {
        /// <summary>
        /// Specifies the mode that the setting value is updated.
        /// </summary>
        public SettingUpdateOption UpdateOption;

        /// <summary>
        /// The test setting.
        /// </summary>
        public KeyValuePair<string, string> TestSetting;
    }
}