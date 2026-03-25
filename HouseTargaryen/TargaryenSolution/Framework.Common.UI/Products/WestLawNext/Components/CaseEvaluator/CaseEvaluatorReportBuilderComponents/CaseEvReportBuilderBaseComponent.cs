namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseInputCriteriaComponent
    /// </summary>
    public abstract class CaseEvReportBuilderBaseComponent : BaseModuleRegressionComponent
    {
        private static readonly By InputSectionLocator = By.XPath(".//h2/a[@class='ng-binding']");

        /// <summary>
        /// Expand/Collapse Input Section 
        /// </summary>
        public void ClickInputSection()
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), InputSectionLocator).Click();
    }
}