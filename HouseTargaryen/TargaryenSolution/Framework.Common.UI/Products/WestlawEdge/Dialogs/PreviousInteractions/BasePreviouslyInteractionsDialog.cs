namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.PreviousInteractions
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using OpenQA.Selenium;

    /// <summary>
    /// Base Previously Interactions Dialog
    /// </summary>
    public class BasePreviouslyInteractionsDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator =
            By.XPath("//button[@class = 'co_infoBox_closeButton']");

        /// <summary>
        /// The click close button.
        /// </summary>
        /// <typeparam name="T">
        /// Type is implemented ICreatablePageObject
        /// </typeparam>
        /// <returns>
        /// new instance of T
        /// </returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);
    }
}
