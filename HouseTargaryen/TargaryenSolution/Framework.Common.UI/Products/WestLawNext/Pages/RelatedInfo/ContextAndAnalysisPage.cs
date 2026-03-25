namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Context And Analysis Page
    /// </summary>
    public class ContextAndAnalysisPage : TabPage
    {
        private static readonly By ContentHeadersLocator =
            By.XPath("//div[@id='coid_website_relatedInformationContent']//h3[@class='co_relatedInfo_contextHeading']");

        /// <summary>
        /// Get content header elements
        /// </summary>
        /// <returns>List of header values</returns>
        public List<string> GetAllContentHeaders()
            => DriverExtensions.GetElements(ContentHeadersLocator).Select(e => e.Text).ToList();
    }
}