namespace Framework.Common.UI.Products.WestLawAnalytics.Items
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Location Grid Item
    /// </summary>
    public class LocationGridItem : BaseItem
    {
        #region Location
        private static readonly By EditClientValidationButtonLocator = By.XPath(".//button[contains(@class,'wa_ClientValidationEditButton')]");

        private static readonly By EditLocationButtonLocator = By.XPath(".//button[@name = 'editGrouping']");

        private static readonly By DeleteButtonLocator = By.XPath(".//button[contains(@class,'wa_AccountDeleteButton')]");

        private static readonly By ClientAndMatterButtonLocator = By.XPath(".//button[@class='wa_clientMatter']");

        private static readonly By ReasonCodeButtonLocator = By.XPath(".//button[@class='wa_reasonCode']");

        private static readonly By PracticeAreaButtonLocator = By.XPath(".//button[@class='wa_practiceArea']");

        private static readonly By ExtractCurrentListsButtonLocator = By.XPath(".//button[@class='wa_extractCurrentLists']");

        private static readonly By LocationNameLocator = By.XPath("./preceding-sibling::h3");

        private static readonly By DownloadClientAndMattersButtonLocator = By.XPath(".//button[@class='wa_extractCurrentLists']");
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationGridItem"/> class. 
        /// Location Grid Item constructor
        /// </summary>
        /// <param name="container"> The container. </param>
        public LocationGridItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Location name
        /// </summary>
        /// <returns>Location name</returns>
        public string LocationName =>
            DriverExtensions.GetTextSafe(this.Container, LocationNameLocator);

        /// <summary>
        /// Is location displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsLocationDisplayed => this.Container != null && this.Container.Displayed;

        /// <summary>
        /// Is Client and Matter button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsClientAndMatterButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, ClientAndMatterButtonLocator);

        /// <summary>
        /// Is Reason Code button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsReasonCodeButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, ReasonCodeButtonLocator);

        /// <summary>
        /// Is Practice Area button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsPracticeAreaButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, PracticeAreaButtonLocator);

        /// <summary>
        /// Is Extract Current List button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsExtractCurrentListButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, ExtractCurrentListsButtonLocator);

        /// <summary>
        /// Is Delete button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsDeleteButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, DeleteButtonLocator);

        /// <summary>
        /// Is Add Location button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsEditLocationButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, EditLocationButtonLocator);

        /// <summary>
        /// Is edit client validation button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsEditClientValidationButtonDisplayed =>
            DriverExtensions.IsDisplayed(this.Container, EditClientValidationButtonLocator);

        /// <summary>
        /// Click Client and Matter button
        /// </summary>
        /// <returns>
        /// The <see cref="ClientAndMatterPage"/>.
        /// </returns>
        public ClientAndMatterPage ClickClientAndMatterButton()
        {
            DriverExtensions.GetElement(this.Container, ClientAndMatterButtonLocator).Click();
            return new ClientAndMatterPage();
        }

        /// <summary>
        /// Click Practice Area button
        /// </summary>
        /// <returns>
        /// The <see cref="PracticeAreaPage"/>.
        /// </returns>
        public PracticeAreaPage ClickPracticeAreaButton()
        {
            DriverExtensions.GetElement(this.Container, PracticeAreaButtonLocator).Click();
            return new PracticeAreaPage();
        }

        /// <summary>
        /// Click Edit Reason Code button
        /// </summary>
        /// <returns>
        /// The <see cref="PracticeAreaPage"/>.
        /// </returns>
        public EditReasonCodePage ClickEditReasonCodeButton()
        {
            DriverExtensions.GetElement(this.Container, ReasonCodeButtonLocator).Click();
            return new EditReasonCodePage();
        }

        /// <summary>
        /// Click Client Validation Settings button
        /// </summary>
        /// <returns>
        /// The <see cref="ClientValidationSettingsPage"/>.
        /// </returns>
        public ClientValidationSettingsPage ClickClientValidationButton()
        {
            DriverExtensions.GetElement(this.Container, EditClientValidationButtonLocator).Click();
            return new ClientValidationSettingsPage();
        }

        /// <summary>
        /// Click Delete Button
        /// </summary>
        public void ClickDeleteButton()
        {
            DriverExtensions.GetElement(this.Container, DeleteButtonLocator).Click();
            BrowserPool.CurrentBrowser.Driver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Click Edit Location Button
        /// </summary>
        /// <returns>
        /// The <see cref="EditGroupingPage"/>.
        /// </returns>
        public EditGroupingPage ClickEditLocationButton()
        {
            DriverExtensions.GetElement(this.Container, EditLocationButtonLocator).Click();
            return new EditGroupingPage();
        }

        /// <summary>
        /// Click Download Client and Matters button
        /// </summary>
        public void ClickDownloadClientAndMattersButton() => DriverExtensions.GetElement(this.Container, DownloadClientAndMattersButtonLocator).Click();
    }
}
