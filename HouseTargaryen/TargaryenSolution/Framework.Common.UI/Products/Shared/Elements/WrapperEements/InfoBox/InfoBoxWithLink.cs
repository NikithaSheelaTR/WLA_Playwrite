namespace Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// InfoBox with HighQ link in the text
    /// </summary>
    public class InfoBoxWithLink : InfoBox, IInfoBoxWithLink
    {
        private static readonly By MessageLocator = By.XPath(".//div[@class = 'co_infoBox_message']");
        private static readonly By LinkLocator = By.XPath(".//*[self::a | self::button]");

        /// <summary>
        /// currentContainer - InfoBox container, messageLocator - info text locator, closeLocator - X button locator
        /// </summary>
        /// <param name="currentContainer"></param>       
        public InfoBoxWithLink(By currentContainer) : base(currentContainer, MessageLocator, CloseButtonLocator)
        {
        }

        /// <summary>
        /// Link in the message 
        /// </summary>
        public ILink TextLink => new Link(this.GetContainer(), MessageLocator, LinkLocator);

        /// <summary>
        /// Link in the infobox 
        /// </summary>
        public ILink InternalLink => new Link(this.GetContainer(), LinkLocator);
    }
}
