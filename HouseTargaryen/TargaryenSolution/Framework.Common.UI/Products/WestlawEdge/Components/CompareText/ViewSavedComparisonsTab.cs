namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// View saved comparisons tab
    /// </summary>
    public class ViewSavedComparisonsTab : BaseCompareTextTab
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='panel_ViewSavedComparisons']");
        private static readonly By CountLocator = By.XPath(".//span[@id='coid_redlineLightbox_itemCount']");
        
        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "View saved comparisons";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Count of reports in the View Saved Comparisons tab
        /// </summary>
        public ILabel ReportsCount => new Label(this.ComponentLocator, CountLocator);
    }
}