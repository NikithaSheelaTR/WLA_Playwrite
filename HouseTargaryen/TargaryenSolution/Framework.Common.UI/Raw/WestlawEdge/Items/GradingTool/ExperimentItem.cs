namespace Framework.Common.UI.Raw.WestlawEdge.Items.GradingTool
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The experiment item from the Experiments table on the Experiment Administration page
    /// </summary>
    public class ExperimentItem: BaseItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//input[@type='checkbox']");

        private static readonly By ExperimentNameLocator = By.XPath("./td[3]/a");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="ExperimentItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> The Experiment Item Container. </param>
        public ExperimentItem(IWebElement containerElement): base(containerElement)
        {
        }

        /// <summary>
        /// Gets Experiment Name.
        /// </summary>
        /// <returns> Experiment Name </returns>
        public string ExperimentName => DriverExtensions.GetElement(this.Container, ExperimentNameLocator).Text;

        /// <summary>
        /// Clicks Experiment Name Link.
        /// </summary>
        /// <returns> The <see cref="EdgeExperimentDetailPage"/>. </returns>
        public EdgeExperimentDetailPage ClickExperimentNameLink()
        {
            DriverExtensions.Click(this.Container, ExperimentNameLocator);
            return new EdgeExperimentDetailPage();
        }

        /// <summary>
        /// Checks Experiment checkbox.
        /// </summary>
        /// <param name="selected"> The selected. </param>
        /// <returns>
        /// The <see cref="EdgeExperimentAdministrationPage"/>. 
        /// </returns>
        public EdgeExperimentAdministrationPage SelectExperimentCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, this.Container, CheckboxLocator);
            return new EdgeExperimentAdministrationPage();
        }
    }
}