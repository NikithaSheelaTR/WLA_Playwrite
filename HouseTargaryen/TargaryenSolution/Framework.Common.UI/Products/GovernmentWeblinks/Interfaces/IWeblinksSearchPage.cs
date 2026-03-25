namespace Framework.Common.UI.Products.GovernmentWeblinks.Interfaces
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;

    /// <summary>
    /// Interface for weblinks search pages
    /// </summary>
    public interface IWeblinksSearchPage
    {
        /// <summary>
        /// Input search criteria.
        /// </summary>
        /// <returns>
        /// The list of strings.
        /// </returns>
        List<string> GetQuery();

        /// <summary>
        /// The search.
        /// </summary>
        /// <param name="query">
        /// The criteria.
        /// </param>
        /// <typeparam name="T">
        /// The instance of the page
        /// </typeparam>
        /// <returns>
        /// ICreatablePageObject
        /// </returns>
        T Search<T>(List<string> query) where T : ICreatablePageObject;
    }
}