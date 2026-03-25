namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.ANZ.Pages;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.TourComponents;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Precision home page
    /// </summary>
    public class PrecisionHomePage : EdgeHomePage
    {
        private const string PrecisionTabNameOfPage = "Westlaw Precision";
        private const string EdgePreviewTabNameOfPage = "Westlaw Edge";
        private static readonly By HomeBodyLocator = By.CssSelector("body.co_homepage");
        private static readonly By HomeTourLocator = By.Id("pendo-guide-container");
        private static readonly By ContainerLocator = By.XPath("//*[@class='Athens-browse-tools']");
        private static readonly By ToolLinkLocator = By.ClassName("Athens-browse-tool-content-heading");
        private static readonly By ResumeSession = By.XPath("//*[@value=\"Resume session\"]");
        private static readonly By ResumeSessionOverlayLocator = By.Id("co_suspendBilling");


        /// <summary>
        /// Home tour options - tour the homepage. remind me later
        /// </summary>
        public TourTheHomepageComponent TourTheHomepageComponent { get; } = new TourTheHomepageComponent();

        /// <summary>
        /// Athens Get Started panel component
        /// </summary>
        public PrecisionGetStartedPanel GetStartedPanel { get; } = new PrecisionGetStartedPanel();

        /// <summary>
        /// Get the Browse Component
        /// </summary>
        public new PrecisionBrowseTabPanel BrowseTabPanel { get; } = new PrecisionBrowseTabPanel();

        /// <summary>
        /// Get the Features Included widgets panel
        /// </summary>
        public PrecisionFeaturesIncludedComponent FeaturesIncludedPanel { get; } = new PrecisionFeaturesIncludedComponent();

        /// <summary>
        /// Checks whether or not the user has navigated to the Precision Westlaw Precision home page
        /// </summary>
        /// <returns> true if present</returns>
        public bool IsPrecisionHomePageDisplayed()
            => BrowserPool.CurrentBrowser.Title.Equals(PrecisionTabNameOfPage, StringComparison.InvariantCultureIgnoreCase)
               && DriverExtensions.IsDisplayed(HomeBodyLocator);

        /// <summary>
        /// Checks whether or not the user has navigated to the Westlaw Edge Preview home page
        /// </summary>
        /// <returns> true if present</returns>
        public bool IsEdgePreviewHomePageDisplayed()
            => BrowserPool.CurrentBrowser.Title.Equals(EdgePreviewTabNameOfPage, StringComparison.InvariantCultureIgnoreCase)
               && DriverExtensions.IsDisplayed(HomeBodyLocator);

        /// <summary>
        /// home tour - true if tour is displayed, false otherwise
        /// </summary>
        /// <returns> true if displayed</returns>
        public new bool IsHomeTourDisplayed() => DriverExtensions.IsDisplayed(HomeTourLocator, 5);

        /// <summary>
        /// Browse Tools Tab Labels
        /// </summary>
        public IReadOnlyCollection<ILink> ToolTabLinks => new ElementsCollection<Link>(ContainerLocator, ToolLinkLocator);

        /// <summary>
        /// Click Resume Session Button
        /// </summary>
        public PrecisionHomePage ClickResumeSession()
        {
            DriverExtensions.WaitForCondition(a => DriverExtensions.IsDisplayed(ResumeSessionOverlayLocator, 10000));
            DriverExtensions.WaitForElement(ResumeSession).Click();
            return this;
        }
    }
}
