namespace Framework.Common.UI.Products.Shared.Components.Delivery
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// What To Deliver Component on the Basics/Recipients Tab inside delivery dialog
    /// </summary>
    public class WhatToDeliverComponent : BaseModuleRegressionComponent
    {
        private static readonly By OnlyPagesWithQuotationsCheckboxLocator = By.XPath(
            "//label[text()[normalize-space() = 'Only pages with quotations']]//input[@id='coid_chkDdcLayoutOnlyPagesWithSearchTerms']");

        private static readonly By OnlyPagesWithTermsCheckboxLocator = By.XPath(
            "//label[text()[normalize-space() = 'Only pages with search terms']]//input[@id='co_deliveryWhatToDeliverDocumentPagesWithTermsOnly']");

        private static readonly By IncludeDrawingsAndDescriptionsCheckBoxLocator = By.XPath(
            "//label[text()[normalize-space() = 'Include drawings and descriptions']]//input[@id='coid_chkDdcLayoutIncludeDrawingsAndDescriptions']");

        private static readonly By IncludeItemNotesCheckBoxLocator = By.XPath("//input[@id='co_deliveryWhatToDeliverNotes']");

        private static readonly By AllDrawingsDescriptionsLabelLocator = By.XPath(".//label[contains(@for,'coid_chkDdcLayoutAllDrawingDescriptions')]");

        private static readonly By AllDrawingsDescriptionsCheckboxLocator = By.XPath(".//input[contains(@id,'coid_chkDdcLayoutAllDrawingDescriptions')]");

        private static readonly By PagesLabelLocator = By.XPath(".//label[contains(@for,'coid_chkDdcLayoutPages')]");

        private static readonly By PagesCheckboxLocator = By.XPath(".//input[contains(@id,'coid_chkDdcLayoutPages')]");

        private static readonly By SelectedPagesLabelLocator = By.XPath(".//label[contains(@for,'coid_chkDdcSelectedPages')]");

        private static readonly By SelectedPagesCountLabelLocator = By.XPath(".//div[contains(@id,'co_selectedDrawingsCountDisplay')]");

        private static readonly By SelectedPagesRadioLocator = By.XPath(".//input[contains(@id,'coid_chkDdcSelectedPages')]");

        private static readonly By AllPagesLabelLocator = By.XPath(".//label[contains(@for,'coid_chkDdcAllPages')]");

        private static readonly By AllPagesCountLabelLocator = By.XPath(".//div[contains(@id,'co_drawingsTotalCountDisplay')]");

        private static readonly By AllPagesRadioLocator = By.XPath(".//input[contains(@id,'coid_chkDdcAllPages')]");

        private static readonly By FromToRangeRadioLocator = By.XPath(".//input[contains(@id,'coid_chkDdcFrom')]");

        private static readonly By FromTextboxLocator = By.XPath(".//input[contains(@id,'coid_chkDdcFromInputValue')]");

        private static readonly By ToTextboxLocator = By.XPath(".//input[contains(@id,'coid_chkDdcToInputValue')]");

        private static readonly By IncludeDrawingsAndDescriptionsMoreInfoLocator = By.Id("co_deliveryIncludeDrawingsAndDescriptionsMoreInfo");

        private static readonly By IncludeDrawingsAndDescriptionsInfoLocator = By.Id("co_deliveryIncludeDrawingsAndDescriptionsInfo");

        private static readonly By WarningMessageLocator = By.ClassName("co_errorMessage");

        private static readonly By WhatToDeliverLabelLocator = By.XPath("//*[@id='co_whatToDeliverTitle']");

        private static readonly By WhatToDeliverOptionLabelLocator =
            By.XPath(".//ul/li[not(contains(@style,'none'))]/label");

        private static readonly By CurrentViewRadioLocator = By.XPath("//input[@value = 'TrackerAnalyticsCurrentView']");

        private static readonly By AllViewsRadioLocator = By.XPath("//input[@value = 'TrackerAnalyticsAll']");

        private static readonly By ChartCheckboxLocator = By.XPath("//input[@name= 'Charts' and @type= 'checkbox']");

        private static readonly By AllChartCheckboxLocator = By.XPath("//input[@name= 'AllCharts' and @type= 'checkbox']");

        private static readonly By CurrentViewListOfSummariesCheckboxLocator = By.XPath("//input[@id = 'co_deliveryWhatToDeliverTrackerAnalyticsWYSIWYGList' and @type = 'checkbox']");

        private static readonly By AllViewListOfSummariesCheckboxLocator = By.XPath("//input[@id = 'co_deliveryWhatToDeliverTrackerAnalyticsAllList' and @type = 'checkbox']");

        private static readonly By TrackersAnalyticsErrorMessageLocator = By.Id("co_delivery_trackerAnalyticsSelectionErrorMessage");

        private static readonly By ContainerLocator = By.Id("co_delivery_whatToDeliverContainer");

        private EnumPropertyMapper<WhatToDeliver, WebElementInfo> whatToDeliverMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the WhatToDeliver enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<WhatToDeliver, WebElementInfo> WhatToDeliverMap
            => this.whatToDeliverMap = this.whatToDeliverMap ?? EnumPropertyModelCache.GetMap<WhatToDeliver, WebElementInfo>();

        /// <summary>
        /// Only pages with quotations checkbox
        /// </summary>
        public ICheckBox OnlyPagesWithQuotationsCheckBox => new CheckBox(OnlyPagesWithQuotationsCheckboxLocator);

        /// <summary>
        /// Only pages with terms checkbox
        /// </summary>
        public ICheckBox OnlyPagesWithTermsCheckBox => new CheckBox(OnlyPagesWithTermsCheckboxLocator);

        /// <summary>
        /// Include drawings and descriptions checkbox
        /// </summary>
        public ICheckBox IncludeDrawingsAndDescriptionsCheckBox => new CheckBox(IncludeDrawingsAndDescriptionsCheckBoxLocator);

        /// <summary>
        /// Include item notes checkbox
        /// </summary>
        public ICheckBox IncludeItemNotesCheckBox => new CheckBox(IncludeItemNotesCheckBoxLocator);

        /// <summary>
        /// Include drawings and descriptions Info link
        /// </summary>
        public ILink IncludeDrawingsAndDescriptionsMoreInfoLink => new Link(IncludeDrawingsAndDescriptionsMoreInfoLocator);

        /// <summary>
        /// Include drawings and descriptions Info label
        /// </summary>
        public ILabel IncludeDrawingsAndDescriptionsInfoLabel => new Label(IncludeDrawingsAndDescriptionsInfoLocator);

        /// <summary>
        /// All drawings descriptions
        /// </summary>
        public ILabel AllDrawingsDescriptionsLabelLocatorLabel => new Label(this.ComponentLocator, AllDrawingsDescriptionsLabelLocator);

        /// <summary>
        /// All drawings descriptions checkbox
        /// </summary>
        public ICheckBox AllDrawingsDescriptionsCheckbox => new CheckBox(this.ComponentLocator, AllDrawingsDescriptionsCheckboxLocator);

        /// <summary>
        /// Pages label
        /// </summary>
        public ILabel PagesLabel => new Label(this.ComponentLocator, PagesLabelLocator);

        /// <summary>
        /// Pages checkbox
        /// </summary>
        public ICheckBox PagesCheckbox => new CheckBox(this.ComponentLocator, PagesCheckboxLocator);

        /// <summary>
        /// Selected pages label
        /// </summary>
        public ILabel SelectedPagesLabel => new Label(this.ComponentLocator, SelectedPagesLabelLocator);

        /// <summary>
        /// Selected pages count
        /// </summary>
        public ILabel SelectedPagesCountLabel => new Label(this.ComponentLocator, SelectedPagesCountLabelLocator);

        /// <summary>
        /// Selected pages radiobutton
        /// </summary>
        public IRadiobutton SelectedPagesRadiobutton => new Radiobutton(this.ComponentLocator, SelectedPagesRadioLocator);

        /// <summary>
        /// All pages label
        /// </summary>
        public ILabel AllPagesLabel => new Label(this.ComponentLocator, AllPagesLabelLocator);

        /// <summary>
        /// All pages count label
        /// </summary>
        public ILabel AllPagesCountLabel => new Label(this.ComponentLocator, AllPagesCountLabelLocator);

        /// <summary>
        /// All pages radiobutton
        /// </summary>
        public IRadiobutton AllPagesRadiobutton => new Radiobutton(this.ComponentLocator, AllPagesRadioLocator);

        /// <summary>
        /// from/to range radiobutton
        /// </summary>
        public IRadiobutton FromToRangeRadiobutton => new Radiobutton(this.ComponentLocator, FromToRangeRadioLocator);

        /// <summary>
        /// From textbox
        /// </summary>
        public ITextbox FromTextbox => new Textbox(this.ComponentLocator, FromTextboxLocator);

        /// <summary>
        /// To textbox locator
        /// </summary>
        public ITextbox ToTextbox => new Textbox(this.ComponentLocator, ToTextboxLocator);

        /// <summary>
        /// Warning message label
        /// </summary>
        public ILabel WarningMessageLabel => new Label(WarningMessageLocator);

        /// <summary>
        /// Current view Radiobutton
        /// </summary>
        public IRadiobutton CurrentViewRadiobutton => new Radiobutton(CurrentViewRadioLocator);

        /// <summary>
        /// All views Radiobutton
        /// </summary>
        public IRadiobutton AllViewsRadiobutton => new Radiobutton(AllViewsRadioLocator);

        /// <summary>
        /// Chart Checkbox
        /// </summary>
        public ICheckBox ChartCheckbox => new CheckBox(ChartCheckboxLocator);

        /// <summary>
        /// All Chart Checkbox
        /// </summary>
        public ICheckBox AllChartCheckbox => new CheckBox(AllChartCheckboxLocator);

        /// <summary>
        /// Current view list of summaries Checkbox
        /// </summary>
        public ICheckBox CurrentViewListOfSummariesCheckbox => new CheckBox(CurrentViewListOfSummariesCheckboxLocator);

        /// <summary>
        /// All view list of summaries Checkbox
        /// </summary>
        public ICheckBox AllViewListOfSummariesCheckbox => new CheckBox(AllViewListOfSummariesCheckboxLocator);

        /// <summary>
        /// Trackers Analytics Error Message
        /// </summary>
        public ILabel TrackersAnalyticsErrorMessageLabel => new Label(TrackersAnalyticsErrorMessageLocator);

        /// <summary>
        /// Verify TabOption is Enabled
        /// </summary>
        /// <param name="whatToDeliverOption"> The expected option on The Basics tab </param>
        /// <returns> True if the option is enabled, false otherwise </returns>
        public bool IsOptionEnabled(WhatToDeliver whatToDeliverOption) =>
            DriverExtensions.IsEnabled(By.Id(this.WhatToDeliverMap[whatToDeliverOption].Id));

        /// <summary>
        /// Verify the given What to Deliver option is displayed
        /// </summary>
        /// <param name="whatToDeliverOption"> The option to check for </param>
        /// <returns> True if the option is displayed, false otherwise </returns>
        public bool IsOptionDisplayed(WhatToDeliver whatToDeliverOption) =>
            DriverExtensions.IsDisplayed(By.Id(this.WhatToDeliverMap[whatToDeliverOption].Id));

        /// <summary>
        /// Verify the given What to Deliver option is selected
        /// </summary>
        /// <param name="whatToDeliverOption"> The option to check for </param>
        /// <returns> True if the option is selected, false otherwise </returns>
        public bool IsOptionSelected(WhatToDeliver whatToDeliverOption) =>
            DriverExtensions.IsRadioButtonSelected(By.Id(this.WhatToDeliverMap[whatToDeliverOption].Id));

        /// <summary>
        /// Hover What To Deliver option
        /// </summary>
        /// <param name="whatToDeliverOption"> What To Deliver option </param>
        public void HoverWhatToDeliverOption(WhatToDeliver whatToDeliverOption)
            => DriverExtensions.Hover(By.XPath(this.WhatToDeliverMap[whatToDeliverOption].LocatorString));

        /// <summary>
        /// Select What to Deliver; List of Items/Documents
        /// </summary>
        /// <param name="format">Format of the delivery</param>
        /// <returns>The <see cref="TheBasicsTabComponent"/>.</returns>
        public TheBasicsTabComponent SelectOption(WhatToDeliver format)
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.WhatToDeliverMap[format].Id)).Click();
            return new TheBasicsTabComponent();
        }
        
        /// <summary>
        /// Gets What to deliver option text.
        /// </summary>
        /// <param name="whatToDeliverOption"> The option name  </param>
        /// <returns> The <see cref="string"/>. What to deliver option text. </returns>
        public string GetOptionText(WhatToDeliver whatToDeliverOption)
            => DriverExtensions.GetText(By.XPath(this.WhatToDeliverMap[whatToDeliverOption].LocatorString));

        /// <summary>
        /// Get list of available options to select 
        /// </summary>
        /// <returns>list of options</returns>
        public List<string> GetAvailableWhatToDeliverLabelList() =>
            DriverExtensions.GetElements(ContainerLocator, WhatToDeliverOptionLabelLocator).Select(label => label.Text)
                            .ToList();

        /// <summary>
        /// Verifies that What to deliver label is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if What to deliver label is displayed. </returns>
        public bool IsWhatToDeliverLabelDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, WhatToDeliverLabelLocator);

        /// <summary>
        /// Gets What to deliver label text.
        /// </summary>
        /// <returns> The <see cref="bool"/>. What to deliver label text. </returns>
        public string GetWhatToDeliverLabelText() => DriverExtensions.GetText(this.ComponentLocator, WhatToDeliverLabelLocator);
    }
}