namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.GradingTool
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The create experiment dialog.
    /// </summary>
    public class CreateExperimentDialog : BaseModuleRegressionDialog
    {
        private static readonly By ExperimentNameTextboxLocator = By.Id("coid_experimentName");

        private static readonly By CreateButtonLocator = By.Id("coid_createExperiment");

        private static readonly By DynamicRadiobuttonLocator = By.Id("coid_dynamicExperimentType");

        /// <summary>
        /// Enters experiment name.
        /// </summary>
        /// <param name="experimentName"> The experiment name. </param>
        /// <returns> The <see cref="CreateExperimentDialog"/>. </returns>
        public CreateExperimentDialog EnterExperimentName(string experimentName)
        {
            DriverExtensions.SetTextField(experimentName, ExperimentNameTextboxLocator);
            return this;
        }

        /// <summary>
        /// Clicks create button.
        /// </summary>
        /// <returns> The <see cref="EdgeExperimentAdministrationPage"/>. </returns>
        public EdgeExperimentAdministrationPage ClickCreateButton() => this
            .ClickElement<EdgeExperimentAdministrationPage>(CreateButtonLocator);

        /// <summary>
        /// Clicks Dynamic radio button.
        /// </summary>
        /// <returns> The <see cref="CreateExperimentDialog"/>. </returns>
        public CreateExperimentDialog ClickDynamicRadiobutton() => this
            .ClickElement<CreateExperimentDialog>(DynamicRadiobuttonLocator);
    }
}
