namespace Framework.Common.UI.Products.WestlawAdvantage.Components.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.KnowYourJudge;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    
    /// <summary>
    /// Know Your Judge Tab 
    /// </summary>
    public class KnowYourJudgeTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_knowYourJudge_tab");
        private static readonly By KnowYourJudgeHeaderLabelLocator = By.XPath("//h2[@data-testid='header-title']");
        private static readonly By NewQueryButtonLocator = By.XPath("//saf-button-v3[@data-testid='header-new-query-button']");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Know your judge";

        /// <summary>
        /// Know Your Judge Header Label
        /// </summary>
        public ILabel KnowYourJudgeHeaderLabel => new Label(KnowYourJudgeHeaderLabelLocator);

        /// <summary>
        /// New Query button
        /// </summary>
        public IButton NewQueryButton => new Button(NewQueryButtonLocator);

        /// <summary>
        /// KnowYourJudge Landing Component
        /// </summary>
        public KnowYourJudgeLandingComponent LandingComponent { get; } = new KnowYourJudgeLandingComponent();

        /// <summary>
        /// KnowYourJudge Refine analysis Component
        /// </summary>
        public KnowYourJudgeRefineAnalysisComponent RefineAnalysisComponent { get; } = new KnowYourJudgeRefineAnalysisComponent();

        /// <summary>
        /// KnowYourJudge report Component
        /// </summary>
        public KnowYourJudgeReportComponent ReportComponent { get; } = new KnowYourJudgeReportComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
