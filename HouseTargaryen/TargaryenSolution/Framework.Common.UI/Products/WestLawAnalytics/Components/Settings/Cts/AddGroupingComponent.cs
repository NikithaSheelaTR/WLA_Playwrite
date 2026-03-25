namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.Cts
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Add grouping component
    /// </summary>
    public class AddGroupingComponent : BaseModuleRegressionComponent
    {
        private static readonly By AccountNumberLocator = By.Id("GroupId");
        private static readonly By AccountNameLocator = By.Id("AccountName");
        private static readonly By AccountDescriptionLocator = By.Id("AccountDescription");
        private static readonly By ContactNameLocator = By.Id("ContactName");
        private static readonly By ContactEmailLocator = By.Id("ContactEmail");
        private static readonly By SaveButtonLocator = By.Id("SaveAccount");
        private static readonly By ReturnToButtonLocator = By.Id("Cancel");
        private static readonly By MessageLocator = By.XPath("//div[@class='co_infoBox_message']");
        private static readonly By ContainerLocator = By.Id("wa_accountSettingsAccountSetup");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Add grouping
        /// </summary>
        /// <param name="accountInformation"></param>
        public void AddGrouping(AccountInformationModel accountInformation)
        {
            DriverExtensions.WaitForElement(AccountNumberLocator).SendKeys(accountInformation.AccountNumber);
            DriverExtensions.WaitForElement(AccountNameLocator).SendKeys(accountInformation.AccountName);
            DriverExtensions.WaitForElement(AccountDescriptionLocator).SendKeys(accountInformation.AccountDescription);
            DriverExtensions.WaitForElement(ContactNameLocator).SendKeys(accountInformation.ContactName);
            DriverExtensions.WaitForElement(ContactEmailLocator).SendKeys(accountInformation.ContactEmail);
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
        }

        /// <summary>
        /// Click return to button
        /// </summary>
        /// <returns></returns>
        public CtsPage ClickReturnToButton()
        {
            DriverExtensions.WaitForElement(ReturnToButtonLocator).Click();
            return new CtsPage();
        }

        /// <summary>
        /// GetMessage
        /// </summary>
        /// <returns></returns>
        public string GetMessage() => DriverExtensions.WaitForElement(MessageLocator).Text;
    }
}
