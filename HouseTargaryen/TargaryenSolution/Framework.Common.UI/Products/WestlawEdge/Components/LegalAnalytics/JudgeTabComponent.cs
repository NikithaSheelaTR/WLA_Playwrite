namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Judge Tab Component
    /// </summary>
    public class JudgeTabComponent : BaseSearchTabComponent
    {
        private static readonly By ContainerLocator = By.Id("tab-Judges");
        private static readonly By EnhancedBadgeLabelLocator = By.XPath("//span[@class='Badge badge--white']");

        /// <summary>
        /// tabName
        /// </summary>
        protected override string TabName => "Judge";

        /// <summary>
        /// Enhanced Badge Label 
        /// </summary>
        public ILabel EnhancedBadgeLabel => new Label(EnhancedBadgeLabelLocator);

        /// <summary>
        /// tabName
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
