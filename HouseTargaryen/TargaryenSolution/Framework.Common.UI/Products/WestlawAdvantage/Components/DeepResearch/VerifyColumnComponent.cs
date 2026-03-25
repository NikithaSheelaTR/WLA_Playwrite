namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Verify Column component
    /// </summary>
    public class VerifyColumnComponent : BaseItem
    {
        private static readonly By VerifyHeaderColumnLabelLocator = By.XPath("//th[contains(@id, 'verifier-table-verify')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyColumnComponent"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public VerifyColumnComponent(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Verify column label
        /// </summary>
        public ILabel VerifyLabel => new Label(this.Container, VerifyHeaderColumnLabelLocator);
    }
}
