namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.SearchResultPageComponents.ComparePageComponents
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// CompanyResultListComponent
    /// </summary>
    public class CompanyResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("my-selections");
        private static readonly By CompanyResultListItemLocator = By.XPath("//li[@class='CompanySearchResults-listItem']");

        /// <summary>
        /// ResultList
        /// </summary>
        public List<CompanyResultListItem> CompaniesResultListItems => new ItemsCollection<CompanyResultListItem>(ComponentLocator, CompanyResultListItemLocator).ToList();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}