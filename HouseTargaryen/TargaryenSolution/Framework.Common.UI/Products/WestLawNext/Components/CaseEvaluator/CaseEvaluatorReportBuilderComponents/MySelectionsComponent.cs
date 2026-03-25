namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// MySelectionsComponent
    /// </summary>
    public class MySelectionsComponent : BaseModuleRegressionComponent
    {
        private const string SelectionLctMask = "//h4[@class='ng-binding']/parent::div/ul//li/a[contains(@title,'{0}')]";

        private static readonly By SelectionsListLocator =
            By.XPath("//div[contains(@class,'selectionsCollector')]//div[contains(@class,'documentReportBody')]//div[contains(@ng-if,'sectionIsActive')]//span[2]");

        private static readonly By ContainerLocator = By.CssSelector("#co_rightColumn .co_documentReportSection");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get List of My Selections 
        /// </summary>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public List<string> GetListOfSelections()
            => DriverExtensions.GetElements(SelectionsListLocator).Select(row => row.Text).ToList();

        /// <summary>
        /// Remove Selection
        /// </summary>
        /// <param name="selection">The selection.</param>
        public void RemoveSelection(string selection)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(SelectionLctMask, selection))).Click();
    }
}