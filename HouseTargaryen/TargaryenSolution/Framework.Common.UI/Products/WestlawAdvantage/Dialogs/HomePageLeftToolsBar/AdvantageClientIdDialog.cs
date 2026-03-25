namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Client Id Dialog
    /// </summary>
    public class AdvantageClientIdDialog : AdvantageBaseDialog
    {
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");
        private static readonly By AddNewClientIdButtonLocator = By.XPath(".//div/button[contains(@class,'__anchorButton')]");
        private static readonly By SelectAClientIDTextBoxLocator = By.XPath(".//saf-combobox");
        private static readonly By AddNewClientTextBoxLocator = By.XPath(".//saf-text-field");
        private static readonly By SaveNewClientIdButtonLocator = By.XPath(".//saf-button");
        private static readonly By RecentClientsItemsLocator = By.XPath(".//saf-list-item/button[contains(@class,'__anchorButton')]");
        private static string ClientAlreadyExistMessageMsk = "div.validation.error slot#status-undefined span.message";
        private static string AddNewClientTextBoxMsk = "div.root input.valid.control";
        private static string SelectAClientIDTextBoxMsk = "div.control slot input.selected-value";


        /// <summary>
        /// AddNewClientOrCancelIdButton
        /// </summary>
        public IButton AddNewClientOrCancelIdButton = new Button(ContentTypeContainerLocator, AddNewClientIdButtonLocator);

        /// <summary>
        /// ClientIdTextBox
        /// </summary>
        public ITextbox ClientIdTextBox => new Textbox((IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{SelectAClientIDTextBoxMsk}'));",
            DriverExtensions.GetElement(ContentTypeContainerLocator,SelectAClientIDTextBoxLocator)));

        /// <summary>
        /// ClearClientIdTextBox
        /// </summary>
        public void ClearClientIdTextBox() {
            var a = ((IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{SelectAClientIDTextBoxMsk}'));",
            DriverExtensions.GetElement(ContentTypeContainerLocator, SelectAClientIDTextBoxLocator)));
            a.Clear();
        }

        /// <summary>
        /// AddNewClientTextBox
        /// </summary>
        public ITextbox AddNewClientTextBox => new Textbox((IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{AddNewClientTextBoxMsk}'));",
            DriverExtensions.GetElement(ContentTypeContainerLocator, AddNewClientTextBoxLocator)));

        /// <summary>
        /// ClientAlreadyExistMessag
        /// </summary>
        public ILabel ClientAlreadyExistMessag => new Label((IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{ClientAlreadyExistMessageMsk}'));",
            DriverExtensions.GetElement(ContentTypeContainerLocator, AddNewClientTextBoxLocator)));

        /// <summary>
        /// SaveNewClientIdButton
        /// </summary>
        public IButton SaveNewClientIdButton => new Button(ContentTypeContainerLocator, SaveNewClientIdButtonLocator);

        /// <summary>
        /// RecentClientButton
        /// </summary>
        public IReadOnlyCollection<IButton> RecentClientButton => new ElementsCollection<Button>(ContentTypeContainerLocator, RecentClientsItemsLocator);        
    }
}
