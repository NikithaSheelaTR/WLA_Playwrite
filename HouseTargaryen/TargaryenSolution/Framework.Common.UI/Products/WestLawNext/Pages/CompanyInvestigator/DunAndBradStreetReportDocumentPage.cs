namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// DunAndBradStreet Report Document Page
    /// </summary>
    public class DunAndBradStreetReportDocumentPage : CommonReportDocumentPage
    {
        private static readonly By TitleLocator = By.Id("co_pm_headerName");

        /// <summary>
        /// Get Document Title
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string GetDocumentTitle() => DriverExtensions.WaitForElementDisplayed(TitleLocator).Text;
    }
}