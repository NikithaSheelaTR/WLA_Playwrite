namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The case evaluator report builder with checkboxes base component.
    /// </summary>
    public abstract class CaseEvReportBuilderWithCheckboxesBaseComponent : CaseEvReportBuilderBaseComponent
    {
        private const string ItemCheckboxLctMask = ".//input[@id='{0}' and @type='checkbox']";

        /// <summary>
        /// Input parent of case type
        /// </summary>
        /// <param name="criteria">The case Type.</param>
        public void SelectCriteria(string criteria)
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(ItemCheckboxLctMask, criteria))).Click();
    }
}