namespace Framework.Common.UI.Products.Shared.Dialogs.CCPA
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Process Base Dialog
    /// </summary>
    public abstract class ProcessBaseDialog : BaseModuleRegressionDialog
    {
        private const string TitleLctMask = ".//h3[contains(@id,'coid_lightboxAriaLabel_') and text() = '{0}'] | .//h2[contains(@id,'coid_lightboxAriaLabel_') and text() = '{0}']";

        private static readonly By ContainerLocator = By.ClassName("co_overlayBox_container");
        private static readonly By InfoTextLocator = By.XPath(".//div[@class = 'co_overlayBox_content']");
        private static readonly By VisitButtonLocator = By.XPath(".//button[@id = 'co_confirmationLightbox_portalLink']");
        private static readonly By ConfirmationButtonLocator = By.XPath(".//button[@id = 'co_confirmationLightbox_okButton']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessBaseDialog"/> class. 
        /// </summary>
        /// <param name="title">Title 
        /// </param>
        protected ProcessBaseDialog(string title) => DriverExtensions.WaitForElementDisplayed(60000, ContainerLocator, By.XPath(string.Format(TitleLctMask, title)));

        /// <summary>
        /// Gets the Visit Privacy Portal button.
        /// </summary>
        /// <returns>
        /// The <see cref="IButton"/>.
        /// </returns>
        public IButton VisitPrivacyPortalButton = new Button(ContainerLocator, VisitButtonLocator);

        /// <summary>
        /// Gets the confirmation button.
        /// </summary>
        /// <returns>
        /// The <see cref="IButton"/>.
        /// </returns>
        public IButton ConfirmationButton = new Button(ContainerLocator, ConfirmationButtonLocator);

        /// <summary>
        /// Gets info label
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel InfoLabel = new Label(ContainerLocator, InfoTextLocator);
    }
}
