namespace Framework.Core.CommonTypes.Configuration
{
    /// <summary>
    /// Specifies how the setting value should be updated.
    /// </summary>
    public enum SettingUpdateOption
    {
        /// <summary>
        /// The option specifies that the test settings value should be merged into a comma separated list.
        /// </summary>
        Append,

        /// <summary>
        /// The option specifies that the test settings value should be overwritten.
        /// </summary>
        Overwrite,

        /// <summary>
        /// The option specifies that the test settings value should only be updated if not specified.
        /// </summary>
        UpdateIfUnset
    }
}