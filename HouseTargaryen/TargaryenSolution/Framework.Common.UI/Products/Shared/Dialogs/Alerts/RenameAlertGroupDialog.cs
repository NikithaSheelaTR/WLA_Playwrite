namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// RenameAlertGroupDialog
    /// </summary>
    public class RenameAlertGroupDialog : BaseModuleRegressionDialog
    {
        private static readonly By GroupNameTextboxLocator = By.Id("co_alerts_groupRenameText");

        private static readonly By SaveButtonLocator = By.Id("co_alerts_groupRenameSave");

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Group name textbox
        /// </summary>
        public ITextbox GroupNameTextbox => new Textbox(GroupNameTextboxLocator);
    }
}
