namespace Framework.Common.UI.Products.WestlawEdge.Pages.ItDepends
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNext.Pages.Document;

    using OpenQA.Selenium;

    /// <summary>
    /// ItDepends WLAnswer Page class
    /// </summary>
    public class ItDependsWlAnswerPage : QnAFullTextDocumentPage
    {
        private static readonly By ItDependsLinkLocator = By.XPath("//div[@id='co_factorAnalysisContainer']/a");

        /// <summary>
        /// It depends link
        /// </summary>
        public ILink ItDependsLink => new Link(ItDependsLinkLocator); 
    }
}