namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Precision additional cases result section
    /// </summary>
    public class PrecisionAdditionalCasesComponent
    {
        private static readonly By AdditionalCasesResultSectionLocator = By.XPath("//*[@id='coid_website_athensAdditionalCases']");
        private static readonly By AdditionalCasesResultHeadingLocator = new ByChained(AdditionalCasesResultSectionLocator, By.XPath("//h2[@class='co_search_header']"));
        private static readonly By AdditionalCasesResultListContainerLocator = By.Id("coid_website_athensAdditionalCases");
        private static readonly By AdditionalCasesResultListItemLocator = By.XPath(".//*[contains(@class, 'co_searchContent')]/ancestor::li");

        /// <summary>
        /// Get Additional Cases Result Heading Count
        /// </summary>
        public int AdditionalCasesHeaderCount => DriverExtensions.WaitForElement(AdditionalCasesResultHeadingLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Get Additional Cases View All Link
        /// </summary>
        public ILink AdditionalCasesViewAllLink => new Link(AdditionalCasesResultSectionLocator, By.CssSelector("a.Athens-additional-cases-link"));
        
        /// <summary>
        /// The additional cases results section of the page
        /// </summary>
        public ItemsCollection<PrecisionResultListItem> ResultItems =>
            new ItemsCollection<PrecisionResultListItem>(AdditionalCasesResultListContainerLocator, AdditionalCasesResultListItemLocator);
    }
}
