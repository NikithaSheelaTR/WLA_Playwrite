namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Overruling Risk Dialog
    /// </summary>
    public class OverrulingRiskDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("co_kcFlagPopup_closeButton");
        private static readonly By LinkLocator = By.XPath("//div[@id='co_kcOverrulingFlagPopup' or contains(@class,'co_kcFlagPopup_doc')]//a");
        private static readonly By TitleLocator = By.XPath("//*[@id='co_kcOverrulingFlagPopup']//h3");

        /// <summary>
        /// Document title link
        /// </summary>
        public ILink DocumentTitleLink => new Link(LinkLocator);

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);        
    }
}