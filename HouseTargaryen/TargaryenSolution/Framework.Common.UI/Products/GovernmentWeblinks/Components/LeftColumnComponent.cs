namespace Framework.Common.UI.Products.GovernmentWeblinks.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Enums;
    using Framework.Common.UI.Products.GovernmentWeblinks.Pages;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Left Column Component on Weblinks page
    /// </summary>
    public class LeftColumnComponent : BaseModuleRegressionComponent
    {
        private static readonly By AgreementLocator = By.XPath("//form[@id='agreement_Form']");

        private static readonly By HelpLinkLocator = By.XPath("//div[@class='co_innertube']//li//a[contains(text(), 'Help')]");

        private static readonly By HomeLinkLocator = By.XPath("//div[@class='co_innertube']//li//a[contains(text(), 'Home')]");

        private static readonly By LeftComponentLocator = By.XPath("//div[@class='co_innertube']");

        private static readonly By SearchByLinksLocator = By.XPath(".//div[@class='co_innertube']//li[contains(text(), 'Search by:')]//a");

        private static readonly By RelatedLinksLocator = By.XPath(".//div[@class='co_innertube']//li[contains(text(), 'Related Thomson Reuters Sites')]");

        private EnumPropertyMapper<WeblinksSearchType, BaseTextModel> searchTypeMap;

        /// <summary>
        /// Gets the WeblinksSearchType enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<WeblinksSearchType, BaseTextModel> SearchTypeMap
            =>
                this.searchTypeMap =
                    this.searchTypeMap ?? EnumPropertyModelCache.GetMap<WeblinksSearchType, BaseTextModel>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => LeftComponentLocator;

        /// <summary>
        /// Click the link from Search By section
        /// </summary>
        /// <returns>Instance of Weblinks help page</returns>
        public WeblinksHelpPage ClickHelpLink()
        {
            DriverExtensions.WaitForElement(HelpLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
            return new WeblinksHelpPage();
        }

        /// <summary>
        /// Click the home link from section
        /// </summary>
        /// <returns>Instance of Weblinks home page</returns>
        public WeblinksHomePage ClickHomeLink()
        {
            DriverExtensions.WaitForElement(HomeLinkLocator).Click();
            return new WeblinksHomePage();
        }

        /// <summary>
        /// Click the link from Search By section
        /// For some page (e.g. NY) at first opens License page.
        /// You should accept the license and continue working
        /// </summary>
        /// <param name="link">The link type</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>Instance of Weblinks page</returns>
        public T ClickSearchByLink<T>(WeblinksSearchType link) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(SearchByLinksLocator).First(e => e.Text == this.SearchTypeMap[link].Text).Click();
            DriverExtensions.WaitForPageLoad();

            if (DriverExtensions.IsDisplayed(AgreementLocator, 5))
            {
                new AgreementPage().AgreeLicense<T>();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the href of help link
        /// </summary>
        /// <returns>
        /// Href
        /// </returns>
        public string GetHelpLink() =>
            DriverExtensions.WaitForElement(HelpLinkLocator).GetAttribute("href");

        /// <summary>
        /// Gets the href of home link
        /// </summary>
        /// <returns>
        /// Href
        /// </returns>
        public string GetHomeLink() =>
            DriverExtensions.WaitForElement(HomeLinkLocator).GetAttribute("href");

        /// <summary>
        /// Gets the Enumerable of string from Related section
        /// </summary>
        /// <returns>
        /// Dictionary of string pair
        /// </returns>
        public Dictionary<string, string> GetRelatedLinks() =>
            DriverExtensions.GetElements(RelatedLinksLocator, By.TagName("a")).ToDictionary(e => e.Text, e => e.GetAttribute("href"));

        /// <summary>
        /// Verifies is left section displayed
        /// </summary>
        /// <returns>
        /// True if displayed, false otherwise
        /// </returns>
        public override bool IsDisplayed() =>
            DriverExtensions.IsDisplayed(LeftComponentLocator, 5);

        /// <summary>
        /// Verifies is section displayed
        /// </summary>
        /// <returns>
        /// True if displayed, false otherwise
        /// </returns>
        public bool IsRelatedLinksSectionDisplayed() =>
            DriverExtensions.IsDisplayed(RelatedLinksLocator, 5);
    }
}
