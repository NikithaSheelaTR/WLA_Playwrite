namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NewSearchTemplate
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Precision Search by attribute component
    /// </summary>
    public class PrecisionSearchByAttributeTabComponent : BaseTabComponent
    {
        private static readonly By AddButtonLocator = By.XPath(".//*[@class='Button--small Button-secondary']");
        private static readonly By AreaOfLawButtonLocator = By.XPath(".//button[@id='coid_AreaOfLawDropdown']");
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel_advancedSearch']");
        private static readonly By ChangeJurisdictionButtonLocator = By.XPath(".//div[@class='PrecisionSearchModal-zeroState']//button");
        private static readonly By ConnectorsInfoAreaLocator = By.XPath(".//*[contains(@class, 'SearchFacetSearchWithinHelp-body')]");
        private static readonly By CauseOfActionTextboxLocator = By.XPath(".//*[@id='coid_athensCauseOfAction']");
        private static readonly By ExpandConnectorsButtonLocator = By.XPath(".//*[contains(@class, 'SearchFacet-buttonLink')]");
        private static readonly By FactPatternTextboxLocator = By.XPath(".//*[@id='coid_athensFactPattern']");
        private static readonly By GoverningLawTextboxLocator = By.XPath(".//*[@id='coid_athensGoverningLaw']");
        private static readonly By IndustryTypeButtonLocator = By.XPath(".//button[@id='coid_IndustryTypeDropdown']");
        private static readonly By InfoIconLocator = By.XPath(".//div[contains(@class,'contentSearchInfo')]//button[@class='co_scopeIcon'] | .//div[@class='PrecisionSearchModal-multiSelectInfo']//button");
        private static readonly By InfoBoxMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");
        private static readonly By LegalIssueTextboxLocator = By.XPath(".//*[@id='coid_athensLegalIssue']");
        private static readonly By MotionTypeTextboxLocator = By.XPath(".//*[@id='coid_athensMotionType']");
        private static readonly By MaterialFactsTextboxLocator = By.XPath(".//*[@id='coid_athensMaterialFacts']");
        private static readonly By NoResultsForJurisdictionMessageLocator = By.XPath(".//div[@class='PrecisionSearchModal-zeroState']");
        private static readonly By PartyTypeTextboxLocator = By.XPath(".//*[@id='coid_athensPartyType']");
        private static readonly By TabNameLocator = By.XPath("//*[@id='tab_advancedSearch']");
        private static readonly By TextboxLabelLocator = By.XPath(".//label[@class='Typeahead-label'] | .//div[@class='PrecisionSearchModal-multiSelectHeader']");
        private static readonly By TypeaheadDialogLocator = By.XPath(".//div[@class='Typeahead-wrapper']");
        private static readonly By WarningMessageLabelLocator = By.XPath(".//div[@class='co_infoBox_message']");        
        private static readonly By XClearButtonLocator = By.XPath("./following-sibling::*[@class='Typeahead-clearButton']");
        
        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(DriverExtensions.GetElement(this.ComponentLocator, InfoBoxMessageLocator));

        /// <summary>
        /// Message when there are no results for the selected jurisdiction
        /// </summary>
        public ILabel NoResultsForJurisdictionMessageLabel => new Label(this.ComponentLocator, NoResultsForJurisdictionMessageLocator);

        /// <summary>
        /// Tab name label
        /// </summary>
        public ILabel TabNameLabel => new Label(TabNameLocator);

        /// <summary>
        /// Textbox labels
        /// </summary>
        public IReadOnlyCollection<ILabel> TextboxLabels => new ElementsCollection<Label>(this.ComponentLocator, TextboxLabelLocator);

        /// <summary>
        /// Change jurisdiction button
        /// </summary>
        public IButton ChangeJurisdictionButton => new Button(this.ComponentLocator, ChangeJurisdictionButtonLocator);

        /// <summary>
        /// Add material facts button 
        /// </summary>
        public IButton AddMaterialFactsButton => new Button(this.ComponentLocator, AddButtonLocator);

        /// <summary>
        /// Expand connectors and expanders button 
        /// </summary>
        public IButton ExpandConnectorsButton => new Button(this.ComponentLocator, ExpandConnectorsButtonLocator);
        
        /// <summary>
        /// Warning message label
        /// </summary>
        public IButton WarningMessageLabel => new Button(this.ComponentLocator, WarningMessageLabelLocator);

        /// <summary>
        /// Area of law list button
        /// </summary>
        public IButton AreaOfLawButton => new Button(this.ComponentLocator, AreaOfLawButtonLocator);

        /// <summary>
        /// Industry type button
        /// </summary>
        public IButton IndustryTypeButton => new Button(this.ComponentLocator, IndustryTypeButtonLocator);

        /// <summary>
        /// Info icon buttons
        /// </summary>
        public IReadOnlyCollection<IButton> InfoIconButtons => new ElementsCollection<Button>(this.ComponentLocator, InfoIconLocator);

        /// <summary>
        /// Legal issue textbox
        /// </summary>
        public ITextbox LegalIssueTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, LegalIssueTextboxLocator), XClearButtonLocator);
        
        /// <summary>
        /// Fact pattern textbox
        /// </summary>
        public ITextbox FactPatternTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, FactPatternTextboxLocator), XClearButtonLocator);
        
        /// <summary>
        /// Cause of action textbox
        /// </summary>
        public ITextbox CauseOfActionTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, CauseOfActionTextboxLocator), XClearButtonLocator);
        
        /// <summary>
        /// Motion type textbox
        /// </summary>
        public ITextbox MotionTypeTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, MotionTypeTextboxLocator), XClearButtonLocator);

        /// <summary>
        ///Governing law textbox
        /// </summary>
        public ITextbox GoverningLawTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, GoverningLawTextboxLocator), XClearButtonLocator);

        /// <summary>
        /// Party type textbox
        /// </summary>
        public ITextbox PartyTypeTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, PartyTypeTextboxLocator), XClearButtonLocator);
        
        /// <summary>
        /// Material facts textbox
        /// </summary>
        public ITextbox MaterialFactsTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, MaterialFactsTextboxLocator), XClearButtonLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => TabNameLabel.Text;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Check is connectors and expanders info displayed
        /// </summary>
        /// <returns>
        /// True if the connectors and expanders info is displayed
        /// </returns>
        public bool IsConnectorsInfoDisplayed() =>
            DriverExtensions.WaitForElement(ConnectorsInfoAreaLocator).GetAttribute("aria-hidden").Equals("false");

        /// <summary>
        /// Verify that typeahead dialog is displayed
        /// </summary>
        /// <returns> True if typeahead dialog is displayed, false otherwise </returns>
        public bool IsTypeaheadDialogDisplayed() => DriverExtensions.IsDisplayed(new ByChained(this.ComponentLocator, TypeaheadDialogLocator));
    }
}
