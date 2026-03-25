namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all specific methods pertaining to the JurisdictionOptionsModal in Edge
    /// </summary>
    public class EdgeJurisdictionOptionsDialog : JurisdictionOptionsDialog
    {
        private static readonly By ClearAllButtonLocator = By.Id("co_clearSelectedJurisdictionsBtn");

        /// <summary>
        /// Clear All button
        /// </summary>
        public IButton ClearAllButton => new Button(ClearAllButtonLocator);       
    }
}
