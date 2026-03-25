namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The document analyzer welcome to quick check dialog.
    /// </summary>
    public class QuickCheckWelcomeToQuickCheckDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.ClassName("co_primaryBtn");

        /// <summary>
        /// Gets the close button.
        /// </summary>
        public IButton CloseButton => new Button(this.ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// The container locator.
        /// </summary>
        private By ContainerLocator =>
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.QuickCheckWelcomeVideo].LocatorString);
    }
}
