namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the DocumentGoToWidget
    /// </summary>
    public class GoToDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private static readonly By DropdownLocator = By.XPath("//*[@id='co_docToolbarGoTo']//*[contains(@class,'co_dropdownArrow')]");

        private static readonly By ErrorContainerLocator = By.Id("co_document_starPage_starPageNavError");

        private static readonly By ErrorMessageTextLocator = By.XPath(".//div[@class='co_infoBox_message']");

        private static readonly By GoButtonLocator = By.Id("co_document_starPage_starPageNavGo");

        private static readonly By IsDropdownExpandedElementLocator = By.Id("co_docGoToWidget");

        private static readonly By LinksLocator = By.CssSelector("#co_docGoToWidget a");

        private static readonly By SearchInputLocator = By.Id("co_document_starPage_starPageNavInput");

        /// <summary>
        /// Initializes a new instance of the <see cref="GoToDropdown"/> class.
        /// </summary>
        public GoToDropdown()
        {
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Throw exception, because there is no selected option for GoTo dropdown</returns>
        public override string SelectedOption
        {
            get { throw new NotImplementedException("Can't get selected item for the GoTo dropdown"); }
        }

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(LinksLocator).Select(el => el.GetText()).ToList();

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        /// <summary>
        /// Clear GoTo dropdown text box
        /// </summary>
        public void ClearSearchTextBox() => DriverExtensions.GetElement(SearchInputLocator).Clear();

        /// <summary>
        /// Expands the dropdown.
        /// </summary>
        public void Expand() => this.Dropdown.Click();

        /// <summary>
        /// Checks is the dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(DropdownLocator, 5);

        /// <summary>
        /// Checks is the SearchInput displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSearchInputDisplayed() => DriverExtensions.IsDisplayed(SearchInputLocator);

        /// <summary>
        /// Checks is the Go button displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsGoButtonDisplayed() => DriverExtensions.IsDisplayed(GoButtonLocator);

        /// <summary>
        /// Checks is the error container present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsErrorContainerPresent() => DriverExtensions.IsElementPresent(ErrorContainerLocator);

        /// <summary>
        /// Checks is the dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsExpanded()
            =>
                DriverExtensions.GetAttribute("class", IsDropdownExpandedElementLocator)
                                .Contains("Expanded", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option"> Option to select </param>
        /// <returns> Throw exception, because there is no selected option for GoTo dropdown</returns>
        public override bool IsSelected(string option)
        {
            throw new NotImplementedException("Can't get selected item for the GoTo dropdown");
        }

        /// <summary>
        /// Enter query to the search input and click on the Enter button
        /// </summary>
        /// <param name="query"> Query to search </param>
        /// <returns> Error message text, if error message is displayed, otherwise returns empty string </returns>
        public string NavigateToDocumentUsingEnterButton(string query) => this.NavigateToDocument(query, false);

        /// <summary>
        /// Enter query to the search input and click on the button Go
        /// </summary>
        /// <param name="query"> Query to search </param>
        /// <returns> Error message text, if error message is displayed, otherwise returns empty string </returns>
        public string NavigateToDocumentUsingGoButton(string query) => this.NavigateToDocument(query, true);

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">Option to select</param>
        protected override void SelectOptionFromExpandedDropdown(string option)
        {
            IWebElement element = DriverExtensions.GetElements(LinksLocator)
                                                  .FirstOrDefault(link => link.Text.Trim().Equals(option.Trim()));
            element.Click();
        }

        /// <summary>
        /// Click Dropdown Arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.WaitForElement(DropdownLocator).Click();

        private string NavigateToDocument(string query, bool usingGoButton)
        {
            this.ExpandIfNotExpanded();

            DriverExtensions.WaitForElement(SearchInputLocator).SendKeys(query);
            if (usingGoButton)
            {
                DriverExtensions.Click(GoButtonLocator);
            }
            else
            {
                DriverExtensions.PressKey(Keys.Return);
            }

            DriverExtensions.WaitForJavaScript();

            return DriverExtensions.IsDisplayed(ErrorContainerLocator, 5)
                       ? DriverExtensions.GetElement(ErrorContainerLocator, ErrorMessageTextLocator).Text
                       : string.Empty;
        }
    }
}