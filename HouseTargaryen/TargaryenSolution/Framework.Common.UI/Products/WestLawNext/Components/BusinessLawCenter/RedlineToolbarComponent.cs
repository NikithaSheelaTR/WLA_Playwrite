namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RedlineToolbarComponent
    /// </summary>
    public class RedlineToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By NextDifferenceButtonLocator = By.XPath("//a[@title='Next Difference']");

        private static readonly By PreviousDifferenceButtonLocator = By.XPath("//a[@title='Previous Difference']");

        private static readonly By ContainerLocator = By.XPath("//li[@ng-show='showDifferencesNavigation']");

        private static readonly By ViewModeDropdownLocator = By.XPath("//select[@data-ng-model='selectedMode']");

        /// <summary>
        /// ViewModeDropdown
        /// </summary>
        public IDropdown<ViewModeOptions> ViewModeDropdown { get; } = new RedlineViewModeDropdown(ViewModeDropdownLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Next Difference button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickNextDifferenceButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(NextDifferenceButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that 'Previous Difference' button is disabled
        /// </summary>
        /// <returns> True if button is disabled, false otherwise </returns>
        public bool IsPreviousDifferenceButtonDisabled()
            => DriverExtensions.WaitForElement(PreviousDifferenceButtonLocator).GetAttribute("class").Contains("co_disabled");
    }
}