namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SimpleInputDialog
    /// </summary>
    public class SimpleInputDialog : BaseModuleRegressionDialog
    {
        private static readonly By DialogContainerLocator = By.XPath("//div[@class='co_facet_simpleInputContainer']");

        private static readonly By GoButtonLocator = By.XPath(".//button[@class='co_primaryBtn']");

        private static readonly By TextInputLocator = By.XPath(".//input[@type='text']");

        /// <summary>
        /// SetTextFiled
        /// </summary>
        /// <param name="text">The text</param>
        public void SetTextFiled(string text)
        {
            DriverExtensions.SetTextField(text, DialogContainerLocator, TextInputLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on the Go button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickGoButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(DriverExtensions.GetElement(DialogContainerLocator, GoButtonLocator));
    }
}