namespace Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse
{
    using System.Collections.Generic;

    /// <summary>
    /// The matter.
    /// </summary>
    public class Matter
    {
        /// <summary>
        /// Gets or sets the alternate ids.
        /// </summary>
        public IList<string> AlternateIds { get; set; }

        /// <summary>
        /// Gets or sets the archived by.
        /// </summary>
        public string ArchivedBy { get; set; }

        /// <summary>
        /// Gets or sets the archived date.
        /// </summary>
        public string ArchivedDate { get; set; }

        /// <summary>
        /// Gets or sets the billing type.
        /// </summary>
        public string BillingType { get; set; }

        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        public string Budget { get; set; }

        /// <summary>
        /// Gets or sets the case number.
        /// </summary>
        public string CaseNumber { get; set; }

        /// <summary>
        /// Gets or sets the case type.
        /// </summary>
        public string CaseType { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the closed by.
        /// </summary>
        public string ClosedBy { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        public string ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the court or jurisdiction.
        /// </summary>
        public string CourtOrJurisdiction { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the custom field data.
        /// </summary>
        public IList<string> CustomFieldData { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the external references.
        /// </summary>
        public IList<string> ExternalReferences { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the legacy matter id.
        /// </summary>
        public string LegacyMatterId { get; set; }

        /// <summary>
        /// Gets or sets the matter number.
        /// </summary>
        public string MatterNumber { get; set; }

        /// <summary>
        /// Gets or sets the matter participants.
        /// </summary>
        public IList<MatterParticipant> MatterParticipants { get; set; }

        /// <summary>
        /// Gets or sets the matter status.
        /// </summary>
        public string MatterStatus { get; set; }

        /// <summary>
        /// Gets or sets the matter sub type.
        /// </summary>
        public string MatterSubType { get; set; }

        /// <summary>
        /// Gets or sets the matter type.
        /// </summary>
        public string MatterType { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the note count.
        /// </summary>
        public int NoteCount { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the opened by.
        /// </summary>
        public string OpenedBy { get; set; }

        /// <summary>
        /// Gets or sets the opened date.
        /// </summary>
        public string OpenedDate { get; set; }

        /// <summary>
        /// Gets or sets the organizational unit.
        /// </summary>
        public string OrganizationalUnit { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        public Permissions Permissions { get; set; }

        /// <summary>
        /// Gets or sets the practice area.
        /// </summary>
        public string PracticeArea { get; set; }

        /// <summary>
        /// Gets or sets the practice group.
        /// </summary>
        public string PracticeGroup { get; set; }

        /// <summary>
        /// Gets or sets the reopen date.
        /// </summary>
        public string ReopenDate { get; set; }

        /// <summary>
        /// Gets or sets the reopened by.
        /// </summary>
        public string ReopenedBy { get; set; }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the state province.
        /// </summary>
        public string StateProvince { get; set; }

        /// <summary>
        /// Gets or sets the substantive law.
        /// </summary>
        public string SubstantiveLaw { get; set; }

        /// <summary>
        /// Gets or sets the substantive law id.
        /// </summary>
        public string SubstantiveLawId { get; set; }
    }
}