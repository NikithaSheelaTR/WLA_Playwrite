namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Add Note page
    /// </summary>
    public class AddNotePage : MobileBasePage
    {
        private static readonly By CancelButtonLocator = By.Id("coid_website_docNoteCancel");

        private static readonly By ErrorMessageLocator = By.Id("saveErrorServer");

        private static readonly By NoteTextBoxLocator = By.Id("noteText");

        private static readonly By SaveButtonLocator = By.Id("coid_website_docNoteSave");
        
        /// <summary>
        /// Cancel the note and goes back to the previous page
        /// </summary>
        /// <typeparam name="T">The class of the page object to return</typeparam>
        /// <returns>The page-object</returns>
        public T CancelNote<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the text of the error message
        /// </summary>
        /// <returns>The error message text</returns>
        public string GetErrorText() => DriverExtensions.GetText(ErrorMessageLocator);

        /// <summary>
        /// Saves the note and goes back to the previous page
        /// </summary>
        /// <typeparam name="T">The class of the page object to return</typeparam>
        /// <returns>The page-object</returns>
        public T SaveNote<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Sets the note text field to given text
        /// </summary>
        /// <param name="text">The text to set the field to</param>
        public void SetNoteText(string text)
        {
            DriverExtensions.SetTextField(text, NoteTextBoxLocator);
            DriverExtensions.WaitForJavaScript();
        }
    }
}