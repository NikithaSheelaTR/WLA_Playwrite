namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Sort By Dropdown
    /// </summary>
    public class SortByDropdown : Dropdown<SearchSortOption>
    {
        private const string DefaultPostfix = "(Default)";

        private const string SearchOptionLctMask =
            "//select[@id='co_search_sortOptions']//option[contains(text(), {0})]";

        private static readonly By SortBySelectElement = By.XPath("//select[contains(@id,'sort') or contains(@class,'Sort')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SortByDropdown" /> class.
        /// </summary>
        public SortByDropdown() : base(SortBySelectElement)
        {
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// This method gets current sort type
        /// </summary>
        /// <returns> Sort Type </returns>
        public override SearchSortOption SelectedOption => this.GetOptionByText(this.SelectedOptionText);

        /// <summary>
        /// Verifies is current sort type default
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsCurrentSortTypeDefault() => this.SelectedOptionText.Contains(DefaultPostfix);

        /// <summary>
        /// Sets the Sort Type on the search results page
        /// </summary>
        /// <param name="sortType"> the sort option to select </param>
        public override void SelectOption(SearchSortOption sortType)
        {
            string sort = this.Map[sortType].Text;
            if (DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(SearchOptionLctMask, $"{sort} {DefaultPostfix}")))
            {
                sort += DefaultPostfix;
            }

            this.SelectOption(sort);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Sets the Sort Type on the search results page
        /// </summary>
        /// <param name="optionText"> Option test </param>
        /// <typeparam name="TPage"> New instance of the page </typeparam>
        /// <returns> A new Search Results page, in case the page refreshes </returns>
        public TPage SelectOption<TPage>(string optionText) where TPage : ICreatablePageObject
        {
            this.SelectOption(optionText);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Get option by name
        /// </summary>
        /// <param name="text"> Search Sort Option's text </param>
        /// <returns> Option </returns>
        protected override SearchSortOption GetOptionByText(string text)
        {
            string optionText =
                text.Replace($" {DefaultPostfix}", string.Empty);
            return base.GetOptionByText(optionText);
        }

        /// <summary>
        /// Select Option
        /// </summary>
        /// <param name="optionText">The desired option</param>
        private void SelectOption(string optionText) => DriverExtensions.SelectElementInListByText(SortBySelectElement, optionText);
    }
}