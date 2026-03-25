namespace Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.WestLawNext.Components.AdvancedSearchTemplate;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// News AST Page
    /// </summary>
    public class NewsAstPage : CommonAdvancedSearchPage
    {
        private static readonly By AllCapsInformationIconLocator = By.Id("co_search_advancedSearch_helpLink_ALLCAPS");

        private static readonly By AllOfTheseTermsTermFrequencyLinkLocator = By.Id("co_termfrequency_all");

        private static readonly By AnyOfTheseTermsTermFrequencyLinkLocator = By.Id("co_termfrequency_any");

        private static readonly By AppliedSmartTermsSummaryLocator = By.Id("co_search_alertSmartTermsBellowSummary");

        private static readonly By CapsInformationIconLocator = By.Id("co_search_advancedSearch_helpLink_CAPS");

        private static readonly By ContainerDuplicateCheckboxLocator = By.Id("co_search_advancedSearch_DI_0ListItem");

        private static readonly By DateWithFormatLabelTextLocator =
            By.XPath("//label[@for='co_search_advancedSearch_DA'] | //li[@id='co_search_advancedSearch_listItem_DA']//label");

        private static readonly By IdentifyDuplicateCheckboxLocator = By.Id("co_search_advancedSearch_DI_0");

        private static readonly By IdentifyDuplicateCheckboxTextLocator =
            By.XPath("//label[@for='co_search_advancedSearch_DI_0']");

        private static readonly By LanguageDropDownLocator = By.Id("co_search_advancedSearch_LA");

        private static readonly By NoCapsInformationIconLocator = By.Id("co_search_advancedSearch_helpLink_NOCAPS");

        private static readonly By SelectSmartTermsLinkLocator =
            By.XPath("//a[contains(@id,'co_search_alertSearchSmartTerms') and not(contains(@class,'hideState'))]");

        private static readonly By SmartTermsInformationIconLocator = By.Id("co_search_advancedSearch_helpLink_IND-SMART");

        private static readonly By SmartTermsInformationPopUpLocator = By.XPath("//div[contains(@id,'coid_a11yTooltip') and not(contains(@class,'is-visually-hidden'))]");

        private static readonly By ThisExactPhraseTermFrequencyLinkLocator = By.Id("co_termfrequency_exact");

        private static readonly By DocumentLengthDropdownLocator = By.Id("co_search_advancedSearch_DOCLENGTH");

        private static readonly By DocumentFieldTitleListLocator =
            By.XPath("//fieldset[@id='co_search_advancedSearch_fieldset_1']/ul[@class='co_search_advancedSearchFormFields']/child::li/label");

        private static readonly By CaseSensitivityTermsLinkLocator =
            By.XPath("//a[@id='co_search_advancedSearchCaseSensitivityLink_baseFields']//strong[text()='Case-Sensitivity']");

        private static readonly By CaseSensitivityDocumentFieldsLinkLocator =
            By.XPath("//a[@id='co_search_advancedSearchCaseSensitivityLink_searchFullText']//strong[text()='Case-Sensitivity']");

        /// <summary>
        /// Exclude Document Types Field Box
        /// </summary>
        public ExcludeDocumentTypesFieldBoxComponent ExcludeDocumentTypesFieldBox => new ExcludeDocumentTypesFieldBoxComponent();

        /// <summary>
        /// Document Length Dropdown
        /// </summary>
        public IDropdown<DocumentLengthOptions> DocumentLength => new Dropdown<DocumentLengthOptions>(DocumentLengthDropdownLocator);

        /// <summary>
        /// The language drop down.
        /// </summary>
        public IDropdown<string> LanguageDropDown => new Dropdown(LanguageDropDownLocator);

        /// <summary>
        /// Click on smart terms link
        /// </summary>
        /// <returns>
        /// The <see cref="SelectSmartTermsDialog"/>.
        /// </returns>
        public SelectSmartTermsDialog ClickSmartTermsLink()
        {
            DriverExtensions.WaitForElementDisplayed(SelectSmartTermsLinkLocator).Click();
            return new SelectSmartTermsDialog();
        }

        /// <summary>
        /// Get date with format label text
        /// </summary>
        /// <returns>date with format label text</returns>
        public string GetDateWithFormatLabelText() => DriverExtensions.GetText(DateWithFormatLabelTextLocator);
        

        /// <summary>
        /// Get identify duplicate check-box text
        /// </summary>
        /// <returns>duplicate check-box text</returns>
        public string GetIdentifyDuplicateCheckboxText() => DriverExtensions.GetText(IdentifyDuplicateCheckboxTextLocator);

        /// <summary>
        /// Get select smart terms link text
        /// </summary>
        /// <returns>smart terms link text</returns>
        public string GetSelectSmartTermsLinkText() => DriverExtensions.GetText(SelectSmartTermsLinkLocator);

        /// <summary>
        /// Get smart terms icon pop-up message
        /// </summary>
        /// <returns>smart terms icon pop-up message</returns>
        public string GetSmartTermsDialogText() => DriverExtensions.WaitForElement(SmartTermsInformationPopUpLocator).Text;

        /// <summary>
        /// Get smart terms summary
        /// </summary>
        /// <returns>smart terms summary</returns>
        public string GetSmartTermsSummary() => DriverExtensions.GetText(AppliedSmartTermsSummaryLocator);

        /// <summary>
        /// Hover over smart terms information icon displayed
        /// </summary>
        /// <returns>
        /// The <see cref="NewsAstPage"/>.
        /// </returns>
        public NewsAstPage HoverOverSmartTermsInformationIcon()
        {
            DriverExtensions.Hover(SmartTermsInformationIconLocator);
            return this;
        }

        /// <summary>
        /// Is AllCaps information icon displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsAllCapsInformationIconDisplayed() => DriverExtensions.IsDisplayed(AllCapsInformationIconLocator);

        /// <summary>
        /// Is All Of These Terms term frequency link displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsAllOfTheseTermsTermFrequencyLinkDisplayed() => DriverExtensions.IsDisplayed(AllOfTheseTermsTermFrequencyLinkLocator);

        /// <summary>
        /// Is Any Of These Terms term frequency link displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsAnyOfTheseTermsTermFrequencyLinkDisplayed() => DriverExtensions.IsDisplayed(AnyOfTheseTermsTermFrequencyLinkLocator);

        /// <summary>
        /// Is Caps information icon displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsCapsInformationIconDisplayed() => DriverExtensions.IsDisplayed(CapsInformationIconLocator);

        /// <summary>
        /// Is date with format label text displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsDateWithFormatLabelTextDisplayed() => DriverExtensions.IsDisplayed(DateWithFormatLabelTextLocator);

        /// <summary>
        /// Is identify duplicate check-box displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsIdentifyDuplicateCheckboxDisplayed() => DriverExtensions.IsDisplayed(IdentifyDuplicateCheckboxLocator);

        /// <summary>
        /// Is identify duplicate check-box selected
        /// </summary>
        /// <returns>true if selected</returns>
        public bool IsIdentifyDuplicateCheckboxSelected() => DriverExtensions.IsCheckboxSelected(IdentifyDuplicateCheckboxLocator);
           

        /// <summary>
        /// Is NoCaps information icon displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsNoCapsInformationIconDisplayed() => DriverExtensions.IsDisplayed(NoCapsInformationIconLocator);

        /// <summary>
        /// Is select smart terms link displayed
        /// </summary>
        /// <returns>true if present</returns>
        public bool IsSelectSmartTermsLinkDisplayed() => DriverExtensions.IsDisplayed(SelectSmartTermsLinkLocator);

        /// <summary>
        /// Is smart terms information icon displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsSmartTermsInformationIconDisplayed() => DriverExtensions.IsDisplayed(SmartTermsInformationIconLocator); 

        /// <summary>
        /// Is This Exact Phrase term frequency link displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsThisExactPhraseTermFrequencyLinkDisplayed() => DriverExtensions.IsDisplayed(ThisExactPhraseTermFrequencyLinkLocator);

        /// <summary>
        /// The method to set option from Document Length drop down
        /// </summary>
        /// <param name="advancedSearchField">
        /// drop down on AST
        /// </param>
        /// <param name="value">
        /// value of option in a drop down
        /// </param>
        /// <returns>
        /// The <see cref="NewsAstPage"/>.
        /// </returns>
        public NewsAstPage SelectDocumentLengthDropdownOption(AdvancedSearchField advancedSearchField, DocumentLengthOptions value)
        {
            string fieldDropdawnLocator = this.AdvSearchOptionsMap[advancedSearchField].Id;
            DriverExtensions.SetDropdown(value.GetEnumTextValue(), By.Id(fieldDropdawnLocator));
            return this;
        }

        /// <summary>
        /// Set Identify Duplicate check-box
        /// </summary>
        /// <typeparam name="T"> page type </typeparam>
        /// <param name="setTo"> value of the check-box </param>
        /// <returns> New instance of the page</returns>
        public T SetIdentifyDuplicateCheckbox<T>(bool setTo) where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(IdentifyDuplicateCheckboxLocator, setTo);
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get list of document field titles
        /// </summary>
        /// <returns>list of document field titles</returns>
        public List<string> GetListOfDocumentFieldTitles() => DriverExtensions.GetElements(DocumentFieldTitleListLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Is CaseSensitivity Link For Terms Displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCaseSensitivityLinkForTermsDisplayed() => DriverExtensions.IsDisplayed(CaseSensitivityTermsLinkLocator);

        /// <summary>
        /// Is CaseSensitivity Link For Documents Fields Displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsCaseSensitivityLinkForDocumentFieldsDisplayed() => DriverExtensions.IsDisplayed(CaseSensitivityDocumentFieldsLinkLocator);

        /// <summary>
        /// Click CaseSensitivity Link For Terms
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCaseSensitivityLinkForTerms<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(CaseSensitivityTermsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click CaseSensitivity Link For Documents Fields
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCaseSensitivityLinkForDocumentFields<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(CaseSensitivityDocumentFieldsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}