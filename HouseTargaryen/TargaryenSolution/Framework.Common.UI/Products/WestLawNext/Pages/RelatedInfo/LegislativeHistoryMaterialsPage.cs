namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.Shared.Components.Document.RI;

    /// <summary>
    /// LegislativeHistoryMaterialsPage
    /// </summary>
    public class LegislativeHistoryMaterialsPage : TabPage
    {
        /// <summary>
        /// The reference grid locator.
        /// </summary>
        private const string ReferenceGridLocator = "//table[@id='co_relatedInfo_table']";

        /// <summary>
        /// Legislative History Grid
        /// </summary>
        public ReferenceGridComponent LegislativeHistoryGrid => new ReferenceGridComponent(ReferenceGridLocator);
    }
}