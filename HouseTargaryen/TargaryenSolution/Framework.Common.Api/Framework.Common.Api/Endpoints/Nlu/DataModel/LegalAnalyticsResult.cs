namespace Framework.Common.Api.Endpoints.Nlu.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The legal analytics result.
    /// </summary>
    [DataContract]
    public class LegalAnalyticsResult 
    {
        /// <summary>
        /// The result rot TRD
        /// </summary>
        [DataMember(Name = "labelMap")]
        public LabelMap LabelMap { get; set; }

        /// <summary>
        /// Gets or sets the laintent.
        /// </summary>
        [DataMember(Name = "laintent")]
        public string Laintent { get; set; }

        /// <summary>
        /// Gets or sets the motion action.
        /// </summary>
        [DataMember(Name = "motionAction")]
        public string MotionAction { get; set; }
    }
}
