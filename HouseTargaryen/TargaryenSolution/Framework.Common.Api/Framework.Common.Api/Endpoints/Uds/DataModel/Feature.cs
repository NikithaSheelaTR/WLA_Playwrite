namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// The feature.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public FeatureSelectionOption State { get; set; }
    }
}