namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Litigation Analytics Opportunity Finder Customized Report Page
    /// </summary>
    public class LitigationAnalyticsOpportunityFinderCustomizedReportPage : LitigationAnalyticsOpportunityFinderProfilerPage
    {
        private static readonly By CustomizedReportTableLocator = By.XPath("//table[@class='la-OpFinder-table']");
        private static readonly By CustomizedReportTableColumnLocator = By.XPath("//table[@class='la-OpFinder-table']/thead/tr/th");
        private static readonly By CustomizedReportTableRowLocator = By.XPath("./tbody/tr");
        private static readonly By NoDataMessageLocator = By.ClassName("la-Loading-noDataMessage");
        private static readonly By RowsPerPageLocator = By.Id("combobox1");
        private static readonly By DownloadReportButtonLocator = By.XPath("//button[contains(@class, 'la-OpFinder-downloadButton')]");
        private static readonly By LimitMessageLocator = By.XPath("//*[@class ='message']");
        private static string RowsPerPageLocatorMask = "//div[contains(text(),'{0}') and @role = 'option']";

        /// <summary>
        /// Gets the Litigation Analytics tabs enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<OpportunityFinderColumnsToDisplay, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<OpportunityFinderColumnsToDisplay, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Download Report Button
        /// </summary>
        public IButton DownloadReportButton => new Button(DownloadReportButtonLocator);

        /// <summary>
        /// Limit Message
        /// </summary>
        public ILabel LimitMessageLabel => new Label(LimitMessageLocator);

        /// <summary>
        /// Table Row Items
        ///</summary>
        public List<OpportunityFinderTableRowItem> TableRowItems => new ItemsCollection<OpportunityFinderTableRowItem>(CustomizedReportTableLocator, CustomizedReportTableRowLocator).ToList();

        /// <summary>
        /// SelectTableRowItem
        /// </summary>
        public IWebElement SelectTableRowItem(OpportunityFinderColumnsToDisplay column)
        {
            var tableHeader = DriverExtensions.GetElements(CustomizedReportTableColumnLocator).ToList();
            var tableRows = new ItemsCollection<OpportunityFinderTableRowItem>(CustomizedReportTableLocator, CustomizedReportTableRowLocator).ToList();
            var rawIndex = tableHeader.FindIndex(item => item.Text.Contains(this.TabsMap[column].Text));

            if (column == OpportunityFinderColumnsToDisplay.WebsiteUrl)
            {
                var matchingRow = tableRows.FirstOrDefault(item =>
                {
                    var text = item.RowItemElement(rawIndex + 1).Text;
                    return !string.IsNullOrWhiteSpace(text) && (text.Contains("http") || text.Contains("www") || text.Contains(".com") || text.Contains(".org") || text.Contains(".net"));
                });

                if (matchingRow == null)
                {
                    throw new InvalidOperationException($"No table rows found with a valid Website URL. Total rows: {tableRows.Count}");
                }

                return matchingRow.RowItemElement(rawIndex + 1);
            }
            else
            {
                return tableRows.First().RowItemElement(rawIndex + 1); // Explicitly return the IWebElement
            }

        }

        /// <summary>
        /// SelectTableRowCountPerPage
        /// </summary>
        /// <param name="countPerPage"></param>
        public void SelectTableRowCountPerPage(OpportunityFinderRowCountPerPage countPerPage)
        {
            DriverExtensions.Click(RowsPerPageLocator);
            DriverExtensions.Click(By.XPath(string.Format(RowsPerPageLocatorMask, Convert.ToInt32(countPerPage))));
        }

        /// <summary>
        /// Is No Data Message Displayed
        ///</summary>
        public bool IsNoDataMessageDisplayed()
        {
            return DriverExtensions.IsDisplayed(NoDataMessageLocator);
        }
    }
}