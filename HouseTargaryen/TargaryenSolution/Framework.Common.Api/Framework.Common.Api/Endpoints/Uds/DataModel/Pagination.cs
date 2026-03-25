namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The pagination.
    /// </summary>
    [DataContract]
    public class Pagination
    {
        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        [DataMember(Name = "from")]
        public int From { get; set; }

        /// <summary>
        /// Gets or sets the of.
        /// </summary>
        [DataMember(Name = "of")]
        public int Of { get; set; }

        /// <summary>
        /// Gets or sets the to.
        /// </summary>
        [DataMember(Name = "to")]
        public int To { get; set; }
    }
}