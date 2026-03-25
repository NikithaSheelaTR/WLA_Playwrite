namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Image;

    using OpenQA.Selenium;

    /// <summary>
    /// Case Document Page
    /// </summary>
    public class CaseDocumentPage : CommonDocumentPage
    {
        private static readonly By NewYorkOfficialLogoLocator = By.ClassName("co_imageNewYorkReports");

        /// <summary>
        /// New York Official Logo Image
        /// </summary>
        public IImage NewYorkOfficialLogo => new Image(NewYorkOfficialLogoLocator);
    }
}