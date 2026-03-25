namespace Framework.Common.UI.Products.WestLawNext.Components.PortalManager
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestLawNext.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WestLawNext Content Component
    /// </summary>
    public class WlnContentComponent : BaseModuleRegressionComponent
    {
        private const string CategoryPageLinkLctMask =
            "//*[@id='contentWidgetDiv']//*[@class='co_wizardStep_left_tab co_3Column']//a[text()='{0}']";

        private const string ItemCheckboxLctMask =
            "//*[@id='contentWidgetDiv']//*[text()='{0}']/../i[@role='checkbox' and not(contains(@class,'co_checked'))]";

        private const string TabItemLctMask =
            "//*[@id='contentWidgetDiv']//ul[contains(@class, 'co_tabs')]/li//a[text()='{0}']";

        private static readonly By CasesLocator = By.XPath("id('co_wizardStep_left_Home_Cases')/i");

        private static readonly By DefaultTabItemsLocator = By.XPath(".//ul[@role='list']/li[@role='listitem']/a");

        private static readonly By FirstAutoSuggestedItemLocator = By.XPath("//ul[@id='co_searchSuggestion']/li[2]");

        private static readonly By PathRootLocator = By.XPath("(//*[contains(@class,'co_wizardStep_left_breadcrumb')]/a)[1]");

        private static readonly By SearchTextboxLocator = By.Id("co_search_widget");

        private static readonly By SelectedContentListLocator = By.XPath("id('selectedItemsControlId')/li");

        private static readonly By ContainerLocator = By.CssSelector("#contentSection #co_westclipContainer");

        private EnumPropertyMapper<ContentType, ContentTypeInfo> contentTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentType, ContentTypeInfo> ContentTypeMap
            => this.contentTypeMap = this.contentTypeMap ?? EnumPropertyModelCache.GetMap<ContentType, ContentTypeInfo>();

        /// <summary>
        /// Checks if Default Tab Content is equals to required values
        /// </summary>
        /// <param name="tabs">Tab's names</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckDefaultTabContent(params string[] tabs)
            => DriverExtensions.GetElements(DefaultTabItemsLocator).Select(item => item.Text.Trim()).ToList().SequenceEqual(tabs);

        /// <summary>
        /// Checks if Selected Content is equals to required values
        /// </summary>
        /// <param name="tabs">The tabs.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckSelectedContent(params string[] tabs)
            => DriverExtensions.GetElements(SelectedContentListLocator).Select(item => item.Text).ToList().SequenceEqual(tabs);

        /// <summary>
        /// Removes the specified content
        /// </summary>
        /// <param name="contentTerms">Content terms to remove</param>
        public void RemoveSelectedItems(params string[] contentTerms)
            => DriverExtensions.GetElements(SelectedContentListLocator).Where(item => item.Text.ContainsAnyItem(contentTerms))
                .ToList().ForEach(u => u.Click());

        /// <summary>
        /// Performs search and clicks on the first suggested result
        /// </summary>
        /// <param name="searchQuery">
        /// The query to search.
        /// 11/18/2015 RA: due to elastic search implementation, there can be more than one result, with the exact match bumped on top (Bug 831578, closed as functions as designed)
        /// </param>
        public void SearchAndSelectFirstSuggestedResult(string searchQuery)
        {
            DriverExtensions.WaitForElement(SearchTextboxLocator).Clear();
            DriverExtensions.WaitForElement(SearchTextboxLocator).SendKeysSlow(searchQuery);
            DriverExtensions.Click(DriverExtensions.WaitForElementDisplayed(FirstAutoSuggestedItemLocator));
        }

        /// <summary>
        /// Selects the Cases - All Federal and All States, Key Numbers and Secondary Sources content in the WLN Content section
        /// </summary>
        public void SelectContentPredefined()
        {
            DriverExtensions.Click(CasesLocator);
            var jurisdictionDialog = new JurisdictionOptionsDialog();
            jurisdictionDialog.SelectJurisdictions(true, Jurisdiction.AllFederal, Jurisdiction.AllStates);
            jurisdictionDialog.SaveButton.Click<GlobalSearchFormPage>();

            this.SelectItemsFromPath(
                string.Empty,
                this.ContentTypeMap[ContentType.TopicsAndKeynumbers].Text,
                this.ContentTypeMap[ContentType.SecondarySources].Text);
        }

        /// <summary>
        /// Select sources
        /// </summary>
        /// <param name="pathToSelectIn">
        /// The path To Select In. Example: {segment1}/{segment2}/...{segmentN}.
        /// Set this parameter to empty string to make selections in root
        /// </param>
        /// <param name="itemNamesToSelect">The item Names To Select.</param>
        public void SelectItemsFromPath(string pathToSelectIn, params string[] itemNamesToSelect)
        {
            if (!string.IsNullOrWhiteSpace(pathToSelectIn))
            {
                this.ExpandPath(pathToSelectIn);
            }

            itemNamesToSelect.ToList().ForEach(u => DriverExtensions.GetElement(By.XPath(string.Format(ItemCheckboxLctMask, u))).Click());
        }

        /// <summary>
        /// Go to defined path
        /// </summary>
        /// <param name="path">The path to go to. Example: {segment1}/{segment2}/...{segmentN}</param>
        private void ExpandPath(string path = "All Content")
        {
            string[] linkNames = path.Split('/', '\\').Select(str => str.Trim()).ToArray();

            // linkNames[0]-tab to work in
            DriverExtensions.Click(By.XPath(string.Format(TabItemLctMask, linkNames[0])));

            // go to root element if it is displayed
            if (DriverExtensions.IsDisplayed(PathRootLocator, 5))
            {
                DriverExtensions.GetElement(PathRootLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }

            // skip(1) because 1st item is tab, not item to add
            linkNames.Skip(1).ToList().ForEach(
                u => DriverExtensions.GetElement(By.XPath(string.Format(CategoryPageLinkLctMask, u)))
                                     .Click());

            DriverExtensions.WaitForJavaScript();
        }
    }
}