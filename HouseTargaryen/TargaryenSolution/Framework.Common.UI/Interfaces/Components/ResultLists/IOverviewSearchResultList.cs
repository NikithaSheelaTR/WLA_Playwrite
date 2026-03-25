namespace Framework.Common.UI.Interfaces.Components.ResultLists
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Items.ResultList;

    /// <summary>
    /// The OverviewResultGrid interface.
    /// </summary>
    public interface IOverviewSearchResultList
    {
        /// <summary>
        /// Gets the total search result items count.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets the the number of results sections.
        /// </summary>
        int SectionsCount { get; }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        IList<ContentType> Sections { get; }

        /// <summary>
        /// Gets the more info text.
        /// </summary>
        string MoreInfoText { get; }

        /// <summary>
        /// The Get Search Result Items By Content Type
        /// </summary>
        /// <param name="contentType">Desired content type</param>
        /// <returns>The list of items</returns>
        IList<ResultListItem> GetItems(ContentType contentType);

        /// <summary>
        /// The Get Search Result Items By Content Type
        /// </summary>
        /// <param name="contentType">Desired content type</param>
        /// <param name="index">Index of item</param>
        /// <returns>The item</returns>
        ResultListItem GetItem(ContentType contentType, int index);

        /// <summary>
        /// The Get Search Result Items By Content Type
        /// </summary>
        /// <param name="contentType">Desired content type</param>
        /// <param name="nameOrGuid">Item name or guid</param>
        /// <returns>
        /// The item
        /// </returns>
        ResultListItem GetItem(ContentType contentType, string nameOrGuid);

        /// <summary>
        /// The click view all.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="TPage">Page type</typeparam>
        /// <returns>search result page</returns>
        TPage ClickViewAll<TPage>(ContentType type) where TPage : ICreatablePageObject;

        /// <summary>
        /// The get results headers.
        /// </summary>
        /// <returns>
        /// The list of result headers
        /// </returns>
        IList<string> GetResultsHeaders();

        /// <summary>
        /// The has section.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool HasSection(ContentType type);

        /// <summary>
        /// The has view all link.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool HasViewAllLink(ContentType type);

        /// <summary>
        /// The is more info icon displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsMoreInfoIconDisplayed();
    }
}