namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP
{
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// Ai Assistant Error Page
    /// </summary>
    public class AiAssistedResearchErrorPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ErrorPageHeaderLabelLocator = By.XPath("//h1");
        private static readonly By ErrorPageMessageLabelLocator = By.XPath("./following-sibling::p");
        private static readonly By NewResearchButtonLocator = By.XPath("//button[contains(@class, 'CS-main-start-new-button')]");

        /// <summary>
        /// Error page header label
        /// </summary>
        public ILabel ErrorPageHeaderLabel => new Label(ErrorPageHeaderLabelLocator);

        /// <summary>
        /// Error page message label
        /// </summary>
        public ILabel ErrorPageMessageLabel => new Label(ErrorPageHeaderLabelLocator, ErrorPageMessageLabelLocator);

        /// <summary>
        /// New Research button
        /// </summary>
        public IButton NewResearchButton => new Button(NewResearchButtonLocator);
    }
}
