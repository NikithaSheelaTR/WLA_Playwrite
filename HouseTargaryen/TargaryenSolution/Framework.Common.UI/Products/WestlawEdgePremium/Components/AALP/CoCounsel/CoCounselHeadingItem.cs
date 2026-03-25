namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounsel supporting material item (accordion item)
    /// </summary>
    public class CoCounselHeadingItem : BaseItem
    {
        private static readonly By HeadingButtonLocator = By.XPath("./span[@slot='heading']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCounselHeadingItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public CoCounselHeadingItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Heading button
        /// </summary>
        public IButton HeadingButton => new Button(this.Container, HeadingButtonLocator);
    }
}
