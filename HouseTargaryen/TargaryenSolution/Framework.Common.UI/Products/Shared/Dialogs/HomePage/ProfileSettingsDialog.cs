namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog, that pops out, when you click on Profile Settings Tab (right top corner)
    /// </summary>
    public class ProfileSettingsDialog : BaseModuleRegressionDialog
    {
        private static readonly By MyContentLocator = By.Id("coid_website_mycontent");

        private static readonly By UserEmailLocator = By.XPath("//div[@class='co_signOff_profile'] /ul");

        private static readonly By WestlawPreferencesLocator = By.Id("coid_website_userSetting");

        private static readonly By MyTrLinkLocator = By.XPath("//a[text()='MyTR']");

        private static readonly By SignOffButtonLocator = By.XPath("//*[contains(@class, 'js_website_signoff')] | //li[@id = 'co_oneClickSignoutContainer']");

        private static readonly By TrainingAndSupportLocator = By.Id("coid_website_trainingAndSupport");

        private static readonly By AiFeaturePermissionsLocator = By.Id("coid_website_ai_admin_controls");

        /// <summary>
        /// Clicks My Content from Profile Settings in the header
        /// </summary>
        /// <returns>new my plan object</returns>
        public MyPlanPage ClickMyContent() => this.ClickElement<MyPlanPage>(MyContentLocator);

        /// <summary>
        /// Clicks Westlaw Preferences from Profile Settings in the header
        /// </summary>
        /// <returns>new preferences dialog object</returns>
        public T ClickWestlawPreferences<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(WestlawPreferencesLocator);

        /// <summary>
        /// Get User Email
        /// </summary>
        /// <returns>User Email</returns>
        public string GetUserEmail() => DriverExtensions.GetText(UserEmailLocator);

        /// <summary>
        /// Is MyContent present in Profile Settings
        /// </summary>
        /// <returns>true if link is present</returns>
        public bool IsMyContentDisplayed() => DriverExtensions.IsDisplayed(MyContentLocator);

        /// <summary>
        /// Is MyTr link displayed
        /// </summary>
        /// <returns>true if link is present</returns>
        public bool IsMyTrLinkDisplayed() => DriverExtensions.IsDisplayed(MyTrLinkLocator);

        /// <summary>
        /// Clicks the SignOff link
        /// </summary>
        /// <returns>The <see cref="CommonSignOffPage"/>.</returns>
        public CommonSignOffPage ClickSignOff() => this.ClickElement<CommonSignOffPage>(SignOffButtonLocator);

        /// <summary>
        /// Training and Support Link
        /// </summary>
        public ILink TrainingAndSupportLink => new Link(TrainingAndSupportLocator);

        /// <summary>
        /// Ai Feature Permissions link
        /// </summary>
        public ILink AiFeaturePermissionsLink => new Link(AiFeaturePermissionsLocator);
    }
}