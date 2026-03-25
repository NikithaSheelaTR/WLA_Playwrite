namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Pagination Data Model
    /// </summary>
    [DataContract]
    public class Pagination
    {
        /// <summary>
        /// From 
        /// </summary>
        [DataMember(Name = "from")]
        public int From { get; set; }

        /// <summary>
        /// To
        /// </summary>
        [DataMember(Name = "to")]
        public int To { get; set; }

        /// <summary>
        /// Of
        /// </summary>
        [DataMember(Name = "of")]
        public int Of { get; set; }
    }
}
