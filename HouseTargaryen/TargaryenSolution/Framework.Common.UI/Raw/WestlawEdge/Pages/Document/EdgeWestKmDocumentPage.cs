using System;
namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeWestKmDocumentPage
    /// </summary>
    public class EdgeWestKmDocumentPage: EdgeCommonDocumentPage
    {
        private static readonly By CitingReferencesTabLinkLocator = By.XPath("//li[@id='KMCitingReferencesTab'][not(contains(@class,'Inactive'))]");

        /// <summary>
        /// Verify the KM Citing References Tab is active
        /// </summary>
        /// <returns> True if KM Citing References tab is active, false otherwise</returns>
        public bool IsKmCitingReferencesTabActive() => DriverExtensions.WaitForElement(CitingReferencesTabLinkLocator).Displayed;
        
    }
}
