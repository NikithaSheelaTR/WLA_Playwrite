namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search tips dialog
    /// </summary>
    public class SearchTipsDialog : BaseModuleRegressionDialog
    {
        private static readonly By TipsLocator = By.XPath(".//div[contains(@id,'panel_')]");
        private static readonly By HeadingLocator = By.XPath(".//li[contains(@id,'tab')]");
        private static readonly By ContainerLocator = By.Id("coid_SearchTipsLightbox");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class = 'co_primaryBtn']");
        private static readonly By TitleLocator = By.XPath(".//h2[contains(@id, 'coid_lightboxAriaLabel')]");

        /// <summary>
        /// Dialog title
        /// </summary>
        public ILabel Title => new Label(this.Container, TitleLocator);
        
        /// <summary>
        /// Click 'Close' button
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);

        /// <summary>
        /// List of heading button
        /// </summary>
        public IReadOnlyCollection<IButton> HeadingButtons => new ElementsCollection<Button>(ContainerLocator, HeadingLocator);

        /// <summary>
        /// List of tips
        /// </summary>
        public IReadOnlyCollection<ILabel> Tips => new ElementsCollection<Label>(ContainerLocator, TipsLocator);

        /// <summary>
        /// Container element
        /// </summary>
        private IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);
    }
}