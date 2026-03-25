namespace Framework.Core.CommonTypes.Configuration
{
    /// <summary>
    /// Pool names from Database
    /// Naming - enumeration name should end with Pool
    /// </summary>
    public enum PasswordPool
    {
        /// <summary>
        /// Pool of academic users
        /// </summary>
        AcademicRegression,

        /// <summary>
        /// pool of AU Users
        /// </summary>
        AnzRegression,

        /// <summary>
        /// The Checkpoint Global password.
        /// </summary>
        CheckpointGlobalPool,

        /// <summary>
        /// WLNE pool for Commercial pricing guide.
        /// </summary>
        ComPricingGuide,

        /// <summary>
        /// WLNIR pool for Concourse.
        /// </summary>
        ConcourseIndigoPool,

        /// <summary>
        /// WLNIR pool for Prod Concourse.
        /// </summary>
        ConcourseIndigoProdPool,

        /// <summary>
        /// WLNE pool for Corporate pricing guide.
        /// </summary>
        CorporatePricingGuide,

        /// <summary>
        /// WLNEIR pool for Custom Pages.
        /// </summary>
        CustomPagesIndigoPool,

        /// <summary>
        /// The Outline Builder password pool.
        /// </summary>
        ExternalEnhancements,

        /// <summary>
        /// WLNE pool for Federal Government pricing guide.
        /// </summary>
        FedGovPricingGuide,

        /// <summary>
        /// WLNE pool for Large and Medium Law Firm pricing guide.
        /// </summary>
        FlmFllPricingGuide,

        /// <summary>
        /// Foldering pool.
        /// </summary>
        Foldering,

        /// <summary>
        /// Global Intellectual property default pool
        /// </summary>
        GlobalIpDefault,

        /// <summary>
        /// WLNE pool for State and Local Government Plans Pro pricing guide.
        /// </summary>
        GovGovProPricingGuide,

        /// <summary>
        /// WLNE pool for State and Local Government Plans Plan 2 pricing guide.
        /// </summary>
        GovGovPlan2PricingGuide,
        
        /// <summary>
        /// The Growth Foldering.
        /// </summary>
        Growth_Foldering,

        /// <summary>
        /// WLNE pool for other purposes.
        /// </summary>
        HistoryRetension180,

        /// <summary>
        /// Indigo pool for other Indigo Academic users.
        /// </summary>
        IndigoAcademicUsers,

        /// <summary>
        /// Indigo pool for WLN Indigo access.
        /// </summary>
        IndigoAccessPool,

        /// <summary>
        /// Indigo pool for banff and galapagos features
        /// </summary>
        IndigoBanffGalapagos,


        /// <summary>
        /// Indigo pool for banff and galapagos features
        /// </summary>
        IndigoGalapagos,

        /// <summary>
        /// Indigo pool for banff and galapagos features
        /// </summary>
        GalapagosIndigo,

        /// <summary>
        /// Indigo premium pool Cocites features
        /// </summary>
        IndigoBanffGalapagosPrecision,

        /// <summary>
        /// Indigo premium pool galapagos features
        /// </summary>
        IndigoBanffGalapagosPremium,

        /// <summary>
        /// Indigo migrated firm 1 pool.
        /// </summary>
        IndigoMigratedFirm1Pool,

        /// <summary>
        /// Indigo migrated firm 2 pool.
        /// </summary>
        IndigoMigratedFirm2Pool,

        /// <summary>
        /// Indigo Notification Center
        /// </summary>
        IndigoNotificationCenter,

        /// <summary>
        /// Indigo Notification Center Clear User
        /// </summary>
        IndigoNotificationCenterClearUser,

        /// <summary>
        /// Komodo default pool for WLN.
        /// </summary>
        KomodoDefaultPool,

        /// <summary>
        /// The MafToCobalt password.
        /// </summary>
        MafToCobaltPool,

        /// <summary>
        /// Pool for 'Notification Center' tests
        /// </summary>
        NotificationCenter,

        /// <summary>
        /// Pool with clear users for 'Notification Center' tests
        /// </summary>
        NotificationCenterClearUsers,

        /// <summary>
        /// Pool of NZ users
        /// </summary>
        NzRegression,

        /// <summary>
        /// Olympic default pool for WLN.
        /// </summary>
        OlympicDefaultPool,

        /// <summary>
        /// Out of plan default pool for WLN.
        /// </summary>
        OopPreProd,

        /// <summary>
        /// Default pool for WL AU.
        /// </summary>
        PlAuDefault,

        /// <summary>
        /// Default pool for WL NZ.
        /// </summary>
        PlNzDefault,

        /// <summary>
        /// Default pool for WL NZ.
        /// </summary>
        PlNzRegression,

        /// <summary>
        /// Redlining Prod pool for Indigo.
        /// </summary>
        RedliningIndigoProdPool,

        /// <summary>
        /// SessionTimeout password pool
        /// </summary>
        SessionTimeout,

        /// <summary>
        /// WLNE pool for pricing guide.
        /// </summary>
        SurchargePaid,

        /// <summary>
        /// Taxnet pro 3 pool
        /// </summary>
        TaxnetProPool,

        /// <summary>
        /// Taxnet pro 3 pool
        /// </summary>
        TaxnetProRegressionPool,

        /// <summary>
        /// The test pool.
        /// </summary>
        TESTPool,

        /// <summary>
        /// Westlaw Advantage Canada
        /// </summary>
        WestlawAdvantageCanada,

        /// <summary>
        /// WLN Edge Canada Default Pool
        /// </summary>
        WlnClassicCanadaDefaultPool,

        /// <summary>
        /// WLNE pool for WL Analytics.
        /// </summary>
        WlneAnalyticAccessPool,

        /// <summary>
        /// WLNE alerts admin pool.
        /// </summary>
        WlneAlertAdminPool,  

        /// <summary>
        /// WLNE default pool.
        /// </summary>
        WlneDefaultPool,

        /// <summary>
        /// WLN Edge.
        /// </summary>
        WlnEdge,

        /// <summary>
        /// WLN Edge Canada Default Pool
        /// </summary>
        WlnEdgeCanadaDefaultPool,

        /// <summary>
        /// WLN Edge Canada Prod Pool
        /// </summary>
        WlnEdgeCanadaProdPool,

        /// <summary>
        /// WLNE pool for other purposes.
        /// </summary>
        WlneHistoryPool,

        /// <summary>
        /// WLN Indigo pool for other purposes.
        /// </summary>
        WlnIndigoPool,

        /// <summary>
        /// WLNE pool for other purposes.
        /// </summary>
        WlneReservedPool,

        /// <summary>
        /// WLNE pool for Tax Migrate.
        /// </summary>
        WlneTaxMigrate,

        /// <summary>
        /// General purpose WLNR pool.
        /// </summary>
        WlnrGeneralPurposePool,

        /// <summary>
        /// General purpose WLNR pool of lower environment accounts.
        /// </summary>
        WlnrGeneralPurposePreProdPool,

        /// <summary>
        /// General purpose WLNR pool of production accounts.
        /// </summary>
        WlnrGeneralPurposeProdPool,

        /// <summary>
        /// The pool of KM users.
        /// </summary>
        WlnrKmUserPool,

        /// <summary>
        /// The migrated firm 1.
        /// </summary>
        WlnrMigratedFirm1Pool,

        /// <summary>
        /// The migrated firm 2.
        /// </summary>
        WlnrMigratedFirm2Pool,

        /// <summary>
        /// The migrated firm 3.
        /// </summary>
        WlnrMigratedFirm3Pool,

        /// <summary>
        /// The pool of password without sharing.
        /// </summary>
        WlnrNoSharingPool,

        /// <summary>
        /// The pool of Patron Access users.
        /// </summary>
        WlnrPatronUserPool,

        /// <summary>
        /// The training password.
        /// </summary>
        WlnrTrainingPasswordPool,

        /// <summary>
        /// WLPAU pool.
        /// </summary>
        PrecisionAU,

        /// <summary>
        /// WLPNZ pool.
        /// </summary>
        PrecisionNZ,

        /// <summary>
        /// WLPAU Prod pool.
        /// </summary>
        PrecisionAU_Prod,

        /// <summary>
        /// WLPAU Prod pool.
        /// </summary>
        PrecisionNZProd,

        /// <summary>
        /// WLPNZ QED pool.
        /// </summary>
        WLN_PrecisionNZQED,

        /// <summary>
        /// WLPAU QED pool.
        /// </summary>
        WLN_PrecisionAUQED,

        /// <summary>
        /// WLPNZ Prod pool.
        /// </summary>
        WLN_PrecisionNZProd,

        /// <summary>
        /// WLPAU Prod pool.
        /// </summary>
        WLN_PrecisionAUProd
    }
}