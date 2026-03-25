namespace Framework.Common.UI.Products.Shared.Enums.Search
{
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// The different possible types of search results, as shown by setting LoggingQuantity to Verbose on the routing page
    /// </summary>
    public enum SearchResultSource
    {
        /// <summary>
        /// 
        /// </summary>
        [StringValue("ALL_CASES")]
        AllCases,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("BRIEF_JURIS")]
        BriefJuris,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("CASES_JURIS")]
        CasesJuris,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("CASE_FINDER")]
        CaseFinder,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("COLD_START")]
        ColdStart,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("FERMISEED")]
        FermiSeed,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("FERMIDISCOVERED")]
        FermiDiscovered,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("KEY_NUMBER_FINDER")]
        KeyNumberFinder,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("LOST")]
        Lost,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("LONGTAIL")]
        Longtail,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("NOVUS_PASSTHROUGH")]
        PassThrough,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("PARTYNAME")]
        PartyName,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("STATE_TRIAL_COURT_JURIS")]
        StateTrialCourtJuris,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("TRIAL_COURT_JURIS")]
        TrialCourtJuris,

        /// <summary>
        /// 
        /// </summary>
        [StringValue("OLD_CASE_RERANKED")]
        OldCaseReranked,

        /// <summary>
        /// 
        /// </summary>
        StatutesJuris
    }
}