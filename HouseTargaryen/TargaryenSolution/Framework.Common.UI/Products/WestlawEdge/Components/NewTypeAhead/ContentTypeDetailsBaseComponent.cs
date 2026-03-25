namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Interfaces.Models;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The content type details base component.
    /// </summary>
    public abstract class ContentTypeDetailsBaseComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        /// <summary>
        /// Category Pages Component Locator
        /// </summary>
        protected static readonly By CategoryPagesComponentLocator =
            By.XPath(".//div[@class='co-TRDiscover-categoryPages ng-scope']");

        /// <summary>
        /// Titles Locator
        /// </summary>
        protected static readonly By TitlesLocator = By.XPath(".//h2 | .//h3");

        /// <summary>
        /// Federal Links Locator
        /// </summary>
        protected static readonly By FederalLinksLocator =
            By.XPath(".//div[preceding-sibling::h2[contains(.,'Federal')]]//a");

        /// <summary>
        /// State Links Locator
        /// </summary>
        protected static readonly By StateLinksLocator = By.XPath(
            ".//div[preceding-sibling::h2[contains(.,'State')] and not(preceding-sibling::h2[contains(.,'Federal')])]//a");

        /// <summary>
        /// More Link Locator
        /// </summary>
        private const string MoreLinkLctMask =
            "//h2[text() = '{0}']//following-sibling::a[contains(@id,'moreSuggestions_button')]";

        /// <summary>
        /// The category item lct mask.
        /// </summary>
        private const string CategoryItemLctMask =
                 ".//div[contains(@class,'TRDiscover-category') and .//*[contains(text(),'{0}')]]//*[not(text() = 'More') and contains(@class,'co-TRDiscover-result') or contains(@class,'SearchSuggestionsSection')] " +
            "| .//span[contains(@id,'_ContentTypes') and contains(@style,'block')]//*[contains(text(),'{0}')]//following-sibling::*//li//*[(@href or contains(@class,'button'))and not(contains(@id,'more'))]";

        private static readonly By LinkLocator = By.XPath(".//a");

        private static readonly By CitationLocator = By.XPath("//ul[@class='co-TRDiscover-cite']");

        private static readonly By ContentPagesSuggestionsLocator =
            By.XPath("//*[text()='Content Pages']/following-sibling :: ul//span");

        /// <summary>
        /// Citation Elements
        /// </summary>
        public IReadOnlyCollection<ILabel> Citations => new ElementsCollection<Label>(CitationLocator);

        /// <summary>
        /// Content Pages Suggestions
        /// </summary>
        public IReadOnlyCollection<ILabel> ContentPagesSuggestions => new ElementsCollection<Label>(ContentPagesSuggestionsLocator);

        /// <summary>
        /// Gets or sets SupportedCategories
        /// </summary>
        protected List<NewTypeAheadCategories> SupportedCategories { get; set; }

        /// <summary>
        /// Gets the categories map.
        /// </summary>
        protected EnumPropertyMapper<NewTypeAheadCategories, WebElementInfo> CategoriesMap { get; } =
            EnumPropertyModelCache.GetMap<NewTypeAheadCategories, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/NewTypeAhead");

        /// <summary>
        /// The get suggestions by category name.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns> List of suggestions </returns>
        public List<TrdSuggestionModel> GetSuggestionsByCategoryName(NewTypeAheadCategories category)
        {
            if (!this.SupportedCategories.Any())
            {
                throw new Exception("Initialize SupportedCategories property for your content type component");
            }

            if (!this.SupportedCategories.Contains(category))
            {
                throw new Exception("This category is not supported within current content type tab");
            }

            return this.MapItemsToModelsByCategory(category);
        }

        /// <summary>
        /// The get tittles list.
        /// </summary>
        /// <returns> List of titles </returns>
        public List<NewTypeAheadCategories> GetTitlesList()
        {
            DriverExtensions.WaitForJavaScript();

            // Selects only sections titles 
            var sectionsTitles = DriverExtensions.GetElements(this.ComponentLocator, TitlesLocator).Where(el => el.Text.Any())
                .Select(el => el.Text).Where(title => this.CategoriesMap.Select(s => s.Value.Text).Contains(title));

            return sectionsTitles.Count() != 0
                       ? sectionsTitles.Select(
                           title => title.GetEnumValueByText<NewTypeAheadCategories>(
                               string.Empty,
                               @"Resources/EnumPropertyMaps/WestlawEdge/NewTypeAhead")).ToList()
                       : new List<NewTypeAheadCategories>();
        }

        /// <summary>
        /// The click on suggestion by text.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="linkText">
        /// The link text.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <returns> New instance of Page Object  </returns>
        public T ClickOnSuggestionByText<T>(NewTypeAheadCategories category, string linkText)
            where T : ICreatablePageObject
        {
            this.GetSuggestionElementsListByName(category).FirstOrDefault(x => x.Text.Contains(linkText))?.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is Suggeston link displayed by text.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="linkText">
        /// The link text.
        /// </param>
        /// <returns> True if displayed  </returns>
        public bool IsSuggestionLinkByTextDisplayed(NewTypeAheadCategories category, string linkText) =>
            this.GetSuggestionElementsListByName(category).FirstOrDefault(x => x.Text.Contains(linkText)).Displayed;

        /// <summary>
        /// The click on suggestion by index.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <returns> New instance of Page Object </returns>
        public T ClickOnSuggestionByIndex<T>(NewTypeAheadCategories category, int index)
            where T : ICreatablePageObject
        {
            IWebElement element = this.GetSuggestionElementsListByName(category)[index];
            DriverExtensions.WaitForJavaScript();

            if (element.TagName != "a" && element.TagName !="button")
            {
                element = DriverExtensions.GetElement(element, LinkLocator);
            }

            DriverExtensions.Click(element);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The click more link.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public virtual void ClickMoreLink(NewTypeAheadCategories category) =>
            DriverExtensions.GetElement(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text)))
                            .Click();

        /// <summary>
        /// The is more link displayed.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool IsMoreLinkDisplayed(NewTypeAheadCategories category) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text)));

        /// <summary>
        /// The map items to models by category.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns> List of Suggestion model </returns>
        protected abstract List<TrdSuggestionModel> MapItemsToModelsByCategory(NewTypeAheadCategories category);

        /// <summary>
        /// The get document list.
        /// </summary>
        /// <param name="resultLocator">
        /// The result locator.
        /// </param>
        /// <returns> List of items </returns>
        protected virtual IList<IWebElement> GetItems(params By[] resultLocator)
        {
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetElements(resultLocator).ToList();
        }

        /// <summary>
        /// The get items.
        /// </summary>
        /// <param name="lctMask">
        /// The lct mask.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The <see cref="T:IList"/>.
        /// IList
        /// </returns>
        protected virtual IList<IWebElement> GetItems(string lctMask, params string[] values) =>
            this.GetItems(SafeXpath.BySafeXpath(lctMask, values));

        /// <summary>
        /// The get suggestion list by name.
        /// </summary>
        /// <param name="category">
        /// The categories.
        /// </param>
        /// <returns>
        /// The <see cref="T:IList"/>.
        /// </returns>
        protected virtual IList<IWebElement> GetSuggestionElementsListByName(NewTypeAheadCategories category) =>
            this.GetItems(
                this.ComponentLocator,
                By.XPath(string.Format(CategoryItemLctMask, this.CategoriesMap[category].Text)));

        /// <summary>
        /// The get mapped models list.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of model
        /// </typeparam>
        /// <typeparam name="TItem">
        /// The type of item
        /// </typeparam>
        /// <returns>
        /// The <see cref="List{TModel}"/>.
        /// </returns>
        protected virtual List<TModel> GetMappedModelsList<TModel, TItem>(NewTypeAheadCategories category)
            where TItem : TrdBaseCategoryItem
            where TModel : TrdSuggestionModel =>
            this.GetSuggestionItems<TItem>(category).Select(item => item.ToModel<TModel>()).ToList();

        /// <summary>
        /// The get suggestion items.
        /// </summary>
        /// <typeparam name="TItem">
        /// The type of item
        /// </typeparam>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMappable}"/>.
        /// </returns>
        protected virtual IEnumerable<IMappable> GetSuggestionItems<TItem>(NewTypeAheadCategories category)
            where TItem : TrdBaseCategoryItem =>
            this.GetSuggestionElementsListByName(category)
                .Select(element => Activator.CreateInstance(typeof(TItem), element)).Cast<IMappable>();

        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected abstract void InitializeSupportedCategories();
    }
}