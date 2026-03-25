namespace Framework.Common.UI.Products.WestlawNextCorrectional.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Correctional search Within Document Dialog
    /// </summary>
    public class CorrectionalSearchWithinDocumentDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("co_docSearchWithinCloseButton");

        private static readonly By DialogContainerLocator = By.XPath("//div[@id = 'co_docSearchWithinDropdownContainerInnerContent']");

        private static readonly By NextTermArrowLocator = By.XPath(".//a[contains(@class, 'co_next')]");

        private static readonly By SearchButtonLocator = By.Id("co_docSearchWithin_searchButton");

        private static readonly By SearchInputLocator = By.XPath("//textarea[contains(@id, 'co_docSearchWithin_searchInput')]");

        private static readonly By SearchWithinHelpLocator = By.Id("co_docSearchWithin_help");

        /// <summary>
        /// NextTermArrowButton
        /// </summary>
        public IButton NextTermArrowButton => new Button(DialogContainerLocator, NextTermArrowLocator);

        /// <summary>
        /// CloseButton
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// SearchButton
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);

        /// <summary>
        /// SearchInputTextbox
        /// </summary>
        public ITextbox SearchInputTextbox => new Textbox(SearchInputLocator);

        /// <summary>
        /// SearchWithinHelpButton
        /// </summary>
        public IButton SearchWithinHelpButton => new Button(SearchWithinHelpLocator);

        /// <summary>
        /// This methods searches within the current document
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="searchQuery"> Search term </param>
        /// <returns> New instance of the page </returns>
        public T SearchWithinDocument<T>(string searchQuery) where T : ICreatablePageObject
        {
            this.EnterSearchWithinQuery(searchQuery);
            SearchButton.Click();
            DriverExtensions.WaitForElementDisplayed(NextTermArrowLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// This methods enters Search Within query
        /// </summary>
        /// <param name="searchQuery">Search term</param>
        public void EnterSearchWithinQuery(string searchQuery)
            => SearchInputTextbox.SendKeysSlow(searchQuery);
    }
}
