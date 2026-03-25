namespace Framework.Common.UI.Products.WestLawAnalytics.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents Light box dialog
    /// </summary>
    public class EditBillingInformationSubmittedChangesDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.Id("wa_changesSubmitted_close");
        
        /// <summary>
        /// Close Edit Information Dialog
        /// </summary>
        /// <returns><see cref="BillingInvestigationResultsPage"/></returns>
        public BillingInvestigationResultsPage ClickCloseButton()
            => this.ClickElement<BillingInvestigationResultsPage>(CloseButtonLocator);

        /// <summary>
        /// Checks whether CloseButton appeared or not
        /// </summary>
        /// <returns> true, if CloseButton appeared </returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator, 5);
    }
}