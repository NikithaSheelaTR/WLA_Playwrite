namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Know Your Judge Landing component
    /// </summary>
    public class KnowYourJudgeLandingComponent: BaseModuleRegressionComponent
    {
        private static readonly By LandingContainerLocator = By.XPath("//div[contains(@class,'reportFocusContainer')]");
        private static readonly By LandingTitleLabelLocator = By.XPath("//h3[@data-testid='main-title']");
        private static readonly By ClaimsTextboxLocator = By.XPath("//saf-text-area-v3[@data-testid='claims-textarea']");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('textarea'));";
        private static readonly By ContinueButtonLocator = By.XPath("//saf-button-v3[@data-testid='continue-button']");
        private static readonly By ClearButtonLocator = By.XPath("//saf-button-v3[@data-testid='clear-button']");
        private static readonly By ProgressBarLabelLocator = By.XPath("//saf-progress-ring-v3[@data-testid='continue-spinner']");
        private static readonly By TipsButtonLocator = By.XPath("//saf-button-v3[@id='tips-button']");
        private static readonly By CaseDetailsHeaderLabelLocator = By.XPath("//h4[@data-testid='case-details-title']");
        private static readonly By CaseDetailsNoteLabelLocator = By.XPath("//p[@data-testid='case-details-note']");
        private static readonly By FactsTextboxLocator = By.XPath("//saf-text-area-v3[@data-testid='facts-textarea']");
        private static readonly By SpecificFocusTextboxLocator = By.XPath("//saf-text-area-v3[@data-testid='specific-focus-textarea']");
        private static readonly By PrecedentSummarySwitchButtonLocator = By.XPath("//saf-switch-v3[@data-testid='precedent-summary-switch']");
        private static readonly By ReportErrorLabelLocator = By.XPath("//saf-alert-v3[@data-testid='report-error-alert']");

        /// <summary>
        /// Landing Title label
        /// </summary>
        public ILabel LandingTitleLabel => new Label(ComponentLocator, LandingTitleLabelLocator);

        /// <summary>
        /// Claims Textbox
        /// </summary>
        public ITextbox ClaimsTextbox => new Textbox(ComponentLocator, ClaimsTextboxLocator);

        /// <summary>
        /// Continue button
        /// </summary>
        public IButton ContinueButton => new Button(ComponentLocator, ContinueButtonLocator);

        /// <summary>
        /// Clear button
        /// </summary>
        public IButton ClearButton => new Button(ComponentLocator, ClearButtonLocator);

        /// <summary>
        /// Progress bar label
        /// </summary>
        public ILabel ProgressBarLabel => new Label(ProgressBarLabelLocator);

        /// <summary>
        /// Tips button
        /// </summary>
        public IButton TipsButton => new Button(ComponentLocator, TipsButtonLocator);

        /// <summary>
        /// Case Details Header label
        /// </summary>
        public ILabel CaseDetailsHeaderLabel => new Label(ComponentLocator, CaseDetailsHeaderLabelLocator);

        /// <summary>
        /// Case Details Note label
        /// </summary>
        public ILabel CaseDetailsNoteLabel => new Label(ComponentLocator, CaseDetailsNoteLabelLocator);

        /// <summary>
        /// Precedent Summary Switch Button
        /// </summary>
        public IButton PrecedentSummarySwitchButton => new Button(ComponentLocator, PrecedentSummarySwitchButtonLocator);

        /// <summary>
        /// Report Error label
        /// </summary>
        public ILabel ReportErrorLabel => new Label(ComponentLocator, ReportErrorLabelLocator);

        /// <summary>
        /// Enter claims
        /// </summary>
        /// <param name="claim"> The claim string to enter </param>
        public void EnterClaims(string claim)
        {
            IWebElement Claims = DriverExtensions.GetElement(ComponentLocator, ClaimsTextboxLocator);
            IWebElement ClaimsInput = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, Claims);
            ClaimsInput.SendKeys(claim);
        }

        /// <summary>
        /// Enter Facts
        /// </summary>
        /// <param name="fact"> The fact string to enter </param>
        public void EnterFacts(string fact)
        {
            IWebElement Facts = DriverExtensions.GetElement(ComponentLocator, FactsTextboxLocator);
            IWebElement FactsInput = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, Facts);
            FactsInput.SendKeys(fact);
        }

        /// <summary>
        /// Enter Specific Focus
        /// </summary>
        /// <param name="specificFocus"> The specific focus string to enter </param>
        public void EnterSpecificFocus(string specificFocus)
        {
            IWebElement SpecificFocus = DriverExtensions.GetElement(ComponentLocator, SpecificFocusTextboxLocator);
            IWebElement SpecificFocusInput = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, SpecificFocus);
            SpecificFocusInput.SendKeys(specificFocus);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => LandingContainerLocator;

        /// <summary>
        /// Landing Page Title
        /// </summary>
        public string LandingPageTitle => LandingTitleLabel.Text;
    }
}
