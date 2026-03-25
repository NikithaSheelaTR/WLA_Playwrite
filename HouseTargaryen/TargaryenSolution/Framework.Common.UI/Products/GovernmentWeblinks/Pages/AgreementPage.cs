namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The weblinks page for agreement license
    /// </summary>
    public class AgreementPage : BaseGovernmentWeblinksPage
    {
        private static readonly By ContinueButtonLocator = By.XPath("//input[@class='co_btnCenter co_formBtnGreen']");

        private static readonly By IagreeLocator = By.XPath("//input[@type='radio' and @id='Agree']");

        /// <summary>
        /// Agrees the license
        /// </summary>
        /// <typeparam name="T">ICreatablePageObject</typeparam>
        /// <returns>The instance of the page</returns>
        public T AgreeLicense<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(IagreeLocator).Click();
            DriverExtensions.WaitForElement(ContinueButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
