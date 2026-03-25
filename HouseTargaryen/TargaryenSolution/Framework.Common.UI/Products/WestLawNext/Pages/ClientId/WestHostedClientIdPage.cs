namespace Framework.Common.UI.Products.WestLawNext.Pages.ClientId
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// West Hosted Client Id page
    /// </summary>
    public class WestHostedClientIdPage : BaseModuleRegressionPage
    {
        private static readonly By ClientIdInputLocator = By.Id("clientidr");
        private static readonly By SubmitButtonLocator = By.Id("button");

        /// <summary>
        /// Enter Client Id and click search button
        /// </summary>
        /// <param name="clientId">Client Id</param>
        /// <returns></returns>
        public CommonSearchHomePage EnterClientIdAndClickSubmit(string clientId)
        {
            DriverExtensions.WaitForElement(ClientIdInputLocator).SendKeys(clientId);
            DriverExtensions.WaitForElement(SubmitButtonLocator).Click();
            return new CommonSearchHomePage();
        }
    }
}
