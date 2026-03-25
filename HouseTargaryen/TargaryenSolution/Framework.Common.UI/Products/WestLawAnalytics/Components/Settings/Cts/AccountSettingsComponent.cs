namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.Cts
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Account Settings Component
    /// </summary>
    public class AccountSettingsComponent : BaseModuleRegressionComponent
    {
        #region Account
        private const string ContainerLctMask = "//table[contains(.,'{0}')]/preceding-sibling::li[@class='accountSetup_SubAccount']//span";

        private static readonly By ExpandButtonLocator = By.XPath("//a[contains(@class,'wa_AccountShowDetailsButton')]");

        private static readonly By AccountNameLocator = By.XPath("//div[@class='accountContainer']//div[@class='accountSetup_Account']//h3");

        private static readonly By EditAccountDetailsButtonLocator = By.XPath("//div[@class='accountContainer']//ul[@class='accountSetup_SubAccountList']/button[@class='wa_AccountEditButton']");

        private static readonly By AddGroupingButtonLocator = By.XPath("//div[@class='accountContainer']//button[@class='wa_AddGrouping']");

        private static readonly By ManageCapsButtonLocator = By.XPath("//div[@class='accountContainer']//button[@class='wa_ManageCaps']");

        private static readonly By ContainerLocator = By.XPath("//div[@id='wa_accountSettingsAccountSetup']/div[@class='accountContainer']");
        #endregion

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get LocationGridModel by index
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="LocationGridModel"/>.
        /// </returns>
        public LocationGridModel GetLocationGridModelByZb(string zb) => this.GetLocationGridItemByZb(zb).ToModel<LocationGridModel>();

        #region Get 
        /// <summary>
        /// Get Account name
        /// </summary>
        /// <returns>Account name</returns>
        public string GetAccountName() => DriverExtensions.GetText(AccountNameLocator);
        #endregion

        #region Click Buttons
        /// <summary>
        /// Clicks the Manage Caps Button
        /// </summary>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public CtsPage ClickManageCapsButton()
        {
            DriverExtensions.Click(ManageCapsButtonLocator);
            return new CtsPage();
        }

        /// <summary>
        /// Clicks Add Grouping Button
        /// </summary>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public AddGroupingComponent ClickAddGroupingButton()
        {
            DriverExtensions.Click(AddGroupingButtonLocator);
            return new AddGroupingComponent();
        }

        /// <summary>
        /// Click Client and Matter button
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="ClientAndMatterPage"/>.
        /// </returns>
        public ClientAndMatterPage ClickClientAndMatterButton(string zb) =>
            this.GetLocationGridItemByZb(zb).ClickClientAndMatterButton();

        /// <summary>
        /// Click "Client Validation Settings" button 
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="ClientValidationSettingsPage"/>.
        /// </returns>
        public ClientValidationSettingsPage ClickClientValidationButton(string zb) =>
            this.GetLocationGridItemByZb(zb).ClickClientValidationButton();

        /// <summary>
        /// Click Practice Area button
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="PracticeAreaPage"/>.
        /// </returns>
        public PracticeAreaPage ClickPracticeAreaButton(string zb) =>
            this.GetLocationGridItemByZb(zb).ClickPracticeAreaButton();

        /// <summary>
        /// Click Edit Reason Code button
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="EditReasonCodePage"/>.
        /// </returns>
        public EditReasonCodePage ClickEditReasonCodeButton(string zb) =>
            this.GetLocationGridItemByZb(zb).ClickEditReasonCodeButton();

        /// <summary>
        /// Click Download Client and Matters button
        /// </summary>
        /// /// <param name="zb">
        /// Account number
        /// </param>
        public void ClickDownloadClientAndMattersButton(string zb) =>
            this.GetLocationGridItemByZb(zb).ClickDownloadClientAndMattersButton();

        /// <summary>
        /// Click Delete button
        /// </summary>
        /// <param name="zb">Account name</param>
        public void ClickDeleteButton(string zb) => this.GetLocationGridItemByZb(zb).ClickDeleteButton();

        /// <summary>
        /// Click Edit Location button
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="EditGroupingPage"/>.
        /// </returns>
        public EditGroupingPage ClickEditLocationButton(string zb) => this.GetLocationGridItemByZb(zb).ClickEditLocationButton();
        #endregion

        #region Displaying
        /// <summary>
        /// Is Edit account details button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsExpandtButtonDisplayed() =>
            DriverExtensions.IsDisplayed(ExpandButtonLocator);

        /// <summary>
        /// Is Edit account details button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsEditAccountDetailsButtonDisplayed() =>
            DriverExtensions.IsDisplayed(EditAccountDetailsButtonLocator);

        /// <summary>
        /// Is Add grouping button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsAddGroupingButtonDisplayed() =>
            DriverExtensions.IsDisplayed(AddGroupingButtonLocator);

        /// <summary>
        /// Is Manage Caps button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsManageCapsButtonDisplayed() =>
            DriverExtensions.IsDisplayed(ManageCapsButtonLocator);
        #endregion

        /// <summary>
        /// Get Location Grid Item
        /// </summary>
        /// <param name="zb">
        /// Account name
        /// </param>
        /// <returns>
        /// The <see cref="LocationGridItem"/>.
        /// </returns>
        private LocationGridItem GetLocationGridItemByZb(string zb) =>
            new LocationGridItem(DriverExtensions.SafeGetElement(By.XPath(string.Format(ContainerLctMask, zb))));
    }
}
