namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;

    /// <summary>
    /// Billing Investigator Page
    /// </summary>
    public class BillingInvestigationPage : BasePage
    {
        private static readonly By BillingInvestigatorCenterLocator = By.Id("co_billingInvestigationSearch");

        private static readonly By ClientIdLocator = By.Id("clientId");

        private static readonly By EndDateInputLocator = By.Id("endDate");

        private static readonly By SearchUsageButtonLocator = By.Id("billingInvestigationSearchButton");

        private static readonly By StartDateInputLocator = By.Id("startDate");

        private static readonly By UserElementLocator = By.XPath(".//span[contains(@class,'tokenLabel')]");

        private static readonly By UserNameInputLocator = By.XPath(".//input[contains(@class,'_tokenInput')]");

        private static readonly By UserSearchResultsDropDownLocator =
            By.CssSelector("div#co_billingInvestigationSearchFormUsers div div");

        private static readonly By IncludeSessionsCheckboxtLocator = By.Id("reachRecoveryCapSessions");

        private static readonly By ClearButtonLocator = By.Id("billingInvestigationClearButton");

        private static readonly By StartDateLabelLocator = By.XPath("//label[@for='startDate']");

        private static readonly By EndDateLabelLocator = By.XPath("//label[@for='endDate']");

        private static readonly By EmptySpacelLocator = By.Id("billingInvestigation");
        private static readonly By TimeFrameDropdownLocator = By.Id("timeFrame");

        /// <summary>
        /// Gets the time frame dropdown.
        /// </summary>
        public IDropdown<BillingInvestigationTimeFrameOptions> TimeFrameDropdown { get; } = new Dropdown<BillingInvestigationTimeFrameOptions>(TimeFrameDropdownLocator);

        /// <summary>
        /// This method clicks on the Search Usage button.
        /// </summary>
        /// <returns>
        /// The <see cref="BillingInvestigationResultsPage"/>.
        /// </returns>
        public BillingInvestigationResultsPage ClickSearchButton()
        {
            DriverExtensions.WaitForElementDisplayed(SearchUsageButtonLocator).Click();
            return new BillingInvestigationResultsPage();
        }

        /// <summary>
        /// This method enters a client id in the the client ID field
        /// </summary>
        /// <param name="value">The client id to be entered into the client id field.</param>
        /// <returns> The <see cref="BillingInvestigationPage"></see> </returns>/>
        public BillingInvestigationPage EnterClientId(string value)
        {
            DriverExtensions.SetTextField(value, ClientIdLocator);
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Gets the value of the client ID search field.
        /// </summary>
        /// <returns>The value of the client id search field as a string</returns>
        public string GetClientId() => DriverExtensions.WaitForElementDisplayed(ClientIdLocator).GetAttribute("value");

        /// <summary>
        /// This method gets the date contained in the "End Date" text box.
        /// </summary>
        /// <returns>The date as a string.</returns>
        public string GetEndDate()
            => DriverExtensions.WaitForElementDisplayed(EndDateInputLocator).GetAttribute("value");

        /// <summary>
        /// This method gets the date contained in the "From Date" text box.
        /// </summary>
        /// <returns>The date as a string.</returns>
        public string GetStartDate()
            => DriverExtensions.WaitForElementDisplayed(StartDateInputLocator).GetAttribute("value");

        /// <summary>
        /// This method sets the date contained in the "From Date" text box.
        /// </summary>
        /// <param name="startDate"> The start Date.  </param>
        /// <returns> The <see cref="BillingInvestigationPage"/>. </returns>
        public BillingInvestigationPage SetStartDate(string startDate)
        {
            // Using such approach because DriverExtensions.SetTextField doesn't work correctly for this input
            DriverExtensions.GetElement(StartDateLabelLocator).Click();
            DriverExtensions.GetElement(StartDateInputLocator).SendKeys(Keys.LeftShift + Keys.Home);
            DriverExtensions.GetElement(StartDateInputLocator).SendKeys(startDate);
            DriverExtensions.WaitForElement(EmptySpacelLocator).Click();
            return this;
        }

        /// <summary>
        /// This method sets the date contained in the "To Date" text box.
        /// </summary>
        /// <param name="endDate"> The end Date. </param>
        /// <returns> The <see cref="BillingInvestigationPage"/>. </returns>
        public BillingInvestigationPage SetEndDate(string endDate)
        {
            // Using such approach because DriverExtensions.SetTextField doesn't work correctly for this input
            DriverExtensions.GetElement(EndDateLabelLocator).Click();
            DriverExtensions.GetElement(EndDateInputLocator).SendKeys(Keys.LeftShift + Keys.Home);
            DriverExtensions.GetElement(EndDateInputLocator).SendKeys(endDate);
            DriverExtensions.WaitForElement(EmptySpacelLocator).Click();
            return this;
        }

        /// <summary>
        ///  User ResultsDropDown link
        /// </summary>

        public ITextbox UserNameInput => new Textbox(UserNameInputLocator);

        /// <summary>
        /// todo: merge with or override SortByDropdown of EdgeToolbarComponent
        /// </summary>

        public IButton UserNameButton => new Button(UserSearchResultsDropDownLocator);

        /// <summary>
        /// This method should use the autosuggest functionality within the user input field to enter a user's name.
        /// </summary>
        /// <param name="search"> The name to search. </param>
        /// <returns> Full username text. </returns>
        public string AddTextAndGetUser(string search)
        {
            this.UserNameInput.SendKeysSlow(search);
            DriverExtensions.WaitForJavaScript(7000);
            SafeMethodExecutor.WaitUntil(() => UserNameButton.Displayed);
            DriverExtensions.WaitForElement(UserSearchResultsDropDownLocator).Click();
            return DriverExtensions.GetElement(UserElementLocator).Text;
        }

        /// <summary>
        /// Gets the name of the first user in the user search field.
        /// </summary>
        /// <returns>The name of the user in the user search field as a string</returns>
        public string GetUserName()
            =>
                DriverExtensions.IsDisplayed(UserElementLocator, 5)
                    ? DriverExtensions.GetText(UserElementLocator)
                    : string.Empty;

        /// <summary>
        /// Checks to see if the Billing Investigator Widget is displayed.
        /// </summary>
        /// <returns>True if the Billing Investigator widget is displayed.  False if it is not.</returns>
        public bool IsBillingInvestigatorCenterDisplayed()
            => DriverExtensions.IsDisplayed(BillingInvestigatorCenterLocator, 5);

        /// <summary>
        /// Verifies that Include Sessions Checkbox is Displayed
        /// </summary>
        /// <returns> True if Include Sessions Checkbox is Displayed </returns>
        public bool IsIncludeSessionsCheckboxDisplayed()
            => DriverExtensions.IsDisplayed(IncludeSessionsCheckboxtLocator, 5);

        /// <summary>
        /// Verifies that Include Sessions Checkbox is Checked
        /// </summary>
        /// <returns> True if Include Sessions Checkbox is Checked </returns>
        public bool IsIncludeSessionsCheckboxChecked()
            => DriverExtensions.IsCheckboxSelected(IncludeSessionsCheckboxtLocator);

        /// <summary>
        /// This method clicks on the Clear button.
        /// </summary>
        /// <returns> The <see cref="BillingInvestigationPage"/>. </returns>
        public BillingInvestigationPage ClickClearButton()
        {
            DriverExtensions.WaitForElementDisplayed(ClearButtonLocator).Click();
            return this;
        }

        /// <summary>
        /// This method checks Include Sessions Checkbox
        /// </summary>
        /// <returns> The <see cref="BillingInvestigationPage"/>. </returns>
        public BillingInvestigationPage ClickIncludeSessionsCheckbox()
        {
            DriverExtensions.WaitForElementDisplayed(IncludeSessionsCheckboxtLocator).Click();
            return this;
        }
    }
}