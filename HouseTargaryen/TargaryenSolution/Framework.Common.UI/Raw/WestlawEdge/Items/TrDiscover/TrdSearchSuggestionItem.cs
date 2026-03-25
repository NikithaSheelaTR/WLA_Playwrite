namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// the search suggestions item
    /// </summary>
    public class TrdSearchSuggestionItem : TrdBaseCategoryItem
    {
        private static readonly By PredicateLocator = By.XPath(".//span[@class = 'co_trd_typeahead_predicate']");

        private const string GrayColor = "rgba(86, 86, 86, 1)";

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdSearchSuggestionItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdSearchSuggestionItem(IWebElement container) 
            : base(container)
        {
        }

        /// <summary>
        /// The Predicate text.
        /// </summary>
        public string Predicate => DriverExtensions.GetElement(this.Container, PredicateLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Is color of predicate black
        /// </summary>
        public bool IsPredicateGray
            => DriverExtensions.GetElement(this.Container, PredicateLocator).GetCssValue("color").Equals(GrayColor);

        /// <summary>
        /// The Entity text.
        /// </summary>
        public string Entity => DriverExtensions.GetElement(this.Container).GetText().Replace(this.Predicate, string.Empty).Trim();
    }
}