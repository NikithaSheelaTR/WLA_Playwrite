namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Alerts
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  Alerts Display Page
    /// </summary>
    public class ManageAlertsPage : BaseModuleRegressionPage
    {
        private const string ManageAlertsTitle = "Manage your alerts";

        private const string CheckboxIdLctMask = "co_chkAlert_";

        private static readonly By AppliedToLabelLocator = By.CssSelector("td.wa_manageAlertsTable_appliedTo");

        private static readonly By CapAmountLabelLocator = By.CssSelector("td.wa_manageAlertsTable_capAmount");

        private static readonly By CheckBoxLocator = By.CssSelector("td.wa_manageAlertsTable_checkbox input");

        private static readonly By DeleteButtonLocator = By.CssSelector("#co_delete");

        private static readonly By CreateNewAlertButtonLocator = By.CssSelector("#co_createalert");

        private static readonly By AlertNameLabelLocator = By.CssSelector("td.wa_manageAlertsTable_alertName");

        private static readonly By DeliverToLabelLocator = By.CssSelector("td.wa_manageAlertsTable_deliver");

        private static readonly By DurationLabelLocator = By.CssSelector("td.wa_manageAlertsTable_frequency");

        private static readonly By EditButtonLocator = By.CssSelector("button.wa_editButton");

        private static readonly By ManageAlertsTitleLocator = By.CssSelector("#co_contentColumn h2");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageAlertsPage"/> class. 
        /// </summary>
        public ManageAlertsPage()
        {
            DriverExtensions.WaitForElement(CreateNewAlertButtonLocator);
        }

        /// <summary>
        /// This method clicks the create alert button.
        /// </summary>
        /// <returns><see cref="CreateAlertPage"/></returns>
        public CreateAlertPage ClickCreateAlert()
        {
            DriverExtensions.WaitForElement(CreateNewAlertButtonLocator).Click();
            return new CreateAlertPage();
        }

        /// <summary>
        /// This method deletes all the alerts on the display page.
        /// </summary>
        public void DeleteAllAlerts()
        {
            int alertCount = DriverExtensions.GetElements(CheckBoxLocator).Count;
            for (int i = 0; i < alertCount; i++)
            {
                DriverExtensions.SetCheckbox(By.Id(CheckboxIdLctMask + i), true);
            }

            DriverExtensions.WaitForElement(DeleteButtonLocator).Click();
        }

        /// <summary>
        /// This method deletes the alert on the display page by name.
        /// </summary>
        public void DeleteAlertByName(string alertName)
        {

            int index = DriverExtensions.GetElements(AlertNameLabelLocator).ToList().FindIndex(item => item.Text.Equals(alertName));
            DriverExtensions.SetCheckbox(By.Id(CheckboxIdLctMask + index), true);
            DriverExtensions.WaitForElement(DeleteButtonLocator).Click();
        }

        /// <summary>
        /// This method clicks on the edit button for the alert by index in the alerts list
        /// </summary>
        /// <returns>A new UpdateAlertPage object.</returns>
        public UpdateAlertPage EditAlert(int alertIndex = 0)
        {
            DriverExtensions.GetElements(EditButtonLocator).ElementAt(alertIndex).Click();
            return new UpdateAlertPage();
        }

        /// <summary>
        /// This method clicks on the edit button for the alert by name in the alerts list
        /// </summary>
        /// <param name="alertName"></param>
        /// <returns>A new UpdateAlertPage object.</returns>
        public UpdateAlertPage EditAlertByName(string alertName)
        {
            int index = DriverExtensions.GetElements(AlertNameLabelLocator).ToList().FindIndex(item => item.Text.Equals(alertName));
            return this.EditAlert(index);
        }

        /// <summary>
        /// This method verifies that the user is on the alerts display page by checking for the correct text.
        /// </summary>
        /// <returns>True if the correct text is present in the source.</returns>
        public bool IsAlertsPageDisplayed()
        {
            bool isAlertsPageTitleDisplayed = DriverExtensions.IsDisplayed(ManageAlertsTitleLocator, 5);
            bool isManageAlertsTextCorrect = DriverExtensions.GetText(ManageAlertsTitleLocator).Equals(ManageAlertsTitle);
            bool isCreateAlertButtonDisplayed = DriverExtensions.IsDisplayed(CreateNewAlertButtonLocator, 5);

            return isAlertsPageTitleDisplayed && isManageAlertsTextCorrect && isCreateAlertButtonDisplayed;
        }

        /// <summary>
        /// Get Alert
        /// </summary>
        /// <param name="alertIndex"> Alert index </param>
        /// <returns> The <see cref="AlertModel"/>. </returns>
        public AlertModel GetAlert(int alertIndex)
        {
            var alertModel = new AlertModel();
            alertModel.Email = DriverExtensions.GetElements(DeliverToLabelLocator).ElementAt(alertIndex).Text;
            alertModel.AlertName = DriverExtensions.GetElements(AlertNameLabelLocator).ElementAt(alertIndex).Text;
            alertModel.CapAmount = DriverExtensions.GetElements(CapAmountLabelLocator).ElementAt(alertIndex).Text;
            alertModel.ApplyTo =
                DriverExtensions.GetElements(AppliedToLabelLocator)
                                .ElementAt(alertIndex)
                                .Text.GetEnumValueByText<ApplyToOptions>();
            alertModel.TimeFrame =
                DriverExtensions.GetElements(DurationLabelLocator)
                                .ElementAt(alertIndex)
                                .Text.GetEnumValueByText<TimeFrameOptions>();
            return alertModel;
        }

        /// <summary>
        /// Get Alert by name
        /// </summary>
        /// <param name="alertName"> Alert index </param>
        /// <returns> The <see cref="AlertModel"/>. </returns>
        public AlertModel GetAlertByName(string alertName)
        {
           int index = DriverExtensions.GetElements(AlertNameLabelLocator).ToList().FindIndex(item => item.Text.Equals(alertName));
           return this.GetAlert(index);
       }

    }
}