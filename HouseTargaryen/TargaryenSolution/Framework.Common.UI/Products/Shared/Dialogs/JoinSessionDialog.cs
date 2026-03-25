namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// Join session? dialog
    /// Is displayed on a page when the page has been served from the previous session and a newer one does exist.
    /// </summary>
    public class JoinSessionDialog : BaseModuleRegressionDialog
    {
        private static readonly By JoinSessionDialogLocator = By.Id("co_websiteJoinSessionDialog");

        private static readonly By JoinSessionButtonLocator = By.XPath(".//button[@name='JoinSession']");

        private static readonly By ViewHistoryButtonLocator = By.XPath(".//button[@name='ViewHistory']");

        private static readonly By ViewHomeButtonLocator = By.XPath(".//button[@name='ViewHome']");

        /// <summary>
        /// Join session button
        /// </summary>
        public IButton JoinSessionButton => new Button(JoinSessionDialogLocator, JoinSessionButtonLocator);

        /// <summary>
        /// View history button
        /// </summary>
        public IButton ViewHistoryButton => new Button(JoinSessionDialogLocator, ViewHistoryButtonLocator);

        /// <summary>
        /// View home button
        /// </summary>
        public IButton ViewHomeButton => new Button(JoinSessionDialogLocator, ViewHomeButtonLocator);
    }
}
