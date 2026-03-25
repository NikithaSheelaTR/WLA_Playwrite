namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The manage alerts page, where you can delete and search for alerts
    /// </summary>
    public class AlertCenterPage : BaseModuleRegressionPage, IAlertCenterPage
    {
        private const string AlertByGroupNameCheckboxLctMask = "//input[@type = 'checkbox'][@alertgroup = '{0}']";

        private const string AlertByTypeCheckboxLctMask = "//input[@type='checkbox'][@alerttype='{0}']";

        private const string HistoryByNameLinkLctMask = "//li[@class='co_user_alert_item' and .//span[text()='{0}']]//a[@class='co_alertHistory']";

        private const string EditByNameLinkLctMask = "//li[.//span[text()='{0}']]//a[@class='co_editAlert']";

        private const string AlertByNameCheckboxLctMask = "//input[@alertname='{0}']";

        private const string AlertContentLctMask =
            "//li[@class='co_user_alert_item' and .//span[text()='{0}']]//li[contains(@id,'alertContentID_')]";

        private const string AlertbyNameLctMask = "//li[@class='co_user_alert_item' and .//label[text()='{0}']]";

        private const string AlertSearchTermLctMask =
            "//li[@class='co_user_alert_item' and .//span[text()='{0}']]//li[contains(text(), 'Search')]";

        private const string AlertByTypeLctMask = "//*[contains(@class,'co_navDropDownHeader')]/*[text()='{0}']";

        private static readonly By AlertRecipientLocator = By.XPath("//input[contains(@id, 'coid_alert_recipient')]");

        private static readonly By AlertTypeTextLabelLocator =
            By.XPath("//div[contains(@id, 'co_viewResultsBy')]//h3[contains(@class, 'co_genericBoxHeader')]//strong");

        private static readonly By CapitolWatchReportLinkLocator = By.Id("co_createReportCapitolWatchReportMenuItem");

        private static readonly By CreateAlertLinkLocator = By.Id("co_createAlertMenu");

        private static readonly By AlertsHistoryLinkLocator = By.Id("co_alertHistory");

        private static readonly By CreateNewsletterLinkLocator = By.Id("co_createNewsletter");

        private static readonly By CreateNewsletterMenuLocator = By.Id("co_createNewsletterMenu");

        private static readonly By NewNewsletterLinkLocator = By.XPath("//div[@id='co_createNewsletterNewsletterMenuItem']/a");

        private static readonly By CreateReportsLinkLocator = By.Id("co_createReportMenu");

        private static readonly By DeleteButtonLocator = By.Id("cobalt_ro_detail_trash");

        private static readonly By EditAlertLinkLocator = By.CssSelector("a.co_editAlert");

        private static readonly By EditSelectedLinkLocator = By.Id("co_alert_multipleEdit");

        private static readonly By FirstAlertCheckboxLocator =
            By.XPath("//ul[contains(@id, 'alertListBody')]//li[contains(@class, 'co_user_alert_item')]//input");

        private static readonly By NewslettersLeftPaneLinkLocator =
            By.XPath("//ul[contains(@id, 'co_alertNav_menu')]//li[contains(@class, 'co_alertNav_digest')]/button");

        private static readonly By AlertsLeftPaneLinkLocator = 
            By.XPath("//ul[contains(@id, 'co_alertNav_menu')]//li[contains(@class, 'co_alertNav_alert')]/button");

        private static readonly By NoAlertsMessageLocator = By.XPath("//span[@id = 'co_alerts_center_nocontent']");

        private static readonly By ReportsLeftPaneLinkLocator = By.XPath("//li[@class='co_alertNav_report']/button");

        private static readonly By SelectAllItemsCheckBoxLocator = By.Id("checkbox_all_items_select");

        private static readonly By ManageAlertGroupsLinkLocator = By.Id("co_settings");

        private static readonly By SuccessfullyCreatedMessageLocator = By.XPath(
                 "//div[@class='co_infoBox_message']/span[contains(text(),' been saved.') or contains(text(),'été enregistrée.')] |//div[@class='co_infoBox_message' and (contains(text(),' been saved.') or contains(text(),'été enregistrée.'))]");

        private static readonly By LastUpdateLabelLocator =
            By.XPath(".//ul[@class='co_inlineList']/li[not(contains(@class,'co_icon_email'))][1]");

        private static readonly By NextUpdateLabelLocator = By.XPath(".//ul[@class='co_inlineList']/li[2]");

        private static readonly By ClientIdLocator = By.XPath(".//ul[@class='co_inlineList']/li[3]");

        private static readonly By EmailRecipientsLinkLocator = By.ClassName("co_alert_emailRecipientLink");

        private static readonly By EmailRecipientLocator = By.XPath(".//li[contains(@class,'co_icon_email')]");

        private static readonly By AlertIfEvenNoResultsLabelLocator = By.XPath(".//li[contains(@id, 'noDocsMessage')]");

        private static readonly By SuccessfullyDeletedMessageLocator =
            By.XPath("//div[@class='co_infoBox_message' and contains(text(),' has been successfully deleted') or contains(text(), 'été supprimé avec succès')]");

        private static readonly By ListViewContainerLocator = By.XPath("//ul[@id = 'alertListBody']");

        private static readonly By ListViewItemsLocator = By.XPath("./li[@class='co_user_alert_item']");

        private static readonly By GridViewContainerLocator = By.XPath("//table[@id='alertGridBody']//tbody");

        private static readonly By GridViewItemsLocator = By.XPath("./tr");

        private EnumPropertyMapper<AlertType, WebElementInfo> alertTypeMap;

        /// <summary>
        ///  Alert List items collection
        /// </summary>
        /// <returns>collection of alert grid items</returns>
        public ItemsCollection<AlertViewItem> AlertListItems => new ItemsCollection<AlertViewItem>(ListViewContainerLocator, ListViewItemsLocator);

        /// <summary>
        ///  Alert grid items collection
        /// </summary>
        /// <returns>collection of alert grid items</returns>
        public ItemsCollection<AlertViewItem> AlertGridItems => new ItemsCollection<AlertViewItem>(GridViewContainerLocator, GridViewItemsLocator);

        /// <summary> 
        /// Get Alert Facet.
        /// </summary>
        public AlertFacetComponent AlertFacet { get; private set; } = new AlertFacetComponent();

        /// <summary> 
        /// Gets header. 
        /// </summary>
        public WestlawNextHeaderComponent Header { get; private set; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Gets NarrowPaneBase.
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; private set; } = new NarrowPaneComponent();

        /// <summary>
        /// Toolbar
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();
        
        /// <summary>
        /// Gets the AlertType enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AlertType, WebElementInfo> AlertTypeMap
            => this.alertTypeMap = this.alertTypeMap ?? EnumPropertyModelCache.GetMap<AlertType, WebElementInfo>("AlertPage");

        /// <summary>
        /// Verify that content is displayed for the specific alert
        /// </summary>
        /// <param name="alertName"> The alert name. </param>
        /// <returns> Alert content </returns>
        public string GetAlertContent(string alertName)
            => DriverExtensions.GetText(By.XPath(string.Format(AlertContentLctMask, alertName)));

        /// <summary>
        /// Verify that alert is displayed
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> True if alert is displayed, false otherwise </returns>
        public bool IsAlertDisplayed(string alertName)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(AlertbyNameLctMask, alertName)), 10);

        /// <summary>
        /// Click history link by name.
        /// </summary>
        /// <param name="name"> Name of report. </param>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        public T ClickHistoryLinkByName<T>(string name) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.XPath(string.Format(HistoryByNameLinkLctMask, name))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the create newsletter link
        /// On Alerts page click "Create Newsletter" link
        /// On Newsletter page click "Create Newsletter" button and click "New Newsletter" link
        /// </summary>
        /// <returns>The newsletter page</returns>
        public CreateNewsletterPage ClickCreateNewsletterLink()
        {
            this.ScrollPageToTop();
            if (DriverExtensions.IsDisplayed(CreateNewsletterMenuLocator))
            {
                DriverExtensions.WaitForElement(CreateNewsletterMenuLocator).Click();
                DriverExtensions.WaitForElement(NewNewsletterLinkLocator).Click();
            }
            else
            {
                DriverExtensions.WaitForElement(CreateNewsletterLinkLocator).Click();
            }

            return new CreateNewsletterPage();
        }

        /// <summary>
        /// Click on the 'Alerts History' link 
        /// </summary>
        /// <returns>
        /// The <see cref="AlertsHistoryPage"/>.
        /// </returns>
        public T ClickAlertsHistoryLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(AlertsHistoryLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the edit alert link.
        /// </summary>
        /// <typeparam name="T"> 
        /// Type of object to return.
        /// </typeparam>
        /// <returns> The next page. </returns>
        public T ClickEditAlertLink<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.GetElement(EditAlertLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click edit report link.
        /// </summary>
        /// <param name="reportName"> The report Name. </param>
        /// <typeparam name="T"> Type of object to return.  </typeparam>
        /// <returns> New page instance. </returns>
        public T ClickEditLinkByName<T>(string reportName) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.XPath(string.Format(EditByNameLinkLctMask, reportName))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        ///  Click edit selected link.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        public T ClickEditSelected<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(EditSelectedLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the left pane newsletter link
        /// </summary>
        /// <returns>The newsletter page</returns>
        public AlertCenterPage ClickNewsletterLeftPaneLink()
        {
            this.ScrollPageToTop();
            DriverExtensions.WaitForElement(NewslettersLeftPaneLinkLocator).Click();
            return new AlertCenterPage();
        }

        /// <summary>
        /// Clicks the left pane newsletter link
        /// </summary>
        /// <returns>The newsletter page</returns>
        public AlertCenterPage ClickAlertLeftPaneLink()
        {
            this.ScrollPageToTop();
            DriverExtensions.WaitForElement(AlertsLeftPaneLinkLocator).Click();
            return new AlertCenterPage();
        }

        /// <summary>
        /// Clicks the left pane Reports link
        /// </summary>
        public void ClickReportsLink()
        {
            DriverExtensions.GetElement(ReportsLeftPaneLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click settings link.
        /// </summary>
        /// <typeparam name="T">instance to return </typeparam>
        /// <returns> New page object. </returns>
        public T ClickManageAlertGroupsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(ManageAlertGroupsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Creates an alert of the specified type
        /// </summary>
        /// <typeparam name="T"> 
        /// Type of object to return.
        /// </typeparam>
        /// <param name="alertType"> The alert Type. </param>
        /// <returns> new create alert page </returns>
        public T ClickCreateAlertButton<T>(AlertType alertType) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.GetElement(CreateAlertLinkLocator).Click();

            // select dropdown button based on type
            DriverExtensions.WaitForElement(By.XPath(string.Format(AlertByTypeLctMask, this.AlertTypeMap[alertType].Text))).Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Create capitol watch report.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        public T CreateCapitolWatchReport<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(CreateReportsLinkLocator).Click();
            DriverExtensions.WaitForElementDisplayed(CapitolWatchReportLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Deletes Alert By Name
        /// </summary>
        /// <param name="nameOfAlert">name of Alert</param>
        public void DeleteAlertByName(string nameOfAlert)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(AlertByNameCheckboxLctMask, nameOfAlert))).Click();
            this.ScrollPageToTop();
            DeleteReportsAlertsNewslettersDialog confirmationDialog = this.ClickDeleteButton();
            confirmationDialog.ClickYesButton<AlertCenterPage>();
        }

        /// <summary>
        /// Deletes all of the alerts on the AlertSearchResult page
        /// </summary>
        public void DeleteAllAlerts()
        {
            if (DriverExtensions.IsDisplayed(SelectAllItemsCheckBoxLocator))
            {
                this.SelectAllItemsCheckbox();
                DeleteReportsAlertsNewslettersDialog confirmationDialog = this.ClickDeleteButton();
                confirmationDialog.ClickYesButton<AlertCenterPage>();
            }

            DriverExtensions.WaitForElement(NoAlertsMessageLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get search term by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Search term </returns>
        public string GetSearchTermByAlertName(string alertName)
        {
            string text = DriverExtensions.GetText(By.XPath(string.Format(AlertSearchTermLctMask, alertName)));
            return text.Substring(text.IndexOf(":", StringComparison.Ordinal) + 2);
        }

        /// <summary>
        /// Get update date by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Update date </returns>
        public string GetLastUpdateDateByAlertName(string alertName)
            => DriverExtensions.GetText(By.XPath(string.Format(AlertbyNameLctMask, alertName)), LastUpdateLabelLocator);

        /// <summary>
        /// Get next update date by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Next update date </returns>
        public string GetNextUpdateDateByAlertName(string alertName)
            => DriverExtensions.GetText(By.XPath(string.Format(AlertbyNameLctMask, alertName)), NextUpdateLabelLocator);

        /// <summary>
        /// Get Client Id by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Client Id </returns>
        public string GetClientIdByAlertName(string alertName)
            => DriverExtensions.GetText(By.XPath(string.Format(AlertbyNameLctMask, alertName)), ClientIdLocator);

        /// <summary>
        /// Verify that email recipients link is displayed for specific alert
        /// </summary>
        /// <param name="alertName"> Alert Name </param>
        /// <returns> True if link is displayed, false otherwise </returns>
        public bool IsEmailRecipientsLinkForAlertDisplayed(string alertName)
            =>
                DriverExtensions.IsDisplayed(
                    DriverExtensions.GetElement(By.XPath(string.Format(AlertbyNameLctMask, alertName))),
                    EmailRecipientsLinkLocator);

        /// <summary>
        /// Get text from field 'Alert even no results'
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> 'Alert even no results' text </returns>
        public string GetAlertEvenNoResultsText(string alertName)
            => DriverExtensions.GetText(By.XPath(string.Format(AlertbyNameLctMask, alertName)), AlertIfEvenNoResultsLabelLocator);

        /// <summary>
        /// Get recipients count
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Recipients count </returns>
        public int GetRecipientsCount(string alertName)
            =>
                DriverExtensions.WaitForElement(
                    DriverExtensions.GetElement(By.XPath(string.Format(AlertbyNameLctMask, alertName))),
                    EmailRecipientLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Get alert name by group.
        /// </summary>
        /// <param name="groupName"> The group Name. </param>
        /// <returns> Alert name. </returns>
        public string GetAlertNameByGroup(string groupName)
            => DriverExtensions.GetElement(By.XPath(string.Format(AlertByGroupNameCheckboxLctMask, groupName))).GetAttribute("alertname");

        /// <summary>
        /// Get alert name by type.
        /// </summary>
        /// <param name="typeName">Type name. </param>
        /// <returns> Alert name. </returns>
        public string GetAlertNameByType(string typeName)
            => DriverExtensions.GetElement(By.XPath(string.Format(AlertByTypeCheckboxLctMask, typeName))).GetAttribute("alertname");

        /// <summary>
        /// Gets all recipients
        /// </summary>
        /// <returns> List of recipients </returns>
        public List<string> GetAlertRecipients() => DriverExtensions.GetElements(AlertRecipientLocator).Select(elem => elem.GetAttribute("value")).ToList();

        /// <summary>
        /// Gets the sidebar strong text.
        /// </summary>
        /// <returns>Alert type text.</returns>
        public string GetAlertTypeFacetTitleText() => DriverExtensions.GetElement(AlertTypeTextLabelLocator).Text;

        /// <summary>
        /// Is success message displayed.
        /// </summary>
        /// <returns> True if displayed,false otherwise. </returns>
        public bool IsSuccessMessageDisplayed() => DriverExtensions.IsDisplayed(SuccessfullyCreatedMessageLocator);

        /// <summary>
        /// Is success deleted message displayed.
        /// </summary>
        /// <returns> True if displayed,false otherwise. </returns>
        public bool IsSuccessDeletingMessageDisplayed()
            => DriverExtensions.IsDisplayed(SuccessfullyDeletedMessageLocator);

        /// <summary>
        /// Verify no Alerts displayed
        /// </summary>
        /// <returns>true if no alerts on the page, false otherwise</returns>
        public bool IsNoAlertsMessageDisplayed() => DriverExtensions.IsDisplayed(NoAlertsMessageLocator, 5);

        /// <summary>
        /// Set Select all items checkbox
        /// </summary>
        /// <param name="setTo">The set To.</param>
        public void SelectAllItemsCheckbox(bool setTo = true)
            => DriverExtensions.SetCheckbox(SelectAllItemsCheckBoxLocator, setTo);

        /// <summary>
        /// Set the first alert include checkbox to the specified value
        /// </summary>
        /// <param name="setTo">The set To.</param>
        public void SelectFirstAlertCheckbox(bool setTo = true)
            => DriverExtensions.SetCheckbox(FirstAlertCheckboxLocator, setTo);

        private DeleteReportsAlertsNewslettersDialog ClickDeleteButton()
        {
            DriverExtensions.WaitForElement(DeleteButtonLocator).Click();
            return new DeleteReportsAlertsNewslettersDialog();
        }
    }
}