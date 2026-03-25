namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research right column component
    /// </summary>
    public class AiDeepResearchRightColumnComponent : BaseModuleRegressionComponent
    {
        private static readonly By RightColumnContainerLocator = By.XPath("//div[contains(@class,'rightColumn')]");
        private static readonly By ResearchReportLocator = By.XPath(".//saf-tab-v3[@id='reportTab']");
        private static readonly By SourcesLocator = By.XPath(".//saf-tab-v3[@id='sourcesTab']");
        private static readonly By COBSTitleLabelLocator = By.XPath(".//h3[contains(@id, 'cases-on-both-sides')]");
        private static readonly By ShowMoreButtonLocator = By.XPath(".//saf-button-v3[contains(@class, 'TableFormatter-module')]");
        private static readonly By COBSTableLabelLocator = By.XPath(".//div[contains(@class, 'TableFormatter-module__casesOnBothSides')]/table");
        private static readonly By ResearchReportContentLabelLocator = By.XPath(".//div[@data-testid='report-content']");
        private static readonly By RunDeeperReportButtonLocator = By.XPath(".//button[@data-testid='request-report-response-button']");
        private static readonly By TabsDropdownLocator = By.XPath(".//div[contains(@class, 'RightColumnContent-module__responsiveSelectContainer')]");
        private static readonly By CopyLinkButtonLocator = By.XPath(".//saf-button-v3[contains(@class, 'CopyLink')]");
        private static readonly By SaveToFolderButtonLocator = By.XPath(".//saf-button-v3[contains(@class, 'FolderingButton')]");
        private static readonly By StatusMessageTextLocator = By.XPath(".//div[contains(@class, 'StatusMessageDialogBox-module__statusDialogText')]");
        private static readonly By SuccessfulStatusMessageLocator = By.XPath(".//saf-icon-v3[contains(@aria-label, 'Success')]");
        private static readonly By ResultDisclaimerLabelLocator = By.XPath(".//p[@data-testid='disclaimer']");

        /// <summary>
        /// Research Report Label
        /// </summary>
        public ILabel ResearchReportLabel => new Label(ComponentLocator, ResearchReportLocator);

        /// <summary>
        /// Sources Label
        /// </summary>
        public ILabel SourcesLabel => new Label(ComponentLocator, SourcesLocator);

        /// <summary>
        /// Get the right column header
        /// </summary>
        public RightColumnHeaderComponent RightColumnHeader { get; } = new RightColumnHeaderComponent();

        /// <summary>
        /// COBS Title label
        /// </summary>
        public ILabel COBSTitleLabel => new Label(ComponentLocator, COBSTitleLabelLocator);

        /// <summary>
        /// Show more button
        /// </summary>
        public IButton ShowMoreButton => new Button(ComponentLocator, ShowMoreButtonLocator);

        /// <summary>
        /// COBS Table label
        /// </summary>
        public ILabel COBSTableLabel => new Label(ComponentLocator, COBSTableLabelLocator);

        /// <summary>
        /// Research Report Content label
        /// </summary>
        public ILabel ResearchReportContentLabel => new Label(ComponentLocator, ResearchReportContentLabelLocator);

        /// <summary>
        /// Run deeper report button
        /// </summary>
        public IButton RunDeeperReportButton => new Button(ComponentLocator, RunDeeperReportButtonLocator);

        /// <summary>
        /// Result disclaimer label
        /// </summary>
        public ILabel ResultDisclaimerLabel => new Label(ComponentLocator, ResultDisclaimerLabelLocator);

        /// <summary>
        /// The scroll to COBS Title.
        /// </summary>
        public void ScrollToCOBSTitle() => DriverExtensions.ScrollIntoView(COBSTitleLabelLocator, 100);

        /// <summary>
        /// Is tabs dropdown Displayed
        /// </summary>
        /// <returns> true if tabs dropdown is displayed</returns>
        public bool IsTabsDropdownDisplayed => DriverExtensions.IsDisplayed(TabsDropdownLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => RightColumnContainerLocator;
    }
}


