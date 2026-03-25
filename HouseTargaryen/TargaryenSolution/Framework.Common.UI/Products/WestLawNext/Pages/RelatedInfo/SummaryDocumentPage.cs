namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Summary Document Page
    /// </summary>
    public class SummaryDocumentPage : TabPage
    {     
        private static readonly By DocumentLocator = By.XPath("//li[@id='DocumentTab']//li");

        private static readonly By DocumentTabExpanderLocator = By.Id("co_MedLitNavAnchor");

        /// <summary>
        /// sometimes a summary document will have multiple summary versions/pages
        /// </summary>
        /// <returns> list of summary documents </returns>
        public List<string> GetListOfSummaryDocuments()
        {
            DriverExtensions.WaitForElementDisplayed(DocumentTabExpanderLocator).Click();
            return DriverExtensions.GetElements(DocumentLocator).Select(element => element.Text).ToList();
        }
    }
}