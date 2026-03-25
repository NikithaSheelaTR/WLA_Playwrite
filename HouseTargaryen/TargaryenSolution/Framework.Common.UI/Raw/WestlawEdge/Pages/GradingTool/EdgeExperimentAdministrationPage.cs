namespace Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.GradingTool;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.GradingTool;
    using Framework.Common.UI.Raw.WestlawEdge.Items.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo grading tool experiment administration page.
    /// </summary>
    public class EdgeExperimentAdministrationPage : BaseEdgeGradingPage
    {
        private static readonly By CreateButtonLocator =
            By.Id("coid_showCreateLightbox");

        private static readonly By ExperimentTableContainerLocator = By.XPath("//table[@class='co_detailsTable']");

        private static readonly By ExperimentContainerLocator = By.XPath(".//tr[@class]");

        private static readonly By DeleteButtonLocator = By.Id("coid_deleteExperiment");

        /// <summary>
        /// The Grading tool bread crumb.
        /// </summary>
        public GradingToolBreadCrumbComponent BreadCrumb { get; } = new GradingToolBreadCrumbComponent();

        /// <summary>
        /// Clicks experiment link by name.
        /// </summary>
        /// <param name="experimentName"> The experiment name. </param>
        /// <returns> The <see cref="EdgeExperimentDetailPage"/>. </returns>
        public EdgeExperimentDetailPage ClickExperimentLinkByName(string experimentName) 
            => this.GetListOfExperimentItems().First(item => item.ExperimentName.Equals(experimentName)).ClickExperimentNameLink();
        
        /// <summary>
        /// Clicks Create An Experiment button.
        /// </summary>
        /// <returns> The <see cref="CreateExperimentDialog"/>. </returns>
        public CreateExperimentDialog ClickCreateExperimentButton()
        {
            DriverExtensions.Click(CreateButtonLocator);
            return new CreateExperimentDialog();
        }

        /// <summary>
        /// Checks experiment by name.
        /// </summary>
        /// <param name="selected"> The selected. </param>
        /// <param name="experimentName"> The experiment name. </param>
        /// <returns> The <see cref="EdgeExperimentAdministrationPage"/>. </returns>
        public EdgeExperimentAdministrationPage CheckExperimentByName(bool selected, params string[] experimentName)
        {
            var listOfExperiment = this.GetListOfExperimentItems();

            foreach (var experiment in experimentName)
            {
                listOfExperiment.First(item => item.ExperimentName.Equals(experiment)).SelectExperimentCheckbox(selected);
            }

            experimentName.Select(
                name => listOfExperiment.First(item => item.ExperimentName.Equals(name))).ToList()
                .ForEach(elem => elem.SelectExperimentCheckbox(selected));

            return this;
        }

        /// <summary>
        /// Clicks delete button.
        /// </summary>
        /// <returns> The <see cref="ConfirmDeletionsDialog"/>. </returns>
        public ConfirmDeletionsDialog ClickDeleteButton()
        {
            DriverExtensions.Click(DeleteButtonLocator);
            return new ConfirmDeletionsDialog();
        }

        private List<ExperimentItem> GetListOfExperimentItems()
            => DriverExtensions.GetElements(ExperimentTableContainerLocator, ExperimentContainerLocator)
                               .Select(item => new ExperimentItem(item)).ToList();
    }
}
