namespace Framework.Common.UI.Products.CaseNotebook.Pages
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.CaseNotebook.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Westlaw Search Options Page for Case Notebook
    /// </summary>
    public class WestlawSearchOptionsPage : BaseModuleRegressionPage
    {
        private static readonly By SaveButtonLocator = By.XPath("//input[@id='btnSave']");

        private EnumPropertyMapper<WestlawOptions, WebElementInfo> westlawOptionsMap;

        /// <summary>
        /// Gets the Westlaw Options Map
        /// </summary>
        private EnumPropertyMapper<WestlawOptions, WebElementInfo> WestlawOptionsMap
            =>
                this.westlawOptionsMap =
                    this.westlawOptionsMap ?? EnumPropertyModelCache.GetMap<WestlawOptions, WebElementInfo>();

        /// <summary>
        /// Clicks Save button
        /// </summary>
        /// <typeparam name="T">The type of the page</typeparam>
        /// <returns>The instance of the page</returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Selects westlaw option
        /// </summary>
        /// <param name="option">Westlaw Options</param>
        public void SelectOption(WestlawOptions option) => 
            DriverExtensions.WaitForElement(By.Id(this.WestlawOptionsMap[option].Id)).Click();

        /// <summary>
        /// Selects westlaw option
        /// </summary>
        /// <param name="option">Westlaw Options</param>
        /// <param name="tabName">The name of the tab</param>
        /// <typeparam name="T">The type of the page</typeparam>
        /// <returns>The instance of the page</returns>
        public T SelectOption<T>(WestlawOptions option, string tabName = null) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.Id(this.WestlawOptionsMap[option].Id)).Click();

            return option == WestlawOptions.WestlawNext 
                ? this.ClickSaveButton<T>() 
                : this.ClickSaveAndOpenNewTab<T>(tabName);
        }

        /// <summary>
        /// The click save and open new tab.
        /// </summary>
        /// <param name="newTabName">
        /// The new Tab Name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        private T ClickSaveAndOpenNewTab<T>(string newTabName) where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();

            // Case Notebook opens one tab then closes it and open anotrer one.
            // Next two line wait for closing temporary tab
            string handle = BrowserPool.CurrentBrowser.TabHandles.Last();
            DriverExtensions.WaitForCondition(condition => !BrowserPool.CurrentBrowser.TabHandles.Contains(handle), 5);

            BrowserPool.CurrentBrowser.CreateTab(newTabName);
            BrowserPool.CurrentBrowser.ActivateTab(newTabName);
            DriverExtensions.WaitForNewTabLoaded(browserTabsCount);
            DriverExtensions.RefreshPage();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
