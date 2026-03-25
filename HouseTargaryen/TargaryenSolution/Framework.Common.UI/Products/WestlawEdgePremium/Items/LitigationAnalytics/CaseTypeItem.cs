namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Case Type item
    /// </summary>
    public class CaseTypeItem : BaseItem
    {
        private static readonly By CaseTypeTitleLocator = By.XPath("./span/span");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="CaseTypeItem"/> class. 
        /// </summary>
        public CaseTypeItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Nmae link
        /// </summary>
        public ILink NameLink => new Link(this.Container, CaseTypeTitleLocator);

    }
}