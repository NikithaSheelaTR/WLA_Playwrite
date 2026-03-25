namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WestlawNext Document Page
    /// </summary>
    public class EdgeCompanyInvestigatorDocumentPage : EdgeCommonReportDocumentPage
    {
        private static readonly By TitleLocator = By.Id("title");

        /// <summary>
        /// Returns the TitleLocator of the document
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string GetDocumentTitle() => DriverExtensions.GetText(TitleLocator);
    }
}
