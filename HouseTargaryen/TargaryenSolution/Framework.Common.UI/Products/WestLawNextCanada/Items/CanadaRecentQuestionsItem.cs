namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Recent Questions Item
    /// </summary>
    public class CanadaRecentQuestionsItem : BaseItem
    {
        private static readonly By RecentQuestionLocator = By.XPath("//li[contains(@class,'recent-search-dropdown-list-item')]/span");
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="containerElement"></param>
        public CanadaRecentQuestionsItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Recent Question list item
        /// </summary>
        public ILink RecentQuestion => new Link(this.Container, RecentQuestionLocator);
    }
}