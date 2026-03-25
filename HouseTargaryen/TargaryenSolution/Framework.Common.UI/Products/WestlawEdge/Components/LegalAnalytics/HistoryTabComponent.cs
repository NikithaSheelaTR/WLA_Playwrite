using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    
    /// <summary>
    /// THe HistoryTab class
    /// </summary>
    public class HistoryTabComponent : WestLawNext.Components.BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_caseHistoryReport_tab");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Experience";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
