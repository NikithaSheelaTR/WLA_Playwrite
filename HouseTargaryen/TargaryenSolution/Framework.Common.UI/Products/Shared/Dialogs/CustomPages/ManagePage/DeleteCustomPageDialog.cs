namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage
{
    using Framework.Common.UI.Interfaces;

    using OpenQA.Selenium;

    /// <summary>
    /// Delete Custom Page LightBox
    /// </summary>
    public class DeleteCustomPageDialog : BaseManagePageDialog
    {
        private static readonly By YesButtonLocator = By.XPath("//input[@id='cp_deletePage_confirm']");

        /// <summary>
        /// Click on the 'Yes' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickYesButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(YesButtonLocator);
    }
}