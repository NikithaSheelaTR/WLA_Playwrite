namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Assertion Column component
    /// </summary>
    public class AssertionColumnComponent : BaseItem
    {
        private static readonly By AssertionHeaderColumnLabelLocator = By.XPath("//th[contains(@id, 'verifier-table-statement')]");
        private static readonly By AssertionColumnTextLocator = By.XPath("//td[contains(@headers, 'verifier-table-statement')]/p");
        private static readonly By AssertionColumnLinkLocator = By.XPath("//td[contains(@headers, 'verifier-table-statement')]/saf-anchor-v3");

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionColumnComponent"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public AssertionColumnComponent(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Assertion column label
        /// </summary>
        public ILabel AssertionLabel => new Label(this.Container, AssertionHeaderColumnLabelLocator);

        /// <summary>
        /// Assertion column Text label
        /// </summary>
        public ILabel AssertionTextLabel => new Label(this.Container, AssertionColumnTextLocator);

        /// <summary>
        /// Assertion column Link label
        /// </summary>
        public ILabel AssertionLinkLabel => new Label(this.Container, AssertionColumnLinkLocator);
    }
}
