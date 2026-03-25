namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Contains all methods pertaining to the JurisdictionOptionsModal
    /// </summary>
    public class JurisdictionOptionsDialog : BaseModuleRegressionDialog
    {
        private const string JurisdictionSelectionLctMask = "//form[@id='co_jurisdictionSelectorForm']//*[.//*[contains(text(),{0})]]/input[@type='checkbox']";

        private static readonly By CancelButtonLocator = By.Id("co_jurisdictionCancel");

        private static readonly By CircuitHelpLocator = By.Id("co_jurisdictionCircuitCourtHelpLink");

        private static readonly By CloseButtonLocator = By.Id("co_jurisdictionCancelButton");

        private static readonly By ErrorMessageLocator = By.Id("co_jurisdictionErrorMessage");

        private static readonly By HeaderTitleLocator = By.ClassName("co_jurisdictionHeader");

        private static readonly By InfoMessageLocator = By.Id("co_jurisdictionInfoMsg");

        private static readonly By JurisdictionCheckboxesLocator = By.XPath("//form[@id='co_jurisdictionSelectorForm']//input[contains(@class, 'co_checkbox')]");

        private static readonly By PartyItemLocator = By.XPath(".//input");

        private static readonly By QuickCheckNoCitationsWarningLocator = By.XPath("//div[@id='co_additionalInfoContainer']");

        private static readonly By QuickCheckOpposingNoPartyWarningLocator = By.XPath("//div[@id='co_jurisdictionErrorMessage']");

        private static readonly By QuickCheckOpposingPartiesContainerLocator = By.XPath("//*[@class='JurisdictionDocumentAuthor']");

        private static readonly By RelatedFederalHelpLocator = By.Id("co_jurisdictionStateFederalHelpLink");

        private static readonly By SaveButtonLocator = By.Id("co_jurisdictionSave");

        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> jurisdictionsMap;

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        private EnumPropertyMapper<Jurisdiction, JurisdictionInfo> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, JurisdictionInfo>();
       
        /// <summary>
        /// Quick Check Jurisdiction selector no citations info label
        /// </summary>
        public ILabel QuickCheckNoCitationsLabel => new Label(QuickCheckNoCitationsWarningLocator);

        /// <summary>
        /// Quick Check Jurisdiction selector no party info label
        /// </summary>
        public ILabel QuickCheckNoPartyLabel => new Label(QuickCheckOpposingNoPartyWarningLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Error message label
        /// </summary>
        public ILabel ErrorMessageLabel => new Label(ErrorMessageLocator);

        /// <summary>
        /// Info message label
        /// </summary>
        public ILabel InfoMessageLabel => new Label(InfoMessageLocator);

        /// <summary>
        /// Header title label
        /// </summary>
        public ILabel HeaderTitleLabel => new Label(HeaderTitleLocator);

        /// <summary>
        /// Circuit help link
        /// </summary>
        public ILink CircuitHelpLink => new Link(CircuitHelpLocator);

        /// <summary>
        /// Related federal help link
        /// </summary>
        public ILink RelatedFederalHelpLink => new Link(RelatedFederalHelpLocator);

        /// <summary>
        /// Get list of Jurisdictions selector party radio buttons
        /// </summary>
        public IReadOnlyCollection<IRadiobutton> QuickCheckPartiesButtons => DriverExtensions
            .GetElements(QuickCheckOpposingPartiesContainerLocator, PartyItemLocator).Select(element => new Radiobutton(element)).ToList();

        /// <summary>
        /// Click the 'Select' button
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickSelectButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);

        /// <summary>
        /// Clears all selected checkboxes
        /// </summary>
        public void ClearAllOtherJurisdictions()
            => DriverExtensions.GetElements(JurisdictionCheckboxesLocator).ToList().ForEach(checkbox => checkbox.SetCheckbox(false));

        /// <summary>
        /// Verifies that Jurisdiction checkbox displayed
        /// </summary>
        /// <param name="jurisdiction"> the jurisdiction to get the checkbox element for </param>
        /// <returns> True if Military Courts Checkbox is present </returns>
        public bool IsJurisdictionCheckboxDisplayed(Jurisdiction jurisdiction)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(JurisdictionSelectionLctMask, this.JurisdictionsMap[jurisdiction].JurisdictionName));

        /// <summary>
        /// Selects the jurisdictions
        /// Important: not more than 3 jurisdiction
        /// </summary>
        /// <param name="clearAllOtherJurisdictions">
        /// True to clear the other jurisdictions other than what is passed in, false to leave them there
        /// </param>
        /// <param name="jurisdictions">List of jurisdictions to select</param>
        /// <returns>The <see cref="JurisdictionOptionsDialog"/>.</returns>
        public JurisdictionOptionsDialog SelectJurisdictions(bool clearAllOtherJurisdictions, params Jurisdiction[] jurisdictions)
        {
            if (clearAllOtherJurisdictions)
            {
                this.ClearAllOtherJurisdictions();
            }

            foreach (Jurisdiction jurisdiction in jurisdictions.ToList())
            {
                // it is needed because IncludeRelatedFederal have different text for Westlaw and Westlaw Edge
                By checkboxLocator = jurisdiction == Jurisdiction.IncludeRelatedFederal
                                         ? By.XPath(this.JurisdictionsMap[jurisdiction].LocatorString)
                                         : SafeXpath.BySafeXpath(
                                             JurisdictionSelectionLctMask,
                                             this.JurisdictionsMap[jurisdiction].JurisdictionName);
                DriverExtensions.WaitForElement(checkboxLocator).SetCheckbox(true);
            }

            return this;
        }

        /// <summary>
        /// Default Jurisdiction - All state, All federal
        /// </summary>
        /// <returns>The new instance of T page</returns>
        public JurisdictionOptionsDialog SelectDefaultJurisdiction() => this.SelectJurisdictions(
            true,
            Jurisdiction.AllStates,
            Jurisdiction.AllFederal);

        /// <summary>
        /// Count selected jurisdiction checkboxes
        /// </summary>
        /// <returns> The number of selected jurisdiction checkboxes </returns>
        public int GetSelectedJurisdictionsCount()
            => DriverExtensions.GetElements(JurisdictionCheckboxesLocator).Count(checkbox => checkbox.Selected);
    }
}