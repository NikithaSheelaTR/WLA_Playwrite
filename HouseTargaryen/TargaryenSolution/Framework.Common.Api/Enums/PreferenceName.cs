namespace Framework.Common.Api.Enums
{
    /// <summary>
    /// The preference name that is used for /V1/Preference request.
    /// </summary>
    public enum PreferenceName
    {
        /// <summary>
        /// Chevron help info box displayed
        /// </summary>
        ChevronHelpDisplayed,

        /// <summary>
        /// The compare versions suppress intro tip.
        /// </summary>
        CompareVersionsSuppressIntroTip,

        /// <summary>
        /// Countries to search (Preference to set jurisdictions to search)
        /// </summary>
        CountriesToSearch,

        /// <summary>
        /// Include hyperlink reference
        /// </summary>
		CitationIncludeHyperlinkReference,

		/// <summary>
		/// Delivery File Container (SingleMergedFile, MultipleFiles)
		/// </summary>
		DeliveryFileContainer,

        /// <summary>
        /// Delivery File Format
        /// </summary>
        DeliveryFileFormat,

        /// <summary>
        /// Default delivery option selected
        /// </summary>
        DeliveryOptionSelected,

        /// <summary>
        /// Default Page
        /// </summary>
        DefaultPage,

        /// <summary>
        /// Default sort type for 'CASES'
        /// </summary>
        DefaultSortTypeCase,

        /// <summary>
        /// Document level TOC help info box displayed
        /// </summary>
        DocumentLevelTOCHelpDisplayed,

        /// <summary>
        /// Expand document level TOC
        /// </summary>
        ExpandDocumentLevelTOC,

        /// <summary>
        /// Font Size (Normal, Large)
        /// </summary>
        FontSize,

        /// <summary>
        /// Force Terms and Connectors 
        /// </summary>
        ForceTermsAndConnectors,

        /// <summary>
        /// Sets Jurisdiction
        /// </summary>
        JurisdictionsGlobal,

        /// <summary>
        /// Applicable for Edge. Multi-facet selection preference
        /// </summary>
        MultiFacetsEnabled,

        /// <summary>
        /// QuickCheckDefaultUploadView
        /// </summary>
        QuickCheckDefaultUploadView,

        /// <summary>
        /// Pagination (20,50,100)
        /// </summary>
        ResultsPerPage,

        /// <summary>
        /// IP View type (List, Grid, Tile)
        /// </summary>
        SearchResults_IP_ViewType,

        /// <summary>
        /// Doc Analyzer Tour: 'Check your work' mode
        /// </summary>
        ShowDocAnalyzerOverview,

        /// <summary>
        /// Doc Analyzer Tour: 'Analyze opponent's work' mode
        /// </summary>
        ShowDocAnalyzerOppWorkOverview,

        /// <summary>
        /// Show Filter Panel tooltip in Edge
        /// </summary>
        ShowFilterPanelRedesignOverview,

        /// <summary>
        /// Folders page redesign Tour 
        /// </summary>
        ShowFolderRedesignOverview,

        /// <summary>
        /// Show Quick Access panel
        /// </summary>
        ShowQuickAccessPanel,

        /// <summary>
        /// Show tree view panel
        /// </summary>
        ShowTreeViewPanel,

        /// <summary>
        /// Snippet Navigation New Tour
        /// </summary>
        SnippetNavigationNewTour,

        /// <summary>
        /// DisplayQuickCheckHomePageModal
        /// </summary>
        DisplayQuickCheckHomePageModal,

        /// <summary>
        /// Focus Highlighting Overview info tooltip
        /// </summary>
        ShowDocumentFocusHighlightingOverview,

        /// <summary>
        /// The Home Page Tour for Edge
        /// </summary>
        ShowHomePageOverview,

        /// <summary>
        /// The show quick check intro video.
        /// </summary>
        ShowQuickCheckIntroVideo,

        /// <summary>
        /// Quotations facet infobox
        /// </summary>
        SuppressQuotationFacetsChevronInfo,

        /// <summary>
        /// To show Quoatations mini Tour cards.
        /// </summary>
        ShowShortQuoteOverview,

        /// <summary>
        /// Detail level (1 - less, 2 - more, 3 most)
        /// </summary>
        SliderDetail,

        /// <summary>
        /// Suppress copy citation intro tooltip
        /// </summary>
        SuppressCopyCitationIntroTooltip,

        /// <summary>
        /// The suppress inline key cite flags intro tooltip.
        /// </summary>
        SuppressInlineKeyCiteFlagsIntroTooltip,

        /// <summary>
        /// SuppressMultipleSearchWithinTooltip
        /// </summary>
        SuppressMultipleSearchWithinTooltip,

        /// <summary>
        /// SuppressRecentFiltersTooltip
        /// </summary>
        SuppressRecentFiltersTooltip,

        /// <summary>
        /// The suppress inline key cite flags intro tooltip.
        /// </summary>
        SuppressProceduralPostureTooltip,

        /// <summary>
        /// SuppressRepealedStatusFacetTooltip
        /// </summary>
        SuppressRepealedStatusFacetTooltip,

        /// <summary>
        /// show tour for Judicial Quick check feature
        /// </summary>
        ShowQuickCheckJudicialOverview
    }
}