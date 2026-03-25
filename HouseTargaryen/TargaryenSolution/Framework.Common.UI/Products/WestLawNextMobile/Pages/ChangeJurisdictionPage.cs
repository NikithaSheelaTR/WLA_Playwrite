namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Change jurisdiction page
    /// </summary>
    public class ChangeJurisdictionPage : MobileBasePage
    {
        private static readonly By CancelButtonLocator = By.Id("coid_website_cancelButton");

        private static readonly By IncludeFederalCheckboxLocator = By.Id("includeRelevantFederalCheckBox");

        private static readonly By SaveButtonLocator = By.Id("coid_website_changeJurisdictionButton");

        private static readonly By ValidationMessageLocator = By.Id("co_mobile_selectJurisdictionsValidationSummary");

        private static readonly By FederalDropdownLocator = By.Id("federalJurisdictionDropDown");

        private static readonly By StateDropdownLocator = By.Id("stateJurisdictionDropDown");

        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsMap;

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        protected EnumPropertyMapper<Jurisdiction, JurisdictionInfo> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();

        private SelectElement FederalDropdown
            => new SelectElement(DriverExtensions.WaitForElement(FederalDropdownLocator));

        private SelectElement StateDropdown
            => new SelectElement(DriverExtensions.WaitForElement(StateDropdownLocator));

        /// <summary>
        /// Click on the 'Cancel' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the 'Save' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the validation message on the page if there is one
        /// </summary>
        /// <returns>Validation message</returns>
        public string GetValidationMessage() => DriverExtensions.GetText(ValidationMessageLocator);

        /// <summary>
        /// Determines if a jurisdiction is in the list
        /// </summary>
        /// <param name="jurisdiction">Jurisdiction to look for</param>
        /// <returns>True if the jurisdiction is present, false otherwise</returns>
        public bool IsJurisdictionDisplayed(Jurisdiction jurisdiction)
        {
            string jurs = this.JurisdictionsMap[jurisdiction].JurisdictionName;
            return this.IsItemInExistList(this.StateDropdown, jurs)
                   || this.IsItemInExistList(this.FederalDropdown, jurs);
        }

        /// <summary>
        /// Selects two jurisdictions
        /// </summary>
        /// <param name="state">State jurisdiction to select</param>
        /// <param name="federal">Federal jurisdiction to select</param>
        public void SelectStateAndFederal(Jurisdiction state, Jurisdiction federal)
        {
            this.SelectState(state);
            this.SelectFederal(federal);
        }

        /// <summary>
        /// Selects a given state jurisdiction
        /// </summary>
        /// <param name="jurisdiction">Jurisdiction to select</param>
        public void SelectState(Jurisdiction jurisdiction) => this.SelectJurisdiction(jurisdiction, this.StateDropdown);

        /// <summary>
        /// Selects a given federal jurisdiction
        /// </summary>
        /// <param name="jurisdiction">Jurisdiction to select</param>
        public void SelectFederal(Jurisdiction jurisdiction) => this.SelectJurisdiction(jurisdiction, this.FederalDropdown);

        /// <summary>
        /// Set IncludeFederal checkbox
        /// </summary>
        /// <param name="isSet"> Set if true, unset otherwise </param>
        public void SetIncludeFederalCheckbox(bool isSet = true)
            => DriverExtensions.SetCheckbox(IncludeFederalCheckboxLocator, isSet);

        private bool IsItemInExistList(SelectElement dropdown, string text)
            => dropdown.Options.Any(e => e.Text.Contains(text));

        private void SelectJurisdiction(Jurisdiction jurisdiction, SelectElement dropdown)
        {
            string value = this.JurisdictionsMap[jurisdiction].JurisdictionName;
            if (dropdown.Options.Any(element => element.Text.Trim() == value))
            {
                dropdown.SelectByText(value);
            }
        }
    }
}