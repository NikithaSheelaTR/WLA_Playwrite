namespace Framework.Common.UI.Products.Shared.Models.EnumProperties
{
    /// <summary>
    /// The jurisdiction info.
    /// </summary>
    public class JurisdictionInfo : WebElementInfo
    {
        /// <summary>
        /// boolean indicating whether or not the jurisdiction is available
        /// on the full WLN site
        /// </summary>
        public bool IsAvailableOnWlnFullSite { get; set; }

        /// <summary>
        /// boolean indicating whether or not the jurisdiction is available
        /// on the WLN Mobile site
        /// </summary>
        public bool IsAvailableOnWlnMobileSite { get; set; }

        /// <summary>
        /// boolean indicating whether or not the jurisdiction is a Federal jurisdiction
        /// </summary>
        public bool IsFederal { get; set; }

        /// <summary>
        /// boolean indicating whether or not the jurisdiction is a State jurisdiction
        /// </summary>
        public bool IsState { get; set; }

        /// <summary>
        /// boolean indicating whether or not the jurisdiction is valid
        /// for use in global searches
        /// </summary>
        public bool IsValidSearchJurisdiction { get; set; }

        /// <summary>
        /// name/label of the jurisdiction
        /// </summary>
        public string JurisdictionName { get; set; }
    }
}