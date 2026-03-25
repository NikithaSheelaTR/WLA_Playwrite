namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Dialogs;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base page for Create Alert page and Update Alert pages
    /// </summary>
    public abstract class BaseAlertPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// Alert Name textbox
        /// </summary>
        protected static readonly By AlertNameTextboxLocator = By.CssSelector("#wa_alertNameForNewAlert");

        /// <summary>
        /// Cap Amount textbox
        /// </summary>
        protected static readonly By CapAmountTextboxLocator = By.CssSelector("#wa_alertCapAmount");

        /// <summary>
        /// Email textbox
        /// </summary>
        protected static readonly By DeliverToTextboxLocator = By.CssSelector("#co_delivery_emailAddress");

        private const string AlertNameErrorMessage = "AlertName textbox is absent on the Alerts page";
        private const string EmailErrorMessage = "Email textbox is absent on the Alerts page";
        private const string CapAmountErrorMessage = "CapAmount textbox is absent on the Alerts page";
        private const string ApplyToErrorMessage = "ApplyTo dropdown is absent on the Alerts page";
        private const string CostConditionErrorMessage = "CostCondition dropdown is absent on the Alerts page";
        private const string GreaterOrLessThanErrorMessage = "GreaterOrLessThan dropdown is absent on the Alerts page";
        private const string TimeFrameErrorMessage = "TimeFrame dropdown is absent on the Alerts page";
        private const string SelectButtonErrorMessage = "Select button is absent on the Alerts page";

        private static readonly By CreateUpdateAlertButtonLocator = By.CssSelector("#co_savealert");
        private static readonly By SelectButtonLocator = By.CssSelector("#wa_alertApplyTo");
        private static readonly By FromDateTextboxLocator = By.CssSelector("#startDate");
        private static readonly By ToDateTextboxLocator = By.CssSelector("#endDate");
        private static readonly By ErrorMessageLocator = By.XPath("//div[@style='display: block;']/div[@class='co_infoBox_message']");
        private static readonly By ManageYourAlertsButtonLocator = By.CssSelector("#DisplayAlertsView");
        private static readonly By CapValueErrorMessageLocator = By.CssSelector("#wa_capAlertErrorPlaceHolder div");
        private static readonly By ApplyToDropdownLocator = By.XPath("//select[@id='wa_clientType']");
        private static readonly By TimeFrameDropdownLocator = By.XPath("//select[@id='wa_alertCapTimePeriod']");
        private static readonly By GreaterOrLessThanDropdownLocator = By.XPath("//select[@id='wa_alertGreaterOrLessThan']");
        private static readonly By CostConditionDropdownLocator = By.XPath("//select[@id='wa_costCondition']");

        /// <summary>
        /// Gets the Apply To dropdown
        /// </summary>
        public IDropdown<ApplyToOptions> ApplyToDropdown { get; } = new Dropdown<ApplyToOptions>(ApplyToDropdownLocator);

        /// <summary>
        /// Time Frame dropdown
        /// </summary>
        public IDropdown<TimeFrameOptions> TimeFrameDropdown { get; } = new Dropdown<TimeFrameOptions>(TimeFrameDropdownLocator);

        /// <summary>
        /// Greater Or Less Than Dropdown
        /// </summary>
        public IDropdown<GreaterOrLessThanOptions> GreaterOrLessThanDropdown { get; } = new Dropdown<GreaterOrLessThanOptions>(GreaterOrLessThanDropdownLocator);

        /// <summary>
        /// Cost Condition Dropdown
        /// </summary>
        public IDropdown<CostConditionOptions> CostConditionDropdown { get; } = new Dropdown<CostConditionOptions>(CostConditionDropdownLocator);

        /// <summary>
        /// This method enters an Alert name into the Alert name field.
        /// </summary>
        /// <param name="alertName">The name we want to use for the alert.</param>
        public void EnterAlertName(string alertName) => DriverExtensions.SetTextField(alertName, AlertNameTextboxLocator);

        /// <summary>
        /// This method enters a value into the cap amount field
        /// </summary>
        /// <param name="amount">The value to be entered into the cap amount field.</param>
        public void EnterCapAmount(string amount) => DriverExtensions.SetTextField(amount, CapAmountTextboxLocator);

        /// <summary>
        /// This method enters an email or list of comma separated emails into the Delivery to field
        /// </summary>
        /// <param name="email">A string of an email or list of emails separated by commas.</param>
        public void EnterEmail(string email) => DriverExtensions.SetTextField(email, DeliverToTextboxLocator);

        /// <summary>
        /// Click on the Select Button
        /// </summary>
        /// <returns> The <see cref="SelectDialog"/>. </returns>
        public SelectDialog ClickSelectButton()
        {
            DriverExtensions.WaitForElement(SelectButtonLocator).Click();
            return new SelectDialog();
        }

        /// <summary>
        /// Get error message text
        /// </summary>
        /// <returns> Error message </returns>
        public string GetErrorMessageText() => DriverExtensions.WaitForElement(ErrorMessageLocator).Text;

        /// <summary>
        /// This method clicks on the Back to Display Alerts button
        /// </summary>
        /// <returns>A new Alerts Display Page object</returns>
        public ManageAlertsPage ClickManageYourAlertsButton()
        {
            DriverExtensions.WaitForElement(ManageYourAlertsButtonLocator).Click();
            return new ManageAlertsPage();
        }

        /// <summary>
        /// Verify that Alert Name, Email, CapAmount textboxes, 
        /// Apply To, CostCondition, GreaterOrLessThan, TimeFrame dropdowns
        /// and Select Users button are displayed
        /// </summary>
        /// <returns> Return empty string is all elements are displayed, 
        /// otherwise return message which element is absent</returns>
        public string VerifyThatAllElementsAreDisplayedOnThePage()
        {
            string errorMessage = this.IsElementDisplayed(AlertNameTextboxLocator, AlertNameErrorMessage);
            errorMessage = errorMessage + this.IsElementDisplayed(DeliverToTextboxLocator, EmailErrorMessage);
            errorMessage = errorMessage + this.IsElementDisplayed(CapAmountTextboxLocator, CapAmountErrorMessage);
            errorMessage = errorMessage + this.IsDropdownDisplayed(this.ApplyToDropdown.IsDisplayed(), ApplyToErrorMessage);
            errorMessage = errorMessage + this.IsDropdownDisplayed(this.CostConditionDropdown.IsDisplayed(), CostConditionErrorMessage);
            errorMessage = errorMessage + this.IsDropdownDisplayed(this.GreaterOrLessThanDropdown.IsDisplayed(), GreaterOrLessThanErrorMessage);
            errorMessage = errorMessage + this.IsDropdownDisplayed(this.TimeFrameDropdown.IsDisplayed(), TimeFrameErrorMessage);
            errorMessage = errorMessage + this.IsElementDisplayed(SelectButtonLocator, SelectButtonErrorMessage);
            return errorMessage;
        }

        /// <summary>
        /// Get text from the select button
        /// </summary>
        /// <returns> Select button text </returns>
        public string GetSelectButtonText() => DriverExtensions.GetText(SelectButtonLocator);

        /// <summary>
        /// Get error message text
        /// </summary>
        /// <returns> Error message </returns>
        public string GetCapAmountErrorMessage() => DriverExtensions.GetText(CapValueErrorMessageLocator);

        /// <summary>
        /// Click on the Create Alert button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        protected T ClickCreateUpdateAlertButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CreateUpdateAlertButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Fill alert's fields
        /// </summary>
        /// <param name="alertObject">
        /// The alert Object.
        /// </param>
        protected void FillAlertFields(
            AlertModel alertObject)
        {
            this.EnterEmail(alertObject.Email);
            this.EnterAlertName(alertObject.AlertName);
            this.EnterCapAmount(alertObject.CapAmount);

            this.ApplyToDropdown.SelectOption(alertObject.ApplyTo);
            this.CostConditionDropdown.SelectOption(alertObject.CostCondition);
            this.GreaterOrLessThanDropdown.SelectOption(alertObject.GreaterOrLessThan);
            this.TimeFrameDropdown.SelectOption(alertObject.TimeFrame);

            if (alertObject.TimeFrame == TimeFrameOptions.TimedCap)
            {
                DriverExtensions.SetTextField(alertObject.FromDate, FromDateTextboxLocator);
                DriverExtensions.SetTextField(alertObject.ToDate, ToDateTextboxLocator);
            }

            this.ApplyClientType();
        }

        private string IsElementDisplayed(By elementLocator, string errorMessage) => DriverExtensions.IsDisplayed(elementLocator) ? string.Empty : errorMessage + " ";

        private string IsDropdownDisplayed(bool isDisplayed, string errorMessage) => isDisplayed ? string.Empty : errorMessage;

        private void ApplyClientType(int locationIdex = 0)
        {
            SelectDialog selectDialog = this.ClickSelectButton();
            if (selectDialog.IsExpandLocationButtonDisplayed())
            {
                selectDialog.SelectAllUsers();
            }
            else
            {
                selectDialog.SelectOption(locationIdex);
            }
        }
    }
}
