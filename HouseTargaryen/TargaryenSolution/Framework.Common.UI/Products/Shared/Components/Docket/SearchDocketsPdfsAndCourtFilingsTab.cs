namespace Framework.Common.UI.Products.Shared.Components.Docket
{
    using Framework.Common.UI.Products.WestLawNext.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Search dockets, available PDFs, and court filings tab
    /// </summary>
    public class SearchDocketsPdfsAndCourtFilingsTab : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_advancedSearchContent']//div[@id='ITCBRFPDF']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Search dockets, available PDFs, and court filings";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
