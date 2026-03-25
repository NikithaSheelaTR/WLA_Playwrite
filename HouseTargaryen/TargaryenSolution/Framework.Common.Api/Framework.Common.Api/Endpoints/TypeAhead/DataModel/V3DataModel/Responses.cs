namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V3DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel;

    /// <summary>
    /// The responses data contract
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
        [DataMember(Name = "cases", IsRequired = false)]
        public List<Case> Cases { get; set; }

        /// <summary>
        /// docket
        /// </summary>
        [DataMember(Name = "dockets", IsRequired = false)]
        public List<Case> Dockets { get; set; }

        /// <summary>
        /// statute
        /// </summary>
        [DataMember(Name = "statutes", IsRequired = false)]
        public List<Case> Statutes { get; set; }

        /// <summary>
        /// regulation
        /// </summary>
        [DataMember(Name = "regulations", IsRequired = false)]
        public List<Admindecision> Regulations { get; set; }

        /// <summary>
        /// admindecision
        /// </summary>
        [DataMember(Name = "admindecisions", IsRequired = false)]
        public List<Admindecision> AdminDecisions { get; set; }

        /// <summary>
        /// analytical
        /// </summary>
        [DataMember(Name = "analyticals", IsRequired = false)]
        public List<Admindecision> Analyticals { get; set; }

        /// <summary>
        /// othercourtdocument
        /// </summary>
        [DataMember(Name = "othercourtdocuments", IsRequired = false)]
        public List<OtherCourtDocument> OtherCourtDocuments { get; set; }

        /// <summary>
        /// categorypage
        /// </summary>
        [DataMember(Name = "categorypages", IsRequired = false)]
        public List<CategoryPage> CategoryPages { get; set; }

        /// <summary>
        /// athens_cause_of_action
        /// </summary>
        [DataMember(Name = "athens_cause_of_action", IsRequired = false)]
        public List<AthensSuggestion> AthensCauseOfAction { get; set; }

        /// <summary>
        /// athens_fact_pattern
        /// </summary>
        [DataMember(Name = "athens_fact_pattern", IsRequired = false)]
        public List<AthensSuggestion> AthensFactPattern { get; set; }

        /// <summary>
        /// athens_legal_issue
        /// </summary>
        [DataMember(Name = "athens_legal_issue", IsRequired = false)]
        public List<AthensLegalIssue> AthensLegalIssue { get; set; }
    }
}
