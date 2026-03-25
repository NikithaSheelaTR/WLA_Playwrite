namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The responces data contract
    /// </summary>
    [DataContract]
    public class Responses
    {
        /// <summary>
        /// trdiscover
        /// </summary>
        [DataMember(Name = "trdiscover", IsRequired = false)]
        public Trdiscover Trdiscover { get; set; }

        /// <summary>
        /// legal_analytics
        /// </summary>
        [DataMember(Name = "legal_analytics", IsRequired = false)]
        public List<LegalAnalytics> LegalAnalytics { get; set; }

        /// <summary>
        /// case
        /// </summary>
        [DataMember(Name = "case", IsRequired = false)]
        public List<Case> Case { get; set; }

        /// <summary>
        /// docket
        /// </summary>
        [DataMember(Name = "docket", IsRequired = false)]
        public List<Case> Docket { get; set; }

        /// <summary>
        /// statute
        /// </summary>
        [DataMember(Name = "statute", IsRequired = false)]
        public List<Case> Statute { get; set; }

        /// <summary>
        /// regulation
        /// </summary>
        [DataMember(Name = "regulation", IsRequired = false)]
        public List<Admindecision> Regulation { get; set; }

        /// <summary>
        /// admindecision
        /// </summary>
        [DataMember(Name = "admindecision", IsRequired = false)]
        public List<Admindecision> Admindecision { get; set; }

        /// <summary>
        /// analytical
        /// </summary>
        [DataMember(Name = "analytical", IsRequired = false)]
        public List<Admindecision> Analytical { get; set; }

        /// <summary>
        /// othercourtdocument
        /// </summary>
        [DataMember(Name = "othercourtdocument", IsRequired = false)]
        public List<OtherCourtDocument> Othercourtdocument { get; set; }

        /// <summary>
        /// categorypage
        /// </summary>
        [DataMember(Name = "categorypage", IsRequired = false)]
        public List<CategoryPage> Categorypage { get; set; }

        /// <summary>
        /// lpa
        /// </summary>
        [DataMember(Name = "lpa", IsRequired = false)]
        public List<Lpa> Lpa { get; set; }

        /// <summary>
        /// oa
        /// </summary>
        [DataMember(Name = "oa", IsRequired = false)]
        public List<Oa> Oa { get; set; }
    }
}