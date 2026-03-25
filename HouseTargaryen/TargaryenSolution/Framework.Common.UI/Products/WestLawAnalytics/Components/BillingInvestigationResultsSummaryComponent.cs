namespace Framework.Common.UI.Products.WestLawAnalytics.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BillingInvestigationResultsSummary
    /// </summary>
    public class BillingInvestigationResultsSummaryComponent : BaseModuleRegressionComponent
    {
        private static readonly By EditSearchButtonLocator = By.Id("co_billingInvestigationEditSearchButton");

        private static readonly By NewSearchButtonLocator = By.Id("co_billingInvestigationNewSearchButton");

        private static readonly By TimeFrameLabelLocator = By.Id("co_billingInvestigationResultsSummaryTimeFrame");

        private static readonly By ContainerLocator = By.Id("co_billingInvestigationResultsSummary");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the 'Edit Search' button on the billing investigator search results page.
        /// </summary>
        /// <returns> The <see cref="BillingInvestigationPage"/>. </returns>
        public BillingInvestigationPage ClickEditSearchButton()
        {
            DriverExtensions.WaitForElementDisplayed(EditSearchButtonLocator).Click();
            return new BillingInvestigationPage();
        }

        /// <summary>
        /// Verify that Edit Search button is displayed
        /// </summary>
        /// <returns> True if button is displayed, false otherwise </returns>
        public bool IsEditSearchButtonDisplayed() => DriverExtensions.IsDisplayed(EditSearchButtonLocator, 5);

        /// <summary>
        /// Click on the 'New Search' button on the billing investigator search results page.
        /// </summary>
        /// <returns> The <see cref="BillingInvestigationPage"/>. </returns>
        public BillingInvestigationPage ClickNewSearchButton()
        {
            DriverExtensions.WaitForElementDisplayed(NewSearchButtonLocator).Click();
            return new BillingInvestigationPage();
        }

        /// <summary>
        ///     Gets the time frame as one string
        /// </summary>
        /// <returns>The time frame  as a string</returns>
        public string GetTimeFrame() => DriverExtensions.GetText(TimeFrameLabelLocator);
    }
}