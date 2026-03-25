namespace Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// POM for the error page, which is displayed if the content against which the user is searching has issues such as
    /// user does not have access
    /// content is not searchable
    /// content is not available etc.
    /// </summary>
    public class SearchPortalErrorPage : BaseModuleRegressionPage
    {
        private static readonly By SearchPortalErrorLocator =
            By.XPath("id('co_searchPortalError')//div[@class='co_genericBoxContent']/h2");

        /// <summary>
        /// Returns the message of displayed error.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetErrorMessage() => DriverExtensions.WaitForElement(SearchPortalErrorLocator).Text.Trim();
    }
}