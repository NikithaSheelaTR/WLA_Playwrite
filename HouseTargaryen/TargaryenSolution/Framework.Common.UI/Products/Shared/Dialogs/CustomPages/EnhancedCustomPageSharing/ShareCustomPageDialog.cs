namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages.EnhancedCustomPageSharing
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.CustomPage.EnhancedCustomPageSharing;
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Share Custom Page Dialog
    /// </summary>
    public class ShareCustomPageDialog : ShareCustomPageBaseDialog
    {
        private const string ContactLctMask = "//a[contains(@class, 'ui-corner-all') and contains(text(), '{0}')]";

        private static readonly By ContactItemLocator = By.CssSelector("a[class*='ui-corner-all'], span.highlight");

        private static readonly By CustomPagesContactsLocator = By.Id("coCustomPagesGroups");

        private static readonly By AssignSharingButtonLocator = By.Id("co_CustomPagesShareContinue");

        private static readonly By UserInputAreaLocator = By.XPath("//*[@id='usersSelected-tokenfield']|//*[@id='usersSelected-ts-control']");

        /// <summary>
        /// Click on Assign button
        /// </summary>
        /// <returns>The <see cref="AssignTabComponent"/>.</returns>
        public AssignTabComponent ClickAssignButton() => this.ClickElement<AssignTabComponent>(AssignSharingButtonLocator);

        /// <summary>
        /// Click on Custom Pages Contacts link
        /// </summary>
        /// <returns>The <see cref="ContactsDialog"/>.</returns>
        public CustomPageContactsDialog ClickCustomPagesContactsLink() => this.ClickElement<CustomPageContactsDialog>(CustomPagesContactsLocator);

        /// <summary>
        /// Enter name of contact for sharing 
        /// </summary>
        /// <param name="userName">The user Name.</param>
        /// <returns>The <see cref="ShareCustomPageDialog"/>.</returns>
        public ShareCustomPageDialog EnterUsernameForShare(string userName)
        {
            DriverExtensions.WaitForElement(UserInputAreaLocator).SendKeys(userName);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(ContactItemLocator);
           return new ShareCustomPageDialog();
        }

        /// <summary>
        /// The select contact by index
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectContactByIndex(int index = 0)
            => DriverExtensions.GetElements(ContactItemLocator).ElementAt(index).Click();

        /// <summary>
        /// The select contact by name
        /// </summary>
        /// <param name="name">The index.</param>
        public void SelectContactByName(string name)
            => DriverExtensions.Click(By.XPath(string.Format(ContactLctMask, name)));

    }
}
