namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Works with the Custom Page sharing widget actions
    /// </summary>
    public class ShareCustomPageDialog : ShareCustomPageBaseDialog
    {
        private static readonly By ContactItemLocator = By.CssSelector("a[class*='ui-corner-all']");

        private static readonly By ContinueSharingButtonLocator = By.Id("co_CustomPagesShareContinue");

        private static readonly By CustomPagesContactsLinkLocator = By.Id("coCustomPagesGroups");

        private static readonly By UserInputAreaLocator = By.Id("usersSelected-tokenfield");

        /// <summary>
        /// Go to ShareCPSettingsModal using selected users
        /// </summary>
        /// <returns>The <see cref="ShareCustomPageSettingsDialog"/>.</returns>
        public ShareCustomPageSettingsDialog ClickContinue()
            => this.ClickElement<ShareCustomPageSettingsDialog>(ContinueSharingButtonLocator);

        /// <summary>
        /// Close sharing modal dialog
        /// </summary>
        /// <param name="userName">The user Name.</param>
        public void EnterUsernameForShare(string userName)
        {
            IWebElement userInputAreaIwe = DriverExtensions.WaitForElement(UserInputAreaLocator);
            userInputAreaIwe.Click();
            userInputAreaIwe.SendKeys(userName);
            DriverExtensions.WaitForElement(ContactItemLocator);
        }

        /// <summary>
        /// Open new Custom Page Contacts widget
        /// </summary>
        /// <returns>
        /// The <see cref="CustomPageContactsDialog"/>.
        /// </returns>
        public CustomPageContactsDialog OpenCustomPageContactsModal()
            => this.ClickElement<CustomPageContactsDialog>(CustomPagesContactsLinkLocator);

        /// <summary>
        /// The select first contact in list then click continue.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectContactInListByIndex(int index = 0) => DriverExtensions.GetElements(ContactItemLocator).ElementAt(index).Click();
    }
}