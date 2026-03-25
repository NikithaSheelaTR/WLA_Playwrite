namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using OpenQA.Selenium;

    /// <summary>
    /// CaseTypeInputComponent
    /// </summary>
    public class IndustyInputComponent : CaseEvReportBuilderWithExpandedCheckboxesBaseComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@section='templateData.industry']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}