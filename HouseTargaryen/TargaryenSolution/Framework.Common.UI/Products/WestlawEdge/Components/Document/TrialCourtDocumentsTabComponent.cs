namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.WestLawNext.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Trial court documents tab
    /// </summary>
    public class TrialCourtDocumentsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab_trialCourtDocumentsTabId");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Trial Court Documents";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
