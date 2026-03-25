namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.Cts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CTS Page
    /// </summary>
    public class CtsPage : BaseModuleRegressionPage
    {
        private static readonly By SearchAccountInputLocator = By.Id("wa_accountNumber");

        private static readonly By SearchButtonLocator = By.Id("wa_searchAccounts");

        private static readonly By MessageLocator = By.XPath("//div[@class='co_infoBox_inner']/div");

        /// <summary>
        /// Account Settings
        /// </summary>
        public AccountSettingsComponent AccountSettings { get; } = new AccountSettingsComponent();

        /// <summary>
        /// Manage Caps
        /// </summary>
        public ManageCapsComponent ManageCaps { get; } = new ManageCapsComponent();

        /// <summary>
        /// Enters Account or Customer Number in the search input
        /// </summary>
        /// <param name="account"> Account or Customer number </param>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public CtsPage EnterAccountOrCustomerNumber(string account)
        {
            DriverExtensions.SetTextField(account, SearchAccountInputLocator);
            return this;
        }

        /// <summary>
        /// Clicks the Search Button
        /// </summary>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public CtsPage ClickSearchButton()
        {
            DriverExtensions.Click(SearchButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// GetMessage
        /// </summary>
        /// <returns></returns>
        public string GetMessage() => DriverExtensions.WaitForElement(MessageLocator).Text;

        #region Displaying
        /// <summary>
        /// Is serach field displayed
        /// </summary>
        /// <returns></returns>
        public bool IsSearchFieldDisplayed() => DriverExtensions.IsDisplayed(SearchAccountInputLocator);

        /// <summary>
        /// Is search button displayed
        /// </summary>
        /// <returns></returns>
        public bool IsSearchButtonDisplayed() => DriverExtensions.IsDisplayed(SearchButtonLocator);
        #endregion
    }
}
