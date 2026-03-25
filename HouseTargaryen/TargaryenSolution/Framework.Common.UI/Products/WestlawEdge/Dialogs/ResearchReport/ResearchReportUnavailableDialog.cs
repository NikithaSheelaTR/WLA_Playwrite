namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.ResearchReport
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The research report unavailable dialog. Appears in case of creating report from an empty folder
    /// </summary>
    public class ResearchReportUnavailableDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//a[contains(@class,'closeLightbox')]");

        private static readonly By DialogTitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h3");

        private static readonly By DialogMessageLocator = By.Id("cobalt_ro_folder_action_label");

        /// <summary>
        /// The get dialog title text.
        /// </summary>
        /// <returns> Dialog title </returns>
        public string GetDialogTitleText() => DriverExtensions.GetText(DialogTitleLocator).Trim();

        /// <summary>
        /// The get dialog message text.
        /// </summary>
        /// <returns> Message </returns>
        public string GetDialogMessageText() => DriverExtensions.GetText(DialogMessageLocator).Trim();

        /// <summary>
        /// The is close button displayed.
        /// </summary>
        /// <returns> True if Close button is displayed, false otherwise </returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator);
    }
}
