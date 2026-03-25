namespace Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WestlawNext Document Page
    /// </summary>
    public class CompanyInvestigatorDocumentPage : CommonReportDocumentPage
    {
        private static readonly By TitleLocator = By.Id("title");

        private static readonly By TitleForDandBReportsLocator = By.Id("co_pm_headerName");

        /// <summary>
        /// Returns the TitleLocator of the document
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string GetDocumentTitle() =>
            DriverExtensions.IsDisplayed(TitleLocator, 5)
                ? DriverExtensions.GetText(TitleLocator)
                : DriverExtensions.GetText(TitleForDandBReportsLocator);
    }
}