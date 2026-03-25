namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Schedule data model
    /// </summary>
    [DataContract]
    public class Schedule
    {
        /// <summary>
        /// Frequency
        /// </summary>
        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

        /// <summary>
        /// Next Run date
        /// </summary>
        [DataMember(Name = "nextRundate")]
        public string NextRundate { get; set; }

        /// <summary>
        /// Period
        /// </summary>
        [DataMember(Name = "period")]
        public int Period { get; set; }
    }
}
