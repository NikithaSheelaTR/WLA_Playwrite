namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// State Government Sites page
    /// </summary>
    public class StateGovernmentSitesPage : BaseGovernmentWeblinksPage
    {
        private static readonly By SiteListLocator = By.XPath("//ul[@class = 'co_genericWhiteBox']//a");

        /// <summary>
        /// Get State Government sites list
        /// </summary>
        /// <returns>list of the  State Government sites</returns>
        public List<string> GetStateGovernmentSitesList() =>
            DriverExtensions.GetElements(SiteListLocator).Select(site => site.Text).ToList();
    }
}