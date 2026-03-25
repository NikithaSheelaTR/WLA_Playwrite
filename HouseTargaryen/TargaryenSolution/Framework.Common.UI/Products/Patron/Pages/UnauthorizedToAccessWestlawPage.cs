namespace Framework.Common.UI.Products.Patron.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for invalid sponsor links
    /// </summary>
    public class UnauthorizedToAccessWestlawPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// The error box
        /// </summary>
        protected static readonly By ErrorMessageLocator = By.XPath("//div[@class='co_overlayBox_content']//div//h3");

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedToAccessWestlawPage"/> class. 
        /// SponsorUnauthorizedPage
        /// </summary>
        public UnauthorizedToAccessWestlawPage()
        {
            DriverExtensions.WaitForElement(ErrorMessageLocator);
        }

        /// <summary>
        /// Gets the text of the unauthorized page
        /// </summary>
        /// <returns> The message text. </returns>
        public string GetUnauthorizedMessage()
            =>
                DriverExtensions.IsDisplayed(ErrorMessageLocator, 5)
                    ? DriverExtensions.GetText(ErrorMessageLocator)
                    : string.Empty;
    }
}