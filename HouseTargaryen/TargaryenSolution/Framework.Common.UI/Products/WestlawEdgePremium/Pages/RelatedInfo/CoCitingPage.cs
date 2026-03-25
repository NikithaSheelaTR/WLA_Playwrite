namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.RelatedInfo
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;

    /// <summary>
    ///  The Co-citing page
    /// </summary>
    public class CoCitingPage : BaseCoCitationsPage
    {
        private static readonly By BackToCoCitedCasesLinkLocator = By.XPath(".//a[text() = 'Back to Cited With cases']");

        /// <summary>
        /// BackToCoCitedCasesLink
        /// </summary>
        public ILink BackToCoCitedCasesLink => new Link(this.Container, BackToCoCitedCasesLinkLocator);
    }
}