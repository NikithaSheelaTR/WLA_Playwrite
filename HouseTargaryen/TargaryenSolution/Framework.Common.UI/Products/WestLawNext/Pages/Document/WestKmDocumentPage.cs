namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Document page for West KM
    /// </summary>
    public class WestKmDocumentPage : CommonDocumentPage
    {
        private static readonly By CitingReferencesTabLinkLocator = By.XPath("//li[@id='KMCitingReferencesTab'][not(contains(@class,'Inactive'))]");

        /// <summary>
        /// Verify the KM Citing References Tab is active
        /// </summary>
        /// <returns>true if KM Citing References tab is active, false otherwise</returns>
        public bool IsKmCitingReferencesTabActive() => DriverExtensions.IsDisplayed(CitingReferencesTabLinkLocator, 20);
    }
}
