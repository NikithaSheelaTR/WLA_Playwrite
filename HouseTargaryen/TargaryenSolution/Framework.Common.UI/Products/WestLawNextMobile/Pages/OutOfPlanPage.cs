namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Out of Plan page
    /// </summary>
    public class OutOfPlanPage : MobileBasePage
    {
        private const string PageTitle = "Out of Plan";

        private static readonly By BackButtonLocator = By.ClassName("cancelLink");

        private static readonly By OutOfPlanMessageLocator = By.ClassName("padBot");

        private static readonly By PageTitleLocator = By.ClassName("hdr");

        private static readonly By ViewDocumentButtonLocator = By.Id("viewDocumentLink");

        /// <summary>
        /// Clicks the back button on the page
        /// </summary>
        /// <typeparam name="T">The class of the object to return</typeparam>
        /// <returns>A new object of the class specified to return</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the view document button on the page
        /// </summary>
        /// <typeparam name="T">The class of the object to return</typeparam>
        /// <returns>A new object of the class specified to return</returns>
        public T ClickViewDocumentButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ViewDocumentButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the text of the Out of Plan message
        /// </summary>
        /// <returns>The text</returns>
        public string GetOutOfPlanMessage() => DriverExtensions.GetText(OutOfPlanMessageLocator);

        /// <summary>
        /// Checks if the cancel button is present and visible.
        /// </summary>
        /// <returns>If the cancel button is present and visible.</returns>
        public bool IsCancelButtonDisplayed() => DriverExtensions.IsDisplayed(BackButtonLocator, 5);

        /// <summary>
        /// Checks whether the Out of Plan page is open based on the page header
        /// </summary>
        /// <returns> True if title is expected </returns>
        public bool IsPageTitleExpected() => DriverExtensions.GetText(PageTitleLocator).Equals(PageTitle);

        /// <summary>
        /// Checks if the view document button is present and visible.
        /// </summary>
        /// <returns>If the view document button is present and visible.</returns>
        public bool IsViewDocumentButtonDisplayed() => DriverExtensions.IsDisplayed(ViewDocumentButtonLocator, 5);
    }
}