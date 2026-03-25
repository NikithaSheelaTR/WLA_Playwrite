namespace Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch
{
    using Framework.Common.UI.Products.Shared.Enums.PortalManagerSearch;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Components.PortalManager;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// POM for the form displayed when user clicks on Tools > Portal Manager > Global Search tab > Create button
    /// </summary>
    public class GlobalSearchFormPage : BaseModuleRegressionPage
    {
        private static readonly By ContentSelectionRequiredErrorMsgLocator =
            By.XPath("//div[@class='co_infoBox_message'and contains (.,'You must select content for the module.')]");

        private static readonly By FormDescriptionTextBoxLocator = By.Id("co_notes");

        private static readonly By FormNameTextBoxLocator = By.Id("co_modulename");

        private static readonly By NameRequiredErrorMsgLocator =
            By.XPath("//div[@class='co_infoBox_message' and contains (.,'Please enter a Name')]");

        private static readonly By SubmitButtonLocator = By.Id("saveFormButton2");

        private static readonly By IncludeSearchOrContentLinksModuleCheckboxLocator = By.Id("chk_createCombinePortal");

        private EnumPropertyMapper<GlobalSearchFormRadioButtons, WebElementInfo> buttonMap;

        /// <summary>
        /// The custom page widget.
        /// </summary>
        public CustomPageComponent CustomPageWidget { get; } = new CustomPageComponent();

        /// <summary>
        /// The content widget.
        /// </summary>
        public WlnContentComponent WlnContentWidget { get; } = new WlnContentComponent();

        private EnumPropertyMapper<GlobalSearchFormRadioButtons, WebElementInfo> ButtonMap
            => this.buttonMap = this.buttonMap ?? EnumPropertyModelCache.GetMap<GlobalSearchFormRadioButtons, WebElementInfo>();

        /// <summary>
        /// Returns the value in the description textbox
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDescriptionTextBoxValue() => DriverExtensions.WaitForElement(FormDescriptionTextBoxLocator).Text;

        /// <summary>
        /// True if the at least one 'Content is required' tooltip id displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsContentRequiredMsgDisplayed() => DriverExtensions.IsDisplayed(ContentSelectionRequiredErrorMsgLocator);

        /// <summary>
        /// True if the name required tooltip is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNameRequiredMsgDisplayed() => DriverExtensions.IsDisplayed(NameRequiredErrorMsgLocator);

        /// <summary>
        /// Returns the value in the Name textbox
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetNameTextBoxValue() => DriverExtensions.WaitForElement(FormNameTextBoxLocator).GetAttribute("value");

        /// <summary>
        /// Add Description 
        /// </summary>
        /// <param name="description">the description to set.</param>
        public void AddDescription(string description)
            => DriverExtensions.SetTextField(description, FormDescriptionTextBoxLocator);

        /// <summary>
        /// Add Name
        /// </summary>
        /// <param name="name">The name to set.</param>
        public void AddName(string name) => DriverExtensions.SetTextField(name, FormNameTextBoxLocator);

        /// <summary>
        /// Sets the checkbox to include module for content links / search.
        /// </summary>
        public void SetIncludeSearchOrContentLinksModuleCheckbox()
            => DriverExtensions.SetCheckbox(IncludeSearchOrContentLinksModuleCheckboxLocator, true);

        /// <summary>
        /// Submit Form
        /// </summary>
        /// <typeparam name="T">type of page</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickSubmit<T>() where T : BaseModuleRegressionPage, new()
        {
            DriverExtensions.WaitForElement(SubmitButtonLocator).Click();
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// SelectContentTypeRadioButton
        /// </summary>
        /// <param name="radiobutton">GlobalSearchFormRadioButton</param>
        public void SelectContentTypeRadioButton(GlobalSearchFormRadioButtons radiobutton) =>
            DriverExtensions.WaitForElement(By.XPath(this.ButtonMap[radiobutton].LocatorString)).CustomClick();
    }
}