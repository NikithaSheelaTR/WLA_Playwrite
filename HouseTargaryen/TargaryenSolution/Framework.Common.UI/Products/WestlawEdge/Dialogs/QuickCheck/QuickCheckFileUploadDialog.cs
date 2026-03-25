namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Optional adjustments=>Get report=>Dialog
    /// </summary>
    public sealed class QuickCheckFileUploadDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_ba_fileUploadLightbox");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='co_overlayBox_closeButton co_iconBtn']");
        private static readonly By MinimizeButtonLocator = By.XPath(".//li/*[@class='co_primaryBtn']");
        private static readonly By CancelButtonLocator = By.XPath(".//button[contains(text(),'Cancel')]");
        private static readonly By OcrMessageLocator = By.XPath(".//div[@class='co-doc-analyzer-animation-ocr-message']");

        /// <summary>
        /// Gets the close button.
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Gets the minimize button.
        /// </summary>
        public IButton MinimizeButton => new Button(ContainerLocator, MinimizeButtonLocator);

        /// <summary>
        /// Gets the cancel button.
        /// </summary>
        public IButton CancelButton => new Button(ContainerLocator, CancelButtonLocator);

        /// <summary>
        /// Gets the ocr message label
        /// </summary>
        public ILabel OcrMessageLabel => new Label(ContainerLocator, OcrMessageLocator);

        /// <summary>
        /// The WaitUntilFileUpload
        /// </summary>
        /// <param name="waitTime">The time to wait</param>
        public void WaitUntilFileUpload(int waitTime = 2100000) =>
            DriverExtensions.WaitForElementNotDisplayed(waitTime, ContainerLocator);

        /// <summary>
        /// The wait until file upload and get report page.
        /// </summary>
        /// <param name="waitTime">
        /// The wait Time.
        /// </param>
        /// <returns>
        /// The <see cref="QuickCheckRecommendationsPage"/>.
        /// </returns>
        public QuickCheckRecommendationsPage WaitUntilFileUploadAndGetReportPage(int waitTime = 1260000)
        {
            this.WaitUntilFileUpload(waitTime);
            return new QuickCheckRecommendationsPage();
        }

        /// <summary>
        /// The wait until file upload and get report page.
        /// </summary>
        /// <param name="waitTime">
        /// The wait Time.
        /// </param>
        /// <returns>
        /// The <see cref="WestlawAdvantageQuickCheckRecommendationsPage"/>.
        /// </returns>
        public WestlawAdvantageQuickCheckRecommendationsPage WestlawAdvantageWaitUntilFileUploadAndGetReportPage(int waitTime = 1260000)
        {
            this.WaitUntilFileUpload(waitTime);
            return new WestlawAdvantageQuickCheckRecommendationsPage();
        }
    }
}