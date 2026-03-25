namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Verify Result Grid Item
    /// </summary>
    public class VerifyResultGridItem : BaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyResultGridItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public VerifyResultGridItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Assertion Column
        /// </summary>
        public AssertionColumnComponent AssertionColumn => new AssertionColumnComponent(this.Container);

        /// <summary>
        /// Verify Column
        /// </summary>
        public VerifyColumnComponent VerifyColumn => new VerifyColumnComponent(this.Container);

        /// <summary>
        /// Verify Column
        /// </summary>
        public ExploreMoreColumnComponent ExploreMoreColumn => new ExploreMoreColumnComponent(this.Container);
    }
}
