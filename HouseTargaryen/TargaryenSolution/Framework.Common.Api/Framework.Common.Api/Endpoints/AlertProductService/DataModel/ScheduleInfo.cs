namespace Framework.Common.Api.Endpoints.AlertProductService.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The Schedule Info data model.
    /// </summary>
    [DataContract]
    public class ScheduleInfo
    {
        /// <summary>
        /// Frequency
        /// </summary>
        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

        /// <summary>
        /// Next Run Date
        /// </summary>
        [DataMember(Name = "nextRunDate")]
        public string NextRunDate { get; set; }

        /// <summary>
        /// Period
        /// </summary>
        [DataMember(Name = "period")]
        public object Period { get; set; }
    }
}
