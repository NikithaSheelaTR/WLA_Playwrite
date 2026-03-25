namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using Framework.Common.UI.Products.Shared.Dialogs.Contacts;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using OpenQA.Selenium;

    /// <summary>
    /// The Indigo "Profile" pop-up dialog (Preferences and Sign out") that is displayed when the User icon is clicked (right top corner)
    /// </summary>
    public class EdgeProfileSettingsDialog : ProfileSettingsDialog
    {
        private static readonly By ContactsLocator = By.Id("coid_website_contacts");

        private static readonly By SubscriptionLinkLocator = By.CssSelector("a#coid_website_mycontent");

        /// <summary>
        /// Clicks Contacts links from Profile dialog
        /// </summary>
        /// <returns>new <see cref="MyContactsDialog"/> class.</returns>
        public MyContactsDialog ClickContacts()
           => this.ClickElement<MyContactsDialog>(ContactsLocator);

        /// <summary>
        /// Clicks on Subscription link
        /// </summary>
        /// <returns></returns>
        public MyPlanPage ClickSubscriptionLink() => this.ClickElement<MyPlanPage>(SubscriptionLinkLocator);
    }
}