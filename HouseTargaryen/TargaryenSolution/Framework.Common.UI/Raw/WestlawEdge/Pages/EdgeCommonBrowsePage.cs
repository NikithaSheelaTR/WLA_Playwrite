namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Common Browse Page
    /// </summary>
    public class EdgeCommonBrowsePage : CommonBrowsePage
    {
        private static readonly By SearchAndSummarizeTaxButtonLocator = By.XPath("//a[text() = 'Search & Summarize Tax']");

        /// <summary>
        /// Header
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Footer
        /// </summary>
        public new EdgeFooterComponent Footer { get; } = new EdgeFooterComponent();

        /// <summary>
        /// Compare text component
        /// </summary>
        public CompareTextComponent CompareText { get; } = new CompareTextComponent();

        /// <summary>
        /// Search And Summarize Tax Button
        /// </summary>
        public IButton SearchAndSummarizeTaxButton => new Button(SearchAndSummarizeTaxButtonLocator);
    }
}
