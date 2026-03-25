namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The document meta info.
    /// </summary>
    [DataContract]
    public class DocumentMetaInfo
    {
        /// <summary>
        /// Gets or sets the access control.
        /// </summary>
        [DataMember(Name = "accessControl")]
        public string AccessControl { get; set; }

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        [DataMember(Name = "accessionNumber")]
        public string AccessionNumber { get; set; }

        /// <summary>
        /// Gets or sets the acquirer company.
        /// </summary>
        [DataMember(Name = "acquirerCompany")]
        public string AcquirerCompany { get; set; }

        /// <summary>
        /// Gets or sets the active date.
        /// </summary>
        [DataMember(Name = "activeDate")]
        public string ActiveDate { get; set; }

        /// <summary>
        /// Gets or sets the additional links.
        /// </summary>
        [DataMember(Name = "additionalLinks")]
        public string AdditionalLinks { get; set; }

        /// <summary>
        /// Gets or sets the agreement display date.
        /// </summary>
        [DataMember(Name = "agreementDisplayDate")]
        public string AgreementDisplayDate { get; set; }

        /// <summary>
        /// Gets or sets the announcement date.
        /// </summary>
        [DataMember(Name = "announcementDate")]
        public string AnnouncementDate { get; set; }

        /// <summary>
        /// Gets or sets the article attorney author name.
        /// </summary>
        [DataMember(Name = "articleAttorneyAuthorName")]
        public string ArticleAttorneyAuthorName { get; set; }

        /// <summary>
        /// Gets or sets the article firm author name.
        /// </summary>
        [DataMember(Name = "articleFirmAuthorName")]
        public string ArticleFirmAuthorName { get; set; }

        /// <summary>
        /// Gets or sets the article title.
        /// </summary>
        [DataMember(Name = "articleTitle")]
        public string ArticleTitle { get; set; }

        /// <summary>
        /// Gets or sets the borrower.
        /// </summary>
        [DataMember(Name = "borrower")]
        public string Borrower { get; set; }

        /// <summary>
        /// Gets or sets the case required.
        /// </summary>
        [DataMember(Name = "caseRequired")]
        public string CaseRequired { get; set; }

        /// <summary>
        /// Gets or sets the cic officer.
        /// </summary>
        [DataMember(Name = "cicOfficer")]
        public string CicOfficer { get; set; }

        /// <summary>
        /// Gets or sets the cip officer.
        /// </summary>
        [DataMember(Name = "cipOfficer")]
        public string CipOfficer { get; set; }

        /// <summary>
        /// Gets or sets the cite.
        /// </summary>
        [DataMember(Name = "cite")]
        public string Cite { get; set; }

        /// <summary>
        /// Gets or sets the cite abbreviation.
        /// </summary>
        [DataMember(Name = "citeAbbreviation")]
        public string CiteAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the cite primary.
        /// </summary>
        [DataMember(Name = "citePrimary")]
        public string CitePrimary { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        [DataMember(Name = "collection")]
        public string Collection { get; set; }

        /// <summary>
        /// Gets or sets the compound title.
        /// </summary>
        [DataMember(Name = "compoundTitle")]
        public string CompoundTitle { get; set; }

        /// <summary>
        /// Gets or sets the container id.
        /// </summary>
        [DataMember(Name = "containerId")]
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the container type.
        /// </summary>
        [DataMember(Name = "containerType")]
        public string ContainerType { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the content type id.
        /// </summary>
        [DataMember(Name = "contentTypeId")]
        public string ContentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the correlation ids.
        /// </summary>
        [DataMember(Name = "correlationIds")]
        public string CorrelationIds { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the court.
        /// </summary>
        [DataMember(Name = "court")]
        public string Court { get; set; }

        /// <summary>
        /// Gets or sets the court document type.
        /// </summary>
        [DataMember(Name = "courtDocumentType")]
        public string CourtDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        [DataMember(Name = "courtNumber")]
        public string CourtNumber { get; set; }

        /// <summary>
        /// Gets or sets the court year.
        /// </summary>
        [DataMember(Name = "courtYear")]
        public string CourtYear { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the date file.
        /// </summary>
        [DataMember(Name = "dateFile")]
        public string DateFile { get; set; }

        /// <summary>
        /// Gets or sets the deal date.
        /// </summary>
        [DataMember(Name = "dealDate")]
        public string DealDate { get; set; }

        /// <summary>
        /// Gets or sets the deal type.
        /// </summary>
        [DataMember(Name = "dealType")]
        public string DealType { get; set; }

        /// <summary>
        /// Gets or sets the deal value.
        /// </summary>
        [DataMember(Name = "dealValue")]
        public string DealValue { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the doc family guid.
        /// </summary>
        [DataMember(Name = "docFamilyGuid")]
        public string DocFamilyGuid { get; set; }

        /// <summary>
        /// Gets or sets the doc guid.
        /// </summary>
        [DataMember(Name = "docGuid")]
        public string DocGuid { get; set; }

        /// <summary>
        /// Gets or sets the doc link.
        /// </summary>
        [DataMember(Name = "docLink")]
        public string DocLink { get; set; }

        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        [DataMember(Name = "documentTitle")]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Gets or sets the edgar file number.
        /// </summary>
        [DataMember(Name = "edgarFileNumber")]
        public string EdgarFileNumber { get; set; }

        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        [DataMember(Name = "entityId")]
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets the expert type.
        /// </summary>
        [DataMember(Name = "expertType")]
        public string ExpertType { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file number.
        /// </summary>
        [DataMember(Name = "fileNumber")]
        public string FileNumber { get; set; }

        /// <summary>
        /// Gets or sets the filer cik.
        /// </summary>
        [DataMember(Name = "filerCik")]
        public string FilerCik { get; set; }

        /// <summary>
        /// Gets or sets the filer name.
        /// </summary>
        [DataMember(Name = "filerName")]
        public string FilerName { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        [DataMember(Name = "fileSize")]
        public int FileSize { get; set; }

        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        [DataMember(Name = "fileType")]
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the filing date.
        /// </summary>
        [DataMember(Name = "filingDate")]
        public string FilingDate { get; set; }

        /// <summary>
        /// Gets or sets the filing year.
        /// </summary>
        [DataMember(Name = "filingYear")]
        public string FilingYear { get; set; }

        /// <summary>
        /// Gets or sets the firm name.
        /// </summary>
        [DataMember(Name = "firmName")]
        public string FirmName { get; set; }

        /// <summary>
        /// Gets or sets the form number.
        /// </summary>
        [DataMember(Name = "formNumber")]
        public string FormNumber { get; set; }

        /// <summary>
        /// Gets or sets the form sub type.
        /// </summary>
        [DataMember(Name = "formSubType")]
        public string FormSubType { get; set; }

        /// <summary>
        /// Gets or sets the form type.
        /// </summary>
        [DataMember(Name = "formType")]
        public string FormType { get; set; }

        /// <summary>
        /// Gets or sets the form type display.
        /// </summary>
        [DataMember(Name = "formTypeDisplay")]
        public string FormTypeDisplay { get; set; }

        /// <summary>
        /// Gets or sets the form volume.
        /// </summary>
        [DataMember(Name = "formVolume")]
        public string FormVolume { get; set; }

        /// <summary>
        /// Gets or sets the functional cite.
        /// </summary>
        [DataMember(Name = "functionalCite")]
        public string FunctionalCite { get; set; }

        /// <summary>
        /// Gets or sets the fund firm compound title.
        /// </summary>
        [DataMember(Name = "fundFirmCompoundTitle")]
        public string FundFirmCompoundTitle { get; set; }

        /// <summary>
        /// Gets or sets the fund location.
        /// </summary>
        [DataMember(Name = "fundLocation")]
        public string FundLocation { get; set; }

        /// <summary>
        /// Gets or sets the fund name.
        /// </summary>
        [DataMember(Name = "fundName")]
        public string FundName { get; set; }

        /// <summary>
        /// Gets or sets the fundraising status.
        /// </summary>
        [DataMember(Name = "fundraisingStatus")]
        public string FundraisingStatus { get; set; }

        /// <summary>
        /// Gets or sets the fund target size.
        /// </summary>
        [DataMember(Name = "fundTargetSize")]
        public string FundTargetSize { get; set; }

        /// <summary>
        /// Gets or sets the fund type.
        /// </summary>
        [DataMember(Name = "fundType")]
        public string FundType { get; set; }

        /// <summary>
        /// Gets or sets the fund year.
        /// </summary>
        [DataMember(Name = "fundYear")]
        public string FundYear { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether get toc data.
        /// </summary>
        [DataMember(Name = "getTocData")]
        public bool GetTocData { get; set; }

        /// <summary>
        /// Gets or sets the has drafting notes.
        /// </summary>
        [DataMember(Name = "hasDraftingNotes")]
        public string HasDraftingNotes { get; set; }

        /// <summary>
        /// Gets or sets the impersonation key.
        /// </summary>
        [DataMember(Name = "impersonationKey")]
        public string ImpersonationKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether inline key cite flags included.
        /// </summary>
        [DataMember(Name = "inlineKeyCiteFlagsIncluded")]
        public bool InlineKeyCiteFlagsIncluded { get; set; }

        /// <summary>
        /// Gets or sets the inline key cite flags resolution status.
        /// </summary>
        [DataMember(Name = "inlineKeyCiteFlagsResolutionStatus")]
        public string InlineKeyCiteFlagsResolutionStatus { get; set; }

        /// <summary>
        /// Gets or sets the insider name.
        /// </summary>
        [DataMember(Name = "insiderName")]
        public string InsiderName { get; set; }

        /// <summary>
        /// Gets or sets the ip assignments action.
        /// </summary>
        [DataMember(Name = "ip_assignmentsAction")]
        public string IpAssignmentsAction { get; set; }

        /// <summary>
        /// Gets or sets the ip cite parallel.
        /// </summary>
        [DataMember(Name = "ip_citeParallel")]
        public string IpCiteParallel { get; set; }

        /// <summary>
        /// Gets or sets the ip derwent published date.
        /// </summary>
        [DataMember(Name = "ip_derwentPublishedDate")]
        public string IpDerwentPublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the ip docket.
        /// </summary>
        [DataMember(Name = "ip_docket")]
        public string IpDocket { get; set; }

        /// <summary>
        /// Gets or sets the ip document date.
        /// </summary>
        [DataMember(Name = "ip_documentDate")]
        public string IpDocumentDate { get; set; }

        /// <summary>
        /// Gets or sets the ip issued date.
        /// </summary>
        [DataMember(Name = "ip_issuedDate")]
        public string IpIssuedDate { get; set; }

        /// <summary>
        /// Gets or sets the ip lit alert attorney.
        /// </summary>
        [DataMember(Name = "ip_litAlertAttorney")]
        public string IpLitAlertAttorney { get; set; }

        /// <summary>
        /// Gets or sets the ip patent owner.
        /// </summary>
        [DataMember(Name = "ip_patentOwner")]
        public string IpPatentOwner { get; set; }

        /// <summary>
        /// Gets or sets the ip pat stat code.
        /// </summary>
        [DataMember(Name = "ip_patStatCode")]
        public string IpPatStatCode { get; set; }

        /// <summary>
        /// Gets or sets the ip published date.
        /// </summary>
        [DataMember(Name = "ip_publishedDate")]
        public string IpPublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the ip short title.
        /// </summary>
        [DataMember(Name = "ip_shortTitle")]
        public string IpShortTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is display mini report.
        /// </summary>
        [DataMember(Name = "isDisplayMiniReport")]
        public bool IsDisplayMiniReport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is gateway document.
        /// </summary>
        [DataMember(Name = "isGatewayDocument")]
        public bool IsGatewayDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is large document.
        /// </summary>
        [DataMember(Name = "isLargeDocument")]
        public bool IsLargeDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is persisted content.
        /// </summary>
        [DataMember(Name = "isPersistedContent")]
        public bool IsPersistedContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is public records blocked by permissible use.
        /// </summary>
        [DataMember(Name = "isPublicRecordsBlockedByPermissibleUse")]
        public bool IsPublicRecordsBlockedByPermissibleUse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is public records blocked by social security admin.
        /// </summary>
        [DataMember(Name = "isPublicRecordsBlockedBySocialSecurityAdmin")]
        public bool IsPublicRecordsBlockedBySocialSecurityAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is public records document.
        /// </summary>
        [DataMember(Name = "isPublicRecordsDocument")]
        public bool IsPublicRecordsDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is reg change document.
        /// </summary>
        [DataMember(Name = "isRegChangeDocument")]
        public bool IsRegChangeDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is secure content.
        /// </summary>
        [DataMember(Name = "isSecureContent")]
        public bool IsSecureContent { get; set; }

        /// <summary>
        /// Gets or sets the issue date.
        /// </summary>
        [DataMember(Name = "issueDate")]
        public string IssueDate { get; set; }

        /// <summary>
        /// Gets or sets the issuer name.
        /// </summary>
        [DataMember(Name = "issuerName")]
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets the job description.
        /// </summary>
        [DataMember(Name = "jobDescription")]
        public string JobDescription { get; set; }

        /// <summary>
        /// Gets or sets the juris abbrev.
        /// </summary>
        [DataMember(Name = "jurisAbbrev")]
        public string JurisAbbrev { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction.
        /// </summary>
        [DataMember(Name = "jurisdiction")]
        public string Jurisdiction { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction facet.
        /// </summary>
        [DataMember(Name = "jurisdictionFacet")]
        public string JurisdictionFacet { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction text.
        /// </summary>
        [DataMember(Name = "jurisdictionText")]
        public string JurisdictionText { get; set; }

        /// <summary>
        /// Gets or sets the knos.
        /// </summary>
        [DataMember(Name = "knos")]
        public KnosItems Knos { get; set; }

        /// <summary>
        /// Gets or sets the last round exit comb.
        /// </summary>
        [DataMember(Name = "lastRoundExitComb")]
        public string LastRoundExitComb { get; set; }

        /// <summary>
        /// Gets or sets the legacy id.
        /// </summary>
        [DataMember(Name = "legacyId")]
        public string LegacyId { get; set; }

        /// <summary>
        /// Gets or sets the letter type.
        /// </summary>
        [DataMember(Name = "letterType")]
        public string LetterType { get; set; }

        /// <summary>
        /// Gets or sets the location of incorporation.
        /// </summary>
        [DataMember(Name = "locationOfIncorporation")]
        public string LocationOfIncorporation { get; set; }

        /// <summary>
        /// Gets or sets the max offering price.
        /// </summary>
        [DataMember(Name = "maxOfferingPrice")]
        public string MaxOfferingPrice { get; set; }

        /// <summary>
        /// Gets or sets the nature of transaction.
        /// </summary>
        [DataMember(Name = "natureOfTransaction")]
        public string NatureOfTransaction { get; set; }

        /// <summary>
        /// Gets or sets the news image href large.
        /// </summary>
        [DataMember(Name = "newsImageHrefLarge")]
        public string NewsImageHrefLarge { get; set; }

        /// <summary>
        /// Gets or sets the news image href small.
        /// </summary>
        [DataMember(Name = "newsImageHrefSmall")]
        public string NewsImageHrefSmall { get; set; }

        /// <summary>
        /// Gets or sets the normalized doc type.
        /// </summary>
        [DataMember(Name = "normalizedDocType")]
        public string NormalizedDocType { get; set; }

        /// <summary>
        /// Gets or sets the normalized form type.
        /// </summary>
        [DataMember(Name = "normalizedFormType")]
        public string NormalizedFormType { get; set; }

        /// <summary>
        /// Gets or sets the offering type.
        /// </summary>
        [DataMember(Name = "offeringType")]
        public string OfferingType { get; set; }

        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        [DataMember(Name = "organizationName")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        [DataMember(Name = "pageCount")]
        public string PageCount { get; set; }

        /// <summary>
        /// Gets or sets the persisted doc guid.
        /// </summary>
        [DataMember(Name = "persistedDocGuid")]
        public string PersistedDocGuid { get; set; }

        /// <summary>
        /// Gets or sets the portfolio company location.
        /// </summary>
        [DataMember(Name = "portfolioCompanyLocation")]
        public string PortfolioCompanyLocation { get; set; }

        /// <summary>
        /// Gets or sets the portfolio company name.
        /// </summary>
        [DataMember(Name = "portfolioCompanyName")]
        public string PortfolioCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the primary editorial service.
        /// </summary>
        [DataMember(Name = "primaryEditorialService")]
        public string PrimaryEditorialService { get; set; }

        /// <summary>
        /// Gets or sets the pr meta info 1.
        /// </summary>
        [DataMember(Name = "prMetaInfo1")]
        public string PrMetaInfo1 { get; set; }

        /// <summary>
        /// Gets or sets the pr meta info 2.
        /// </summary>
        [DataMember(Name = "prMetaInfo2")]
        public string PrMetaInfo2 { get; set; }

        /// <summary>
        /// Gets or sets the pr meta info 3.
        /// </summary>
        [DataMember(Name = "prMetaInfo3")]
        public string PrMetaInfo3 { get; set; }

        /// <summary>
        /// Gets or sets the pr meta info 4.
        /// </summary>
        [DataMember(Name = "prMetaInfo4")]
        public string PrMetaInfo4 { get; set; }

        /// <summary>
        /// Gets or sets the pr meta info 5.
        /// </summary>
        [DataMember(Name = "prMetaInfo5")]
        public string PrMetaInfo5 { get; set; }

        /// <summary>
        /// Gets or sets the profile type.
        /// </summary>
        [DataMember(Name = "profileType")]
        public string ProfileType { get; set; }

        /// <summary>
        /// Gets or sets the pu.
        /// </summary>
        [DataMember(Name = "PU")]
        public string Pu { get; set; }

        /// <summary>
        /// Gets or sets the pub series.
        /// </summary>
        [DataMember(Name = "pubSeries")]
        public string PubSeries { get; set; }

        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        [DataMember(Name = "questionText")]
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether redlined.
        /// </summary>
        [DataMember(Name = "redlined")]
        public bool Redlined { get; set; }

        /// <summary>
        /// Gets or sets the registration date.
        /// </summary>
        [DataMember(Name = "registrationDate")]
        public string RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the rendition id.
        /// </summary>
        [DataMember(Name = "renditionId")]
        public string RenditionId { get; set; }

        /// <summary>
        /// Gets or sets the royalty id.
        /// </summary>
        [DataMember(Name = "royaltyId")]
        public string RoyaltyId { get; set; }

        /// <summary>
        /// Gets or sets the rulebook n views.
        /// </summary>
        [DataMember(Name = "rulebookNViews")]
        public string RulebookNViews { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        [DataMember(Name = "serialNumber")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the sub content type.
        /// </summary>
        [DataMember(Name = "subContentType")]
        public string SubContentType { get; set; }

        /// <summary>
        /// Gets or sets the sub content type id.
        /// </summary>
        [DataMember(Name = "subContentTypeId")]
        public string SubContentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the sub title.
        /// </summary>
        [DataMember(Name = "subTitle")]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the target company.
        /// </summary>
        [DataMember(Name = "targetCompany")]
        public string TargetCompany { get; set; }

        /// <summary>
        /// Gets or sets the testimony type.
        /// </summary>
        [DataMember(Name = "testimonyType")]
        public string TestimonyType { get; set; }

        /// <summary>
        /// Gets or sets the title html.
        /// </summary>
        [DataMember(Name = "titleHtml")]
        public string TitleHtml { get; set; }

        /// <summary>
        /// Gets or sets the title text.
        /// </summary>
        [DataMember(Name = "titleText")]
        public string TitleText { get; set; }

        /// <summary>
        /// Gets or sets the total amount issued with currency.
        /// </summary>
        [DataMember(Name = "totalAmountIssuedWithCurrency")]
        public string TotalAmountIssuedWithCurrency { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        [DataMember(Name = "totalPages")]
        public string TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the trademark status.
        /// </summary>
        [DataMember(Name = "trademarkStatus")]
        public string TrademarkStatus { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount with currency.
        /// </summary>
        [DataMember(Name = "transactionAmountWithCurrency")]
        public string TransactionAmountWithCurrency { get; set; }

        /// <summary>
        /// Gets or sets the transaction value.
        /// </summary>
        [DataMember(Name = "transactionValue")]
        public string TransactionValue { get; set; }

        /// <summary>
        /// Gets or sets the valuation comb.
        /// </summary>
        [DataMember(Name = "valuationComb")]
        public string ValuationComb { get; set; }

        /// <summary>
        /// Gets or sets the westlaw database identifier.
        /// </summary>
        [DataMember(Name = "westlawDatabaseIdentifier")]
        public string WestlawDatabaseIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the westlaw document number.
        /// </summary>
        [DataMember(Name = "westlawDocumentNumber")]
        public string WestlawDocumentNumber { get; set; }
    }
}