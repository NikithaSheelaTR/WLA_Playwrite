namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.ResultList
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Litigation Analytics page result list component.
    /// </summary>
    public class LitigationAnalyticsPageResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'co_searchResultsList']");
        private static readonly By ResultListTitleLocator = By.XPath("//div[@class = 'co_searchResultsList']//h2");
        private static readonly By ResultListItemLocator = By.XPath("//div[@class ='LA_results_listItem']");

        /// <summary>
        /// Litigation Analytics page result list component.
        /// </summary>
        public LitigationAnalyticsPageResultListComponent()
        {
            DriverExtensions.WaitForElementNotPresent(By.XPath("//div[@class ='la-Loading co_clearfix la-Loading-extra-space']"), 30000);
            DriverExtensions.WaitForElementPresent(By.XPath("//div[@class ='la-Loading co_clearfix la-Loading-Success']"));
        }

        /// <summary>
        /// Get all result items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAllSearchResultItems<T>() where T : ResultListItem
        {
            DriverExtensions.WaitForElementDisplayed(ResultListItemLocator, 60000);
            return DriverExtensions.GetElements(this.ComponentLocator, ResultListItemLocator).Select(el => (T)Activator.CreateInstance(typeof(T), el));
        }

        /// <summary>
        /// Get dockets count.
        /// </summary>
        public int GetDocketsCount()
        {
            SafeMethodExecutor.WaitUntil(() => DriverExtensions.IsDisplayed(ResultListTitleLocator), 30);
            return int.Parse(Regex.Match(DriverExtensions.GetElement(ResultListTitleLocator).Text, @"[\d,]+").Value.Replace(",", ""));
        }

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

    }
}