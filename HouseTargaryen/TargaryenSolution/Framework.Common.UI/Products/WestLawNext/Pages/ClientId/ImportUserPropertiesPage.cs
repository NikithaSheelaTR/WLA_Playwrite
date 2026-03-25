namespace Framework.Common.UI.Products.WestLawNext.Pages.ClientId
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Enums.Signon;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Page for user emulation textbox on login
    /// </summary>
    public class ImportUserPropertiesPage : BaseModuleRegressionPage
    {
        private static readonly By CancelButtonLocator = By.Id("co_cancelSignon");

        private EnumPropertyMapper<ImportUser, WebElementInfo> importUsersMap;

        /// <summary>
        /// Gets the ImportUser enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<ImportUser, WebElementInfo> ImportUsersMap
            => this.importUsersMap = this.importUsersMap ?? EnumPropertyModelCache.GetMap<ImportUser, WebElementInfo>();

        /// <summary>
        /// Clicks the cancel button
        /// </summary>
        /// <typeparam name="T">type of object to return</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }    

        /// <summary>
        /// Checks if the chosen checkbox is visible
        /// </summary>
        /// <param name="checkbox">The checkbox enum value</param>
        /// <returns>If the checkbox is visible. If not found, returns false.</returns>
        public bool IsCheckboxDisplayed(ImportUser checkbox) => DriverExtensions.IsDisplayed(By.CssSelector(this.ImportUsersMap[checkbox].LocatorString));
    }
}