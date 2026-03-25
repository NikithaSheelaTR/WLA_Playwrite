namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Sort By Dropdown in Citing Reference tab
    /// </summary>
    public class CitingReferenceSortByDropdown : Dropdown<CitingReferenceSortOptions>
    {
        private static readonly By CitingReferenceSortBySelectElement = By.CssSelector("[id*='sort'] select");

        private static readonly By CourtLevelSortByOptionLocator = By.Id("courtlevelcases");

        /// <summary>
        /// Initializes a new instance of the <see cref="CitingReferenceSortByDropdown" /> class.
        /// </summary>
        public CitingReferenceSortByDropdown() : base(CitingReferenceSortBySelectElement)
        {
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get message of Court Level option
        /// </summary>
        /// <returns> Message of Court Level option</returns>
        public string GetCourtLevelOptionMessage() =>
            DriverExtensions.GetAttribute("title", CourtLevelSortByOptionLocator);
    }
}
