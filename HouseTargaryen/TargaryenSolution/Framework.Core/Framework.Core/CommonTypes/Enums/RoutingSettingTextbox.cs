namespace Framework.Core.CommonTypes.Enums
{
    /// <summary>
    /// Feature selection option
    /// </summary>
    public enum RoutingSettingTextbox
    {
        /// <summary>
        /// Override the default value of AI Claims Finder search daily limit
        /// </summary>
        AIClaimsFinderLimit,

        /// <summary>
        /// Override the default value of AI Guided Research Daily Limit
        /// </summary>
        AIGuidedResearchDailyLimit,

        /// <summary>
        /// Override the default value of Jurisdictional Surveys Daily Limit
        /// </summary>
        AIJurisdictionalSurveysDailyLimit,

        /// <summary>
        /// Override the default value of AI abusive use conversational search daily limit
        /// </summary>
        AISearchDailyLimit,

        /// <summary>
        /// Override the default value of AI abusive use Treatise search daily limit
        /// </summary>
        AITreatiseSearchDailyLimit,

        /// <summary>
        /// Sets the routing option for the category page collection set
        /// </summary>
        CategoryPageCollectionSet,
        
        /// <summary>
        /// The number of particular types of events that can occur before content abuse is logged. 
        /// Separate values with commas (e.g., Search:10, Browse:20).
        /// </summary>
        ContentAbuseEventThresholds,
        
        /// <summary>
        /// Sets the default value of daily delivery limit
        /// </summary>
        DailyDeliveryLimit,

        /// <summary>
        /// Override default Data Orchestration
        /// </summary>
        DataOrchestration,

        /// <summary>
        /// Override default DataRoom.
        /// </summary>
        DataRoom,

        /// <summary>
        /// Override default DataRoomBulk
        /// </summary>
        DataRoomBulk,

        /// <summary>
        /// Override default Document server
        /// </summary>
        Document,

        /// <summary>
        /// Override default Catch all daily limit
        /// </summary>
        DocViewLimitCatchAllDaily,

        /// <summary>
        /// Override default PL daily limit
        /// </summary>
        DocViewLimitPracticalLawDaily,

        /// <summary>
        /// Feature Access Controls to be turned ON.
        /// </summary>
        FacsOn,

        /// <summary>
        /// Feature Access Controls to be turned OFF.
        /// </summary>
        FacsOff,
        
        /// <summary>
        /// USED FOR TESTING ONLY. Queue used to override default history queue.
        /// </summary>
        HistoryListenerQueueOverride,
        
        /// <summary>
        /// IAC to be turned ON.
        /// </summary>
        InfrastructureAccessControlsOn,
        
        /// <summary>
        /// IAC to be turned OFF.
        /// </summary>
        InfrastructureAccessControlsOff,
        
        /// <summary>
        /// Sets the routing option for KM.
        /// </summary>
        Km,

        /// <summary>
        /// Maximum number of Novus cases documents that will be used for filtered results.
        /// </summary>
        MaximumCasesDocumentCountLimit,

        /// <summary>
        /// Maximum size Novus document can be delivered in bytes (only applies to certain content types currently).
        /// </summary>
        MaxiumDeliverySize,

        /// <summary>
        /// The products under test.
        /// </summary>
        NextLAIndexVersionUSOverride,

        /// <summary>
        /// OnlineCharges
        /// </summary>
        OnlineCharges,

        /// <summary>
        /// Property to override PAM case types to released.
        /// </summary>
        OverridePamCaseTypeToReleased,

        /// <summary>
        /// PAM Subscription Denied FACs Override.
        /// </summary>
        PamSubscriptionDeniedOverride,

        /// <summary>
        /// Override default PMD data version. (Leave blank to use default for currently selected PMD endpoint.)
        /// </summary>
        PmdDataVersion,
        
        /// <summary>
        /// Sets the routing option for Sync.
        /// </summary>
        ProductMetadata,

        /// <summary>
        /// Sets the routing value for RAS conversation host
        /// </summary>
        RasConversationHost,

        /// <summary>
        /// Request headers to be added to outgoing REST requests. 
        /// Enter headers as they appear on HTTP requests (e.g., Header: Value)
        /// NOTE: These headers won't be added to UDS AuthSession calls.
        /// </summary>
        RequestHeaders,
        
        /// <summary>
        /// Optional Product Name for just the event. Can prepopulate with SessionStartEventProductNameOverride= in url.
        /// </summary>
        SessionStartEventProductOverride,
        
        /// <summary>
        /// Add an additional printer to STP printer drop down. (Leave blank to not add printer)
        /// </summary>
        StpPrinterIdOverride,

        /// <summary>
        /// Session Timeout Override
        /// </summary>
        SessionTimeoutOverride,
       
        /// <summary>
        /// Supported Features to be turned ON.
        /// </summary>
        SupportedFeaturesOn,

        /// <summary>
        /// Supported Features to be turned OFF.
        /// </summary>
        SupportedFeaturesOff,

        /// <summary>
        /// Override the default request pagination size for DataRoom.AccountSettings.Project.List.Minimal.Get.
        /// </summary>
        SetRequestSizeForDataRoomAccountSettingsProjectListMinimalGet,

        /// <summary>
        /// Sets the routing option for Sync.
        /// </summary>
        Sync,

        /// <summary>
        /// Sets the default value of session delivery limit
        /// </summary>
        SessionDeliveryLimit,

        /// <summary>
        /// Sets an IP address
        /// </summary>
        UserIpAddressOverride,

        /// <summary>
        /// Ai chat Assitance service config
        /// </summary>
        AIChatAssistantServiceConfig
    }
}