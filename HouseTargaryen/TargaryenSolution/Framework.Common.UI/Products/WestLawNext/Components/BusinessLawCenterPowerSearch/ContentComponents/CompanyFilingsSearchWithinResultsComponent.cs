namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The company filings search within results section.
    /// </summary>
    public class CompanyFilingsSearchWithinResultsComponent : BaseFilingResultsComponent
    {
        private static readonly By FirstResultsListItemLocator =
            By.XPath("(//li/span[@class='companyFilingResult ng-binding'])[1]");

        private static readonly By SearchResultsItemsLocator = By.XPath("//div[@class='listItems itemClickable ng-scope']//li");

        private static readonly By SearchWithinBreadCrumbLocator =
            By.XPath("//span[@class='co_breadCrumbItem ng-scope']/a[contains(.,'Filings Results')]");

        private static readonly By ContainerLocator = By.Id("coid_docBreadcrumbContainer");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyFilingsSearchWithinResultsComponent"/> class.
        /// </summary>
        public CompanyFilingsSearchWithinResultsComponent()
        {
            DriverExtensions.WaitForElementDisplayed(SearchWithinBreadCrumbLocator);
            DriverExtensions.WaitForElementDisplayed(SearchResultsItemsLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The click on first result list item.
        /// </summary>
        /// <returns> The <see cref="DocumentDetailsPage"/>. </returns>
        public DocumentDetailsPage ClickOnFirstResultListItem()
        {
            DriverExtensions.WaitForElement(FirstResultsListItemLocator).Click();
            return new DocumentDetailsPage();
        }
    }
}