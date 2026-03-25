namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base filing results section.
    /// </summary>
    public abstract class BaseFilingResultsComponent : BaseResultsComponent
    {
        private static readonly By HideInsiderFilingsLocator = By.XPath("//div[@class='hideFilingsLink']/a[contains(.,'Hide Insider Filings')]");

        private static readonly By ShowInsiderFilingsLocator = By.XPath("//div[@class='hideFilingsLink']/a[contains(.,'Show Insider Filings')]");

        private static readonly By ContainerLocator = By.ClassName("co_searchToolbar");

        /// <summary>
        /// Gets the document view section.
        /// </summary>
        public DeliveryAndSaveToProjectComponent DeliveryAndSaveToProjectSection { get; } = new DeliveryAndSaveToProjectComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks insider filings links
        /// </summary>
        public void ClickHideInsiderFilingsLink()
        {
            DriverExtensions.WaitForElement(HideInsiderFilingsLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElementDisplayed(ShowInsiderFilingsLocator);
        }

        /// <summary>
        /// Clicks insider filings links
        /// </summary>
        public void ClickShowInsiderFilingsLink()
        {
            DriverExtensions.WaitForElement(ShowInsiderFilingsLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElementDisplayed(HideInsiderFilingsLocator);
        }

        /// <summary>
        /// Get the text value of the Show / Hide Insider Filings link
        /// </summary>
        /// <returns>text value of the current state of Show/Hide Insider Filings link</returns>
        public string GetInsiderFilingsDisplayStatus() => DriverExtensions.WaitForElement(ShowInsiderFilingsLocator).Text;
    }
}