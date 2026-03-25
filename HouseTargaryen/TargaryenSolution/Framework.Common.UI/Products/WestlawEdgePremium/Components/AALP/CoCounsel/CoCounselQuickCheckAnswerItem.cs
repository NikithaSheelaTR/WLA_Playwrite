namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounselQuickCheckAnswerItem
    /// </summary>
    public class CoCounselQuickCheckAnswerItem : BaseItem
    {
        private static readonly By ExpandAnswerItemLocator = By.XPath(".//saf-accordion-item[contains(@class,'ResultsList-module__accordion')]");
        private static readonly By DocumentLinkLocator = By.XPath(".//saf-anchor[@data-testid='document-link']");

        /// <summary>
        /// Potential Mischaracterization Card Component
        /// </summary>
        public PotentialMischaracterizationCardComponent PotentialMischaracterizationCard => new PotentialMischaracterizationCardComponent(this.Container);

        /// <summary>
        /// Answer Item Expand Button
        /// </summary>
        public IButton AnswerItemExpandButton => new Button(this.Container, ExpandAnswerItemLocator);

        /// <summary>
        /// Document Link
        /// </summary>
        public ILink DocumentLink => new Link(this.Container, DocumentLinkLocator);

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="CoCounselQuickCheckAnswerItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public CoCounselQuickCheckAnswerItem(IWebElement containerElement) : base(containerElement)
        {
        }
    }
}
