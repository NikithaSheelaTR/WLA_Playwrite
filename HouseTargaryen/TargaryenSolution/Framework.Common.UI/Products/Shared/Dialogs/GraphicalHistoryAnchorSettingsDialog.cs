namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when clicking Settings button on anchor
    /// </summary>
    public class GraphicalHistoryAnchorSettingsDialog : BaseModuleRegressionDialog
    {
        private static readonly By EmphasizeHighActivityCheckboxLocator = By.XPath("//input[@id = 'GH-Zoom-Settings-EmphasizeHighActivityPaths']");
        private static readonly By MinimizeLowActivityCheckboxLocator = By.XPath("//input[@id = 'GH-Zoom-Settings-MinimizeLowActivityPaths']");
        private static readonly By ShowEventOrderCheckboxLocator = By.XPath("//input[@id = 'GH-Zoom-Settings-ShowEventOrder']");
        private static readonly By ShowMiniMapCheckboxLocator = By.XPath("//input[@id = 'GH-Zoom-Settings-ShowMinimap']");
        private static readonly By ApplyButtonLocator = By.XPath("//div[@class='GH-Zoom-Settings-buttons']//button[@class='Button-primary']");

        /// <summary>
        /// Apply button 
        /// </summary>
        public IButton ApplyButton => new Button(ApplyButtonLocator);

        /// <summary>
        /// Emphasize High Activity checkbox
        /// </summary>
        public ICheckBox EmphasizeHighActivityCheckBox => new CheckBox(EmphasizeHighActivityCheckboxLocator);

        /// <summary>
        /// Minimize Low Activity checkbox
        /// </summary>
        public ICheckBox MinimizeLowActivityCheckBox => new CheckBox(MinimizeLowActivityCheckboxLocator);

        /// <summary>
        /// Show event order checkbox
        /// </summary>
        public ICheckBox ShowEventOrderCheckBox => new CheckBox(ShowEventOrderCheckboxLocator);

        /// <summary>
        /// Show Mini Map checkbox
        /// </summary>
        public ICheckBox ShowMiniMapCheckBox => new CheckBox(ShowMiniMapCheckboxLocator);
    }
}
