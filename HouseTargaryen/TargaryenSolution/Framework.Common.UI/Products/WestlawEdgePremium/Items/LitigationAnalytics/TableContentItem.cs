namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Table Content Item
    /// </summary>
    public class TableContentItem : BaseItem
    {
        private static readonly By ItemLocator = By.XPath("//*[@class = 'stackedBlock']");

        /// <summary>
        /// Table Content Item
        /// </summary>
        /// <param name="container"></param>

        public TableContentItem(IWebElement container) : base(container)
        {
        }
    }
}