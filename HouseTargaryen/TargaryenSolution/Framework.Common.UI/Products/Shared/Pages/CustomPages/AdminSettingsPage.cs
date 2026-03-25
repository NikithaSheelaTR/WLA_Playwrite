namespace Framework.Common.UI.Products.Shared.Pages.CustomPages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.CustomPage.EnhancedCustomPageSharing;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Admin Settings Page 
    /// </summary>
    public class AdminSettingsPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By AdminSettingsPageHeaderLocator = By.XPath("//div[@id='co_adminMenuHeader']/h1");
        private static readonly By CustomPageNameLinkLocator = By.XPath("//div[@id='co_adminMenuHeader']/h1/following-sibling::*");
    
        /// <summary>
        /// Header
        /// </summary>
        public string PageHeader => DriverExtensions.WaitForElement(AdminSettingsPageHeaderLocator).Text;

        /// <summary>
        /// Graph component
        /// </summary>
        public AdminSettingsMenuPanel AdminSettingsMenuPanel { get; } = new AdminSettingsMenuPanel();

        /// <summary>
        /// Get Custom Page Link Name
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCustomPageLinkName() => DriverExtensions.WaitForElement(CustomPageNameLinkLocator).Text;

        /// <summary>
        /// Is Link enabled
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPageHeaderEnabled() =>
            string.IsNullOrEmpty(
                DriverExtensions.WaitForElement(CustomPageNameLinkLocator).GetAttribute("disabled"));

        /// <summary>
        /// The click custom page name.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ClickCustomPageName<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CustomPageNameLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}