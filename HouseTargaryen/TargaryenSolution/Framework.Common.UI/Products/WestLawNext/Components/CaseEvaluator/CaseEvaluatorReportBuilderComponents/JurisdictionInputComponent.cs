namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using OpenQA.Selenium;

    /// <summary>
    /// JurisdictionInputComponent
    /// </summary>
    public class JurisdictionInputComponent : CaseEvReportBuilderWithCheckboxesBaseComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@section='templateData.jurisdictions']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}