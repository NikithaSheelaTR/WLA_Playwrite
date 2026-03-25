namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Title
{
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Title toolbar component
    /// </summary>
    public class TitleToolBarComponent : Toolbar
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[@class='la-Layout-titleToolBar']");

        private readonly By ContentTitleLocator = By.XPath(".//h2");
        /// <summary>
        /// Content title.
        /// </summary>
        public string ContentTitle => DriverExtensions.GetText(ComponentLocator, ContentTitleLocator);

        /// <summary>
        /// The delivery dropdown
        /// </summary>
        public new DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(this.ComponentLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;

    }
}