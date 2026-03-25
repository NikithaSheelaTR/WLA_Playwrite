
namespace Framework.Common.UI.Products.WestLawNextLinks.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Out of plan dialog on document page (appears after clicking on any link in the current document)
    /// </summary>
    public class WlnLinksOutOfPlanDialog : BaseModuleRegressionDialog
    {
        private static readonly By SignInToWLNButtonLocator = By.Id("co_outOfPlanSignInToWLNButton");

        private static readonly By DocWarningMsgLocator = By.ClassName("co_overlayBox_headline");

        private static readonly By OutOfPlanCancelButtonLocator = By.Id("co_OutOfPlanCancelButton");

        /// <summary>
        /// Gets warning message text
        /// </summary>
        /// <returns>warning msg text</returns>
        public string GetDocWarningMessageText() => DriverExtensions.WaitForElement(DocWarningMsgLocator).Text;

        /// <summary>
        /// Verify that sign in to WLN button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSignInToWlnButtonDisplayed() => DriverExtensions.IsDisplayed(SignInToWLNButtonLocator, 5);

        /// <summary>
        /// Click on the 'Cancel' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(OutOfPlanCancelButtonLocator);
    }
}
