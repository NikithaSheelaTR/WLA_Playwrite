namespace Framework.Common.UI.Products.Shared.Pages.Error
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page for unauthorized access to WestlawNext things that show an error message
    /// </summary>
    public class UnauthorizedPage : BaseModuleRegressionPage, INotFindDocument
    {
        private static readonly By ErrorMessageTextLocator = By.XPath("//div[@id='co_blockedBox']/div[@class='co_genericBoxContent']//p");

        private static readonly By BlockDocumentTitleLocator = By.ClassName("co_blockDocumentTitle");

        /// <summary>
        /// Gets the text of the unauthorized page
        /// </summary>
        /// <returns>The message text.</returns>
        public string GetUnauthorizedText()
            => DriverExtensions.IsDisplayed(ErrorMessageTextLocator, 5) ? DriverExtensions.GetText(ErrorMessageTextLocator) : string.Empty;

        /// <summary>
        /// Returns true if the blocked message is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsBlockedContentMessageDisplayed() => DriverExtensions.IsDisplayed(BlockDocumentTitleLocator, 5);
    }
}