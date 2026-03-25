namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using OpenQA.Selenium;

    /// <summary>
    /// The cases tab component.
    /// </summary>
    public class EdgeCasesTabComponent : EdgeBaseFolderAnalysisTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//a[contains(@title,'Cases')]");

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
