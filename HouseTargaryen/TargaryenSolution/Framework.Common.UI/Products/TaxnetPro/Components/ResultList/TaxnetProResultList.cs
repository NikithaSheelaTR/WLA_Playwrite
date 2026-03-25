namespace Framework.Common.UI.Products.TaxnetPro.Components.ResultList
{
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResultListItem"></typeparam>
    public class TaxnetProResultList<TResultListItem> : SearchResultList<TResultListItem>
        where TResultListItem : ResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly By ResultItemLocator = By.XPath(".//li[contains(@id, 'cobalt_search_results_')]");

        private static readonly By ResultListOfPaidPerViewItemLocator = By.XPath(".//li[@class='tnp_ppv']/ancestor::li");

        private static readonly By ResultListOfBlockedItemLocator = By.XPath(".//li[@class='tnp_blocked']/ancestor::li");

        private static readonly By ResultListOfOOPItemLocator = By.XPath(".//li[contains(@class, 'co_outOfPlan')]");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public TaxnetProResultList(IWebElement container)
            : base(container, ResultItemLocator)
        {
        }

        /// <summary>
        /// Get all search result items
        /// </summary>
        /// <typeparam name="TModel"> Model </typeparam>
        /// <returns>The generic model</returns>
        public IEnumerable<TModel> GetAllSearchResultItems<TModel>()
        {
            DriverExtensions.WaitForElement(this.Container, ResultItemLocator);
            return DriverExtensions.GetElements(this.Container, ResultItemLocator).Where(x => x.Displayed)
                                   .Select(item => new EdgeResultListItem(item)).Select(x => x.ToModel<TModel>())
                                   .ToList();
        }

        /// <summary>
        /// Get the items of Paid Per View documents
        /// </summary>
        /// <typeparam name="TModel"> Model </typeparam>
        /// <returns>The generic model</returns>
        public IEnumerable<TModel> GetResultListPaidPerViewItemModels<TModel>()
        {
            return DriverExtensions.GetElements(this.Container, ResultListOfPaidPerViewItemLocator).Where(x => x.Displayed)
                                   .Select(item => new EdgeResultListItem(item)).Select(x => x.ToModel<TModel>())
                                   .ToList();
        }

        /// <summary>
        /// Get the items of Blocked documents
        /// </summary>
        /// <typeparam name="TModel"> Model </typeparam>
        /// <returns>The generic model</returns>
        public IEnumerable<TModel> GetResultListBlockedItemModels<TModel>()
        {
            return DriverExtensions.GetElements(this.Container, ResultListOfBlockedItemLocator).Where(x => x.Displayed)
                                   .Select(item => new EdgeResultListItem(item)).Select(x => x.ToModel<TModel>())
                                   .ToList();
        }

        /// <summary>
        /// Get the items of OOP documents
        /// </summary>
        /// <typeparam name="TModel"> Model </typeparam>
        /// <returns>The generic model</returns>
        public IEnumerable<TModel> GetResultListOOPItemModels<TModel>()
        {
            DriverExtensions.WaitForElement(this.Container, ResultListOfOOPItemLocator);
            return DriverExtensions.GetElements(this.Container, ResultListOfOOPItemLocator)
                                   .Select(item => new EdgeResultListItem(item)).Select(x => x.ToModel<TModel>())
                                   .ToList();
        }
    }
}