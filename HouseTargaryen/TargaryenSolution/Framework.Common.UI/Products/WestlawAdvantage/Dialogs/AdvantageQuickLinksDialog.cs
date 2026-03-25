namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// Advantage Quick Links Dialog
    /// </summary>
    public class AdvantageQuickLinksDialog : BaseModuleRegressionDialog
    {
        private readonly By ContainerLocator = By.XPath("//*[contains(@class, '__quickLinksModal')]");
        private readonly By SearchFieldHostLocator = By.XPath("//saf-search-field[contains(@class, '__searchField')]");
        private readonly By ClearAllButtonLocator = By.XPath("//saf-button[contains(@class, '__clearAllButton')]");
        private readonly By SaveButtonLocator = By.XPath("//saf-button[contains(@class, '__saveButton')]");
        private readonly By CancelButtonLocator = By.XPath("//saf-button[contains(@class, '__cancelButton')]");
        private readonly By CateroryItemLocator = By.XPath("//fieldset/div[contains(@class, '__item')]");

        private readonly string CheckboxLocator = "//saf-checkbox[contains(@class, '__checkbox') and text()={0}]";
        private readonly string  SearchFieldLocator = "input";

        /// <summary>
        /// Clear All Button
        /// </summary>
        public IButton ClearAllButton => new Button(ClearAllButtonLocator);

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Save Button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Enter Category
        /// </summary>
        public void EnterCategory(string category)
        {
            var hostElement = DriverExtensions.GetElement(SearchFieldHostLocator);
            var searchCategoryfield = (IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{SearchFieldLocator}'));",
            hostElement);
            searchCategoryfield.SendKeys(category);
            DriverExtensions.PressEnterKey();
        }

        /// <summary>
        /// Is Save Button Enabled
        /// </summary>
        public bool IsSaveButtonEnabled => SaveButton.GetAttribute("class").Contains("disabled");

        /// <summary>
        /// Select Category Checkbox
        /// </summary>
        /// <param name="categoryName"> Category string</param>
        public void SelectCategoryCheckbox(string categoryName)
        {
            var hostElement = DriverExtensions.GetElement(SafeXpath.BySafeXpath(CheckboxLocator, categoryName));
            var productLink = (IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{SearchFieldLocator}'));",
            hostElement);
            productLink.JavascriptClick();
        }
    }
}
