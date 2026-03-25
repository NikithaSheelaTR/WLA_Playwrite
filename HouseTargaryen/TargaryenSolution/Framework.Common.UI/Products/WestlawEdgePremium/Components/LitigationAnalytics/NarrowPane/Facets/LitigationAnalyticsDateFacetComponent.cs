namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using OpenQA.Selenium;
    using System;
    using System.Linq;

    /// <summary>
    /// Litigation Analytics Date Facet Component
    /// </summary>
    public class LitigationAnalyticsDateFacetDialog : LitigationAnalyticsFacetDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'co_divider co_entry_facet']");
        private static readonly By DateExpandButtonLocator = By.XPath(".//button[@id ='co_la_dateRange_filterCategory']");
        private static readonly By DateRangeOptionDropdownLocator = By.XPath(".//div[@class = 'la-DateFilter']/select");
        private static readonly By DateFacetOutputLocator = By.XPath(".//span[@class = 'SearchFacet-outputText']");
        private string dateFormat = "mm/dd/yyyy";

        /// <summary>
        /// Litigation Analytics Date Facet Component
        /// </summary>
        public LitigationAnalyticsDateFacetDialog()
        {

        }

        /// <summary>
        /// Date Facet expand button
        /// </summary>
        public IButton DateExpandFacetButton => new Button(this.ComponentLocator, DateExpandButtonLocator);

        /// <summary>
        /// Gets the Date Range dropdown.
        /// </summary>
        public IDropdown<LitigationAnalyticsDateRangeOptions> DateRangeOptionDropdown { get; } = new Dropdown<LitigationAnalyticsDateRangeOptions>(ContainerLocator, DateRangeOptionDropdownLocator);

        /// <summary>
        /// Date Facet expand button
        /// </summary>
        public ILabel DateFacetOutput => new Label(this.ComponentLocator, DateFacetOutputLocator);

        ///<summary>
        ///Gets facet label text
        /// </summary>
        public int GetDateFacetOutputOption()
        {
            string DateOutputString = DateFacetOutput.Text.Substring(25);
            string[] SplitDate = DateOutputString.Split('-');

            int startDate = DateTime.ParseExact(SplitDate.First(), dateFormat, null).Year;
            int endtDate = DateTime.ParseExact(SplitDate.Last(), dateFormat, null).Year;

            return (endtDate - startDate);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}