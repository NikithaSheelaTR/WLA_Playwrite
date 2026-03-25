namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Collections.Generic;

    /// <summary>
    /// The features group.
    /// </summary>
    public class FeaturesGroup
    {
        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        public List<Feature> Features { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}