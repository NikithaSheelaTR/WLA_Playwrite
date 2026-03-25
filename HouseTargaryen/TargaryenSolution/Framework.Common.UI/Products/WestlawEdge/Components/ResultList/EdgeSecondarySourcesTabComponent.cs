namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using OpenQA.Selenium;

    /// <summary>
    /// The Secondary Sources tab component.
    /// </summary>
    public class EdgeSecondarySourcesTabComponent : EdgeBaseFolderAnalysisTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//a[contains(@title,'Secondary Sources')]");

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
