namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The favorite search item container.
    /// </summary>
    [DataContract]
    public class FavoriteSearchItemContainer
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [DataMember(Name = "items")]
        public List<FavoriteSearchItem> Items { get; set; }
    }
}

