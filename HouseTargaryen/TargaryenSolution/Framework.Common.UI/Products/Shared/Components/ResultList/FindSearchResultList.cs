namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The find search result grid.
    /// </summary>
    /// <typeparam name="TSearchResultItem">
    /// the type of item
    /// </typeparam>
    public sealed class FindSearchResultList<TSearchResultItem>
        : SearchResultList<TSearchResultItem>, IFindSearchResultList<TSearchResultItem>
        where TSearchResultItem : ResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private const string ItemsForCitationLctMask
            = ".//h2[@id='cobalt_citation_search_{0}_results_header']/..//a[@docguid]";

        private static readonly By ResultsSection = By.XPath(".//div[contains(@id,'cobalt_citation_search_results_')]");

        private static readonly By SectionCitationLocator = By.XPath(".//strong");

        /// <inheritdoc />
        public FindSearchResultList(IWebElement container)
            : base(container)
        {
        }

        /// <inheritdoc />
        public IList<string> GetHeaderCitations() =>
            DriverExtensions.GetElements(this.Container, ResultsSection, SectionCitationLocator)
                            .Select(citation => citation.Text).ToList();

        /// <inheritdoc />
        public IList<TSearchResultItem> GetItemsForCitation(string citation) =>
            DriverExtensions.GetElements(this.Container, By.XPath(string.Format(ItemsForCitationLctMask, citation)))
                            .Where(el => el.Displayed).Select(
                                element => (TSearchResultItem)Activator.CreateInstance(
                                    typeof(TSearchResultItem),
                                    element.GetParentElement("li"))).ToList();
    }
}