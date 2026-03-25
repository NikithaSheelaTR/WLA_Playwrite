namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Rename Page Dialog
    /// </summary>
    public class RenamePageDialog : BaseModuleRegressionDialog
    {
        private static readonly By NameInputLocator = By.Id("cp_rename_input");

        private static readonly By SaveButtonLocator = By.Id("cp_rename_save");

        /// <summary>
        /// Enter Name and click Save
        /// </summary>
        /// <param name="pageName">The page Name.</param>
        /// <returns>The <see cref="ICreatablePageObject"/>.</returns>
        public T EnterNameAndClickSaveButton<T>(string pageName) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(pageName, NameInputLocator);
            return this.ClickElement<T>(SaveButtonLocator);
        }
    }
}