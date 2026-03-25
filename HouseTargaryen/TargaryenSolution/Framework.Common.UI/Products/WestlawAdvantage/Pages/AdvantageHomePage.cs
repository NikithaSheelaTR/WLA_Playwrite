namespace Framework.Common.UI.Products.WestlawAdvantage.Pages
{
    using System;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Advantage home page
    /// </summary>
    public class AdvantageHomePage : PrecisionHomePage
    {
        private const string AdvantageTabNameOfPage = "Westlaw Advantage";
        private static readonly By HomeBodyLocator = By.CssSelector("body.co_homepage");
        private static readonly By WestlawAdvantageLogoLinkLocator = By.Id("coid_website_logo");
        private const string BrowseContentLinkLctMask = ".//*[@id='coid_browseTabs']/li[contains(text(),'{0}')]";

        /// <summary>
        /// Get the Features Included widgets panel
        /// </summary>
        public AdvantageFeaturesIncludedComponent KeyFeaturesIncludedPanel { get; } = new AdvantageFeaturesIncludedComponent();

        /// <summary>
        /// Advantage Browse Tab Panel
        /// </summary>
        public AdvantageBrowseTabPanel TabPanel { get; } = new AdvantageBrowseTabPanel();

        /// <summary>
        /// Checks whether or not the user has navigated to the Precision Westlaw Precision home page
        /// </summary>
        /// <returns> true if present</returns>
        public bool IsAdvantageHomePageDisplayed()
            => BrowserPool.CurrentBrowser.Title.Equals(AdvantageTabNameOfPage, StringComparison.InvariantCultureIgnoreCase)
               && DriverExtensions.IsDisplayed(HomeBodyLocator);

        /// <summary>
        /// Westlaw Advantage Logo Link
        /// </summary>
        public ILink WestlawAdvantageLogoLink => new Link(WestlawAdvantageLogoLinkLocator);

        /// <summary>
        /// Browse Content Link by title in responsive mode
        /// </summary>
        /// <param name="title">browse content title</param>
        public ILink BrowseContentLink(string title) => new Link(By.XPath(string.Format(BrowseContentLinkLctMask, title)));

        /// <summary>
        /// WLA AJS Survey result Component
        /// </summary> 
        public WLAAiJurisdictionalSurveysResultComponent WlaAjsSurveyResult { get; } = new WLAAiJurisdictionalSurveysResultComponent();

        /// <summary>
        /// Get Global Search results and Smart Browse components
        /// </summary>
        public AdvantageSmartBrowseComponent globalSearchResults { get; } = new AdvantageSmartBrowseComponent();
    }
}
