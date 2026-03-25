namespace Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Know Your Judge Report component
    /// </summary>
    public class KnowYourJudgeReportComponent : BaseModuleRegressionComponent
    {
        private static readonly By ReportContainerLocator = By.XPath("//div[contains(@class,'ReportFlowManager-module')]");
        private static readonly By LoadingResultsLabelLocator = By.XPath("//saf-progress-v3[@data-testid='progress-bar']");
        private static readonly By ReportContentLocator = By.XPath("//div[contains(@class, 'ReportDisplayer-module')]");
        private static readonly By ReportHeadingButtonLocator = By.XPath("//saf-accordion-item-v3[contains(@class, 'ReportSummary-module__summaryHeader')]");
        private static readonly By ReportHeadingLabelLocator = By.XPath(".//span/h3");
        private static readonly By SummaryDetailsLabelLocator = By.XPath("./div/h4");
        private static readonly By EmailMeButtonLocator = By.XPath("//saf-button-v3[@data-testid='email-button']");
        private static readonly By EmailMeTextLocator = By.XPath("//p[@data-testid='email-sent-message']");

        /// <summary>
        /// Loading Results Label
        /// </summary>
        public ILabel LoadingResultsLabel => new Label(LoadingResultsLabelLocator);

        /// <summary>
        /// Report Display Element
        /// </summary>
        public IWebElement ReportContentElement => DriverExtensions.GetElement(ReportContentLocator);

        /// <summary>
        /// Report Heading button
        /// </summary>
        public IButton ReportHeadingButton => new Button(ReportHeadingButtonLocator);

        /// <summary>
        /// Report Heading Label
        /// </summary>
        public ILabel ReportHeadingLabel => new Label(ReportHeadingButtonLocator, ReportHeadingLabelLocator);

        /// <summary>
        /// Summary Details Label
        /// </summary>
        public ILabel SummaryDetailsLabel => new Label(ReportHeadingButtonLocator, SummaryDetailsLabelLocator);

        /// <summary>
        /// Email me button
        /// </summary>
        public IButton EmailMeButton => new Button(EmailMeButtonLocator);

        /// <summary>
        /// Email me text
        /// </summary>
        public ILabel EmailMeText => new Label(EmailMeTextLocator);

        /// <summary>
        /// KnowYourJudge Report Tab Panel
        /// </summary>
        public KnowYourJudgeReportTabPanel knowYourJudgeReportTabPanel = new KnowYourJudgeReportTabPanel();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ReportContainerLocator;
    }
}
