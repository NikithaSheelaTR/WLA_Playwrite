namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The image guids map response model.
    /// </summary>
    public class ImageGuidsMapResponseModel
    {
        /// <summary>
        /// Gets or sets the image guids map.
        /// </summary>
        [JsonProperty("ImageGuidsMap")]
        public List<ImageGuidsMap> ImageGuidsMap { get; set; }
    }
}
