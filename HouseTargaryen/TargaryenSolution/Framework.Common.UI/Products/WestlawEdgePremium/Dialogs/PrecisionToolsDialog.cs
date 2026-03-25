namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// This class models the tools flyout dialog.
    /// </summary>
    public class PrecisionToolsDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class='Panel-right']");
        private static readonly By CloseButtonLocator = By.XPath(".//*[@class='Panel-close']");
        private static readonly By ToolLinkLocator = By.ClassName("Athens-Promo-Panel-tool-label");

        /// <summary>
        /// Tool Flyout Labels
        /// </summary>
        public IReadOnlyCollection<ILink> ToolLinks => new ElementsCollection<Link>(ContainerLocator, ToolLinkLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);
    }
}
