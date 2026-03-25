namespace Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo grading tool home page.
    /// </summary>
    public class EdgeGradingToolHomePage : BaseModuleRegressionPage
    {
        private static readonly By AccessYourAssignmentsButtonLocator =
            By.XPath("//button[contains(@class, 'co_assignmentButton')]");

        private static readonly By NewQueryButtonLocator =
            By.XPath("//button[contains(@class, 'co_newQueryButton')]");

        private static readonly By ExperimentAdministrationButtonLocator =
            By.XPath("//button[contains(@class, 'co_experimentButton')]");

        /// <summary>
        /// Clicks access your assignments button.
        /// </summary>
        /// <returns> The <see cref="EdgeAssignmentsPage"/>. </returns>
        public EdgeAssignmentsPage ClickAccessYourAssignmentsButton()
        {
            DriverExtensions.Click(AccessYourAssignmentsButtonLocator);
            return new EdgeAssignmentsPage();
        }

        /// <summary>
        /// Clicks New Query button.
        /// </summary>
        /// <returns> The <see cref="EdgeNewQueryPage"/>. </returns>
        public EdgeNewQueryPage ClickNewQueryButton()
        {
            DriverExtensions.Click(NewQueryButtonLocator);
            return new EdgeNewQueryPage();
        }

        /// <summary>
        /// Clicks Experiment Administration button.
        /// </summary>
        /// <returns> The <see cref="EdgeExperimentAdministrationPage"/>. </returns>
        public EdgeExperimentAdministrationPage ClickExperimentAdministrationButton()
        {
            DriverExtensions.Click(ExperimentAdministrationButtonLocator);
            return new EdgeExperimentAdministrationPage();
        }

        /// <summary>
        /// Verifies that the Access your assignments button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if Access your assignments button is displayed </returns>
        public bool IsAccessYourAssignmentsButtonDisplayed() => DriverExtensions.IsDisplayed(
            AccessYourAssignmentsButtonLocator);

        /// <summary>
        /// Verifies that the New query button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if New query button is displayed </returns>
        public bool IsNewQueryButtonDisplayed() => DriverExtensions.IsDisplayed(
            NewQueryButtonLocator);

        /// <summary>
        /// Verifies that the Experiment Administration button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if Experiment Administration button is displayed </returns>
        public bool IsExperimentAdministrationButtonDisplayed() => DriverExtensions.IsDisplayed(
            ExperimentAdministrationButtonLocator);
    }
}
