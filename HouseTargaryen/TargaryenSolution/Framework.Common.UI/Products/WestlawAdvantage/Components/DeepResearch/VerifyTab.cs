namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Verify Tab
    /// </summary>
    public class VerifyTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-verifytab']");
        private static readonly By VerifyReportButtonLocator = By.XPath(".//saf-button-v3[@saf='button']");
        private static readonly By VerifyProgressBarLabelLocator = By.XPath(".//saf-progress-v3[@data-testid = 'verify-progress-bar']");
        private static readonly By VerifyResultGridItemLocator = By.XPath(".//table[contains(@aria-label, 'verification results')]");
        private static readonly By TotalAssertionButtonLocator = By.XPath(".//saf-chip-v3[contains(@class, 'FilterBy-module__filterChip__wKkgE') and contains(text(),'total assertions') ]");
        private static readonly By PotentialIssuesButtonLocator = By.XPath(".//saf-chip-v3[contains(@class, 'FilterBy-module__filterChip__wKkgE') and contains(text(),'potential issues') ]");
        private static readonly By GoToLatestVersionReportButtonLocator = By.XPath(".//saf-button-v3[@saf='button' and contains(text(),'Go to latest version of report')]"); 
       
        /// <summary>
        /// Verify Report Button
        /// </summary>
        public IButton VerifyReportButton => new Button(this.ComponentLocator, VerifyReportButtonLocator);

        /// <summary>
        /// Total Assertion Button
        /// </summary>
        public IButton TotalAssertionButton => new Button(this.ComponentLocator, TotalAssertionButtonLocator);

        /// <summary>
        /// Potential Issues Button
        /// </summary>
        public IButton PotentialIssuesButton => new Button(this.ComponentLocator, PotentialIssuesButtonLocator);

        /// <summary>
        /// Go to latest version of report Button
        /// </summary>
        public IButton GoToLatestVersionReportButton => new Button(this.ComponentLocator, GoToLatestVersionReportButtonLocator);

        /// <summary>
        /// Verify Progress bar label
        /// </summary>
        public ILabel VerifyProgressBarLabel => new Label(this.ComponentLocator, VerifyProgressBarLabelLocator);

        /// <summary>
        /// Verify result Item list
        /// </summary>
        /// <returns>List of answers</returns>
        public ItemsCollection<VerifyResultGridItem> VerifyResultGridItems => new ItemsCollection<VerifyResultGridItem>(this.ComponentLocator, VerifyResultGridItemLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Verify";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
