namespace Framework.Common.UI.Products.Shared.Dialogs.CCPA
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Working dialog
    /// </summary>
    public class WorkingDialog : ICreatablePageObject
    {
        private static readonly By ContainerLocator = By.ClassName("co_overlayBox_container");
        private static readonly By TitleLocator = By.XPath(".//h3[contains(@id,'coid_lightboxAriaLabel_') and text() = 'Working...']|.//h2[contains(@id,'coid_lightboxAriaLabel_') and text() = 'Working...']");
        private static readonly By SpinnerLocator = By.XPath(".//div[@class = 'co_loading']");

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingDialog"/> class.
        /// </summary>
        public WorkingDialog()
        {
            DriverExtensions.WaitForElementDisplayed(60000, ContainerLocator, TitleLocator);
        }

        /// <summary>
        /// Verify if the spinner diplayed on modal
        /// </summary>
        /// <returns>True if its's displayed</returns>
        public bool IsSpinnerDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, SpinnerLocator);
    }
}
