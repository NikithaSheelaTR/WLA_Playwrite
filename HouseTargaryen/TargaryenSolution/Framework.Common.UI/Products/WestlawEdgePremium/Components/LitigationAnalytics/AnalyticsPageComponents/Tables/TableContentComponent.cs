namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Tables
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Table Content Component
    /// </summary>
    public class TableContentComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Table-container']");
        private static readonly By TableHeaderLocator = By.XPath(".//th[@scope= 'col']");
        private static readonly By ShowMoreButtonTableViewLocator = By.XPath(".//button[@class='la-Chart-showMore co_secondaryBtn']");
        private static readonly By ShowFullTableLinkLocator = By.XPath(".//a[@class ='co_linkOut']");
        private static readonly By TableItemLocator = By.XPath("//table[contains(@class, 'la-Table')]/tbody/tr");

        /// <summary>
        /// Table Content Component
        /// </summary>
        public TableContentComponent()
        {
            DriverExtensions.WaitForElementNotPresent(By.XPath("//div[@class ='la-Loading co_clearfix la-Loading-extra-space']"), 30000);
        }

        /// <summary>
        /// TableHeader
        /// </summary>
        public List<string> TableHeaderTitles => TableHeaderItems.Select(header => header.Text).ToList();

        /// <summary>
        /// Show more button for tables
        /// </summary>
        public IButton ShowMoreButtonTableView => new Button(this.ComponentLocator, ShowMoreButtonTableViewLocator);

        ///<summary>
        ///Show Full table.
        ///<typeparam name="T">T</typeparam>
        ///<returns>New instance of the page</returns>
        ///</summary>
        public T ClickOnShowFullTable<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator, ShowFullTableLinkLocator);
            new Link(this.ComponentLocator, ShowFullTableLinkLocator).Click();
            DriverExtensions.WaitForNewTabLoaded(1, 30);
            string tabName = BrowserPool.CurrentBrowser.TabHandles.Last();
            BrowserPool.CurrentBrowser.CreateTab(tabName);
            BrowserPool.CurrentBrowser.ActivateTab(tabName);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the chart content list.
        /// </summary>
        /// <value>
        /// The recent research list.
        /// </value>
        private IList<IWebElement> TableHeaderItems =>
            DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator), TableHeaderLocator);

        /// <summary>
        /// Gets the chart content list.
        /// </summary>
        /// <value>
        /// The recent research list.
        /// </value>
        public List<TableContentItem> TableContentItemsList =>
            DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator), TableItemLocator)
            .Select(element => new TableContentItem(element))
            .ToList();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}