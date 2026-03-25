namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Recent
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base page for RecentDocuments and RecentSearches pages
    /// </summary>
    public class BaseRecentPage : MobileBasePage
    {
        private const string TitleLctMask = "//div[@class='hdr' and contains(text(), '{0}')]";
        private static readonly By RecentItemLocator = By.XPath("//*[contains(@id, 'cobalt_foldering_ro_item_name_')]");
        private static readonly By ItemsCountTextLocator = By.Id("coid_website_orientationCue");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRecentPage"/> class.  
        /// </summary>
        /// <param name="pageName"> Page Name </param>
        public BaseRecentPage(string pageName)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(TitleLctMask, pageName)));
        }

        /// <summary>
        /// Get list of recent searches
        /// </summary>
        /// <returns>List of recent searches</returns>
        public List<string> GetRecentitemList()
            => DriverExtensions.GetElements(RecentItemLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Clicks a recent search link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="search"> Link to click </param>
        /// <returns> New instance of the page </returns>
        public T ClickRecentitemByName<T>(string search) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(RecentItemLocator).FirstOrDefault(elem => elem.Text.Equals(search))?.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the orientation cue text
        /// </summary>
        /// <returns>Orientation cue text</returns>
        public string GetRecentItemsCountText() => DriverExtensions.GetText(ItemsCountTextLocator);
    }
}
