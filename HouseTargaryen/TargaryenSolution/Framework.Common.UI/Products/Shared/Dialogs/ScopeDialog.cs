namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Dialog that pops up when you click on the Scope icon next to a publication title
    /// </summary>
    public class ScopeDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//input[@value='Close']");

        private static readonly By LegalNoticeHeaderLocator = By.Id("coid_scope_legal");

        private static readonly By ScopeTitleLocator = By.XPath("//*[contains(@id,'coid_lightboxAriaLabel_')]");

        private static readonly By SummaryHeaderLocator = By.Id("coid_scope_summary");

        private static readonly By XCloseButtonLocator = By.Id("co_browseScope_closeLink");

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// X close button
        /// </summary>
        public IButton XCloseButton => new Button(XCloseButtonLocator);

        /// <summary>
        /// Scope title label
        /// </summary>
        public ILabel ScopeTitleLabel => new Label(ScopeTitleLocator);

        /// <summary>
        /// Legal notice header label
        /// </summary>
        public ILabel LegalNoticeHeaderLabel => new Label(LegalNoticeHeaderLocator);

        /// <summary>
        /// Summary header label
        /// </summary>
        public ILabel SummaryHeaderLabel => new Label(SummaryHeaderLocator);
    }
}