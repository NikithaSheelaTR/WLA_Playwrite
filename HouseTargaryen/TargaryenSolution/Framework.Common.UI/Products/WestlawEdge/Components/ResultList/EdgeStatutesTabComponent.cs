namespace Framework.Common.UI.Products.WestlawEdge.Components.ResultList
{
    using OpenQA.Selenium;

    /// <summary>
    /// The statutes tab component.
    /// </summary>
    public class EdgeStatutesTabComponent : EdgeBaseFolderAnalysisTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//a[contains(@title,'Statutes & Court Rules')]");

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
