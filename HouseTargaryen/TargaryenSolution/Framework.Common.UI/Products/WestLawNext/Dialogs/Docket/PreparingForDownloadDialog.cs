namespace Framework.Common.UI.Products.WestLawNext.Dialogs.Docket
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Pages.Dockets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Preparing For Download Dialog
    /// </summary>
    public class PreparingForDownloadDialog : BaseModuleRegressionDialog
    {
        private static readonly By DialogContainerLocator =
            By.XPath("//div[@class= 'co_overlayBox_container co_overlayBox_delivery']");

        private static readonly By PdfDownloadBlockedTextLocator = By.Id("co_pdfDownloadBlockedText");

        private static readonly By CheckboxLocator = By.XPath(".//input[@type = 'checkbox']");

        private static readonly By SubmitButtonLocator = By.Id("co_warningBlockSubmitButton");

        private static readonly By CancelButtonLocator = By.Id("co_warningBlockCancelButton");

        /// <summary>
        /// Click on 'Cancel' button
        /// </summary>
        /// <returns><see cref="PrepareMultiplePdfRequestPage"/></returns>
        public PrepareMultiplePdfRequestPage ClickCancelButton() =>
            this.ClickElement<PrepareMultiplePdfRequestPage>(CancelButtonLocator);

        /// <summary>
        /// Click on 'Submit' button
        /// </summary>
        /// <returns><see cref="PrepareMultiplePdfRequestPage"/></returns>
        public PrepareMultiplePdfRequestPage ClickSubmitButton() =>
            this.ClickElement<PrepareMultiplePdfRequestPage>(SubmitButtonLocator);

        /// <summary>
        ///  Get count of the selected checkboxes
        /// </summary>
        /// <returns> Count of the selected checkboxes </returns>
        public int GetSelectedCheckboxesCount() =>
            this.GetListCheckboxes().Where(elem => DriverExtensions.IsCheckboxSelected(elem)).ToList().Count;

        /// <summary>
        /// Set check box by index
        /// </summary>
        /// <param name="index"> the index of the checkbox </param>
        /// <param name="isSet"> the status of the checkbox</param>
        public void SetCheckBoxByIndex(int index, bool isSet) =>
            this.GetListCheckboxes().ElementAt(index).SetCheckbox(isSet);

        /// <summary>
        /// Get Pdf Download Blocked Text
        /// </summary>
        /// <returns>The text of Pdf Download Blocked message</returns>
        public string GetPdfDownloadBlockedText() =>
            DriverExtensions.GetElement(PdfDownloadBlockedTextLocator).Text;

        /// <summary>
        /// Are all checkboxes enabled
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
        public bool AreAllCheckboxesEnabled() => this.GetListCheckboxes().All(checkbox => checkbox.Enabled);

        /// <summary>
        /// Is 'Submit' button displayed
        /// </summary>
        /// /// <returns>True if displayed, false otherwise</returns>
        public bool IsSubmitButtonDisplayed() => DriverExtensions.IsDisplayed(SubmitButtonLocator);

        /// <summary>
        /// Is 'Submit' button enabled
        /// </summary>
        /// /// <returns>True if enabled, false otherwise</returns>
        public bool IsSubmitButtonEnabled() => DriverExtensions.IsEnabled(SubmitButtonLocator);

        /// <summary>
        /// Get list checkboxes
        /// </summary>
        /// <returns>The list of checkboxes</returns>
        private List<IWebElement> GetListCheckboxes() =>
            DriverExtensions.GetElements(DialogContainerLocator, CheckboxLocator).ToList();
    }
}