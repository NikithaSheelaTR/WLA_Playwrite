using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Textboxes;
using Framework.Common.UI.Products.WestLawNext.Components;
using OpenQA.Selenium;
using Framework.Common.UI.Products.Shared.Elements;
using System.Collections.Generic;
using Framework.Common.UI.Products.Shared.Elements.Links;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    /// <summary>
    /// Keyword And Boolean Search Tab Panel
    /// </summary>
    public class KeywordAndBooleanSearchTabPanel : BaseTabComponent
    {
        private static readonly By QueryTextAreaLocator = By.XPath(".//saf-text-area[@id='searchInputId']");
        private static readonly By SearchButtonLocator = By.XPath(".//saf-button[@id ='search-button']");
        private static readonly By KeywordBooleanSearchHeaderLocator = By.XPath("//h1[@id='co_resultsPageLabel']");
        private static readonly By RecentSearchPanelComponetLocator = By.XPath("//saf-tabs[@activeid='recentSearches']");
        private static readonly By RecentSearchPanelListLocator = By.XPath(".//li");
        private static readonly By StarIconButtonLocator = By.XPath(".//input[@id='RecentSearchListItem_0']");
        private static readonly By SavedSearchesButtonLocator = By.XPath("//saf-tab[@id='savedSearches']");
        private static readonly By SavedSearchPanelComponetLocator = By.XPath("//saf-tabs[@activeid='savedSearches']");
        private static readonly By NoSavedSearchLabelLocator = By.XPath(".//saf-tab-panel[@aria-labelledby='savedSearches']");

        private readonly string SavedStarIconLocator = "//saf-tab-panel[@aria-labelledby='savedSearches']//saf-button[text()={0}]/../div[contains(@class,'__searchesFavorites')]//input";

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Keyword & Boolean Search";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//*[@id = 'panel-2']");

        /// <summary>
        /// QueryTextArea
        /// </summary>
        public ITextbox QueryTextArea => new Textbox(ComponentLocator, QueryTextAreaLocator);

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(ComponentLocator, SearchButtonLocator);

        /// <summary>
        /// KeywordAndBooleanSearch Header
        /// </summary>
        public ILabel KeywordBooleanSearchHeader => new Label(KeywordBooleanSearchHeaderLocator);

        /// <summary>
        /// search results Panel List
        /// </summary>
        public IReadOnlyCollection<ILink> RecentSearchPanelList => new ElementsCollection<Link>(RecentSearchPanelComponetLocator, RecentSearchPanelListLocator);

        /// <summary>
        /// Star Icon button
        /// </summary>
        public IButton StarIconButton => new Button(RecentSearchPanelComponetLocator, StarIconButtonLocator);

        /// <summary>
        /// Saved Search button
        /// </summary>
        public IButton SavedSearchesButton => new Button(SavedSearchesButtonLocator);

        /// <summary>
        /// Selected Star Icon button
        /// </summary>
        public IButton SelectedStarIconButton => new Button(SavedSearchPanelComponetLocator, RecentSearchPanelListLocator);

        /// <summary>
        /// search results Panel List
        /// </summary>
        public IReadOnlyCollection<ILink> SelectedStarIconPanelList => new ElementsCollection<Link>(SavedSearchPanelComponetLocator, RecentSearchPanelListLocator);

        /// <summary>
        /// Select Saved StarIcon 
        /// </summary>
        /// <param name="categoryName"> Category string</param>
        public void SelectSavedStarIcon(string categoryName)
        {
            var hostElement = DriverExtensions.GetElement(SafeXpath.BySafeXpath(SavedStarIconLocator, categoryName));
            hostElement.Click();
        }

        /// <summary>
        /// No Saved Search Label
        /// </summary>
        public ILabel NoSavedSearchLabel => new Label(SavedSearchPanelComponetLocator, NoSavedSearchLabelLocator);
    }
}
