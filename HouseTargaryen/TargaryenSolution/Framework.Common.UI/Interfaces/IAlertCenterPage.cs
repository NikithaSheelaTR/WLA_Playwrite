namespace Framework.Common.UI.Interfaces
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Pages;

    /// <summary>
    /// AlertCenterPage interface
    /// </summary>
    public interface IAlertCenterPage : ICreatablePageObject
    {
        /// <summary>
        /// Get content for the specific alert
        /// </summary>
        /// <param name="alertName">The alert name.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetAlertContent(string alertName);

        /// <summary>
        /// Verify that alert is displayed
        /// </summary>
        /// <param name="alertName">Alert name</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsAlertDisplayed(string alertName);

        /// <summary>
        /// Click on history link by name.
        /// </summary>
        /// <param name="name"> Name of report. </param>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        T ClickHistoryLinkByName<T>(string name) where T : ICreatablePageObject;

        /// <summary>
        /// Click on the edit alert link.
        /// </summary>
        /// <typeparam name="T">Type of object to return.</typeparam>
        /// <returns> New page instance. </returns>
        T ClickEditAlertLink<T>() where T : BaseModuleRegressionPage;

        /// <summary>
        /// Click on edit report link.
        /// </summary>
        /// <param name="reportName"> The report Name. </param>
        /// <typeparam name="T"> Type of object to return.  </typeparam>
        /// <returns> New page instance. </returns>
        T ClickEditLinkByName<T>(string reportName) where T : ICreatablePageObject;

        /// <summary>
        ///  Click on edit selected link.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        T ClickEditSelected<T>() where T : ICreatablePageObject;

        /// <summary>
        /// Click on the left pane Reports link
        /// </summary>
        void ClickReportsLink();

        /// <summary>
        /// Click on the settings link.
        /// </summary>
        /// <typeparam name="T">instance to return </typeparam>
        /// <returns> New page object. </returns>
        T ClickManageAlertGroupsLink<T>() where T : ICreatablePageObject;

        /// <summary>
        /// Create an alert of the specified type
        /// </summary>
        /// <typeparam name="T"> 
        /// Type of object to return.
        /// </typeparam>
        /// <param name="alertType"> The alert Type. </param>
        /// <returns> new create alert page </returns>
        T ClickCreateAlertButton<T>(AlertType alertType) where T : ICreatablePageObject;

        /// <summary>
        /// Create capitol watch report.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        T CreateCapitolWatchReport<T>() where T : ICreatablePageObject;

        /// <summary>
        /// Delete Alert By Name
        /// </summary>
        /// <param name="nameOfAlert">name of Alert</param>
        void DeleteAlertByName(string nameOfAlert);

        /// <summary>
        /// Deletes all of the alerts on the AlertSearchResult page
        /// </summary>
        void DeleteAllAlerts();

        /// <summary>
        /// Get alert name by group.
        /// </summary>
        /// <param name="groupName"> The group Name. </param>
        /// <returns> Alert name. </returns>
        string GetAlertNameByGroup(string groupName);

        /// <summary>
        /// Get alert name by type.
        /// </summary>
        /// <param name="typeName">Type name. </param>
        /// <returns> Alert name. </returns>
        string GetAlertNameByType(string typeName);

        /// <summary>
        /// Get all recipients
        /// </summary>
        /// <returns> List of recipients </returns>
        List<string> GetAlertRecipients();

        /// <summary>
        /// Is success message displayed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsSuccessMessageDisplayed();

        /// <summary>
        /// Is success deleted message displayed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsSuccessDeletingMessageDisplayed();

        /// <summary>
        /// Verify if no Alerts message is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsNoAlertsMessageDisplayed();

        /// <summary>
        /// Set Select all items checkbox
        /// </summary>
        /// <param name="setTo">The set To.</param>
        void SelectAllItemsCheckbox(bool setTo);

        /// <summary>
        /// Set the first alert include checkbox to the specified value
        /// </summary>
        /// <param name="setTo">The set To.</param>
        void SelectFirstAlertCheckbox(bool setTo);

        /// <summary>
        /// Get update date by alert name
        /// </summary>
        /// <param name="alertName">Alert name </param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetLastUpdateDateByAlertName(string alertName);

        /// <summary>
        /// Get next update date by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetNextUpdateDateByAlertName(string alertName);

        /// <summary>
        /// Get Client Id by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetClientIdByAlertName(string alertName);

        /// <summary>
        /// Verify that email recipients link is displayed for specific alert
        /// </summary>
        /// <param name="alertName"> Alert Name </param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsEmailRecipientsLinkForAlertDisplayed(string alertName);

        /// <summary>
        /// Get text from field 'Alert even no results'
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetAlertEvenNoResultsText(string alertName);

        /// <summary>
        /// Get recipients count
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns>The <see cref="int"/>.</returns>
        int GetRecipientsCount(string alertName);

        /// <summary>
        /// Get search term by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetSearchTermByAlertName(string alertName);

        /// <summary>
        /// Gets the sidebar strong text.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string GetAlertTypeFacetTitleText();
    }
}