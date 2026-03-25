namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System.Linq;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;

    /// <summary>
    /// Jurisidction dialog on AI Suggested Claims page
    /// </summary>
    public class AiJurisdictionDialog: EdgeJurisdictionOptionsDialog
    {
        private const string JurisdictionSelectionLctMask = ".//input[@title={0}] | .//input[@name={0}]";

        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'co_overlayBox_container')]|//saf-dialog[contains(@class,'__jurisdictionModal') and not(@hidden)]");

        private static readonly By SaveButtonLocator = By.XPath(".//button[@class='co_primaryBtn']|.//saf-button[@appearance='primary']");

        private static readonly By JurisdictionCheckboxesLocator = By.XPath(".//input[contains(@type, 'checkbox')]|.//saf-checkbox[@name='jurisdictions']");

        private const string ClaimsExplorerJurisdictionLctMask = "saf-checkbox[id='{0}']";
        private const string ClaimsExplorereCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";


        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsMap;

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> JurisdictionsMap => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();

        /// <summary>
        /// Save button
        /// </summary>
        public new IButton SaveButton => new Button(this.ComponentLocator, SaveButtonLocator);
       
        /// <summary>
        /// Clears all selected checkboxes
        /// </summary>
        public new void ClearAllOtherJurisdictions() => DriverExtensions.GetElements(this.ComponentLocator, JurisdictionCheckboxesLocator).ToList().ForEach(checkbox => checkbox.SetCheckbox(false));

        /// <summary>
        /// Verifies that Jurisdiction checkbox displayed
        /// </summary>
        /// <param name="jurisdiction"> the jurisdiction to get the checkbox element for </param>
        /// <returns> True if Military Courts Checkbox is present </returns>
        public new bool IsJurisdictionCheckboxDisplayed(Jurisdiction jurisdiction) => DriverExtensions.IsDisplayed(new ByChained(this.ComponentLocator, SafeXpath.BySafeXpath(JurisdictionSelectionLctMask, this.JurisdictionsMap[jurisdiction].JurisdictionName)));

        /// <summary>
        /// Selects the jurisdictions
        /// Important: not more than 3 jurisdiction
        /// </summary>
        /// <param name="clearAllOtherJurisdictions">
        /// True to clear the other jurisdictions other than what is passed in, false to leave them there
        /// </param>
        /// <param name="jurisdictions">List of jurisdictions to select</param>
        /// <returns>The <see cref="AiJurisdictionDialog"/>.</returns>
        public new AiJurisdictionDialog SelectJurisdictions(bool clearAllOtherJurisdictions, params Jurisdiction[] jurisdictions)
        {
            if (clearAllOtherJurisdictions)
            {
                this.ClearAllOtherJurisdictions();
            }

            foreach (Jurisdiction jurisdiction in jurisdictions.ToList())
            {              
                By checkboxLocator = new ByChained(this.ComponentLocator, SafeXpath.BySafeXpath(JurisdictionSelectionLctMask, this.JurisdictionsMap[jurisdiction].JurisdictionName));
                DriverExtensions.WaitForElement(checkboxLocator).SetCheckbox(true);
            }

            return this;
        }

        /// <summary>
        /// Clears all selected checkboxes for claims explorer
        /// </summary>
        public void ClearAllOtherJurisdictionsClaimsExplorer()
        {
            var checkboxElements = DriverExtensions.GetElements(this.ComponentLocator, JurisdictionCheckboxesLocator).ToList();

            foreach (IWebElement safCheckbox in checkboxElements)
            {
                // Get the input element from shadow root using JavaScript
                IWebElement shadowInput = (IWebElement)DriverExtensions.ExecuteScript(ClaimsExplorereCheckboxScript,safCheckbox);

                // Check if checkbox is currently selected
                bool isChecked = (bool)DriverExtensions.ExecuteScript( "return arguments[0].checked;",shadowInput);

                // Only click if it's currently checked (to uncheck it)
                if (isChecked)
                {
                    shadowInput.Click();
                }
            }
        }

        /// <summary>
        /// Selects the jurisdictions for claims explorer
        /// Important: not more than 3 jurisdiction
        /// </summary>
        /// <param name="clearAllOtherJurisdictions">
        /// True to clear the other jurisdictions other than what is passed in, false to leave them there
        /// </param>
        /// <param name="jurisdictions">List of jurisdictions to select</param>
        /// <returns>The <see cref="AiJurisdictionDialog"/>.</returns>
        public AiJurisdictionDialog SelectJurisdictionsClaimsExplorer(bool clearAllOtherJurisdictions, params Jurisdiction[] jurisdictions)
        {
            if (clearAllOtherJurisdictions)
            {
                this.ClearAllOtherJurisdictionsClaimsExplorer();
            }

            foreach (Jurisdiction jurisdiction in jurisdictions.ToList())
            {
                string jurisdictionName = this.JurisdictionsMap[jurisdiction].JurisdictionName;

                // Build locator for saf-checkbox - try both id and title attributes
                By checkboxLocator = By.CssSelector(string.Format(ClaimsExplorerJurisdictionLctMask, jurisdictionName));

                // Wait for and get the saf-checkbox element
                IWebElement safCheckbox = DriverExtensions.WaitForElement(new ByChained(this.ComponentLocator, checkboxLocator));

                // Get the input element from shadow root using the ClaimsExplorereCheckboxScript
                IWebElement shadowInput = (IWebElement)DriverExtensions.ExecuteScript( ClaimsExplorereCheckboxScript,safCheckbox);

                shadowInput.Click();
            }

            return this;
        }

        /// <summary>
        /// Clicks the Save button for Claims Explorer jurisdiction dialog (handles shadow root)
        /// </summary>
        public void ClickSaveButtonClaimsExplorer()
        {
            // Find the saf-button element using the SaveButtonLocator
            IWebElement saveButtonElement = DriverExtensions.WaitForElement(new ByChained(this.ComponentLocator, SaveButtonLocator));

            // Use JavaScript to click the button inside the shadow root
            DriverExtensions.ExecuteScript(
                "var button = arguments[0].shadowRoot.querySelector('button[class=\"control\"]');button.click();",saveButtonElement);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}
