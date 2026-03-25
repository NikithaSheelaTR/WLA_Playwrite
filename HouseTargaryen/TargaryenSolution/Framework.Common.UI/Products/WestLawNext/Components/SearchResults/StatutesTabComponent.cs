namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using OpenQA.Selenium;

    /// <summary>
    /// The statutes tab component.
    /// </summary>
    public class StatutesTabComponent : BaseFolderAnalysisTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//li[.//a[contains(@title,'Statutes')]]");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Statutes";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}