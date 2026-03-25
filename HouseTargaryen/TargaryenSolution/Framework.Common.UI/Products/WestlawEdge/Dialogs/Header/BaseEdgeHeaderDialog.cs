namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;

    /// <summary>
    /// Base header for Westlaw Edge
    /// </summary>
    public abstract class BaseEdgeHeaderDialog : BaseModuleRegressionDialog
    {
        private static readonly By ViewAllLinkLocator = By.XPath(".//a[.='View all'] | .//a[.='Afficher tout']");
        private static readonly By FavoritesViewAllLinkLocator = By.XPath(".//a[text()='View all favorites']");

        /// <summary>
        /// Element container 
        /// </summary>
        protected virtual IWebElement Container { get; set; }

        /// <summary>
        /// View All link
        /// </summary>
        public ILink ViewAllLink => new Link(this.Container, ViewAllLinkLocator);

        /// <summary>
        /// View All link
        /// </summary>
        public ILink FavoritesViewAllLink => new Link(this.Container, FavoritesViewAllLinkLocator);
    }
}
