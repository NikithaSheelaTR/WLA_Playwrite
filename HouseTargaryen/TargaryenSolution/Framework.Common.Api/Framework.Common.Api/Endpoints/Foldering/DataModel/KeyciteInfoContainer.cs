namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The keycite info container.
    /// </summary>
    [DataContract]
    public class KeyciteInfoContainer
    {
        /// <summary>
        /// Items
        /// </summary>
        [DataMember(Name = "items")]
        public List<KeyciteInfo> Items { get; set; }
    }
}
