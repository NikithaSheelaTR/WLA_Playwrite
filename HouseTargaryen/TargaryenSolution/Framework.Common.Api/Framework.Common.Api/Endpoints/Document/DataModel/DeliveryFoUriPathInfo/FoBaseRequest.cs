namespace Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The fo base request .
    /// </summary>
    [DataContract]
    public class FoBaseRequest
    {
        /// <summary>
        /// Gets or sets a value indicating whether annotations.
        /// </summary>
        [DataMember(Name = "Annotations")]
        public bool Annotations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether brief it.
        /// </summary>
        [DataMember(Name = "BriefIt")]
        public bool BriefIt { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        [DataMember(Name = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the delivery format.
        /// </summary>
        [DataMember(Name = "DeliveryFormat")]
        public int DeliveryFormat { get; set; }

        /// <summary>
        /// Gets or sets the delivery medium.
        /// </summary>
        [DataMember(Name = "DeliveryMedium")]
        public int DeliveryMedium { get; set; }

        /// <summary>
        /// Gets or sets the document guid.
        /// </summary>
        [DataMember(Name = "DocumentGuid")]
        public string DocumentGuid { get; set; }

        /// <summary>
        /// Gets or sets the document sections.
        /// </summary>
        [DataMember(Name = "DocumentSections")]
        public string DocumentSections { get; set; }

        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        [DataMember(Name = "DocumentTitle")]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether dual column.
        /// </summary>
        [DataMember(Name = "DualColumn")]
        public bool DualColumn { get; set; }

        /// <summary>
        /// Gets or sets the fermi content type.
        /// </summary>
        [DataMember(Name = "FermiContentType")]
        public string FermiContentType { get; set; }

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        [DataMember(Name = "FontSize")]
        public int FontSize { get; set; }

        /// <summary>
        /// Gets or sets the footnote format.
        /// </summary>
        [DataMember(Name = "FootnoteFormat")]
        public int FootnoteFormat { get; set; }

        /// <summary>
        /// Gets or sets the full text metadata.
        /// </summary>
        [DataMember(Name = "FullTextMetadata")]
        public string FullTextMetadata { get; set; }

        /// <summary>
        /// Gets or sets the headnotes.
        /// </summary>
        [DataMember(Name = "Headnotes")]
        public int Headnotes { get; set; }

        /// <summary>
        /// Gets or sets the highlighted terms.
        /// </summary>
        [DataMember(Name = "HighlightedTerms")]
        public string[] HighlightedTerms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include abridgment classification.
        /// </summary>
        [DataMember(Name = "IncludeAbridgmentClassification")]
        public bool IncludeAbridgmentClassification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include case annotation.
        /// </summary>
        [DataMember(Name = "IncludeCaseAnnotation")]
        public bool IncludeCaseAnnotation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include inline key cite flags.
        /// </summary>
        [DataMember(Name = "IncludeInlineKeyCiteFlags")]
        public bool IncludeInlineKeyCiteFlags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include markman display.
        /// </summary>
        [DataMember(Name = "IncludeMarkmanDisplay")]
        public bool IncludeMarkmanDisplay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include non west headnotes.
        /// </summary>
        [DataMember(Name = "IncludeNonWestHeadnotes")]
        public bool IncludeNonWestHeadnotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include public annotations.
        /// </summary>
        [DataMember(Name = "IncludePublicAnnotations")]
        public bool IncludePublicAnnotations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include shared highlights.
        /// </summary>
        [DataMember(Name = "IncludeSharedHighlights")]
        public bool IncludeSharedHighlights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include shared notes.
        /// </summary>
        [DataMember(Name = "IncludeSharedNotes")]
        public bool IncludeSharedNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is court wire delivery.
        /// </summary>
        [DataMember(Name = "IsCourtWireDelivery")]
        public bool IsCourtWireDelivery { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is dockets update.
        /// </summary>
        [DataMember(Name = "IsDocketsUpdate")]
        public bool IsDocketsUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is duplex print.
        /// </summary>
        [DataMember(Name = "IsDuplexPrint")]
        public bool IsDuplexPrint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is quick draft delivery.
        /// </summary>
        [DataMember(Name = "IsQuickDraftDelivery")]
        public bool IsQuickDraftDelivery { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is rule book mode.
        /// </summary>
        [DataMember(Name = "IsRuleBookMode")]
        public bool IsRuleBookMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether key cite treatment.
        /// </summary>
        [DataMember(Name = "KeyCiteTreatment")]
        public bool KeyCiteTreatment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether km flag.
        /// </summary>
        [DataMember(Name = "KMFlag")]
        public bool KmFlag { get; set; }

        /// <summary>
        /// Gets or sets the link color.
        /// </summary>
        [DataMember(Name = "LinkColor")]
        public int LinkColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether link underline.
        /// </summary>
        [DataMember(Name = "LinkUnderlin")]
        public bool LinkUnderline { get; set; }

        /// <summary>
        /// Gets or sets the list item identifier.
        /// </summary>
        [DataMember(Name = "ListItemIdentifier")]
        public string ListItemIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether novus search handle.
        /// </summary>
        [DataMember(Name = "NovusSearchHandle")]
        public string NovusSearchHandle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether original image link.
        /// </summary>
        [DataMember(Name = "OriginalImageLink")]
        public bool OriginalImageLink { get; set; }

        /// <summary>
        /// Gets or sets the page dimensions.
        /// </summary>
        [DataMember(Name = "PageDimensions")]
        public PageDimensions PageDimensions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pages with search terms.
        /// </summary>
        [DataMember(Name = "PagesWithSearchTerms")]
        public bool PagesWithSearchTerms { get; set; }

        /// <summary>
        /// Gets or sets the persisted document.
        /// </summary>
        [DataMember(Name = "PersistedDocument")]
        public PersistedDocument PersistedDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether red line toggle.
        /// </summary>
        [DataMember(Name = "RedLineToggle")]
        public bool RedLineToggle { get; set; }

        /// <summary>
        /// Gets or sets the request time utc.
        /// </summary>
        [DataMember(Name = "RequestTimeUtc")]
        public string RequestTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the request type.
        /// </summary>
        [DataMember(Name = "RequestType")]
        public int RequestType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether right note margin.
        /// </summary>
        [DataMember(Name = "RightNoteMargin")]
        public bool RightNoteMargin { get; set; }

        /// <summary>
        /// Gets or sets the search within context.
        /// </summary>
        [DataMember(Name = "SearchWithinContext")]
        public string SearchWithinContext { get; set; }

        /// <summary>
        /// Gets or sets the secondary highlighted terms.
        /// </summary>
        [DataMember(Name = "SecondaryHighlightedTerms")]
        public string SecondaryHighlightedTerms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show document.
        /// </summary>
        [DataMember(Name = "ShowDocument")]
        public bool ShowDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show drafting notes.
        /// </summary>
        [DataMember(Name = "ShowDraftingNotes")]
        public bool ShowDraftingNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show educational banner.
        /// </summary>
        [DataMember(Name = "ShowEducationalBanner")]
        public bool ShowEducationalBanner { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show key rule checklist.
        /// </summary>
        [DataMember(Name = "ShowKeyRuleChecklist")]
        public bool ShowKeyRuleChecklist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show key rule documents.
        /// </summary>
        [DataMember(Name = "ShowKeyRuleDocuments")]
        public bool ShowKeyRuleDocuments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show key rule timing.
        /// </summary>
        [DataMember(Name = "ShowKeyRuleTiming")]
        public bool ShowKeyRuleTiming { get; set; }

        /// <summary>
        /// Gets or sets the star pages ranges.
        /// </summary>
        [DataMember(Name = "StarPagesRanges")]
        public string StarPagesRanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether statutory text only.
        /// </summary>
        [DataMember(Name = "StatutoryTextOnly")]
        public bool StatutoryTextOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether table of contents.
        /// </summary>
        [DataMember(Name = "TableOfContents")]
        public bool TableOfContents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether term highlighting.
        /// </summary>
        [DataMember(Name = "TermHighlighting")]
        public bool TermHighlighting { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        [DataMember(Name = "TimeZone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the unique document id.
        /// </summary>
        [DataMember(Name = "UniqueDocumentId")]
        public string UniqueDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the urm id.
        /// </summary>
        [DataMember(Name = "UrmId")]
        public string UrmId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use footer without copyright.
        /// </summary>
        [DataMember(Name = "UseFooterWithoutCopyright")]
        public bool UseFooterWithoutCopyright { get; set; }

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        [DataMember(Name = "UserFirstName")]
        public string UserFirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        [DataMember(Name = "UserLastName")]
        public string UserLastName { get; set; }
    }
}