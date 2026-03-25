namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
	using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
	using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Client Id drop down
    /// </summary>
    public class ClientIdDropdown : BaseModuleRegressionCustomDropdown<string>
    {
        private static readonly By ClientIdTextBoxLocator = By.XPath("//input[contains(@class,'co_clientIDTextbox') or @id='co_clientIDTextbox'] | //input[@id='clientidr']");

        private static readonly By DropdownLinksLocator = By.XPath("//ul[contains(@id,'co_clientID')]/li");

        private static readonly By DropdownRecentLocator = By.Id("co_clientIDRecentsDropdown");

        private static readonly By DropdownOopLocator = By.Id("co_clientIDOOPRecentsDropdown");

        private static readonly By OopLabelLocator = By.XPath("//label[@for='co_clientIDOOPTextbox']");

        private static readonly By DropdownArrowLocator = By.XPath("//*[contains(text(),'Select a previous Client ID')]");

        private static readonly By ClientIdAutocompleteLocator = By.Id("co_clientIDAutocomplete");
        
        /// <summary>
        /// Return Selected Client Id
        /// </summary>
        public override string SelectedOption => DriverExtensions.GetText(ClientIdTextBoxLocator);

        /// <summary>
        /// Dropdown Element
        /// </summary>
        protected override IWebElement Dropdown =>
            !DriverExtensions.IsDisplayed(OopLabelLocator)
                ? DriverExtensions.GetElement(DropdownRecentLocator)
                : DriverExtensions.GetElement(DropdownOopLocator);

        /// <summary>
        /// Get Options from Dropdown
        /// </summary>
        protected override IEnumerable<string> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(DropdownLinksLocator).Select(el => el.Text);

        /// <summary>
        /// AutoSuggestionDialog
        /// </summary>
        /// <returns></returns>
        public AutoSuggestionDialog AutoSuggestionDialog => new AutoSuggestionDialog();

        /// <summary>
        /// Enter Client Id
        /// </summary>
        /// <param name="clientId">Client Id</param>
        public void EnterClientId(string clientId)
        {
            DriverExtensions.WaitForElement(ClientIdTextBoxLocator).SetTextField(clientId);
            if (this.IsDropdownArrowDisplayed() && this.IsDropdownExpanded())
            {
                this.ClickDropdownArrow();
            }
        }

        /// <summary>
        /// Type Client Id slowly
        /// </summary>
        /// <param name="clientId"></param>
        public void TypeClientId(string clientId)
		{
            DriverExtensions.WaitForElement(ClientIdTextBoxLocator).Clear();
            DriverExtensions.WaitForElement(ClientIdTextBoxLocator).SendKeysSlow(clientId);
        }

        /// <summary>
        /// Is not implemented as not applicable for this drop down
        /// </summary>
        /// <param name="option">Option to verify</param>
        /// <returns> NotImplementedException </returns>
        public override bool IsSelected(string option)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The max length attribute of the client id textbox
        /// </summary>
        /// <returns> Length of client id </returns>
        public int GetClientIdMaxLength() =>
            DriverExtensions.GetElement(ClientIdTextBoxLocator).GetAttribute("maxlength").ConvertCountToInt();

        /// <summary>
        /// Select Client Id
        /// </summary>
        /// <param name="option">Client id</param>
        public new void SelectOption(string option) => this.SelectOptionFromExpandedDropdown(option);

        /// <summary>
        /// Checks if the client id page exists.
        /// </summary>
        /// <returns>Whether the client id page is the current page</returns>
        public bool IsClientIdTextboxDisplayed() => DriverExtensions.IsDisplayed(ClientIdTextBoxLocator);

        /// <summary>
        /// Click Dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            if (this.IsDropdownArrowDisplayed())
            {
                DriverExtensions.GetElement(DropdownArrowLocator).Click();
            }
        }

        /// <summary>
        /// Is Dropdown Expanded
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        protected override bool IsDropdownExpanded() =>
            DriverExtensions.IsDisplayed(ClientIdAutocompleteLocator) || base.IsDropdownExpanded();

        /// <summary>
        /// Enter client id
        /// </summary>
        /// <param name="option">Client id</param>
        protected override void SelectOptionFromExpandedDropdown(string option) =>
            DriverExtensions.SetTextField(option, ClientIdTextBoxLocator);

        private bool IsDropdownArrowDisplayed() => DriverExtensions.IsDisplayed(DropdownArrowLocator);
    }
}