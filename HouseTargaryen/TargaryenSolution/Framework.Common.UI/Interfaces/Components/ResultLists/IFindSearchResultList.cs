namespace Framework.Common.UI.Interfaces.Components.ResultLists
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Items;

    /// <inheritdoc />
    /// <summary>
    /// The FindSearchResultGrid interface.
    /// todo: make this interface internal when Search Manager is implemented
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of item
    /// </typeparam>
    public interface IFindSearchResultList<TItem> : ISearchResultList<TItem>
        where TItem : BaseItem
    {
        /// <summary>
        /// The get header citations of search results.
        /// </summary>
        /// <returns>
        /// The list of citations
        /// </returns>
        IList<string> GetHeaderCitations();

        /// <summary>
        /// The get items for citation.
        /// </summary>
        /// <param name="citation">
        /// The citation.
        /// </param>
        /// <returns>
        /// The list of result items per citation.
        /// </returns>
        IList<TItem> GetItemsForCitation(string citation);
    }
}