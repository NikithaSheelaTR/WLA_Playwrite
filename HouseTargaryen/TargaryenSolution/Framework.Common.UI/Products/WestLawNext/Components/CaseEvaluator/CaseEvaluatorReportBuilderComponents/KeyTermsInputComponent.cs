namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// KeyTermsInputComponent
    /// </summary>
    public class KeyTermsInputComponent : CaseEvReportBuilderBaseComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@section='templateData.keyTerms']");

        private static readonly By KeywordTextBoxLocator = By.XPath(".//div[contains(@section,'.keyTerms')]//input");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// input passed keyword string
        /// </summary>
        /// <param name="keyword">
        /// The keyword.
        /// </param>
        public void AddKeyword(string keyword) => DriverExtensions.SetTextField(keyword, KeywordTextBoxLocator);
    }
}