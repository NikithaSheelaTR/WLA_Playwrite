namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Create Custom Page Dialog
    /// </summary>
    public class CreateCustomPageDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.Id("cp_customPageCreate_cancel");

        private static readonly By CloseButtonLocator = By.XPath("//*[@class='co_overlayBox_closeButton co_iconBtn']");

        private static readonly By CreateButtonLocator = By.Id("cp_customPageCreate_create");

        private static readonly By DialogTitleLocator = By.XPath(".//div[@class='co_overlayBox_headline']/div/*[self::h2 or self::h3]");

        private static readonly By NameInputLocator = By.Id("cp_customPageCreate_name_input");

        private static readonly By NeedHelpTrainingLinkLocator = By.CssSelector("a#cp_customPageCreate_help_link");

        /// <summary>
        /// Need Help/Training link
        /// </summary>
        public ILink NeedHelpLink => new Link(NeedHelpTrainingLinkLocator);

        /// <summary>
        /// Click Cancel Button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page.</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Click Close Dialog Button
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page.</returns>
        public T ClickCloseDialogButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Enter Name And Click Create button
        /// </summary>
        /// <param name="pageName">Custom Page Name</param>
        /// <returns>new Custom Page</returns>
        public T EnterNameAndClickCreateButton<T>(string pageName) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(pageName, NameInputLocator);
            return this.ClickElement<T>(CreateButtonLocator);
        }

        /// <summary>
        /// Get Dialog Title
        /// </summary>
        /// <returns>string with Dialog name</returns>
        public string GetDialogTitle() => DriverExtensions.GetText(DialogTitleLocator);
    }
}