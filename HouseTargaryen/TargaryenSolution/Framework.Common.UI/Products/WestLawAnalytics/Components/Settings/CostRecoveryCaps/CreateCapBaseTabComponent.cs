namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Items;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Create Cap Base Tab Component
    /// </summary>
    public abstract class CreateCapBaseTabComponent : BaseTabComponent
    {
        private static readonly By CapNameInputLocator = By.Id("wa_overrideName");

        private static readonly By ClientIdInputLocator = By.Id("wa_clientExpression");

        private static readonly By SelectBillingGroupsListLocator = By.Id("wa_billingGroups");

        private static readonly By BillingGroupLocator = By.ClassName("group");

        private static readonly By ShowMoreLinkLocator = By.XPath("//a[@class = 'expandCollapseLink collapsed']");

        private static readonly By ShowLessLinkLocator = By.XPath("//a[@class = 'expandCollapseLink']");

        private static readonly By CapAmountInputLocator = By.Id("wa_overrideValue");

        private static readonly By CapAmountTextLocator = By.XPath("//label[@for='wa_overrideValue']");

        private static readonly By BeginDateInputLocator = By.XPath("//input[@id='wa_effectiveDate']");

        private static readonly By EndDateInputLocator = By.XPath("//input[@id='wa_endDate']");

        private static readonly By SwitchSessionRadiobuttonLocator = By.Id("wa_replaceClient");

        private static readonly By SwitchSessionClientIdInputLocator = By.Id("wa_replacementClientId");

        private static readonly By KeepCurrentClientIdRadiobuttonLocator = By.Id("wa_keepClient");

        private static readonly By CreateCapButtonLocator = By.Id("wa_saveBtn");

        private static readonly By CapNameInlineMessageLocator = By.Id("wa_overrideNameError");

        private static readonly By ClientIdInlineMessageLocator = By.Id("wa_clientExpressionError");

        private static readonly By SwitchSessionClientIdInlineMessageLocator = By.Id("wa_replacementClientError");

        private static readonly By EndDateInlineMessageLocator = By.Id("wa_endDateError");

        private static readonly By BeginDateInlineMessageLocator = By.Id("wa_effectiveDateError");

        private static readonly By InlineConflictMessageLocator = By.XPath("//div[@id='wa_recoveryCapInfoPlaceholder']//div[@class='co_infoBox_message']");

        private static readonly By CloseInlineMessageButtonLocator = By.ClassName("co_infoBox_closeButton");

        private static readonly By StartWithDropdownLocator = By.Id("wa_isStartsWith");

        /// <summary>
        /// Starts With Dropdown
        /// </summary>
        public IDropdown<StartsWithOptions> StartsWithDropdown { get; } = new Dropdown<StartsWithOptions>(StartWithDropdownLocator);

        /// <summary>
        /// Fake Tab Name
        /// This property lets child class to override this field from BaseTabComponent
        /// </summary>
        protected override string TabName => "Fake Create Cap Tab";

        /// <summary>
        /// Enters the Cap Name
        /// </summary> <param name="capName"> Cap Name  </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage EnterCapName(string capName)
        {
            DriverExtensions.SetTextField(capName, CapNameInputLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Verifies that the Cap Name textbox is Displayed
        /// </summary>
        /// <returns> True if the Cap Name textbox is Displayed </returns>
        public bool IsCapNameTextboxDisplayed() => DriverExtensions.IsDisplayed(CapNameInputLocator);

        /// <summary>
        /// Gets the cap name from the Cap Name Input
        /// </summary>
        /// <returns> The Cap Name </returns>
        public string GetCapName() => DriverExtensions.GetText(CapNameInputLocator);

        /// <summary>
        /// Verifies that the Client Id Input is Displayed
        /// </summary>
        /// <returns> True if the Client Id Input is Displayed </returns>
        public bool IsClientIdInputDisplayed() => DriverExtensions.IsDisplayed(ClientIdInputLocator);

        /// <summary>
        /// Enters the Client Id
        /// </summary>
        /// <param name="clientId"> Client Id </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage EnterClientId(string clientId)
        {
            DriverExtensions.SetTextField(clientId, ClientIdInputLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Gets the Client ID from the Client Id Input
        /// </summary>
        /// <returns> The Client ID </returns>
        public string GetClientId() => DriverExtensions.GetText(ClientIdInputLocator);

        /// <summary>
        /// Verifies that the Billing Groups list is Displayed
        /// </summary>
        /// <returns> True if the Select Location Button is Displayed </returns>
        public bool IsBillingGroupsListDisplayed() => DriverExtensions.IsDisplayed(SelectBillingGroupsListLocator);

        /// <summary>
        /// Click Show More Link
        /// </summary>
        public void ClickShowMoreBillingGroupLink() =>
            DriverExtensions.Click(ShowMoreLinkLocator);

        /// <summary>
        /// Click Show Less Link
        /// </summary>
        public void ClickShowLessBillingGroupLink() =>
            DriverExtensions.Click(ShowLessLinkLocator);
        /// <summary>
        /// Click Remove/ Add Location link by number
        /// </summary>
        /// <param name="number"></param>
        public void ClickRemoveOrAddLocationByNumber(int number) =>
            this.GetBillingGroupList().ElementAt(number).ClickRemoveOrAddButton();

        /// <summary>
        /// Get list of Billing Group
        /// </summary>
        /// <returns></returns>
        public List<BillingGroupModel> GetListOfBillingGroup() =>
            this.GetBillingGroupList().Select(el => el.ToModel<BillingGroupModel>()).ToList();

        /// <summary>
        /// Verifies that the Cap Amount Input is Displayed
        /// </summary>
        /// <returns> True if the Cap Amount Input is Displayed </returns>
        public bool IsCapAmountInputDisplayed() => DriverExtensions.IsDisplayed(CapAmountInputLocator);

        /// <summary>
        /// Enters the Cap Amount
        /// </summary>
        /// <param name="capAmount"> Cap Amount </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage EnterCapAmount(string capAmount)
        {
            DriverExtensions.SetTextField(capAmount, CapAmountInputLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Gets the cap amount from the Cap Amount Input
        /// </summary>
        /// <returns> The cap amount </returns>
        public string GetCapAmount() => DriverExtensions.GetText(CapAmountInputLocator);
      

        /// <summary>
        /// Gets titlw for cap amount field
        /// </summary>
        /// <returns> title</returns>
        public string GetCapAmountTitle() => DriverExtensions.GetText(CapAmountTextLocator);

        /// <summary>
        /// Verifies that the Begin Date Input is Displayed
        /// </summary>
        /// <returns> True if the Begin Date Input is Displayed </returns>
        public bool IsBeginDateInputDisplayed() => DriverExtensions.IsDisplayed(BeginDateInputLocator);

        /// <summary>
        /// Sets the Begin Date
        /// </summary>
        /// <param name="beginDate"> The Begin Date </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage SetBeginDate(string beginDate)
        {
            DriverExtensions.SetTextField(beginDate, BeginDateInputLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Gets the date from the Begin Date Input
        /// </summary>
        /// <returns> The date </returns>
        public string GetBeginDate() => DriverExtensions.GetText(BeginDateInputLocator);

        /// <summary>
        /// Verifies that the End Date Input is Displayed
        /// </summary>
        /// <returns> True if the End Date Input is Displayed </returns>
        public bool IsEndDateInputDisplayed() => DriverExtensions.IsDisplayed(EndDateInputLocator);

        /// <summary>
        /// Sets the End Date
        /// </summary>
        /// <param name="endDate"> The End Date </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage SetEndDate(string endDate)
        {
            DriverExtensions.SetTextField(endDate, EndDateInputLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Gets the date from the End Date Input
        /// </summary>
        /// <returns> The date </returns>
        public string GetEndDate() => DriverExtensions.GetText(EndDateInputLocator);

        /// <summary>
        /// Verifies that the Switch Session radio button is Displayed
        /// </summary>
        /// <returns> True if the Switch Session radio button is Displayed </returns>
        public bool IsSwitchSessionRadiobuttonDisplayed() => DriverExtensions.IsDisplayed(SwitchSessionRadiobuttonLocator);

        /// <summary>
        /// Verifies that the Switch Session radio button is selected
        /// </summary>
        /// <returns> True if the Switch Session radio button is selected </returns>
        public bool IsSwitchSessionRadiobuttonSelected() => DriverExtensions.IsRadioButtonSelected(SwitchSessionRadiobuttonLocator);

        /// <summary>
        /// Selects the Switch Session radio button
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage SelectSwitchSessionRadiobutton()
        {
            DriverExtensions.Click(SwitchSessionRadiobuttonLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Verifies that the Switch Session Client ID Input is Displayed
        /// </summary>
        /// <returns> True if the Switch Session Client ID Input is Displayed </returns>
        public bool IsSwitchSessionClientIdInputDisplayed() => DriverExtensions.IsDisplayed(SwitchSessionClientIdInputLocator);

        /// <summary>
        /// Enters the the Switch Session Client ID
        /// </summary>
        /// <param name="clientId"> The Switch Session Client ID </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage EnterSwitchSessionClientId(string clientId)
        {
            DriverExtensions.SetTextField(clientId, SwitchSessionClientIdInputLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Gets the Client ID from the Switch Session Client ID input
        /// </summary>
        /// <returns> The Client ID </returns>
        public string GetSwitchSessionClientId() => DriverExtensions.GetText(SwitchSessionClientIdInputLocator);

        /// <summary>
        /// Verifies that the Keep Current Client ID Radio button is Displayed
        /// </summary>
        /// <returns> True if the Keep Current Client ID Radio button is Displayed </returns>
        public bool IsKeepCurrentClientIdRadiobuttonDisplayed() => DriverExtensions.IsDisplayed(KeepCurrentClientIdRadiobuttonLocator);

        /// <summary>
        /// Verifies that the Keep Current Client Id radio button is selected
        /// </summary>
        /// <returns> True if the Keep Current Client Id radio button is selected </returns>
        public bool IsKeepCurrentClientIdRadiobuttonSelected()
            => DriverExtensions.IsRadioButtonSelected(KeepCurrentClientIdRadiobuttonLocator);

        /// <summary>
        /// Selects the Keep Current Client Id radio button
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage SelectKeepCurrentClientIdRadiobutton()
        {
            DriverExtensions.Click(KeepCurrentClientIdRadiobuttonLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Verifies that the Create Cap Button is Displayed
        /// </summary>
        /// <returns> True if the Create Cap Button is Displayed </returns>
        public bool IsCreateCapButtonDisplayed() => DriverExtensions.IsDisplayed(CreateCapButtonLocator);

        /// <summary>
        /// Clicks the 'Create Cap' button
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage ClickCreateCapButton()
        {
            DriverExtensions.Click(CreateCapButtonLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Gets Cap Name inline message
        /// </summary>
        /// <returns> Cap Name inline message </returns>
        public string GetCapNameInlineMessage() => DriverExtensions.GetText(CapNameInlineMessageLocator);

        /// <summary>
        /// Gets Client Id inline message
        /// </summary>
        /// <returns> Client Id inline message </returns>
        public string GetClientIdInlineMessage() => DriverExtensions.GetText(ClientIdInlineMessageLocator);

        /// <summary>
        /// Gets Switch Session Client Id inline message
        /// </summary>
        /// <returns> Switch Session Client Id inline message </returns>
        public string GetSwitchSessionClientIdInlineMessage() => DriverExtensions.GetText(SwitchSessionClientIdInlineMessageLocator);

        /// <summary>
        /// Gets Begin Date inline message
        /// </summary>
        /// <returns> Begin Date inline message </returns>
        public string GetBeginDateInlineMessage() => DriverExtensions.GetText(BeginDateInlineMessageLocator);

        /// <summary>
        /// Verifies that the Begin Date inline message is displayed
        /// </summary>
        /// <returns> True If the Begin Date inline message is displayed </returns>
        public bool IsBeginDateInlineMessageDisplayed() => DriverExtensions.IsDisplayed(BeginDateInlineMessageLocator);

        /// <summary>
        /// Gets End Date inline message
        /// </summary>
        /// <returns> End Date inline message </returns>
        public string GetEndDateInlineMessage() => DriverExtensions.GetText(EndDateInlineMessageLocator);

        /// <summary>
        /// Verifies that the End Date inline message is displayed
        /// </summary>
        /// <returns> True If the End Date inline message is displayed </returns>
        public bool IsEndDateInlineMessageDisplayed() => DriverExtensions.IsDisplayed(EndDateInlineMessageLocator);

        /// <summary>
        /// Gets text of the inline conflict message
        /// </summary>
        /// <returns> Text of the inline conflict message </returns>
        public string GetInlineConflictMessageText() => DriverExtensions.GetText(InlineConflictMessageLocator);

        /// <summary>
        /// Clicks close inline conflict message button
        /// </summary>
        public void ClickCloseInlineMessageButton() => DriverExtensions.Click(CloseInlineMessageButtonLocator);

        /// <summary>
        /// Verifies that inline conflict message is Displayed
        /// </summary>
        /// <returns> True If inline conflict message is Displayed </returns>
        public bool IsInlineConflictMessageDisplayed() => DriverExtensions.IsDisplayed(InlineConflictMessageLocator);

        /// <summary>
        /// Verifies that the 'Begin Date' is enabled
        /// </summary>
        /// <returns> True If the 'Begin Date' is enabled </returns>
        public bool IsBeginDateEnabled() => DriverExtensions.IsEnabled(BeginDateInputLocator);

        /// <summary>
        /// Clicks begin date input.
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickBeginDateInput<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BeginDateInputLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks end date input.
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickEndDateInput<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(EndDateInputLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        private List<BillingGroupItem> GetBillingGroupList() =>
            DriverExtensions.GetElements(SelectBillingGroupsListLocator, BillingGroupLocator)
                            .Select(el => new BillingGroupItem(el)).ToList();

    }
}

