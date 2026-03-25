namespace Framework.Common.UI.Products.Shared.Components.AdvancedSearchTemplates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The find and key cite widget.
    /// </summary>
    public class FindAndKeyCiteComponent : BaseModuleRegressionComponent
    {
        private const string JurisdictionDropdownLctMask = "//input[@name='jurisdiction' and @value={0}]";

        private const string StateRegExp = @"[^District of Columbia]District of|\sRegulation|\sStatute[s]?|\sCase";

        private static readonly By ErorMessageLocator =
            By.CssSelector("#co_keyciteWidgetMessagePlaceholder div.co_infoBox_message");

        private static readonly By SearchTypeFindRadioButtonLocator = By.Id("docFind");

        private static readonly By SearchTypeKeyCiteRadioButtonLocator = By.Id("docKC");

        private static readonly By SpecificTemplateDropDownLocator = By.Id("specificTemplate");

        private static readonly By StateDropDownLocator = By.Id("co_stateSelect");

        private static readonly By TemplateComponentLocator = By.XPath("//div[contains(@id, 'input_div')]");

        private static readonly By TemplateNameLocator = By.XPath("//div[@id='co_templateDisplay']/label/strong");

        private static readonly By TemplateTypeDropDownLocator = By.Id("templateType");

        private static readonly By ContainerLocator = By.Id("co_search_advancedSearch_left");

        /// <summary>
        /// Gets the template name.
        /// </summary>
        public string TemplateName => DriverExtensions.GetText(TemplateNameLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the templates list.
        /// </summary>
        public IList<FindAndKeyCiteTemplateItem> TemplatesList
            => DriverExtensions.GetElements(TemplateComponentLocator)
                                .Select(container => new FindAndKeyCiteTemplateItem(container))
                                .ToList();

        /// <summary>
        /// The get error message.
        /// </summary>
        /// <returns> Error Message text </returns>
        public string GetErrorMessageText()
            => this.IsErrorMessageDisplayed() ? DriverExtensions.GetText(ErorMessageLocator) : string.Empty;

        /// <summary>
        /// The is error message present.
        /// </summary>
        /// <returns> True if error message is displayed, false otherwise </returns>
        public bool IsErrorMessageDisplayed() => DriverExtensions.IsDisplayed(ErorMessageLocator, 5);

        /// <summary>
        /// Use find.
        /// </summary>
        /// <returns>
        /// Find And KeyCite Widget instance
        /// </returns>
        public FindAndKeyCiteComponent SelectFindRadioButton()
        {
            DriverExtensions.WaitForElement(SearchTypeFindRadioButtonLocator).Click();
            return this;
        }

        /// <summary>
        /// The select jurisdiction.
        /// </summary>
        /// <param name="jurisdiction"> The jurisdiction. </param>
        /// <returns> Find And KeyCite Widget instance </returns>
        public FindAndKeyCiteComponent SelectJurisdiction(string jurisdiction)
        {
            IWebElement jurisdictionElement =
                DriverExtensions.GetElements(SafeXpath.BySafeXpath(JurisdictionDropdownLctMask, jurisdiction))
                                .FirstOrDefault();
            jurisdictionElement?.Click();
            return this;
        }

        /// <summary>
        /// The use key city.
        /// </summary>
        /// <returns>
        /// Find And KeyCite Widget instance
        /// </returns>
        public FindAndKeyCiteComponent SelectKeyCiteRadioButton()
        {
            DriverExtensions.WaitForElement(SearchTypeKeyCiteRadioButtonLocator).Click();
            return this;
        }

        /// <summary>
        /// The select specific template.
        /// </summary>
        /// <param name="specificTemplate">
        /// The specific template.
        /// </param>
        /// <returns> The <see cref="FindAndKeyCiteComponent"/>. </returns>
        public FindAndKeyCiteComponent SelectSpecificTemplate(string specificTemplate)
        {
            if (DriverExtensions.IsDisplayed(StateDropDownLocator))
            {
                this.SelectState(Regex.Replace(specificTemplate, StateRegExp, string.Empty));
            }

            DriverExtensions.WaitForElement(SpecificTemplateDropDownLocator).SetDropdown(specificTemplate);

            return this;
        }

        /// <summary>
        /// The select state.
        /// </summary>
        /// <param name="state"> The state. </param>
        /// <returns> The <see cref="FindAndKeyCiteComponent"/>. </returns>
        public FindAndKeyCiteComponent SelectState(string state)
        {
            DriverExtensions.WaitForElement(StateDropDownLocator).SetDropdown(state);
            return this;
        }

        /// <summary>
        /// The select template type.
        /// </summary>
        /// <param name="templateType"> The template type. </param>
        /// <returns> Find And KeyCite Widget instance </returns>
        public FindAndKeyCiteComponent SelectTemplateType(string templateType)
        {
            DriverExtensions.WaitForElement(TemplateTypeDropDownLocator).SetDropdown(templateType);
            return this;
        }
    }
}