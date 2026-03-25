namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Image;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Searches;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The advanced search page (Template)
    /// </summary>
    public class CommonAdvancedSearchPage : CommonAuthenticatedWestlawNextPage, IAdvancedSearchPage
    {
        private const string AreaLocator = "//label[text() = {0}]";

        private const string AreaCheckboxLctMask = "//label[text() = {0}]/input";

        private const string AdvancedSearchFieldLctMask = "//li[./label[text()='{0}']]/input | //li[./label[text()='{0}']]/textarea";

        private static readonly By AstContainerLocator = By.Id("co_search_advancedSearch_left");

        private static readonly By AdvancedLinkLocator = By.Id("co_search_advancedSearchLink");

        private static readonly By AdvancedSearchWarningMessageLocator = By.Id("co_search_advancedSearch_errorMsgBox");

        private static readonly By AddToFavoriteLocator = By.Id("co_foldering_categoryPage");

        private static readonly By DateDropdownContainerLocator = By.XPath("//div[@class='co_facet_sad_options']//parent::div[@id] | //*[@id='co_dateWidget_cb_DATE']//button");

        private static readonly By MoreInfoIconLocator = By.Id("co_moreInfoLink");

        private static readonly By ScopeIconLocator = By.Id("coid_website_browsePageScopeMoreInfo");

        private static readonly By FieldRelatedLabelLocator = By.XPath("./parent::li/label");

        private static readonly By ConnectorsAndExpandersLocator = By.Id("co_search_advancedSearch_tncLegendBox");

        private static readonly By DocumentImageLocator = By.XPath("//div[@class='co_advancedSearch_documentImage']//img");

        private EnumPropertyMapper<AdvancedSearchField, WebElementInfo> advSearchOptionsMap;

        private EnumPropertyMapper<TermsFrequency, WebElementInfo> termsFrequencyMap;

        private static readonly By ExposeIdentifyCheckboxLocator = By.XPath("//*[@id='co_search_advancedSearch_DI_0']");

        private static readonly By DateButtonLocator = By.XPath("//button[@class='a11yDateWidget-button co_defaultBtn'] | //div[@id='co_search_advancedSearch_inner']//li[@id='co_search_advancedSearch_listItem_cb_DATE']//button[@class='a11yDateWidget-button co_defaultBtn']");

        private static readonly By PointInTimeRadioButtonLocator = By.XPath("//label[text()='Search By Point in Time']");

        private static readonly By PointInTimeTextBoxLocator = By.Id("co_search_advancedSearch_EFF-DATE");

        /// <summary>
        /// DateDropdown
        /// </summary>
        public DateDropdown DateDropdown => new DateDropdown(DateDropdownContainerLocator);

        /// <summary>
        /// The Breadcrumb Component.
        /// </summary>
        public BreadCrumbComponent Breadcrumb { get; } = new BreadCrumbComponent();

        /// <summary>
        /// The Breadcrumb Component.
        /// </summary>
        public FavoritesComponent Favorites { get; } = new FavoritesComponent();

        /// <summary>
        /// TermsFrequencyDialog
        /// </summary>
        public TermsFrequencyDialog TermsFrequencyDialog => new TermsFrequencyDialog();

        /// <summary>
        /// DateDialogue
        /// </summary>
        public DateDialogue DateOptionDialog => new DateDialogue();

        /// <summary>
        /// Tools And Resources Widget
        /// Might be present on the right hand side for some category pages
        /// </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesComponent { get; private set; } = new ToolsAndResourcesFacetComponent();

        /// <summary>
        /// Gets the TermsFrequency enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermsFrequency, WebElementInfo> TermsFrequencyMap
            => this.termsFrequencyMap = this.termsFrequencyMap ?? EnumPropertyModelCache.GetMap<TermsFrequency, WebElementInfo>();

        /// <summary>
        /// Gets the AdvancedSearchField enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AdvancedSearchField, WebElementInfo> AdvSearchOptionsMap => this.advSearchOptionsMap = this.advSearchOptionsMap ?? EnumPropertyModelCache.GetMap<AdvancedSearchField, WebElementInfo>();

        /// <summary>
        ///  Date Button 
        /// </summary>
        public IButton DateButton => new Button(DateButtonLocator);

        /// <summary>
        /// Get the list of available AdvancedSearchFields
        /// </summary>
        /// <returns>List of advanced search fields</returns>
        public List<AdvancedSearchField> GetAdvancedSearchFields() => this
            .AdvSearchOptionsMap.Where(x => DriverExtensions.IsDisplayed(By.Id(x.Value.Id))).Select(e => e.Key).ToList();

        /// <summary>
        /// Retrieve the displayed field name
        /// </summary>
        /// <param name="field">Field</param>
        /// <returns>Field name</returns>
        public string GetDisplayedFieldName(AdvancedSearchField field) =>
            DriverExtensions.IsDisplayed(By.Id(this.AdvSearchOptionsMap[field].Id))
                ? DriverExtensions.GetText(By.Id(this.AdvSearchOptionsMap[field].Id), FieldRelatedLabelLocator)
                : string.Empty;

        /// <summary>
        /// Is text in the field default
        /// </summary>
        /// <param name="field"></param>
        /// <returns>bool</returns>
        public bool IsTextDefaultInField(AdvancedSearchField field) =>
            DriverExtensions.GetElement(By.Id(this.AdvSearchOptionsMap[field].Id)).GetAttribute("class")
                            .Equals("watermark co_defaultInput");

        /// <summary>
        /// ClickTermFrequency
        /// </summary>
        /// <param name="termsFrequency">termFrequency</param>
        /// <returns>TermsFrequencyDialog</returns>
        public TermsFrequencyDialog ClickTermFrequency(TermsFrequency termsFrequency)
        {
            this.GetTermFrequencyLinkElement(termsFrequency).Click();
            return new TermsFrequencyDialog();
        }

        /// <summary>
        /// The click scope icon.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parameter
        /// </typeparam>
        /// <returns>
        /// The in
        /// </returns>
        public T ClickScopeIcon<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ScopeIconLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Sets the specified text field on an advanced search page to the specified query value
        /// </summary>
        /// <param name="advancedSearchField">the field to set</param>
        /// <param name="query">the text to enter into the field</param>
        /// <param name="waitForApplying">Define whether we wait for applying</param>
        /// // <param name="sendSlow">Define whether entering slow</param>
        /// <returns>The <see cref="CommonAdvancedSearchPage"/>.</returns>
        public CommonAdvancedSearchPage EnterTextInField(
            string advancedSearchField,
            string query,
            bool waitForApplying = true,
            bool sendSlow = true) =>
            this.EnterTextInField(
                DriverExtensions.WaitForElement(By.XPath(string.Format(AdvancedSearchFieldLctMask, advancedSearchField))),
                query,
                waitForApplying,
                sendSlow);

        /// <summary>
        /// Sets the specified text field on an advanced search page to the specified query value
        /// </summary>
        /// <param name="advancedSearchField">the field to set</param>
        /// <param name="query">the text to enter into the field</param>
        /// <param name="waitForApplying">Define whether we wait for applying</param>
        /// // <param name="sendSlow">Define whether entering slow</param>
        /// <returns>The <see cref="CommonAdvancedSearchPage"/>.</returns>
        public CommonAdvancedSearchPage EnterTextInField(
            AdvancedSearchField advancedSearchField,
            string query,
            bool waitForApplying = true,
            bool sendSlow = true) =>
            this.EnterTextInField(
                DriverExtensions.WaitForElement(By.Id(this.AdvSearchOptionsMap[advancedSearchField].Id)),
                query,
                waitForApplying,
                sendSlow);

        /// <summary>
        /// Determines if there is a warning message
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetAdvancedSearchWarningMessageText() => DriverExtensions.GetText(AdvancedSearchWarningMessageLocator);

        /// <summary>
        /// Determines if there is a warning message
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAdvancedSearchWarningMessageDisplayed() => DriverExtensions.IsDisplayed(AdvancedSearchWarningMessageLocator);

        /// <summary>
        /// Gets the current text from an advanced search text field
        /// </summary>
        /// <param name="advancedSearchField">the field to get the text from</param>
        /// <returns>Text <see cref="string" />.</returns>
        public string GetTextInField(AdvancedSearchField advancedSearchField)
            => DriverExtensions.GetText(By.Id(this.AdvSearchOptionsMap[advancedSearchField].Id));

        /// <summary>
        /// The is advanced link displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAdvancedLinkDisplayed() => DriverExtensions.IsDisplayed(AdvancedLinkLocator);

        /// <summary>
        /// Is Advanced Search Page Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAdvancedSearchTemplateDisplayed() => DriverExtensions.IsDisplayed(AstContainerLocator);

        /// <summary>
        /// Verifies if field of ast item  is displayed
        /// </summary>
        /// <param name="searchField">
        /// The search Field.
        /// </param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAstFieldDisplayed(AdvancedSearchField searchField)
            => DriverExtensions.IsDisplayed(By.Id(this.AdvSearchOptionsMap[searchField].Id));

        /// <summary>
        /// Verifies if title of ast item is displayed
        /// </summary>
        /// <param name="searchField">The search Field.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAstTitleDisplayed(AdvancedSearchField searchField)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(AreaLocator, this.AdvSearchOptionsMap[searchField].Text));

        /// <summary>
        /// Verifies if More info icon is displayed
        /// </summary>
        /// <returns>true if icon is present</returns>
        public bool IsMoreInfoIconDisplayed() => DriverExtensions.IsDisplayed(MoreInfoIconLocator);

        /// <summary>
        /// Verifies if Add To Favorite icon is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAddToFavoriteIconDisplayed() => DriverExtensions.IsDisplayed(AddToFavoriteLocator);

        /// <summary>
        /// The is scope icon displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsScopeIconDisplayed() => DriverExtensions.IsDisplayed(ScopeIconLocator);

        /// <summary>
        /// IsTermFrequencyDisplayed
        /// </summary>
        /// <param name="termFrequency">term frequency</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTermFrequencyDisplayed(TermsFrequency termFrequency) => this.GetTermFrequencyLinkElement(termFrequency).Displayed;

        /// <summary>
        /// Verifies if if connectors and expanders is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsConnectorsAndExpandersDisplayed() => DriverExtensions.IsDisplayed(ConnectorsAndExpandersLocator);

        /// <summary>
        /// Selects Area of Expertise checkbox
        /// </summary>
        /// <param name="area">Area to select</param>
        /// <param name="setTo">The select.</param>
        public void SelectAreaOfExpertise(string area, bool setTo = true)
        {
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(AreaCheckboxLctMask, area)).SetCheckbox(setTo);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Checks whether specific Area of expertise checkbox is selected
        /// </summary>
        /// <param name="area">area name</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsAreaOfExpertiseCheckboxSelected(string area)
            => DriverExtensions.IsCheckboxSelected(DriverExtensions.GetElement(SafeXpath.BySafeXpath(AreaCheckboxLctMask, area)));

        /// <summary>
        /// Click pdf image and open new browser tab
        /// </summary>
        /// <typeparam name="T">Page to return</typeparam>
        /// <param name="newTabName">The new Tab Name.</param>
        /// <returns> New Page Object</returns>
        public T ClickPdfImageAndOpenNewBrowserTab<T>(string newTabName)
            where T : ICreatablePageObject =>
            this.ClickAndOpenNewBrowserTab<T>(
                DriverExtensions.GetElement(DocumentImageLocator),
                newTabName);

        /// <summary>
        /// Document Image
        /// </summary>
        public IImage DocumentImage => new Image(DocumentImageLocator);

        /// <summary>
        /// The get term frequency link element.
        /// </summary>
        /// <param name="termFrequency">The frequency link.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        private IWebElement GetTermFrequencyLinkElement(TermsFrequency termFrequency)
            => DriverExtensions.WaitForElement(By.Id(this.TermsFrequencyMap[termFrequency].Id));

        /// <summary>
        /// Selects expose and identify duplicate document checkbox
        /// </summary>
        // <param name="setTo">The select.</param>
        public void SelectExposeIdentifyDuplicateDoc(bool setTo = true)
        {
            DriverExtensions.GetElement(ExposeIdentifyCheckboxLocator).SetCheckbox(setTo);
            DriverExtensions.WaitForJavaScript();
        }

        private CommonAdvancedSearchPage EnterTextInField(
            IWebElement fieldElement,
            string query,
            bool waitForApplying,
            bool sendSlow)
        {
            fieldElement.JavascriptClick();
            fieldElement.Clear();
            fieldElement.JavascriptClick();

            if (sendSlow)
            {
                fieldElement.SendKeysSlow(query);
            }
            else
            {
                fieldElement.SendKeys(query);
            }

            if (waitForApplying)
            {
                Thread.Sleep(1000); // wait for applying search query
            }

            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// PointInTimeRadioButton
        /// </summary>
        public IButton PointInTimeRadioButton => new Button(PointInTimeRadioButtonLocator);

        /// <summary>
        /// PointInTimeTextField
        /// </summary>
        public void EnterPointInTime(string date)
        {
            var input = DriverExtensions.WaitForElement(PointInTimeTextBoxLocator);
            input.Clear();
            input.SendKeys(date);
        }
    }
}