namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BillDraftsPage
    /// </summary>
    public class BillDraftsPage : TabPage
    {
        private static readonly By BillDraftsTitleLocator = By.ClassName("co_relatedInfo_billDrafts_title");

        private static readonly By MainContentWrapperLocator = By.Id("co_relatedInfo_mainContentWrapper");

        /// <summary>
        /// Get text all bill draft links
        /// </summary>
        /// <returns>List of text for the links</returns>
        public IList<string> GetAllBillDraftLinks()
            => DriverExtensions.GetElements(this.ContentResultContainer, BillDraftsTitleLocator)
               .Select(res => res.Text).ToList();

        /// <summary>
        /// Verify main content wrapper is on the page
        /// </summary>
        /// <returns>
        /// True - if main content wrapper is displayed, false - otherwise
        /// </returns>
        public bool IsMainContentWrapperDisplayed() => DriverExtensions.IsDisplayed(MainContentWrapperLocator, 5);
    }
}