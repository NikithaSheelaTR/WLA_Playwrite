namespace Framework.Common.UI.Products.Shared.Models.CustomPages.EnhancedCustomPageSharing
{
    /// <summary>
    /// Set of options for sharing Custom page
    /// </summary>
    public class CustomPageSharingModel
    {
        /// <summary>
        /// Gets a value indicating whether make start page.
        /// </summary>
        public bool MakeStartPage { get; set; }

        /// <summary>
        /// Gets a value indicating whether make non billable zone.
        /// </summary>
        public bool MakeNonBillableZone { get; set; }

        /// <summary>
        /// Gets a value indicating whether make e library.
        /// </summary>
        public bool MakeELibrary { get; set; }

        /// <summary>
        /// Gets a value indicating whether make enhanced search results.
        /// </summary>
        public bool MakeEnhancedSearchResults { get; set; }

        /// <summary>
        /// Gets the zone name.
        /// </summary>
        public string ZoneName { get; set; }
    }
}