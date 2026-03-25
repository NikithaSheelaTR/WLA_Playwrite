namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseGroup section class
    /// </summary>
    public class BaseGroupSectionComponent : BaseItem
    {
        private static readonly By HeaderLocator = By.XPath(".//span[@class='DA-GroupedContentTitle']");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGroupSectionComponent"/> class. 
        /// </summary>
        /// <param name="container">The container.</param>
        public BaseGroupSectionComponent(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// section header
        /// </summary>
        public ILabel HeaderLabel => new Label(this.Container, HeaderLocator);
    }
}
