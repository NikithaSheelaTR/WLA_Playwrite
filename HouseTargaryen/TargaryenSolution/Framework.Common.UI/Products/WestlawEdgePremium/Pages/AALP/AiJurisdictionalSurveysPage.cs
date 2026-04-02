namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.AiJurisdictionalSurveys;

    /// <summary>
    /// AI Jurisdictional Surveys Landing Page
    /// </summary>
    public class AiJurisdictionalSurveysPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By CreateSurveyButtonTopLocator = By.XPath("//saf-button[@id='Create-Survey-Button-1']");
        private static readonly By PageDescriptionLocator = By.XPath("//h3[@id='fiftyStateSearchCriteria']");
        private static readonly By UsageLimitLabelLocator = By.XPath("//saf-alert[contains(text(), 'You have exceeded')]");
        private static readonly By PageErrorLabelLocator = By.XPath("//saf-alert[contains(text(), 'Sorry')]");
        private static readonly By ProgressLabelLocator = By.XPath("//saf-status[@message='Creating your survey. It will appear in History when completed.']");
        private static readonly By EmailMeButtonLocator = By.XPath("//saf-button[contains(text(), 'Email me when survey is ready')]");
        private static readonly By EmailSuccessLabelLocator = By.XPath("//div[contains(@class,'emailMessage')]");
        private static readonly By PendoDoneButtonLocator = By.XPath("//*[contains(@class, '_pendo-button-primaryButton') or contains(@class, '_pendo-button')]");
        private static readonly By CopiedLinkSuccessLabelLocator = By.XPath(".//div[contains(@class,'saf-alert__content')]");
        private static readonly By PageHeaderLocator = By.XPath("//h1[@id='fiftyStateHeading'] | //h2[@id='fiftyStateHeading']");
        private static readonly By JurisdictionalSurveysTitleLocator = By.XPath("//h1[@id='fiftyStateHeading']");
        private static readonly By QuestionTextAreaLocator = By.XPath(".//saf-text-area[@id='fiftyStateQuestionInput']");

        /// <summary>
        /// Toolbar Component
        /// </summary>
        public AiJurisdictionalSurveysToolbarComponent Toolbar { get; } = new AiJurisdictionalSurveysToolbarComponent();

        /// <summary>
        /// Query box Component
        /// </summary>
        public AiJurisdictionalSurveysQueryBoxComponent QueryBox { get; } = new AiJurisdictionalSurveysQueryBoxComponent();

        /// <summary>
        /// WLA Query box Component
        /// </summary>
        public WlaAjsQueryBoxComponent WlaQueryBox { get; } = new WlaAjsQueryBoxComponent();

        /// <summary>
        /// 50 State Surveys Jurisdictions Component
        /// </summary>
        public AiJurisdictionalSurveysJurisdictionsComponent Jurisdictions { get; } = new AiJurisdictionalSurveysJurisdictionsComponent();

        /// <summary>
        /// Content type Component
        /// </summary> 
        public AiJurisdictionalSurveysContentTypeComponent ContentType { get; } = new AiJurisdictionalSurveysContentTypeComponent();

        /// <summary>
        /// Survey result Component
        /// </summary> 
        public AiJurisdictionalSurveysResultComponent SurveyResult { get; } = new AiJurisdictionalSurveysResultComponent();

        /// <summary>
        /// WLA Survey result Component
        /// </summary> 
        public WlaAjsResultComponent WlaSurveyResult { get; } = new WlaAjsResultComponent();

        /// <summary>
        /// Create Survey button
        /// </summary>
        public IButton CreateSurveyButtonTop => new Button(CreateSurveyButtonTopLocator);

        /// <summary>
        /// Email Me Button
        /// </summary>
        public IButton EmailMeButton => new Button(EmailMeButtonLocator);

        /// <summary>
        /// Email Success Label
        /// </summary>
        public IButton EmailSuccessLabel => new Button(EmailSuccessLabelLocator);

        /// <summary>
        /// Copied Link Label 
        /// </summary>
        public ILabel CopiedLinkSuccessLabel => new Label( CopiedLinkSuccessLabelLocator);
        /// <summary>
        ///Pendo Done Button
        /// </summary>
        public IButton PendoDoneButton => new Button(PendoDoneButtonLocator);

        /// <summary>
        /// Page description label
        /// </summary>
        public ILabel PageDescription => new Label(PageDescriptionLocator);

        /// <summary>
        /// Jurisdictional Surveys Tiltle label
        /// </summary>
        public ILabel JurisdictionalSurveysTitle => new Label(JurisdictionalSurveysTitleLocator);

        /// <summary>
        /// Question text Area
        /// </summary>
        public ITextbox QuestionTextarea => new Textbox(QuestionTextAreaLocator);

        /// <summary>
        /// Usage limit label
        /// </summary>
        public ILabel UsageLimitLabel => new Label(UsageLimitLabelLocator);

        /// <summary>
        /// Page error label
        /// </summary>
        public ILabel PageErrorLabel => new Label(PageErrorLabelLocator);

        /// <summary>
        /// Progress label
        /// </summary>
        public ILabel ProgressLabel => new Label(ProgressLabelLocator);

        /// <summary>
        /// Waits for the survey results to be fully loaded.
        /// Spinner disappearing does not guarantee content is rendered — this method waits
        /// for actual result items to appear in the DOM after the spinner is gone.
        /// </summary>
        /// <param name="timeoutFromSec">Maximum seconds to wait for the spinner to clear. Defaults to 600 (AI generation can be slow).</param>
        public void WaitForResultsLoaded(int timeoutFromSec = 600)
        {
            SafeMethodExecutor.WaitUntil(() => !this.ProgressLabel.Displayed, timeoutFromSec: timeoutFromSec);
            SafeMethodExecutor.WaitUntil(() => this.SurveyResult.TimeStampLabel.Displayed, timeoutFromSec: 60);
        }

        /// <summary>
        /// Close pendo message
        /// </summary>
        public void ClosePendoMessage()
        {
            if (PendoDoneButton.Displayed)
            {
                PendoDoneButton.Click();
            }
        }

        /// <summary>
        /// Page header label
        /// </summary>
        public ILabel PageHeaderLabel => new Label(PageHeaderLocator);

        /// <summary>
        /// Gets a value indicating whether the Create Survey button is enabled.
        /// </summary>
        public bool IsCreateSurveyButtonDisabled => CreateSurveyButtonTop.GetCssValue("class").Contains("disabled");    
    }
}

