using Framework.Common.UI.Products.Shared.Items;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    /// <summary>
    /// Opportunity Finder Table Row Item
    /// </summary>
    public class OpportunityFinderTableRowItem : BaseItem
    {
        private static string ItemLocator = "./td[{0}]/*";

        /// <summary>
        /// /// <summary>
        /// Opportunity Finder Table Row Item
        /// </summary>
        /// </summary>
        /// <param name="container"></param>
        public OpportunityFinderTableRowItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Row Item Element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IWebElement RowItemElement(int item) => DriverExtensions.GetElement(this.Container, By.XPath(string.Format(ItemLocator, item)));
    }
}