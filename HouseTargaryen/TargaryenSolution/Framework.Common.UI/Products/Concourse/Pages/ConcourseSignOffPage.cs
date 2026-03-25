namespace Framework.Common.UI.Products.Concourse.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Concourse SignOff Page
    /// </summary>
    public class ConcourseSignOffPage : BaseModuleRegressionPage, ICommonSignOffPage
    {
        private static readonly By SignInAgainLinkLocator = By.LinkText("Sign in again");

        /// <summary>
        /// Click on SignOn
        /// </summary>
        /// <typeparam name="T">Page type to return</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickSignOn<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SignInAgainLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}