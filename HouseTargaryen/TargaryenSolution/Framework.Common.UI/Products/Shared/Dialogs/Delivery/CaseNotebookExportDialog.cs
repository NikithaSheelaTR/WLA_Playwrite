namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// 'Export to Case Notebook' Dialog
    /// </summary>
    public class CaseNotebookExportDialog : BaseDeliveryDialog
    {
        private static readonly By ExportButtonLocator = By.Id("co_deliveryExportButton");

        private static readonly By MatterNumberTextboxLocator = By.Id("co_matterNumberTextbox");

        private static readonly By TitleTextboxLocator = By.Id("co_delivery_title");

        /// <summary>
        /// Clicks 'Export' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickExportButton<T>() where T : ICreatablePageObject
            => this.ClickButtonAndWaitSpinnerToDisappear<T>(ExportButtonLocator);

        /// <summary>
        /// Gets matter number text from textbox
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetMatterNumberText() =>
            DriverExtensions.WaitForElement(MatterNumberTextboxLocator).GetAttribute("value");

        /// <summary>
        /// Gets title text from textbox
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTitleText() =>
            DriverExtensions.WaitForElement(TitleTextboxLocator).GetAttribute("value");
    }
}
