namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// LightBox with Save and Cancel buttons
    /// </summary>
    public class BaseManagePageDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.CssSelector("ul>li>a.co_overlayBox_buttonCancel");

        private static readonly By SaveButtonLocator = By.CssSelector("ul>li>input[value='Save']");

        private static readonly By TitleLocator = By.CssSelector("div.co_overlayBox_headline h3");

        /// <summary>
        /// Gets Title of Dialog
        /// </summary>
        public string Title => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Click on the 'Cancel' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelLink<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Click on the 'Save' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);
    }
}