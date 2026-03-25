namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Community Page
    /// </summary>
    public class CommunityPage : CommonAuthenticatedWestlawNextPage, ICommunityPage
    {
        private static readonly By CommunityPageSignInLocator = By.XPath("//h1[text()='Sign in to Community']");

        /// <summary>
        /// Is Community page displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCommunityPageSignInDisplayed() 
            => DriverExtensions.IsDisplayed(CommunityPageSignInLocator, 5);
    }
}