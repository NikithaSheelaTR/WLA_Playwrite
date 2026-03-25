namespace Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Portal Manager
    /// </summary>
    public class PortalManagerPage : BaseModuleRegressionPage
    {
        private const string TitleLctMask = "//a[@class='co_moduleName' and contains(text(),'{0}')]";

        private const string ModuleItemActionsLctMask = "/ancestor::tr//a[text()='{1}']";

        private static readonly By CreateButtonLocator = By.Id("createPortalSubmit");
        
        /// <summary> 
        /// Gets Common Westlaw Next Header Section 
        /// </summary>
        public WestlawNextHeaderComponent Header { get; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Click Create.
        /// </summary>
        /// <typeparam name="T">The Page object.</typeparam>
        /// <returns>The page that navigates to after creation.</returns>
        public T ClickCreate<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElementDisplayed(CreateButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on the delete link in the row of the specified module name to delete the module
        /// </summary>
        /// <param name="moduleName">The Module name.</param>
        /// <returns>The <see cref="DeleteFormDialog"/>.</returns>
        public DeleteFormDialog ClickDelete(string moduleName)
        {
            DriverExtensions.WaitForElementDisplayed(
                By.XPath(string.Format(TitleLctMask + ModuleItemActionsLctMask, moduleName, "Delete"))).Click();
            return new DeleteFormDialog();
        }

        /// <summary>
        /// Click on the GetHTML button
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <returns>The <see cref="GetHtmlDialog"/>.</returns>
        public GetHtmlDialog ClickGetHtml(string moduleName)
        {
            DriverExtensions.WaitForElementDisplayed(
                By.XPath(string.Format(TitleLctMask + ModuleItemActionsLctMask, moduleName, "Get HTML"))).Click();
            return new GetHtmlDialog();
        }

        /// <summary>
        /// Clicks on the specified module name displayed in the history table
        /// </summary>
        /// <param name="moduleName">The Module name.</param>
        /// <typeparam name="T">Page object</typeparam>
        /// <returns>The page to return.</returns>
        public T ClickModuleNameInHistory<T>(string moduleName) where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(TitleLctMask, moduleName))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the Preview button
        /// </summary>
        /// <param name="moduleName">The module name.</param>
        /// <returns>The <see cref="PreviewDialog"/>.</returns>
        public PreviewDialog ClickPreview(string moduleName)
        {
            DriverExtensions.WaitForElementDisplayed(
                By.XPath(string.Format(TitleLctMask + ModuleItemActionsLctMask, moduleName, "Preview"))).Click();
            return new PreviewDialog();
        }

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">Portal Manager tab.</param>
        public void ClickTab(PortalManagerTabs tab)
        {
            var portalManagerTabsMap = EnumPropertyModelCache.GetMap<PortalManagerTabs, WebElementInfo>();
            DriverExtensions.Click(DriverExtensions.WaitForElement(By.Id(portalManagerTabsMap[tab].Id)));
            DriverExtensions.WaitForElementDisplayed(CreateButtonLocator);
        }

        /// <summary>
        /// returns true if the specified module is in the history section
        /// </summary>
        /// <param name="moduleName">The Module name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsModuleNameInHistory(string moduleName)
        {
            DriverExtensions.WaitForElementDisplayed(CreateButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.IsDisplayed(By.XPath(string.Format(TitleLctMask, moduleName)));
        }
    }
}