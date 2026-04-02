namespace Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics
{
    /// <summary>
    /// Specifies the time period within which activity should be included for the Opportunity Finder.
    /// </summary>
    public enum OpportunityFinderIncludeActivityWithinTheLast
    {
        /// <summary>
        /// Include activity from the last five years.
        /// </summary>
        FiveYears,

        /// <summary>
        /// Include activity from the last three years.
        /// </summary>
        ThreeYears,

        /// <summary>
        /// Include activity from the last year.
        /// </summary>
        OneYear,

        /// <summary>
        /// Include activity from the last six months.
        /// </summary>
        SixMonths,

        /// <summary>
        /// Include activity from the last three months.
        /// </summary>
        ThreeMonths,

        /// <summary>
        /// Include activity from the last month.
        /// </summary>
        OneMonth
    }
}