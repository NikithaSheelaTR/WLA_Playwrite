namespace Framework.Core.CommonTypes.Constants
{
    /// <summary>
    /// The names of environment variables that contain externally configurable values.
    /// </summary>
    public static class EnvironmentConstants
    {
        /// <summary>
        /// AI Claims Finder Search Daily Limit.
        /// </summary>
        public const string AIClaimsFinderLimit = "AI_CLAIMS_FINDER_DAILY_LIMIT";

        /// <summary>
        /// AI Guided Research Daily Limit.
        /// </summary>
        public const string AIGuidedResearchDailyLimit = "AI_GUIDED_RESEARCH_DAILY_LIMIT";

        /// <summary>
        /// AI Jurisdictional Surveys Daily Limit.
        /// </summary>
        public const string AIJurisdictionalSurveysDailyLimit = "AI_JURISDICTIONAL_SURVEYS_DAILY_LIMIT";

        /// <summary>
        /// AI Conversational Search Daily Limit.
        /// </summary>
        public const string AISearchDailyLimit = "AI_SEARCH_DAILY_LIMIT";

        /// <summary>
        /// AI Treatise Search Daily Limit
        /// </summary>
        public const string AITreatiseSearchDailyLimit = "AI_TREATISE_SEARCH_DAILY_LIMIT";

        /// <summary>
        ///  BlockCiam Value for regression testing
        /// </summary>
        public const string BlockCiam = "BLOCK_CIAM";

        /// <summary>
        /// Bypass Open AI and other LLMs for regression testing.
        /// </summary>
        public const string BypassExternalLLMs = "BYPASS_EXTERNAL_LLMS";

        /// <summary>
        /// Default Category Page Collection Set.
        /// </summary>
        public const string CategoryPageCollectionSet = "DEFAULT_CPCS";

        /// <summary>
        /// Daily Delivery Limit.
        /// </summary>
        public const string DailyDeliveryLimit = "DAILY_DELIVERY_LIMIT";

        /// <summary>
        /// DataOrchestration
        /// </summary>
        public const string DataOrchestration = "DATAORCHESTRATION";

        /// <summary>
        /// DataRoom.
        /// </summary>
        public const string DataRoom = "DATAROOM";

        /// <summary>
        /// DataRoomBulk.
        /// </summary>
        public const string DataRoomBulk = "DATAROOMBULK";

        /// <summary>
        /// Data Room Read Source.
        /// </summary>
        public const string DataRoomReadSource = "DD_DATA_ROOM_READ_SOURCE";

        /// <summary>
        /// Data Room Read Source.
        /// </summary>
        public const string DataRoomWriteDestination = "DD_DATA_ROOM_WRITE_DESTINATION";

        /// <summary>
        /// Display Dynamic Messages.
        /// </summary>
        public const string DisplayDynamicMessages = "DD_DISPLAY_DINAMIC_MESSAGES";

        /// <summary>
        /// Document.
        /// </summary>
        public const string Document = "DOCUMENT";

        /// <summary>
        /// Doc View Limit Catch All Daily.
        /// </summary>
        public const string DocViewLimitCatchAllDaily = "DOC_VIEW_LIMIT_CATCH_ALL_DAILY";

        /// <summary>
        /// Doc View Limit PracticalLaw Daily.
        /// </summary>
        public const string DocViewLimitPracticalLawDaily = "DOC_VIEW_LIMIT_PL_DAILY";

        /// <summary>
        /// Selenium Drivers Directory
        /// </summary>
        public const string DriverLocation = "SELENIUM_DRIVERS_DIR";

        /// <summary>
        /// The emulate patron access user.
        /// </summary>
        public const string EmulatePatronAccessUser = "DD_EMULATE_PATRON_ACCESS";

        /// <summary>
        ///  EnableCiam Value for regression testing
        /// </summary>
        public const string EnableCiam = "ENABLE_CIAM";

        /// <summary>
        ///  EnableGovCiam Value for Government Version of Ciam
        /// </summary>
        public const string EnableGovCiam = "EnableGovCiam";

        /// <summary>
        /// Feature Access Controls to turn off.
        /// </summary>
        public const string FeatureAccessControlsOff = "FACS_OFF";

        /// <summary>
        /// Feature Access Controls to turn on.
        /// </summary>
        public const string FeatureAccessControlsOn = "FACS_ON";

        /// <summary>
        /// Feature Exposure.
        /// </summary>
        public const string FeatureExposure = "DD_FEATURE_EXPOSURE";

        /// <summary>
        /// Feature Overlay.
        /// </summary>
        public const string FeatureOverlay = "DD_FEATURE_OVERLAY";

        /// <summary>
        /// Specifies whether Workbench (Concourse) data should be prepared or omitted.
        /// </summary>
        public const string FlagToInitWorkbenchData = "INIT_WORKBENCH_DATA";
        
        /// <summary>
        /// Specify Logger name
        /// </summary>
        public const string LoggerName = "LOGGER_NAME";

        /// <summary>
        /// The gateway live external.
        /// </summary>
        public const string GatewayLiveExternal = "DD_GATEWAY_LIVE_EXT";

        /// <summary>
        /// Infrastructure Access Controls to turn off.
        /// </summary>
        public const string InfrastructureAccessControlsOff = "IACS_OFF";

        /// <summary>
        /// Infrastructure Access Controls to turn on.
        /// </summary>
        public const string InfrastructureAccessControlsOn = "IACS_ON";

        /// <summary>
        /// The emulate iPhone mode.
        /// </summary>
        public const string IphoneAppRenderingMode = "DD_IPHONE_MODE";

        /// <summary>
        /// Is DataRoom Region Enabled.
        /// </summary>
        public const string IsDataRoomRegionEnabled = "ISDATAROOMREGIONENABLED";

        /// <summary>
        /// Is PAM Subscription Slicing Enabled.
        /// </summary>
        public const string IsPamSubscriptionSlicingEnabled = "IS_PAM_SUBSCRIPTION_SLICING_ENABLED";

        /// <summary>
        /// KM server.
        /// </summary>
        public const string Km = "KM_SERVER";

        /// <summary>
        /// Logging quantity
        /// </summary>
        public const string LoggingQuantity = "DD_LOGGING_QUANTITY";

        /// <summary>
        /// Maximum cases doc count limit.
        /// </summary>
        public const string MaximumCasesDocumentCountLimit = "MAX_DOC_COUNT_LIMIT";

        /// <summary>
        /// The type of browser under test.
        /// </summary>
        public const string NameOfBrowserUnderTest = "TEST_BROWSER";

        /// <summary>
        /// The name of a business case to report to QRT2.0.
        /// </summary>
        public const string NameOfBusinessCase = "BUSINESS_CASE_NAME";

        /// <summary>
        /// Specifies the culture of tests that affects a globalisation aspect of a process context.
        /// </summary>
        public const string NameOfCultureOfTests = "TEST_CULTURE";

        /// <summary>
        /// The environment under test.
        /// </summary>
        public const string NameOfEnvironmentId = "TEST_ENVIRONMENT";

		/// <summary>
		/// The remote driver uri.
		/// </summary>
	    public const string NameOfRemoteDriverUri = "REMOTE_DRIVER_URI";

        /// <summary>
        /// The name of qrt tags.
        /// </summary>
        public const string NameOfQrtTags = "QRT_TAGS";

        /// <summary>
        /// The test suite to run.
        /// </summary>
        public const string NameOfTestExecutionFolder = "TEST_EXECUTION_DIR";

        /// <summary>
        /// The test suite to run.
        /// </summary>
        public const string NameOfTestSuiteToExecute = "TEST_SUITE";

        /// <summary>
        /// The features under test.
        /// </summary>
        public const string NamesOfFeaturesUnderTest = "TEST_FEATURES";

        /// <summary>
        /// The modules under test.
        /// </summary>
        public const string NamesOfModulesUnderTest = "TEST_MODULES";

        /// <summary>
        /// The products under test.
        /// </summary>
        public const string NamesOfProductsUnderTest = "TEST_PRODUCTS";

        /// <summary>
        /// Online Charges.
        /// </summary>
        public const string OnlineCharges = "ONLINE_CHARGES";

        /// <summary>
        /// Override Online Charges.
        /// </summary>
        public const string OverrideOnlineCharges = "DD_OVERRIDE_OLC";

        /// <summary>
        /// Override Pam Case Type To Released.
        /// </summary>
        public const string OverridePamCaseTypeToReleased = "OVERRIDE_PAM_CASE_TYPES_TO_RELEASED";

        /// <summary>
        /// Password Pool Name Parameter to add.
        /// </summary>
        public const string PamSubscriptionDeniedOverride = "PAM_SUBSCRIPTION_DENIED_OVERRIDE";

        /// <summary>
        /// Password Pool Name Parameter to add.
        /// </summary>
        public const string PasswordPoolName = "PASSWORD_POOL";
        
        /// <summary>
        /// Product Metadata version.
        /// </summary>
        public const string PmdVersion = "PMD_DATA_VERSION";

        /// <summary>
        /// Product under test value to choose language of application
        /// </summary>
        public const string ProductUnderTest = "PRODUCT_UNDER_TEST";

        /// <summary>
        /// The addressees to send QRT summary Email to.
        /// </summary>
        public const string QrtSummaryResultsEmail = "QRT_RESULTS_EMAIL";

        /// <summary>
        /// R and D TypeaheadRoutingEnabled
        /// </summary>
        public const string RDTypeaheadRoutingEnabled = "DD_RD_TYPEAHEAD_ROUTING";

        /// <summary>
        /// R and D SearchRoutingEnabled
        /// </summary>
        public const string RDSearchRoutingEnabled = "DD_RD_SEARCH_ROUTING";

        /// <summary>
        /// Search Debug Info
        /// </summary>
        public const string SkipAnonymousAuthentication = "SKIP_ANONYMOUS_AUTH";

        /// <summary>
        /// Search Debug Info
        /// </summary>
        public const string SearchDebugInfo = "SEARCH_DEBUG_INFO";

        /// <summary>
        /// Supported Features to turn off.
        /// </summary>
        public const string SupportedFeaturesOff = "SUPPORTED_FEATURES_OFF";

        /// <summary>
        /// Supported Features to turn on.
        /// </summary>
        public const string SupportedFeaturesOn = "SUPPORTED_FEATURES_ON";

        /// <summary>
        /// Session Timeout Override
        /// </summary>
        public const string SessionTimeoutOverride = "SESSION_TIMEOUT_OVERRIDE";

        /// <summary>
        /// Session Delivery Limit.
        /// </summary>
        public const string SessionDeliveryLimit = "SESSION_DELIVERY_LIMIT";

        /// <summary>
        /// Usage Limit Override.
        /// </summary>
        public const string UsageLimitOverride = "DD_USAGE_LIMIT_OVERRIDE";

        /// <summary>
        /// Use SSO Authorization
        /// </summary>
        public const string UseSSOAuth = "USE_SSO_AUTH";

        /// <summary>
        /// User IP Address Override
        /// </summary>
        public const string UserIpAddressOverride = "USER_IP_ADDRESS_OVERRIDE";

        /// <summary>
        /// The WLN data room folders read write destination.
        /// </summary>
        public const string WlnDataRoomFoldersReadWriteDestination = "DD_WLN_DR_FOLDERS_RW_DEST";

        /// <summary>
        /// DC exit value to run tests on AWS.
        /// </summary>
        public const string IsDcExit = "IS_DC_EXIT";

        /// <summary>
        /// Routing value for RAS conversation host.
        /// </summary>
        public const string RasConversationHost = "RAS_CONVERSATION_HOST";

        /// <summary>
        /// Is Fed Ramp Product View of products
        /// </summary>
        public const string IsFedRamp = "IS_FED_RAMP";

        /// <summary>
        /// AI Chat Assistant Service Config Override
        /// </summary>
        public const string AIChatAssistantServiceConfig = "AI_CHAT_ASSISTANT_SERVICE_CONFIG";
    }
}