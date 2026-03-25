namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// The Unverified Citations dialog.
    /// </summary>
    public class UnverifiedCitationsDialog : BaseModuleRegressionDialog
    {
        private static readonly By CitationTitleLocator = By.XPath("//ul[@class='DA-UnverifiedCitations']//li//span[2]");
        private static readonly By CloseButtonLocator = By.XPath("//*[@class='co_primaryBtn']");    
        private static readonly By XButtonLocator = By.XPath("//*[@class='co_overlayBox_closeButton co_iconBtn']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2[contains(@id,'coid_lightboxAriaLabel')]");

        /// <summary>
        /// Gets the close button.
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Gets the x button.
        /// </summary>
        public IButton XButton => new Button(XButtonLocator);

        /// <summary>
        /// Citation titles
        /// </summary>
        public IReadOnlyCollection<ILabel> CitationTitles => new ElementsCollection<Label>(CitationTitleLocator);

        /// <summary>
        /// Title of the dialog
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLabelLocator);
    }
}