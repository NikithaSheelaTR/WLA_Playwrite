namespace Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Layout And Limits Tab Component
    /// </summary>
    public class LayoutAndLimitsTabComponent : BaseTabComponent
    {
        private static readonly By AllCheckboxesLocator =
            By.XPath("//div[@class='co_delivery_layoutTab']//input[@type='checkbox']");

        private static readonly By DualColumnLayoutForCasesCheckboxLocator =
            By.Id("coid_chkDdcLayoutUseDualColumnsForCases");

        private static readonly By FootnotesDropdownLocator = By.Id("co_delivery_footnotes");

        private static readonly By ContainerLocator = By.Id("co_deliveryOptionsTabPanel2");

        private static readonly EnumPropertyMapper<LayoutAndLimitsFootnotes, WebElementInfo> LayoutAndLimitsFootnotesMap
            = EnumPropertyModelCache.GetMap<LayoutAndLimitsFootnotes, WebElementInfo>();

        private static readonly EnumPropertyMapper<LayoutAndLimitsInclude, WebElementInfo> LayoutAndLimitsIncludeMap =
            EnumPropertyModelCache.GetMap<LayoutAndLimitsInclude, WebElementInfo>();

        private static readonly EnumPropertyMapper<LayoutAndLimitsPageRanges, WebElementInfo> LayoutAndLimitsPageRangesMap = EnumPropertyModelCache.GetMap<LayoutAndLimitsPageRanges, WebElementInfo>();

        private static readonly EnumPropertyMapper<LayoutAndLimitsTabOption, WebElementInfo> LayoutAndLimitsTabOptionMap
            = EnumPropertyModelCache.GetMap<LayoutAndLimitsTabOption, WebElementInfo>();

        private static readonly By StarPagesInputLocator = By.Id("co_delivery_StarPagesRanges");

        private static readonly By UnderlineCheckboxLocator = By.Id("co_delivery_linkUnderline");

        private static readonly By QuotationHighlightsCheckboxLocator = By.Id("coid_chkDdcLayoutQuotationHighlights");

        private static readonly By CoverPageCommentLocator = By.XPath(".//textarea[@id = 'coid_DdcLayoutCoverPageComment']");

        private static readonly By LegalSimilarityCheckboxLocator = By.Id("coid_chkDdcLayoutLegalSimilarity");

        private static readonly By CoverPageCheckBoxLocator = By.Id("coid_chkDdcLayoutCoverPage");

        private static readonly By DeliveryFullTextDecisionLabelLocator = By.Id("coid_deliveryFullTextDecisionOnly");

        private static readonly By TermHighlightingCheckboxLocator = By.XPath(".//label[@for='coid_chkDdcLayoutTermHighlighting']/input");

        private static readonly By LinkToPdfCheckboxLocator = By.XPath(".//label[@for='coid_chkDdcLayoutOriginalImageLink']/input");

        private static readonly By ExpandedMarginForNotesCheckboxLocator = By.XPath(".//label[@for='coid_chkDdcLayoutRightNoteMarging']/input");

        private static readonly By HeadNoteCheckboxLocator = By.CssSelector("[for='coid_chkDdcLayoutIncludeHeadnotes']");

        private static readonly By TableOfAuthoritiesCheckboxLocator = By.XPath(".//input[@name='coid_chkDdcLayoutTableOfAuthorities']");

        private static readonly By CanadianAbridgmentCheckboxLocator = By.XPath(".//label[@for='coid_chkDdcLayoutAbridgmentClassification']/input");

        private static readonly By FullTextDesicionCheckboxLocator = By.XPath(".//label[@for='coid_chkDdcLayoutExcludeOtherFullText']/input");

        private static readonly By AiSummaryCheckboxLocator = By.XPath(".//label[@for='coid_chkDdcLayoutAISummary']/input");

        private static readonly By AiRelevancyOverviewCheckboxLocator = By.XPath(".//label[@for ='coid_chkDdcLayoutWestSearchRelevancyExplainer']/input");

        /// <summary>
        /// Font Size DropDown
        /// </summary>
        public IDropdown<LayoutAndLimitsFontSizeOptions> FontSizeDropdown { get; } = new FontSizeDropdown();

        /// <summary>
        /// Delivery DropDown
        /// </summary>
        public IDropdown<LayoutAndLimitsLinksOptions> LinksDropdown { get; } = new LinksDropdown();

        /// <summary>
        /// Quick check and Judicial Aditional features
        /// </summary>
        public AdditionalFeaturesDeliveryComponent AdditionalFeaturesComponent => new AdditionalFeaturesDeliveryComponent();

        /// <summary> 
        /// Quotation Highlights checkbox
        /// </summary>
        public ICheckBox QuotationHighlightsCheckbox => new CheckBox(this.ComponentLocator, QuotationHighlightsCheckboxLocator);

        /// <summary>
        /// Canadian Abridgment Checkbox
        /// </summary>
        public ICheckBox CanadianAbridgmentCheckbox => new CheckBox(this.ComponentLocator, CanadianAbridgmentCheckboxLocator);

        /// <summary>
        /// Canadian Abridgment Checkbox
        /// </summary>
        public ICheckBox AiSummaryCheckbox => new CheckBox(this.ComponentLocator, AiSummaryCheckboxLocator);

        /// <summary>
        /// AI Relevancy Overview Checkbox
        /// </summary>
        public ICheckBox AiRelevancyOverviewCheckbox => new CheckBox(this.ComponentLocator, AiRelevancyOverviewCheckboxLocator);

        /// <summary>
        /// Cover page comment textbox
        /// </summary>
        public ITextbox CoverPageComment => new Textbox(this.ComponentLocator, CoverPageCommentLocator);

        /// <summary>
        /// Cover page checkbox
        /// </summary>
        public ICheckBox CoverPageCheckBox => new CheckBox(this.ComponentLocator, CoverPageCheckBoxLocator);

        /// <summary>
        /// Delivery Full Text Decision label
        /// </summary>
        public ILabel DeliveryFullTextDecisionLabel => new Label(this.ComponentLocator, DeliveryFullTextDecisionLabelLocator);

        /// <summary>
        /// Dual Column For Cases Checkbox
        /// </summary>
        public ICheckBox DualColumnForCasesCheckbox => new CheckBox(this.ComponentLocator, DualColumnLayoutForCasesCheckboxLocator);

        /// <summary>
        /// Expanded Margin For Notes Checkbox
        /// </summary>
        public ICheckBox ExpandedMarginForNotesCheckbox => new CheckBox(this.ComponentLocator, ExpandedMarginForNotesCheckboxLocator);

        /// <summary>
        /// Full Text Desicion Checkbox
        /// </summary>
        public ICheckBox FullTextDesicionCheckbox => new CheckBox(this.ComponentLocator, FullTextDesicionCheckboxLocator);

        /// <summary>
        /// Head Note Checkbox
        /// </summary>
        public ICheckBox HeadNoteCheckbox => new CheckBox(this.ComponentLocator, HeadNoteCheckboxLocator);

        /// <summary> 
        /// Legal similarity checkbox
        /// </summary>
        public ICheckBox LegalSimilarityCheckbox => new CheckBox(this.ComponentLocator, LegalSimilarityCheckboxLocator);

        /// <summary>
        /// Link To Pdf Checkbox
        /// </summary>
        public ICheckBox LinkToPdfCheckbox => new CheckBox(this.ComponentLocator, LinkToPdfCheckboxLocator);

        /// <summary>
        /// Table Of Authorities Checkbox
        /// </summary>
        public ICheckBox TableOfAuthoritiesCheckbox => new CheckBox(this.ComponentLocator, TableOfAuthoritiesCheckboxLocator);

        /// <summary>
        /// Term Highlighting Checkbox
        /// </summary>
        public ICheckBox TermHighlightingCheckbox => new CheckBox(this.ComponentLocator, TermHighlightingCheckboxLocator);

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Layout and Limits";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks inside Star Page text box
        /// </summary>
        public void ClickStarPagesTextBox() => DriverExtensions.WaitForElementDisplayed(StarPagesInputLocator).Click();

        /// <summary>
        /// Enter a text into Star Pages text box
        /// </summary>
        /// <param name="pageRangeString"> Text to enter into Star Pages text box </param>
        public void EnterPageRanges(string pageRangeString) => DriverExtensions.SetTextField(pageRangeString, StarPagesInputLocator);

        /// <summary>
        /// Verify the specified Include option on the Layout and Limits tab is displayed
        /// </summary>
        /// <param name="expectedTabOption"> The option to look for </param>
        /// <returns> True if option displayed, false otherwise </returns>
        public bool IsIncludeSectionOptionDisplayed(LayoutAndLimitsInclude expectedTabOption)
            => DriverExtensions.IsDisplayed(By.XPath(LayoutAndLimitsIncludeMap[expectedTabOption].LocatorString));

        /// <summary>
        /// Verify the specified Include option on the Layout and Limits tab is selected
        /// </summary>
        /// <param name="expectedTabOption"> Include option on the Layout and Limits tab </param>
        /// <returns> True if option selected, false otherwise </returns>
        public bool IsIncludeSectionOptionSelected(LayoutAndLimitsInclude expectedTabOption)
            => DriverExtensions.IsCheckboxSelected(By.XPath(LayoutAndLimitsIncludeMap[expectedTabOption].LocatorString));

        /// <summary>
        /// Verify Option Of Page Ranges is Displayed
        /// </summary>
        /// <param name="expectedTabOption"> Page Ranges Option </param>
        /// <returns> True if Page Range option is displayed, false otherwise </returns>
        public bool IsPageRangesOptionDisplayed(LayoutAndLimitsPageRanges expectedTabOption)
            => DriverExtensions.IsDisplayed(By.Id(LayoutAndLimitsPageRangesMap[expectedTabOption].Id));

        /// <summary>
        /// Verify Option Of Page Ranges is Selected
        /// </summary>
        /// <param name="expectedTabOption"> Page Ranges Option </param>
        /// <returns> True if Page Range option is selected, false otherwise </returns>
        public bool IsPageRangesOptionSelected(LayoutAndLimitsPageRanges expectedTabOption)
            => DriverExtensions.IsRadioButtonSelected(By.Id(LayoutAndLimitsPageRangesMap[expectedTabOption].Id));

        /// <summary>
        /// Checks for existence of a text box input for Star Pages.
        /// </summary>
        /// <returns> True if Star Pages Textbox is displayed, false otherwise</returns>
        public bool IsStarPagesTextBoxDisplayed() => DriverExtensions.IsDisplayed(StarPagesInputLocator);

        /// <summary>
        /// Verify specified option on the Layout and Limits tab is displaye
        /// </summary>
        /// <param name="expectedTabOption"> The option to look for </param>
        /// <returns> True if tab option is displayed, false otherwise </returns>
        public bool IsTabOptionDisplayed(LayoutAndLimitsTabOption expectedTabOption)
            => DriverExtensions.IsDisplayed(By.Id(LayoutAndLimitsTabOptionMap[expectedTabOption].Id));

        /// <summary>
        /// Verify specified option on the Layout and Limits tab is presented
        /// </summary>
        /// <param name="expectedTabOption"> The option to look for </param>
        /// <returns> True if tab option is displayed, false otherwise </returns>
        /// IsDisplayed is not working using the IsElementPresent
        public bool IsTabOptionPresent(LayoutAndLimitsTabOption expectedTabOption)
           => DriverExtensions.IsElementPresent(By.Id(LayoutAndLimitsTabOptionMap[expectedTabOption].Id));
        /// <summary>
        /// Verify that 'Underline' checkbox is selected
        /// </summary>
        /// <returns> True if 'Underline' checkbox is checked, false otherwise</returns>
        public bool IsUnderlineCheckboxChecked() => DriverExtensions.IsCheckboxSelected(UnderlineCheckboxLocator);

        /// <summary>
        /// Select all checkboxes of the Layouts and Limits tab
        /// </summary>
        public void SelectAllCheckboxes()
            => DriverExtensions.GetElements(AllCheckboxesLocator).ToList().ForEach(elem => elem.SetCheckbox(true));

        /// <summary>
        /// Select one of the Footnotes Options
        /// </summary>
        /// <param name="expectedTabOption"> Footnotes Option</param>
        public void SelectFootnotesOption(LayoutAndLimitsFootnotes expectedTabOption)
            => DriverExtensions.SetDropdown(LayoutAndLimitsFootnotesMap[expectedTabOption].Text, FootnotesDropdownLocator);

        /// <summary>
        /// Select Include option on the Layout and Limits tab
        /// </summary>
        /// <param name="expectedTabOption"> Include option on the Layout and Limits tab </param>
        /// <param name="setTo"> The set To. </param>
        public void SetIncludeSectionOption(LayoutAndLimitsInclude expectedTabOption, bool setTo = true)
            => DriverExtensions.SetCheckbox(By.XPath(LayoutAndLimitsIncludeMap[expectedTabOption].LocatorString), setTo);

        /// <summary>
        /// Set Page Ranges Option
        /// </summary>
        /// <param name="expectedTabOption">expected Tab Option</param>
        public void SetPageRangesOption(LayoutAndLimitsPageRanges expectedTabOption)
            => DriverExtensions.GetElement(By.Id(LayoutAndLimitsPageRangesMap[expectedTabOption].Id)).Click();

        /// <summary>
        /// Select Underline Checkbox 
        /// </summary>
        /// <param name="setTo"> The set To </param>
        public void SetUnderlineCheckbox(bool setTo = true)
            => DriverExtensions.SetCheckbox(UnderlineCheckboxLocator, setTo);

        /// <summary>
        /// Unselects all checkboxes of the Layouts and Limits tab
        /// </summary>
        public void UnselectAllCheckboxes()
            => DriverExtensions.GetElements(AllCheckboxesLocator).ToList().ForEach(elem => elem.SetCheckbox(false));

        /// <summary>
        /// Get Option Text
        /// </summary>
        /// <param name="expectedTabOption">expectedTabOption</param>
        /// <returns>option text</returns>
        public string GetOptionText(LayoutAndLimitsInclude expectedTabOption)
            => DriverExtensions.GetText(By.XPath(LayoutAndLimitsIncludeMap[expectedTabOption].LocatorString));
    }
}