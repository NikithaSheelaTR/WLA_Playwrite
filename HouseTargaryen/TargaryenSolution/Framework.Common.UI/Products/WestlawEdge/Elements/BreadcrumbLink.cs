namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Breadcrumb link
    /// </summary>
    public class BreadcrumbLink : Link
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbLink"/> class.
        /// </summary>
        /// <param name="currentContainer"></param>
        public BreadcrumbLink(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <summary>
        /// Is link enabled
        /// </summary>
        /// <inheritdoc />
        public override bool Enabled => this.GetContainer().TagName.Equals("button");
    }
}