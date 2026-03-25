namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// List of checkboxes for time selection
    /// </summary>
    public class SheduleAlertTimeBoxesComponent : BaseModuleRegressionComponent
    {
        private static readonly By AmSheduleCheckboxesLocator =
            By.XPath("//label[@id = 'amLabel']/ancestor::*[contains(@class, 'co_formInline')]//label[(contains(@id,'ExecutionTime')) and not(@class='co_hideState')]");

        private static readonly By PmSheduleCheckboxesLocator =
            By.XPath("//label[@id = 'pmLabel']/ancestor::*[contains(@class, 'co_formInline')]//label[(contains(@id,'ExecutionTime')) and not(@class='co_hideState')]");

        private static readonly By CheckBoxLocator = By.XPath("./input");

        private static readonly By ContainerLocator = By.Id("scheduleOptionsDiv");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns AM IWebElements
        /// </summary>
        private IReadOnlyCollection<IWebElement> AmCheckBoxesList => DriverExtensions.GetElements(AmSheduleCheckboxesLocator);

        /// <summary>
        /// Returns PM IWebElements
        /// </summary>
        private IReadOnlyCollection<IWebElement> PmCheckBoxesList => DriverExtensions.GetElements(PmSheduleCheckboxesLocator);

        /// <summary>
        /// AM hours
        /// </summary>
        /// <returns> List of AM hours </returns>
        public List<string> GetAmList()
            => this.AmCheckBoxesList.Count == 0
                    ? new List<string>()
                    : this.AmCheckBoxesList.Select(elem => elem.GetText()).ToList();

        /// <summary>
        /// PM hours
        /// </summary>
        /// <returns> List of PM hours </returns>
        public List<string> GetPmList() => this.PmCheckBoxesList.Count == 0
                                             ? new List<string>()
                                             : this.PmCheckBoxesList.Select(elem => elem.GetText()).ToList();

        /// <summary>
        /// Set AM checkboxes
        /// </summary>
        /// <param name="sheduList">time to select</param>
        /// <param name="action"> true to check</param>
        /// <returns><see cref="SheduleAlertTimeBoxesComponent"/></returns>
        public SheduleAlertTimeBoxesComponent SetAmCheckBoxes(List<string> sheduList, bool action)
        {
            this.AmCheckBoxesList.Where(checkbox => sheduList.Contains(checkbox.GetText())).ToList()
                .ForEach(checkbox => DriverExtensions.GetElement(checkbox, CheckBoxLocator).SetCheckbox(action));

            return this;
        }

        /// <summary>
        /// Set AM checkboxes
        /// </summary>
        /// <param name="sheduList">time to select</param>
        /// <param name="action"> true to check</param>
        /// <returns><see cref="SheduleAlertTimeBoxesComponent"/></returns>
        public SheduleAlertTimeBoxesComponent SetPmCheckBoxes(List<string> sheduList, bool action)
        {
            this.PmCheckBoxesList.Where(checkbox => sheduList.Contains(checkbox.GetText())).ToList()
                .ForEach(checkbox => DriverExtensions.GetElement(checkbox, CheckBoxLocator).SetCheckbox(action));

            return this;
        }
    }
}