namespace Framework.Common.UI.Products.Shared.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.RecentResearch;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.CommonTypes.Enums.Setup;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;

    /// <summary>
    /// WestlawNext ClientIdPage
    /// </summary>
    public class CommonClientIdPage : BaseModuleRegressionPage, ICommonClientIdPage
    {
        /// <summary>
        /// the maximum number of times to attempt to select a client ID
        /// </summary>
        private const int MaxRetryCount = 5;

        private static readonly By ClientIdContinueButtonLocator = By.XPath("//input[@id='co_clientIDContinueButton'] | //input[@value='Submit']");

        private static readonly By ClientIdTextboxLocator = By.Id("co_clientIDTextbox");

        private static readonly By ClientIdErrorBoxLocator = By.Id("co_beginResumeErrorMessagePlaceholder");

        private static readonly By ClientValidationMessageLocator = By.Id("co_beginClientIDErrorMessage");

        private static readonly By DynamicMessageLocator = By.Id("co_dynamicMessageContainer");

        private static readonly By HourlyBillingRadioButtonLocator = By.Id("co_websiteHourlyBillingMethodPreference");

        private static readonly By TransactionalBillingRadioButtonLocator = By.Id("co_websiteTransactionalBillingMethodPreference");

        private static readonly By RestoreSessionEventContainerLocator
            = By.XPath("//div[@id = 'co_resumeResearchSection']//div[contains(@class,'co_recentResearch')]");

        private static readonly By EnabledLinksLocator = By.XPath("./strong/a|//strong/a");

        private static readonly By DisabledLinksLocator = By.XPath("./div[@class='co_disabled']/strong/a");

        private static readonly By WelcomeUserHeadingLocator = By.ClassName("co_overlayBox_subHeader");

        private static readonly By DisabledQueryInfoIconLocator = By.XPath(".//span[contains(@class,'icon_help')]");

        /// <summary>
        /// Component with elements which depend on Account settings in Analytics
        /// </summary>
        public WestHostedClientIdComponent WestHostedClientIdComponent => new WestHostedClientIdComponent();

        /// <summary> 
        /// Links for Westlaw Session Time Outs feature.
        /// </summary>
        public ContinueResearchingComponent ContinueResearching => new ContinueResearchingComponent();

        /// <summary>
        /// Client Id drop down
        /// </summary>
        public ClientIdDropdown ClientIdDropdown { get; } = new ClientIdDropdown();

        /// <summary>
        /// Settings
        /// </summary>
        public TestSettings Settings { get; private set; }

        /// <summary> 
        /// Recent Research Pane. 
        /// </summary>
        public RecentResearchComponent RecentResearchPane { get; } = new RecentResearchComponent();

        /// <summary>
        /// Clicks the continue button
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(ClientIdContinueButtonLocator);

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// IsContinueButtonDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsContinueButtonDisplayed() => DriverExtensions.IsDisplayed(ClientIdContinueButtonLocator, 5);

        /// <summary>
        /// Enters the client id in the textbox (if needed) and clicks the continue button
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="clientid">Client id to enter</param>
        /// <returns> The page object</returns>
        public T EnterClientIdAndClickContinue<T>(string clientid) where T : ICreatablePageObject
          => this.EnterClientIdAndClickContinue<T>(clientid, false);

        /// <summary>
        /// Enters the client id in the textbox (if needed) and clicks the continue button
        /// </summary>
        /// <typeparam name="T">The type of the page to return</typeparam>
        /// <param name="clientid">client id to enter</param>
        /// <param name="retryOnFailure">Boolean indicating whether or not the test should retry selecting a client id on failures</param>
        /// <returns>The type of page to return</returns>
        public T EnterClientIdAndClickContinue<T>(string clientid, bool retryOnFailure) where T : ICreatablePageObject
        {
            this.ClientIdDropdown.EnterClientId(clientid);
            DriverExtensions.Click(ClientIdContinueButtonLocator);

            if (!retryOnFailure)
            {
                return DriverExtensions.CreatePageInstance<T>();
            }

            bool couldPassClientId = !this.ClientIdDropdown.IsClientIdTextboxDisplayed();

            for (int retryCount = 1; !couldPassClientId && retryCount <= MaxRetryCount;)
            {
                Logger.LogDebug("Selecting Client ID - Attempt #" + retryCount);
                DriverExtensions.WaitForJavaScript();

                couldPassClientId = !this.IsErrorBoxDisplayed();

                if (!couldPassClientId)
                {
                    string errorMessage;

                    SafeMethodExecutor.Execute(this.GetErrorText, out errorMessage);
                    Logger.LogError("Error message on Client ID page: " + errorMessage);
                    retryCount++;
                }
            }

            if (!couldPassClientId)
            {
                Logger.LogError("Unable to proceed past Client ID page!");
            }
            else
            {
                Logger.LogDebug("Authorization succeeded on the Client ID page.");
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if the error box exists on the page
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsErrorBoxDisplayed() => DriverExtensions.IsDisplayed(ClientIdErrorBoxLocator, 5);

        /// <summary>
        /// Checks if the error message "Client Validation Suspended" diplayed on the page
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsClientValidationMessageDisplayed() => DriverExtensions.IsDisplayed(ClientValidationMessageLocator, 5);

        /// <summary>
        /// Gets the text from the error box
        /// </summary>
        /// <returns>The text within the error box</returns>
        public string GetErrorText() => DriverExtensions.GetText(ClientIdErrorBoxLocator);

        /// <summary>
        /// Gets the text from info message with title "Client Validation Suspended"
        /// </summary>
        /// <returns>The text from info message</returns>
        public string GetClientValidationMessageText() => DriverExtensions.GetText(ClientValidationMessageLocator);

        /// <summary>
        /// The get restored session links.
        /// </summary>
        /// <returns>The <see cref="IReadOnlyCollection{T}"></see>
        /// </returns>
        public List<string> GetRestoredSessionTextLinks()
            => DriverExtensions.GetElements(RestoreSessionEventContainerLocator, EnabledLinksLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// The get restored session links.
        /// </summary>
        /// <returns>The <see cref="IReadOnlyCollection{T}"></see>
        /// </returns>
        public List<string> GetRestoredDisabledSessionTextLinks()
            => DriverExtensions.GetElements(RestoreSessionEventContainerLocator, DisabledLinksLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Determines if the Dynamic Messaging promo is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDynamicMessagingPromoDisplayed() => DriverExtensions.IsDisplayed(DynamicMessageLocator);

        /// <summary>
        /// Checks if the error box exists on the page
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsWelcomeUserHeadingDisplayed() => DriverExtensions.IsDisplayed(WelcomeUserHeadingLocator);

        /// <summary>
        /// Checks if the client id page exists.
        /// </summary>
        /// <returns>Whether the client id page is the current page</returns>
        public bool IsClientIdTextboxDisplayed() => DriverExtensions.IsDisplayed(ClientIdTextboxLocator);

        /// <summary>
        /// Checks if the info icon (?) is displayed in the Continuous Client
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDisabledQueryInfoIconDisplayed() => DriverExtensions.IsDisplayed(
            RestoreSessionEventContainerLocator,
            DisabledQueryInfoIconLocator);

        /// <summary>
        /// Clicks the hourly billing radio button when 'ask for billing option' preference is set
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <returns>New instance of the page</returns>
        public T SelectHourlyBilling<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(HourlyBillingRadioButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the transactional billing radio button when 'ask for billing option' preference is set
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <returns>New instance of the page</returns>
        public T SelectTransactionalBilling<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(TransactionalBillingRadioButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The click restored session link by index
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="linkIndex">The link index</param>
        /// <returns>New instance of the page</returns>
        public T ClickRestoredSessionLinkByIndex<T>(int linkIndex) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(RestoreSessionEventContainerLocator, EnabledLinksLocator).ElementAt(linkIndex).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks that there is any errors on the page
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsEnvironmentErrorsOnPage()
            => this.IsErrorPage
                || this.IsEnvironmentErrorMessageDisplayed()
                || DriverExtensions.IsDisplayed(ClientIdErrorBoxLocator);
    }
}