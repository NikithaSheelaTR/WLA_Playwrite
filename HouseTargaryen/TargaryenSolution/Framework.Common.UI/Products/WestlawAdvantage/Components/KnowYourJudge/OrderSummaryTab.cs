namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Order Summary Tab
    /// </summary>
    public class OrderSummaryTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='order-summaries-container']");
        private static readonly By OrderSummaryTextLocator = By.XPath("//p[contains(@class, 'OrderSummary-module__bodyTextStrong')]");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Order summary";

        /// <summary>
        /// Order Summary Text elements
        /// </summary>
        public IList<IWebElement> OrderSummaryTextElements => DriverExtensions.GetElements(ComponentLocator, OrderSummaryTextLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}


