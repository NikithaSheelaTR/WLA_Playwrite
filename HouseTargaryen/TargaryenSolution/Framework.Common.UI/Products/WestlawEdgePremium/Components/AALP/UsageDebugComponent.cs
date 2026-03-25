namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Usage debug component
    /// </summary>
    public class UsageDebugComponent : BaseModuleRegressionComponent
    {
        private static readonly By UsageDebugContainerLocator = By.Id("ai-usageDebug-container");
        private static readonly By BackDateExpiryButtonLocator = By.XPath(".//button[text()='Back date expiry']");
        private static readonly By DailyLimitLabelLocator = By.XPath(".//div[@class='ai-usageDebug-abuse']/div[1]/span[2]");
        private static readonly By DailyUsageLabelLocator = By.XPath(".//div[@class='ai-usageDebug-abuse']/div[2]/span[2]");

        /// <summary>
        /// Back date expiry button
        /// </summary>
        public IButton BackDateExpiryButton => new Button(this.ComponentLocator, BackDateExpiryButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => UsageDebugContainerLocator;

        /// <summary>
        /// Daily limit label
        /// </summary>
        public ILabel DailyLimitLabel => new Label(this.ComponentLocator, DailyLimitLabelLocator);

        /// <summary>
        /// Daily usage label
        /// </summary>
        public ILabel DailyUsageLabel => new Label(this.ComponentLocator, DailyUsageLabelLocator);
    }
}

