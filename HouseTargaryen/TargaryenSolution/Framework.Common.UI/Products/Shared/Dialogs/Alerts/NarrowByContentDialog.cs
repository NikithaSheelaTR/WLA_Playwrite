namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts.NarrowByContent;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods and elements pertaining to the NarrowByContentModal
    /// </summary>
    public class NarrowByContentDialog : BaseModuleRegressionDialog
    {
        private static readonly By SaveButtonLocator = By.Id("keycite_alerts_filterbutton");
        
        /// <summary>
        /// Browse Component
        /// </summary>
        public NarrowByContentTabPanelComponent NarrowByContentTabPanelComponent { get; } = new NarrowByContentTabPanelComponent();
           
        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);
    }
}