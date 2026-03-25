namespace Framework.Common.UI.Raw.WestlawEdge.Items.Thesaurus
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Thesaurus result list item
    /// </summary>
    public class ThesaurusResultListItem : BaseItem
    {
        private static readonly By TitleLocator = By.XPath(".//li[@class='RelatedGroup']");
        private static readonly By SelectAllButtonLocator = By.XPath(".//button[contains(@class,'BtnSelectAllRelatedConcepts ') and contains(., 'Select all')]");
        private static readonly By ClearSelectedButtonLocator = By.XPath(".//button[text() ='Clear selected']");
        private static readonly By ItemsSelectedButtonLocator = By.XPath(".//li[@class='RelatedGroupSelectedCount']");
        private static readonly By SuggestionLocator = By.XPath(".//span[@class='TermWrapper']/label");

        /// <summary>
        /// Initializes a new instance of the <see cref="ThesaurusResultListItem"/> class. 
        /// </summary>
        /// <param name="container"> container </param>
        public ThesaurusResultListItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Suggestion checkboxes
        /// </summary>
        public IReadOnlyCollection<ICheckBox> SuggestionCheckBoxes =>
            new ElementsCollection<SuggestionCheckBox>(this.Container, SuggestionLocator);

        /// <summary>
        /// "Select all" button
        /// </summary>
        public IButton SelectAllButton => new CustomEdgeButton(this.Container, SelectAllButtonLocator);

        /// <summary>
        /// "Clear selected" button
        /// </summary>
        public IButton ClearSelectedButton => new Button(this.Container, ClearSelectedButtonLocator);

        /// <summary>
        /// "Items selected" button
        /// </summary>
        public IButton ItemsSelectedButton => new Button(this.Container, ItemsSelectedButtonLocator);

        /// <summary>
        ///  Group labels
        /// </summary>
        public ILabel GroupLabel => new Label(this.Container, TitleLocator);

        /// <summary>
        /// Get all suggestions
        /// </summary>
        /// <returns>
        /// List with suggestions
        /// </returns>
        public List<string> GetAllItemSuggestions() => this.SuggestionCheckBoxes.Select(suggestion => suggestion.Text).ToList();

        /// <summary>
        /// Get Text form title
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetItemTitleText() => this.IsItemTitleDisplayed() ? DriverExtensions.GetText(TitleLocator, this.Container, 0) : string.Empty;

        /// <summary>
        /// Check is 'Group' title displayed
        /// </summary>
        /// <returns>
        /// True - if title is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsItemTitleDisplayed() => DriverExtensions.IsDisplayed(this.Container, TitleLocator);
    }
}