namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation.History
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BillDraftsPage
    /// </summary>
    public class BillDraftsPage : HistoryPage
    {
        private static readonly By ContentIdLocator = By.Id("coid_relatedInfo_resultList_documentLink_0");
        
        /// <summary>
        /// Determines if content is returned.
        /// </summary>
        /// <returns>True if content is returned.</returns>
        public bool IsContentDisplayed() => DriverExtensions.IsDisplayed(ContentIdLocator, 5);
    }
}