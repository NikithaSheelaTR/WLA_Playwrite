namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.Cts
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edit Grouping Component
    /// </summary>
    public class EditGroupingComponent : BaseModuleRegressionComponent
    {
        private static readonly By DescriptionLocator = By.Id("AddSubAccountMemberName");
        private static readonly By AccountNumberLocator = By.Id("AddSubAccountMemberGroupId");
        private static readonly By AddButtonLocator = By.Id("AddSubAccountMember");
        private static readonly By DeleteButtonLocator = By.XPath("//button[@class='wa_SubAccountMemberDeleteButton']");
        private static readonly By MessageLocator = By.XPath("//div[@class='co_infoBox_message']");
        private static readonly By AddEditLinkLocator = By.Id("EditSubAccountMembers");
        private static readonly By ContainerLocator = By.Id("wa_accountSettingsAccountSetup");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Add/Edit link locator
        /// </summary>
        public void ClickAddEditLinkLocator() => DriverExtensions.WaitForElement(AddEditLinkLocator).Click();

        /// <summary>
        /// Add subaccount
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="accountNumber">Account number</param>
        public void AddSubaccount(string description, string accountNumber)
        {
            DriverExtensions.WaitForElement(DescriptionLocator).SendKeys(description);
            DriverExtensions.WaitForElement(AccountNumberLocator).SendKeys(accountNumber);
            DriverExtensions.WaitForElement(AddButtonLocator).Click();
        }

        /// <summary>
        /// Click Delete button
        /// </summary>
        public void ClickDeleteButton() => DriverExtensions.WaitForElement(DeleteButtonLocator).Click();

        /// <summary>
        /// GetMessage
        /// </summary>
        /// <returns></returns>
        public string GetMessage() => DriverExtensions.WaitForElement(MessageLocator).Text;
    }
}