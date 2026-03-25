namespace Framework.Common.Api.Endpoints.Nlu.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The label map.
    /// </summary>
    [DataContract]
    public class LabelMap
    {
        /// <summary>
        /// Gets or sets the judge.
        /// </summary>
        [DataMember(Name = "judge")]
        public List<string> Judge { get; set; }

        /// <summary>
        /// Gets or sets the motion.
        /// </summary>
        [DataMember(Name = "motion")]
        public List<string> Motion { get; set; }
    }
}
