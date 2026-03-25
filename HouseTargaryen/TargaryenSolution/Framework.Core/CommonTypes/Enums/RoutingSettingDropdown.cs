namespace Framework.Core.CommonTypes.Enums
{
    /// <summary>
    /// Feature selection option
    /// </summary>
    public enum RoutingSettingDropdown
    {
        /// <summary>
        /// Property to indicate if Dataroom regions are on or off.
        /// </summary>
        IsDataRoomRegionEnabled,

        /// <summary>
        /// Property to indicate if PAM slicing is on or off.
        /// </summary>
        IsPamSubscriptionSlicingEnabled,

        /// <summary>
        /// Site alignment might not be in sync if you select a site for Cobalt Services that is not on the same site you are already signed on to.
        /// </summary>
        CobaltServicesSiteTarget,

        /// <summary>
        /// BlockCiam Value for regression testing
        /// </summary>
        BlockCiam,

        /// <summary>
        /// Will extra performance data be collected from the web browser and recorded?
        /// </summary>
        BrowserProfiling,

        /// <summary>
        /// Bypass Open AI and other LLMs for regression testing
        /// </summary>
        BypassExternalLLMs,

        /// <summary>
        /// Will the user be allowed to sign on, even if WestlawNext is under heavy load and regular users are not being allowed to sign on?
        /// </summary>
        BypassTierLimitCheck,

        /// <summary>
        /// What Search NORM collection set will be used for Related Info?
        /// </summary>
        CitingRefClient,

        /// <summary>
        /// What is the line count limit for delivery?
        /// </summary>
        DeliveryLineCountLimit,

        /// <summary>
        /// EnableCiam Value for regression testing
        /// </summary>
        EnableCiam,

        /// <summary>
        /// EnableGovCiam Value for Government version of Ciam
        /// </summary>
        EnableGovCiam,

        /// <summary>
        /// Will we show the user features that are not quite ready for 'prime time'?
        /// </summary>
        FeatureExposure,

        /// <summary>
        /// Will we treat the the user as if they are using an iPad?
        /// </summary>
        IPadAppRenderingMode,

        /// <summary>
        /// Will we treat the the user as if they are using an iPhone?
        /// </summary>
        IPhoneAppRenderingMode,

        /// <summary>
        /// Will the servers log extra information for this session?
        /// </summary>
        LoggingQuantity,

        /// <summary>
        /// Will we show the user Novus content that's not finalized yet?
        /// </summary>
        NovusStage,

        /// <summary>
        /// Will we enforce that OnePass authentication is required for most users?
        /// </summary>
        OnePassSignOn,

        /// <summary>
        /// Will we attempt some tricks to give the user extra performance?
        /// </summary>
        PerformanceVisibility,

        /// <summary>
        /// Will we allow the user to see a document even if Related Info fails?
        /// </summary>
        RelatedInfoTabFailure,

        /// <summary>
        /// The Search debug info
        /// </summary>
        SearchDebugInfo,      

        /// <summary>
        /// Usage Limit Override
        /// </summary>
        UsageLimitOverride,

        /// <summary>
        /// 
        /// </summary>
        UseDcsloc,

        /// <summary>
        /// Will the UseReloadDoc option be turned on for NORT?
        /// </summary>
        UseReloadDoc,

        /// <summary>
        /// Enable retrieval of Live gateway information
        /// </summary>
        GatewayLiveExternal,

        /// <summary>
        /// Override "TRMR" Product Name
        /// </summary>
        TRMRProductNameOverride,

        /// <summary>
        /// Data Room Read Source
        /// </summary>
        DataRoomReadSource,

        /// <summary>
        /// Data Room Write Destination
        /// </summary>
        DataRoomWriteDestination,

        /// <summary>
        /// The WLN data room folders read write destination.
        /// </summary>
        WlnDataRoomFoldersReadWriteDestination,

        /// <summary>
        /// 
        /// </summary>
        FolderCachingDisabled,

        /// <summary>
        /// Is Mashup turned on for eligible users?
        /// </summary>
        DisplayMashup,

        /// <summary>
        /// Will we disable some features to emulate a "Patron" user. (Feature selections disabled)
        /// </summary>
        EmulatePatronAccessUser,

        /// <summary>
        /// Override online charges for scale testing. Can prepopulate with SessionStartEventOLCOverride= in url.
        /// </summary>
        OverrideOnlineCharges,

        /// <summary>
        /// property to indicate if Document views should be added to history asynchronously
        /// </summary>
        ProcessDocumentHistoryAsynchronously,

        /// <summary>
        /// property to indicate if Searches should be added to history asynchronously
        /// </summary>
        ProcessSearchHistoryAsynchronously,

        /// <summary>
        /// property to indicate if KM Document views should be added to history asynchronously
        /// </summary>
        ProcessKMDocumentHistoryAsynchronously,

        /// <summary>
        /// property to indicate if Related Info should be added to history asynchronously
        /// </summary>
        ProcessRIHistoryAsynchronously,

        /// <summary>
        /// property to indicate if Matter Benchmarking reports should be added to history asynchronously
        /// </summary>
        ProcessMatterBenchmarkingHistoryAsynchronously,

        /// <summary>
        /// property to indicate if Bankruptcy reports should be added to history asynchronously
        /// </summary>
        ProcessBankruptcyHistoryAsynchronously,

        /// <summary>
        /// Use Content Release Controls for this session
        /// </summary>
        UseContentReleaseControls,

        /// <summary>
        /// Property to indicate the value to be used for New CSS Inliner for DocGather. Leave at Default to use value from CMDB.
        /// </summary>
        NewCssInlinerForDocGather,

        /// <summary>
        /// Property to indicate West Clip Alerts Access.
        /// </summary>
        WestClipAlertsAccess,

        /// <summary>
        /// Property to indicate if alerts address book is on or off.
        /// </summary>
        IsAlertsAddressBookTurnedOn,

        /// <summary>
        /// Property to indicate if alerts batch (docket related) functionality is on or off.
        /// </summary>
        IsAlertsBatchFunctionalityTurnedOn,

        /// <summary>
        /// Property to indicate if alerts calendaring (docket track related) is on or off.
        /// </summary>
        IsAlertsCalendaringTurnedOn,

        /// <summary>
        /// Property to indicate if alerts functionality is on or off.
        /// </summary>
        IsAlertsFunctionalityOn,

        /// <summary>
        /// Property to skip Anonymous Authentication for PL product views
        /// </summary>
        SkipAnonymousAuthentication,

        /// <summary>
        /// Property to indicate whether or not to show Napa content.
        /// </summary>
        ShowNapaContent,

        /// <summary>
        /// Property to indicate whether or not to enable matter management within the application.
        /// </summary>
        MatterManagementEnabled,

        /// <summary>
        /// Property to indicate whether or not to prevent category page jurisdiction reset within the application.
        /// </summary>
        PreventCategoryPageJurisdictionReset,

        /// <summary>
        /// Property to indicate whether or not to allow default category page long tail within the application.
        /// </summary>
        AllowDefaultCategoryPageLongTail,

        /// <summary>
        /// Property to indicate whether or not to allow new facet within the application.
        /// </summary>
        AllowNewFacet,

        /// <summary>
        /// Property to indicate whether or not to login using SSO authorization to Analytics 
        /// </summary>
        UseSSOAuth,

        /// <summary>
        /// Property to indicate if Dynamic Messages should be displayed.
        /// </summary>
        DisplayDynamicMessages,

        /// <summary>
        /// Override SAP preference for client id validation type
        /// </summary>
        SAPClientIdValidationTypePreferenceOverride,

        /// <summary>
        /// Display the key values for Localized UI strings.
        /// </summary>
        GlobalizationKeyModePreference,

        /// <summary>
        /// Property to indicate if Feature Overlay should be displayed.
        /// </summary>
        FeatureOverlay,

        /// <summary>
        /// Property to indicate if RDTypeaheadRoutingEnabled.
        /// </summary>
        RDTypeaheadRoutingEnabled,

        /// <summary>
        /// Property to indicate if RDSearchRoutingEnabled.
        /// </summary>
        RDSearchRoutingEnabled,

        /// <summary>
        /// Property to indicate if Page Header V2 is enabled.
        /// </summary>
        UserPageHeaderV2Enabled      
    }
}