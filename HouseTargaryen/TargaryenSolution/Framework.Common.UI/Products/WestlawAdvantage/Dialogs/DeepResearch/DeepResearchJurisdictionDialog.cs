namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Deep Research Jurisidction dialog
    /// </summary>
    public class DeepResearchJurisdictionDialog : EdgeJurisdictionOptionsDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'JurisdictionDialog-module__dialog')]");
        private static readonly By SaveButtonLocator = By.XPath(".//saf-button-v3[@data-testid='jurisdiction-dialog-confirm']");
        private static readonly By CancelButtonLocator = By.XPath(".//saf-button-v3[@data-testid='jurisdiction-dialog-cancel']");
        private static readonly By ClearAllButtonLocator = By.XPath(".//saf-button-v3[@data-testid='jurisdictions-dialog-clear-all']");
        private const string JurisdictionLctMask = "saf-checkbox-v3[current-value='{0}']";
        private const string JurisdictionCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";

        /// <summary>
        /// Save button
        /// </summary>
        public new IButton SaveButton => new Button(this.ComponentLocator, SaveButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public new IButton CancelButton => new Button(this.ComponentLocator, CancelButtonLocator);

        /// <summary>
        /// Clear all button
        /// </summary>
        public new IButton ClearAllButton => new Button(this.ComponentLocator, ClearAllButtonLocator);

        /// <summary>
        /// Select jurisdiction(s)
        /// </summary>
        /// <param name="jurisdictions">list of jurisdictions</param>
        public void SelectJurisdiction(params string[] jurisdictions)
        {
            this.ClearAllButton.Click();

            foreach (string jurisdiction in jurisdictions.ToList())
            {
                IWebElement jurisdictionElement = DriverExtensions.GetElement(By.CssSelector(string.Format(JurisdictionLctMask, jurisdiction)));
                IWebElement jurisdictionCheckbox = (IWebElement)DriverExtensions.ExecuteScript(JurisdictionCheckboxScript, jurisdictionElement);
                jurisdictionCheckbox.Click();
            }
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}

