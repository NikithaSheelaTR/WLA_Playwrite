namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research Result component
    /// </summary>
    public class AiDeepResearchResultComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResultContainerLocator = By.Id("coid_website_deepResearchSkillLandingPage");
        private static readonly By DualColumnLocator = By.XPath(".//div[contains(@class,'DualColumnLayout-module__dualColumnContainer')]");
        private static readonly By CopiedPageinformationPopupLocator = By.XPath(".//div[@data-testid ='copy-link-toast-message']/saf-alert-v3");
        private static readonly By SkillNudgeButtonLocator = By.XPath(".//saf-button-v3[contains(@class, 'SkillNudge')]");
        private static readonly By GenerateAFullReportButtonLocator = By.XPath(".//*[@data-testid='request-report-response-button']");

        /// <summary>
        /// Deep Research Result Left Nav Component
        /// </summary>
        public AiDeepResearchLeftNavComponent LeftNavComponent { get; } = new AiDeepResearchLeftNavComponent();

        /// <summary>
        /// Deep Research Result Left Column Component
        /// </summary>
        public AiDeepResearchLeftColumnComponent LeftColumnComponent { get; } = new AiDeepResearchLeftColumnComponent();

        /// <summary>
        /// Deep Research Result Right Column Component
        /// </summary>
        public AiDeepResearchRightColumnComponent RightColumnComponent { get; } = new AiDeepResearchRightColumnComponent();

        /// <summary>
        /// Deep Research Result Single Column Component
        /// </summary>
        public AiDeepResearchSingleColumnComponent SingleColumnComponent { get; } = new AiDeepResearchSingleColumnComponent();

        /// <summary>
        /// Get the Deep Research Result Tab Panel 
        /// </summary>
        public DeepResearchResultTabPanel DeepResearchResultTabPanel { get; } = new DeepResearchResultTabPanel();

        /// <summary>
        /// Gets the toolbar instance used to access advanced research features.
        /// </summary>
        public AiDeepResearchToolBar ToolBar { get; } = new AiDeepResearchToolBar();

        /// <summary>
        /// Is dual column Displayed
        /// </summary>
        /// <returns> true if dual column is displayed</returns>
        public bool IsDualColumn => DriverExtensions.IsDisplayed(DualColumnLocator);

        /// <summary>
        /// Copied Page Information Popup
        /// </summary>
        public ILabel CopiedPageInformationPopup => new Label(this.ComponentLocator, CopiedPageinformationPopupLocator);

        /// <summary>
        /// Skill Nudge button
        /// </summary>
        public IButton SkillNudgeButton => new Button(this.ComponentLocator, SkillNudgeButtonLocator);

        /// <summary>
        /// Generate A Full Report Button
        /// </summary>
        public IButton GenerateAFullReportButton => new Button(ComponentLocator, GenerateAFullReportButtonLocator);
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ResultContainerLocator;
    }
}


