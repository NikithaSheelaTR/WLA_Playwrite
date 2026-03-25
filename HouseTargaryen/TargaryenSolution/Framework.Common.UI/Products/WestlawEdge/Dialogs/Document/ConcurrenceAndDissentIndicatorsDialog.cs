namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Concurrence and dissent indicators dialog
    /// </summary>
    public class ConcurrenceAndDissentIndicatorsDialog : BaseModuleRegressionDialog
    {
        private static readonly By HideButtonLocator = By.XPath(".//button[@id ='coid_shadingRemovalSave']");
        private static readonly By CancelButtonLocator = By.XPath(".//button[@id ='coid_shadingRemovalCancel']");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@id ='coid_shadingRemovalClose']");
        private static readonly By DialogHeadingLocator = By.XPath(".//h2[contains(@id, 'coid_lightboxAriaLabel_')]");
        private static readonly By DialogDescriptionLocator = By.XPath(".//div[@class = 'co_overlayBox_content']");

        private readonly IWebElement container = DriverExtensions.GetElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.ConcurrenceAndDissentIndicatorsDialog].LocatorString));
        
        /// <summary>
        /// Hide button
        /// </summary>
        public IButton HideButton => new Button(this.container, HideButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(this.container, CancelButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.container, CloseButtonLocator);

        /// <summary>
        /// Gets the dialog heading label.
        /// </summary>
        public ILabel DialogHeadingLabel => new Label(this.container, DialogHeadingLocator);

        /// <summary>
        /// Gets the dialog description label.
        /// </summary>
        public ILabel DialogDescriptionLabel => new Label(this.container, DialogDescriptionLocator);
    }
}