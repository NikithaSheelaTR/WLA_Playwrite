namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The case evaluator report builder with expanded checkboxes base component.
    /// </summary>
    public abstract class CaseEvReportBuilderWithExpandedCheckboxesBaseComponent
        : CaseEvReportBuilderWithCheckboxesBaseComponent
    {
        private const string ExpandLocatorLctMask = ".//input[@id='{0}']/../button[contains(text(),'Expand')]";

        /// <summary>
        /// Expand parent category and select child criteria
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        public void ExpandAndSelectCriteria(string parent, string child)
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(ExpandLocatorLctMask, parent))).Click();
            this.SelectCriteria(child);
        }
    }
}