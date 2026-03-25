namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Details info
    /// </summary>
    public class DetailsInfo
    {
        /// <summary>
        /// Gets or sets the left column.
        /// </summary>
        [JsonProperty("leftColumn")]
        public List<ColumnOptions> LeftColumn { get; set; }

        /// <summary>
        /// Gets or sets the right column.
        /// </summary>
        [JsonProperty("rightColumn")]
        public List<ColumnOptions> RightColumn { get; set; }
    }
}
