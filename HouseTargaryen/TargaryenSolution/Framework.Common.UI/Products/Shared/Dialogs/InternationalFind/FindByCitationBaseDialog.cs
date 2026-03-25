namespace Framework.Common.UI.Products.Shared.Dialogs.InternationalFind
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Non Unique Citations Dialog
    /// </summary>
    public abstract class FindByCitationBaseDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// The Dialog Header template.
        /// </summary>
        protected const string HeaderLctMask =
            "//div[@class='co_overlayBox_headline']//div[@id='co_headerMessage' and contains(text(), '{0}')]";

        private static readonly By CancelButtonLocator = By.Id("co_deliveryCancelButton");

        private static readonly By NextButtonLocator = By.Id("co_next");

        /// <summary>
        /// Initializes a new instance of the <see cref="FindByCitationBaseDialog"/> class. 
        /// </summary>
        /// <param name="dialogName"> Dialog name</param>
        protected FindByCitationBaseDialog(string dialogName)
        {
            this.DialogHeader = By.XPath(string.Format(HeaderLctMask, dialogName));
            DriverExtensions.WaitForElementDisplayed(this.DialogHeader);
        }

        /// <summary>
        /// The Dialog header text.
        /// </summary>
        public string DialogTitle => DriverExtensions.GetText(this.DialogHeader);

        /// <summary>
        /// Gets the Dialog Header <see cref="By"/> object.
        /// </summary>
        private By DialogHeader { get; }

        /// <summary>
        /// Click Cancel button
        /// </summary>
        /// <typeparam name="TPage">The page object type.</typeparam>
        /// <returns>The page object instance.</returns>
        public TPage ClickCancel<TPage>() where TPage : ICreatablePageObject
            => this.ClickElement<TPage>(CancelButtonLocator);

        /// <summary>
        /// Clicks Next button
        /// </summary>
        /// <typeparam name="TPage">The page object type.</typeparam>
        /// <returns>The page object instance.</returns>
        public TPage ClickNext<TPage>() where TPage : ICreatablePageObject => this.ClickElement<TPage>(NextButtonLocator);
    }
}