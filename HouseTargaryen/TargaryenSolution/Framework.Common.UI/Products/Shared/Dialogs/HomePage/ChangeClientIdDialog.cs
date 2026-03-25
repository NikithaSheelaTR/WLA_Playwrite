namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when you click the client id
    /// </summary>
    public class ChangeClientIdDialog : BaseModuleRegressionDialog
    {
        private static readonly By ChangeButtonLocator = By.XPath(".//*[@id='co_clientIDContinueButton'] | .//*[@id='co_clientIDOOPContinueButton']");

        private static readonly By ChangeClientIdDropdownArrowLocator =
            By.CssSelector("*[title='Select a previous Client ID']");

        private static readonly By ClientIdLightboxLocator = By.Id("co_changeClientId_lightbox");

        private static readonly By CloseButtonLocator = By.Id("co_clientID_close_0");

        private static readonly By ErrorMessageLocator = By.CssSelector("div[id^='co_clientIDErrorMsg']");

        private static readonly By RecentClientIdsLocator = By.CssSelector("#co_clientID_recents li");

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeClientIdDialog"/> class. 
        /// Default constructor, waits for the dialog to appear
        /// </summary>
        public ChangeClientIdDialog()
        {
            this.ClientIdDropdown = new ClientIdDropdown();
            this.MatterIdDropdown = new MatterIdDropdown();
        }

        /// <summary>
        /// Component with elements which depend on Account settings in Analytics 
        /// </summary>
        public WestHostedClientIdComponent WestHostedClientIdComponent => new WestHostedClientIdComponent();

        /// <summary>
        /// Client Id drop down
        /// </summary>
        public ClientIdDropdown ClientIdDropdown { get; }

        /// <summary>
        /// Matter Id drop down
        /// </summary>
        public MatterIdDropdown MatterIdDropdown { get; }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// confirms client id change and closes the client id box
        /// </summary>
        public T ClickChangeButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(ChangeButtonLocator);

        /// <summary>
        /// Inputs a new client Id into the dialog and hits continue
        /// </summary>
        /// <param name="newClientId">The client id to change to</param>
        public T EnterClientIdAndHitContinue<T>(string newClientId) where T : ICreatablePageObject
        {
            this.ClientIdDropdown.SelectOption(newClientId);
            DriverExtensions.WaitForElement(ChangeButtonLocator);
            return this.ClickChangeButton<T>();
        }

        /// <summary>
        /// Get error message text
        /// </summary>
        /// <returns>Error text</returns>
        public string GetErrorText() => DriverExtensions.GetText(ErrorMessageLocator);

        /// <summary>
        /// Gets a list of the listed recent client ids
        /// </summary>
        /// <returns>The list of client ids</returns>
        public List<string> GetRecentClientIds()
            => DriverExtensions.GetElements(RecentClientIdsLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Determines if the client id dropdown displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsClientIdDropdownDisplayed() => DriverExtensions.IsDisplayed(ChangeClientIdDropdownArrowLocator);

        /// <summary>
        /// Determines if the client id dialog has an error message displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsErrorDisplayed() => DriverExtensions.IsDisplayed(ErrorMessageLocator);

        /// <summary>
        /// Determines if the client id lightbox is opened
        /// </summary>
        /// <returns>True if opened, false otherwise</returns>
        public bool IsLightboxOpen() => DriverExtensions.IsDisplayed(ClientIdLightboxLocator);
    }
}