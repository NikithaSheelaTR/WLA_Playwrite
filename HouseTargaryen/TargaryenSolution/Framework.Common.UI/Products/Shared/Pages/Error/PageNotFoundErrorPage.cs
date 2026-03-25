namespace Framework.Common.UI.Products.Shared.Pages.Error
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Generic error page reached when WLN can't find a page
    /// </summary>
    public class PageNotFoundErrorPage : BaseModuleRegressionPage, INotFindDocument
    {
        private static readonly By ErrorPageTextLocator = By.XPath("//div[@class='co_genericBoxContent']");

        private static readonly By ErrorMessageTitleLocator = By.XPath("./h2");

        /// <summary>
        /// Header
        /// </summary>
        public WestlawNextHeaderComponent Header { get; set; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Gets the text of the error page
        /// </summary>
        /// <returns> Error text from the page </returns>
        public string GetErrorText() => DriverExtensions.GetText(ErrorPageTextLocator);

        /// <summary>
        /// Verify that header of the error message is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public string GetErrorMessageTitleText() => 
            DriverExtensions.GetElement(DriverExtensions.WaitForElement(ErrorPageTextLocator),ErrorMessageTitleLocator).Text;
    }
}