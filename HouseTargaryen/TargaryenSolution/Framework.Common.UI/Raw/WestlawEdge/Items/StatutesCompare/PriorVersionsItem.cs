namespace Framework.Common.UI.Raw.WestlawEdge.Items.StatutesCompare
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// Describes prior versions on the Versions page.
    /// </summary>
    public class PriorVersionsItem : BaseItem
    {
        private static readonly By AddToCompareButtonLocator = By.XPath(".//button[@class = 'co_statuteCompare_addToCompareButton']");
        private static readonly By RemoveFromCompareButtonLocator = By.XPath(".//button[@class = 'co_statuteCompare_addToCompareButton co_active']");
        private static readonly By PriorVersionsDocumentLinkLocator = By.XPath(".//div[2]//a");

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorVersionsItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        public PriorVersionsItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Add to compare button
        /// </summary>
        public IButton AddToCompareButton => new Button(this.Container, AddToCompareButtonLocator);

        /// <summary>
        /// Remove from compare button
        /// </summary>
        public IButton RemoveFromCompareButton => new Button(this.Container, RemoveFromCompareButtonLocator);

        /// <summary>
        /// Prior Versions Document Link
        /// </summary>
        public ILink PriorVersionsDocumentLink => new Link(this.Container, PriorVersionsDocumentLinkLocator);
    }
}