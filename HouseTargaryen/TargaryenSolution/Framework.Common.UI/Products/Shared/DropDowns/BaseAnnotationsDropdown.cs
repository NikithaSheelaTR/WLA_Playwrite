namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Annotations Dropdown
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public abstract class BaseAnnotationsDropdown<T>: BaseModuleRegressionCustomDropdown<T> where T : struct
    {
        private static readonly By AddNoteButtonLocator = By.XPath("//li[@id='co_docToolbarAddNoteWidget']");
        private static readonly By DropdownLocator = By.XPath("//*[@class='icon_downMenu-gray' or @id='co_docToolbarAddNoteWidget' ]");

        /// <summary>
        /// Annotations map
        /// </summary>
        protected EnumPropertyMapper<T, WebElementInfo> annotationsMap;

        /// <summary>
        /// Collapse Dropdown
        /// </summary>
        public void CollapseDropdown() => DriverExtensions.WaitForElement(DropdownLocator).Click();

        /// <summary>
        /// Is Annotations drop-down displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsAnnotationsDropDownDisplayed() => DriverExtensions.IsDisplayed(AddNoteButtonLocator);

        /// <summary>
        /// Dropdown Element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);
    }
}