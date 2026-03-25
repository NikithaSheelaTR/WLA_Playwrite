namespace Framework.Core.DataModel.Configuration.Proxies
{
    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Represents an environment under test.
    /// </summary>
    public sealed class EnvironmentInfo : CobaltEntityInfo<EnvironmentId, EnvironmentType>
    {
        /// <summary>
        /// Gets the value whether the environment is a test or production one.
        /// </summary>
        public bool IsLower { get; internal set; }

        /// <summary>
        /// Gets the value whether the environment have a site value
        /// For example: 
        /// for QED this property return false 
        /// for DemoPc1  - true
        /// </summary>
        public bool IsSiteSpecific => !string.IsNullOrEmpty(this.Site);

        /// <summary>
        /// Gets the environment name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the environment site name.
        /// </summary>
        public string Site { get; internal set; }

        /// <summary>
        /// A string representation of the current object.
        /// </summary>
        /// <returns>Returns a string representation of the current object.</returns>
        public override string ToString() =>
            string.Format(
                "{0}, Name={1}, Site={2}, IsLower={3}",
                base.ToString(),
                this.Name,
                this.Site,
                this.IsLower);
    }
}