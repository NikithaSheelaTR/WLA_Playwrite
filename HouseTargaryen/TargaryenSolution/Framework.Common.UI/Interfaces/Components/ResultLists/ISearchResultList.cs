namespace Framework.Common.UI.Interfaces.Components.ResultLists
{
    using Framework.Common.UI.Products.Shared.Items;

    /// <inheritdoc/>
    /// <summary>
    /// The SearchResultGrid interface.
    /// </summary>
    /// <typeparam name="TItem">
    /// Model type
    /// </typeparam>
    public interface ISearchResultList<out TItem> : IItemsCollection<TItem>
        where TItem : BaseItem
    {
        /// <summary>
        /// Gets the items total count from heading.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets the spell check (did you mean) text.
        /// </summary>
        string SpellCheckText { get; }

        /// <summary>
        /// Gets the results heading text.
        /// </summary>
        string ResultsHeader { get; }

        /// <summary>
        /// Gets the more info text.
        /// </summary>
        string MoreInfoText { get; }

        /// <summary>
        /// The click random search result.
        /// </summary>
        /// <param name="onlyOutOfPlan">
        /// The only out of plan.
        /// </param>
        /// <returns>
        /// The random item
        /// </returns>
        TItem GetRandomItem(bool onlyOutOfPlan = true);

        /// <summary>
        /// The click spell check (did you mean) link.
        /// </summary>
        /// <typeparam name="TPage">
        /// The page type
        /// </typeparam>
        /// <returns>
        /// The page
        /// </returns>
        TPage ClickSpellCheckLink<TPage>()
            where TPage : ICreatablePageObject;

        /// <summary>
        /// The is more info icon displayed.
        /// </summary>
        /// <returns>
        /// True if checkbox is displayed
        /// </returns>
        bool IsMoreInfoIconDisplayed();
    }
}