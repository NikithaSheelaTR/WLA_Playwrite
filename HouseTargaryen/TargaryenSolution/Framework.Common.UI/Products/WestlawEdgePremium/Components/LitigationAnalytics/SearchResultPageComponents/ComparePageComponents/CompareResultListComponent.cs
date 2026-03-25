namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.SearchResultPageComponents.ComparePageComponents
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// CompareResultListComponent
    /// </summary>
    public class CompareResultListComponent : PrecisionResultListComponent
    {
        private static readonly By ResultListItemLocator = By.XPath("//li[@class ='la-searchResultsDisplayItems']");
        private static readonly By ContainerLocator = By.XPath("//ol[@class ='co_searchResult_list']");

        /// <summary>
        /// CompareResultListComponent
        /// </summary>
        public CompareResultListComponent(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// ResultList
        /// </summary>
        public List<LawFirmsResultListItem> ResultListItems => new ItemsCollection<LawFirmsResultListItem>(ComponentLocator, ResultListItemLocator).ToList();

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}