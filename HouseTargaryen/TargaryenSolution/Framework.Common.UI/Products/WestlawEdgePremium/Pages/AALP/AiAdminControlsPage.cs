namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using OpenQA.Selenium;

    /// <summary>
    /// Ai Admin controls page
    /// </summary>
    public class AiAdminControlsPage: EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By HeaderLabelLocator = By.XPath("//div[@id='coid_website_AIAssistantAdminControlsLandingPage']");
        private static readonly By SelectUsersButtonLocator = By.XPath("//button[@class='co_primaryBtn CS-main-start-new-button']");

        /// <summary>
        /// Header label
        /// </summary>
        public ILabel HeaderLabel => new Label(HeaderLabelLocator);

        /// <summary>
        /// Select users button
        /// </summary>
        public IButton SelectUsersButton => new Button(SelectUsersButtonLocator);
    }
}
