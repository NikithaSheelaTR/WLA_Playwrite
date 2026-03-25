namespace Framework.Common.UI.Products.WestLawNext.Pages.CaseEvaluator
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator.CaseEvaluatorReportBuilderComponents;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces;

    using OpenQA.Selenium;

    /// <summary>
    /// InputCriteriaPage
    /// </summary>
    public class CaseEvaluatorReportBuilderPage : BaseModuleRegressionPage
    {
        private static readonly By GenerateButtonLocator = By.XPath("//input[@type='button' and @value='Generate Report']");

        private static readonly By InputCrirteriaContainerLocator =
            By.CssSelector("div#co_caseEvaluator.co_browsePageSectionWidget");

        /// <summary>
        /// CaseTypeInputComponent
        /// </summary>
        public CaseTypeInputComponent CaseTypeInputComponent { get; protected set; } = new CaseTypeInputComponent();

        /// <summary>
        /// CompanyInputComponent
        /// </summary>
        public CompanyInputComponent CompanyInputComponent { get; protected set; } = new CompanyInputComponent();

        /// <summary>
        /// IndustyInputComponent
        /// </summary>
        public IndustyInputComponent IndustyInputComponent { get; protected set; } = new IndustyInputComponent();

        /// <summary>
        /// InjuryInputComponent
       /// </summary>
        public InjuryInputComponent InjuryInputComponent { get; protected set; } = new InjuryInputComponent();

        /// <summary>
        /// JurisdictionInputComponent
        /// </summary>
        public JurisdictionInputComponent JurisdictionInputComponent { get; protected set; } = new JurisdictionInputComponent();

        /// <summary>
        /// KeyTermsInputComponent
        /// </summary>
        public KeyTermsInputComponent KeyTermsInputComponent { get; protected set; } = new KeyTermsInputComponent();

        /// <summary>
        /// MySelectionsComponent
        /// </summary>
        public MySelectionsComponent MySelectionComponent { get; protected set; } = new MySelectionsComponent();

        /// <summary>
        /// Clear My Selections
        /// </summary>
        /// <returns>
        /// The <see cref="CaseEvaluatorReportPage"/>.
        /// </returns>
        public T ClickGenerate<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(GenerateButtonLocator);
            //Below wait is adding for after clicking the GenerateButton it is taking some time 
            DriverExtensions.WaitForElementNotDisplayed(GenerateButtonLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// CLicking on 'Case Evaluator Report' from favorite takes user to case evaluator input criteria page
        /// </summary>
        /// <returns>page</returns>
        public bool IsCaseEvalPageDisplayed()
            => DriverExtensions.IsDisplayed(InputCrirteriaContainerLocator, 5) && DriverExtensions.IsDisplayed(GenerateButtonLocator, 5);
    }
}