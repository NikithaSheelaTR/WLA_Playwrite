namespace Framework.Common.UI.Products.Shared.Dialogs.Dockets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Pages.Dockets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Update Requests dialog that appears when clicking the Update button for a docket
    /// </summary>
    public class MultiDocketUpdateRequestsDialog : BaseDocketUpdateDialog
    {
        private static readonly By CaseTitleLocator =
            By.CssSelector("#co_docketUpdatesTable tbody .co_detailsTable_content");

        private static readonly By SelectAllCheckboxLocator = By.Id("co_toggleDocketUpdateCbxs");

        private static readonly By SubmitButtonLocator = By.Id("co_docketUpdateSubmitButton");

        private static readonly By UpdateProgressMessageLocator = By.Id("co_docketUpdateWaitProgress");

        private static readonly By UpdateRequestsDialogLocator = By.Id("co_docketsWaitDialog");

        private static readonly By ViewDocketRequestsButtonLocator = By.Id("co_docketUpdateCompleteButton");

        /// <summary>
        /// Clicks the Submit button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The next opened dialog </returns>
        public T ClickSubmitButton<T>() where T : BaseModuleRegressionDialog => this.ClickElement<T>(SubmitButtonLocator);

        /// <summary>
        /// Gets the titles of the dockets listed in the dialog
        /// </summary>
        /// <returns> List of titles </returns>
        public List<string> GetCaseTitles()
        {
            DriverExtensions.WaitForElementDisplayed(CaseTitleLocator);
            return DriverExtensions.GetElements(UpdateRequestsDialogLocator, CaseTitleLocator).Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Gets the processing update message
        /// </summary>
        /// <returns> Processing update message </returns>
        public string GetUpdateProgressMessage() => DriverExtensions.GetText(UpdateProgressMessageLocator);

        /// <summary>
        /// Sets the Select All checkbox
        /// </summary>
        /// <param name="check"> True if the checkbox should be checked, false otherwise </param>
        public void SetSelectAllCheckbox(bool check) => DriverExtensions.SetCheckbox(SelectAllCheckboxLocator, check);

        /// <summary>
        /// Clicks the View the Document Requests page button
        /// </summary>
        /// <returns> Dockets Requests Page </returns>
        public DocketsRequestsPage ViewDocketRequests()
            => this.ClickElement<DocketsRequestsPage>(ViewDocketRequestsButtonLocator);

        /// <summary>
        /// Waits for the dockets update to complete
        /// </summary>
        /// <typeparam name="T"> T type </typeparam>
        /// <returns> T page </returns>
        public T WaitForUpdateComplete<T>() where T : ICreatablePageObject
            => this.WaitForUpdateComplete<T>(200000);
    }
}