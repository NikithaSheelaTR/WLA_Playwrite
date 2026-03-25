namespace Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// POM for PreviewDialog
    /// </summary>
    public class PreviewDialog : BaseModuleRegressionDialog
    {
        private static readonly By DoneButtonLocator = By.XPath("//div[@id='previewHTMLLightbox']//input[@value='Done']");

        /// <summary>
        /// Verifies that the preview dialog is displayed 
        /// </summary>
        public bool IsDoneButtonDisplayed => DriverExtensions.IsDisplayed(DoneButtonLocator, 5);

        /// <summary>
        /// Click on the done button
        /// </summary>
        public void ClickDone() => this.ClickElement(DoneButtonLocator);
    }
}