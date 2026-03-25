namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// EdgeEndSharingDialog
    /// </summary>
    public class EdgeEndSharingDialog : BaseModuleRegressionDialog
    {
        private static readonly By EndSharingButtonLocator = By.XPath("//input[@value='End Sharing']");

        /// <summary>
        /// End sharing button
        /// </summary>
        public IButton EndSharingButton { get; } = new Button(EndSharingButtonLocator);
    }
}