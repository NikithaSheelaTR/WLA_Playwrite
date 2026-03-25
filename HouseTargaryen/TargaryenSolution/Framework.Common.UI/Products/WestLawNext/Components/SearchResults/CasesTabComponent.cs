namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using OpenQA.Selenium;

    /// <summary>
    /// The cases tab component.
    /// </summary>
    public class CasesTabComponent : BaseFolderAnalysisTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//li[.//a[contains(@title,'Cases')]]");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Cases";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}