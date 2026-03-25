namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The unable to find document page.
    /// </summary>
    public class UnableToFindDocumentPage : CommonAuthenticatedWestlawNextPage, INotFindDocument
    {
        private static readonly By ErrorUnableToFindDocumentLocator = By.XPath("//div[@class='co_infoBox_message']/h2");

        private static readonly By FlagImageLocator = By.CssSelector("#co_docFixedHeader .co_citatorFlag img");

        /// <summary>
        /// GetUnableToFindDocumentErrorMessage
        /// </summary>
        /// <returns>string
        /// </returns>
        public string GetUnableToFindDocumentErrorMessage()
            => DriverExtensions.GetText(ErrorUnableToFindDocumentLocator);

        /// <summary>
        /// Check if Key Cite Flag displayed on the unable to find document page
        /// </summary>
        /// <returns> True if flag displayed, false otherwise </returns>
        public bool IsKeyCiteFlagDisplayed() => DriverExtensions.IsDisplayed(FlagImageLocator);
    }
}