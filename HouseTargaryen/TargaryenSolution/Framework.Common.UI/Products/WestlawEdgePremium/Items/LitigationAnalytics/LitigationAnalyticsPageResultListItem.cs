using Framework.Common.UI.Products.Shared.Items.ResultList;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    /// <summary>
    /// Litigation Analytics Page Result List Item
    /// </summary>
    public class LitigationAnalyticsPageResultListItem : ResultListItem
    {
        private static readonly By TargetSickName = By.XPath(".//h4[contains(text(),'Target')]/parent::div//span[not(contains(@class, 'searchResults_detailsBox__content'))]");


        /// <summary>
        /// Litigation Analytics Page Result List Item
        /// </summary>
        public LitigationAnalyticsPageResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Get Target Sic Name
        /// </summary>
        public string GetTargetSicName => DriverExtensions.GetElement(this.Container, TargetSickName).Text;
    }
}