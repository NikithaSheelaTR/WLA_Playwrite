namespace Framework.Common.UI.Products.Concourse.Pages.Base
{
    using Framework.Common.UI.Products.Concourse.Components;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base concourse page.
    /// </summary>
    public abstract class BaseConcoursePage : BaseModuleRegressionPage
    {
        private static readonly By SignOutButtonLocator = By.LinkText("Sign Out");

        private static readonly By GlobalProfileLocator = By.XPath("//div[@id='globalProfile']/a");

        private HeaderComponent header;

        /// <summary>
        /// Header
        /// </summary>
        public HeaderComponent Header => this.header ?? (this.header = new HeaderComponent());

        /// <summary>
        /// SignOff
        /// </summary>
        public void ClickSignOff()
        {
            DriverExtensions.WaitForElement(GlobalProfileLocator).Click();
            DriverExtensions.WaitForElement(SignOutButtonLocator).Click();
        }
    }
}