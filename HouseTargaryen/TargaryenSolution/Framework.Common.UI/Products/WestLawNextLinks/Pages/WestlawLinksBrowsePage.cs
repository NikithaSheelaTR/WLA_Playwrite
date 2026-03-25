namespace Framework.Common.UI.Products.WestLawNextLinks.Pages
{
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WestlawLinksBrowsePage
    /// </summary>
    public class WestlawLinksBrowsePage : CommonBrowsePage
    {
        private static readonly By SearchAllContentButtonLocator = By.Id("coid_browseHideCheckboxes");

        private static readonly By SpecificContentButtonLocator = By.Id("coid_browseShowCheckboxes");

        /// <summary>
        /// IsSearchAllContentButtonDisplayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSearchAllContentButtonDisplayed() => DriverExtensions.IsDisplayed(SearchAllContentButtonLocator);

        /// <summary>
        ///  IsSpecificContentButtonDisplayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSpecificContentButtonDisplayed() => DriverExtensions.IsDisplayed(SpecificContentButtonLocator);
    }
}
