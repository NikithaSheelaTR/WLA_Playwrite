namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using OpenQA.Selenium;

    /// <summary>
    /// The Secondary Sources tab component.
    /// </summary>
    public class SecondarySourcesTabComponent : BaseFolderAnalysisTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//li[.//a[contains(@title,'Secondary Sources')]]");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Secondary Sources";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}