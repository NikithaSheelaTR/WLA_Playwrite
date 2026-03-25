namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Create New Content Cui Section Dialog
    /// </summary>
    public class ConvertCuiDialog : BaseManagePageDialog
    {
        private static readonly By ConvertButtonLocator = By.Id("cp_convertCuiPage_convert");

        private static readonly By CuiPageUrlTextboxLocator = By.Id("cp_convertCuiPage_input");

        /// <summary>
        /// Add Category Page List
        /// </summary>
        /// <param name="url">URL with Category Pages</param>
        public void AddCuiContent(string url)
        {
            DriverExtensions.SetTextField(url, CuiPageUrlTextboxLocator);
            this.ClickElement(ConvertButtonLocator);
        }
    }
}