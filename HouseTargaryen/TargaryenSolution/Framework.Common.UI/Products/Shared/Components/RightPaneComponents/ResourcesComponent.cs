namespace Framework.Common.UI.Products.Shared.Components.RightPaneComponents
{
    using Framework.Common.UI.Products.Shared.Enums.Widgets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Resources Component
    /// </summary>
    public class ResourcesComponent : LinksComponent
    {
        private static readonly By LiveChatLinkLocator = By.CssSelector(".co_genericBox #coid_websiteFooter_livechatlink");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesComponent"/> class. 
        /// </summary>
        public ResourcesComponent()
            : base(LinksNames.Resources)
        {
        }

        /// <summary>
        /// Is Live Chat Link In Support Resources Component Displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsLiveChatLinkInSupportResourcesComponentDisplayed() => DriverExtensions.IsDisplayed(LiveChatLinkLocator);
    }
}
