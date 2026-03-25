namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using OpenQA.Selenium;

    /// <summary>
    /// Find And Key Cite tab panel
    /// </summary>
    public class FindAndKeyCiteTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("co_browseWidgetTab2");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Find & KeyCite";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}