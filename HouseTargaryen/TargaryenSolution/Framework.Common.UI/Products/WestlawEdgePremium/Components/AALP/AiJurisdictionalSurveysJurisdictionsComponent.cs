namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// AI Jurisdictional Surveys Jurisdiction component
    /// </summary>
    public class AiJurisdictionalSurveysJurisdictionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By JurisdictionsContainerLocator = By.XPath("//div[contains(@class, 'jurisdictionCard')]");
        private static readonly By ClearJurisdictionsButtonLocator = By.XPath(".//saf-button[contains(text(),'Clear selections')]");
        private static readonly By SelectAllAlertLabelLocator = By.XPath(".//saf-alert[contains(text(),'Selecting a large number of jurisdictions may result in a long wait time.')]");
        private static readonly By ClearJurisdictionsFilterButtonLocator = By.XPath(".//saf-button[contains(text(),'Clear Filters')]");
        private static readonly By SelectedCountLabelLocator = By.XPath(".//span[contains(@class,'jurisdictionCardSelectedCount')]");

        private const string JurisdictionLctMask = "saf-checkbox[current-value='{0}']";
        private const string JurisdictionCheckboxScript = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";
        private const string JurisdictionIsDisabledScript = "return arguments[0].shadowRoot.querySelector('input[id=control]').getAttribute('aria-disabled');";
        private const string JurisdictionAriaCheckedScript = "return arguments[0].shadowRoot.querySelector('input[id=control]').getAttribute('aria-checked');";
       
        /// <summary>
        /// Select jurisdiction(s)
        /// </summary>
        /// <param name="jurisdictions">list of jurisdictions</param>
        public void SelectJurisdiction(params string[] jurisdictions)
        {
            SelectJurisdiction(true, jurisdictions);
        }

        /// <summary>
        /// Select jurisdiction(s) with option to bypass clearing all selected
        /// </summary>
        /// <param name="clearAllSelected">If true, clear jurisdictions before selecting</param>
        /// <param name="jurisdictions">list of jurisdictions</param>
        public void SelectJurisdiction(bool clearAllSelected, params string[] jurisdictions)
        {
            if (clearAllSelected)
            {
                this.ClearJurisdictionsButton.Click();
            }

            foreach (string jurisdiction in jurisdictions.ToList())
            {
                IWebElement jurisdictionElement = DriverExtensions.WaitForElement(By.CssSelector(string.Format(JurisdictionLctMask, jurisdiction)));
                IWebElement jurisdictionCheckbox = (IWebElement)DriverExtensions.ExecuteScript(JurisdictionCheckboxScript, jurisdictionElement);
                jurisdictionCheckbox.Click();
            }
        }

        /// <summary>
        /// Clear Jurisdictions button
        /// </summary>
        public IButton ClearJurisdictionsButton => new Button(ClearJurisdictionsButtonLocator);

        /// <summary>
        /// Select All alert label
        /// </summary>
        public ILabel SelectAllAlertLabel => new Label(SelectAllAlertLabelLocator);

        /// <summary>
        /// Selected juris count label
        /// </summary>
        public ILabel SelectedCountLabel => new Label(SelectedCountLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => JurisdictionsContainerLocator;

        /// <summary>
        /// Filter results by selecting specific jurisdictions from the filter panel on results page
        /// </summary>
        /// <param name="jurisdictionNames">List of jurisdiction names to filter by</param>
        public void FilterJurisdictionsByName(params string[] jurisdictionNames)
        {
            this.ClearJurisdictionsFilterButton.Click();
            // Select each specified jurisdiction from the filter panel
            foreach (string jurisdiction in jurisdictionNames.ToList())
            {
                IWebElement jurisdictionElement = DriverExtensions.WaitForElement(
                    By.CssSelector(string.Format(JurisdictionLctMask, jurisdiction)));

                IWebElement jurisdictionCheckbox = (IWebElement)DriverExtensions.ExecuteScript(
                    JurisdictionCheckboxScript,
                    jurisdictionElement);

                // Only click if not already selected
                if (!jurisdictionCheckbox.Selected)
                {
                    jurisdictionCheckbox.Click();                    
                }
            }
        }

        /// <summary>
        /// Clear Filter button
        /// </summary>
        public IButton ClearJurisdictionsFilterButton => new Button(ClearJurisdictionsFilterButtonLocator);

        /// <summary>
        /// Check if a jurisdiction checkbox is disabled
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction current-value</param>
        /// <returns>True if disabled, else false</returns>
        public bool IsJurisdictionSelectionDisabled(string jurisdiction)
        {
            IWebElement jurisdictionElement = DriverExtensions.WaitForElement(By.CssSelector(string.Format(JurisdictionLctMask, jurisdiction)));
            var result = DriverExtensions.ExecuteScript(JurisdictionIsDisabledScript, jurisdictionElement);
            var ariaDisabled = result?.ToString();
            return ariaDisabled != null && ariaDisabled.Contains("true");
        }

        /// <summary>
        /// Check if a jurisdiction is selected
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction current-value</param>
        /// <returns>True if selected, else false</returns>
        public bool IsJurisdictionSelected(string jurisdiction)
        {
            IWebElement jurisdictionElement = DriverExtensions.WaitForElement(By.CssSelector(string.Format(JurisdictionLctMask, jurisdiction)));
            var result = DriverExtensions.ExecuteScript(JurisdictionAriaCheckedScript, jurisdictionElement);
            var ariaChecked = result?.ToString();
            return ariaChecked != null && ariaChecked.Contains("true");
        }

        /// <summary>
        /// Polls until the selected count label contains the expected text.
        /// Use this after clicking a jurisdiction to avoid reading stale label text.
        /// </summary>
        public void WaitForSelectedCountToContain(string expectedText, int timeoutFromSec = 10)
        {
            SafeMethodExecutor.WaitUntil(
                () => SelectedCountLabel.Text.Contains(expectedText),
                timeoutFromSec: timeoutFromSec);
        }

        /// <summary>
        /// Polls until the jurisdiction checkbox reports disabled.
        /// Use before asserting IsJurisdictionSelectionDisabled after a click.
        /// </summary>
        public void WaitForJurisdictionDisabled(string jurisdiction, int timeoutFromSec = 10)
        {
            SafeMethodExecutor.WaitUntil(
                () => IsJurisdictionSelectionDisabled(jurisdiction),
                timeoutFromSec: timeoutFromSec);
        }

        /// <summary>
        /// Polls until the jurisdiction checkbox is no longer disabled.
        /// Use before asserting !IsJurisdictionSelectionDisabled after a click.
        /// </summary>
        public void WaitForJurisdictionEnabled(string jurisdiction, int timeoutFromSec = 10)
        {
            SafeMethodExecutor.WaitUntil(
                () => !IsJurisdictionSelectionDisabled(jurisdiction),
                timeoutFromSec: timeoutFromSec);
        }

        /// <summary>
        /// Polls until the jurisdiction checkbox reports checked.
        /// Use before asserting IsJurisdictionSelected after a click.
        /// </summary>
        public void WaitForJurisdictionSelected(string jurisdiction, int timeoutFromSec = 10)
        {
            SafeMethodExecutor.WaitUntil(
                () => IsJurisdictionSelected(jurisdiction),
                timeoutFromSec: timeoutFromSec);
        }
    }
}
