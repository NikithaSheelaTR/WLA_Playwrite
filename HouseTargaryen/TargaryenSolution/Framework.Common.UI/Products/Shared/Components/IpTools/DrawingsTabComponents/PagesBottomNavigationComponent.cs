namespace Framework.Common.UI.Products.Shared.Components.IpTools.DrawingsTabComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Pages bottom navigation component
    /// </summary>
    public class PagesBottomNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By FirstPageButtonLocator = By.XPath("./li/a[@class='co_first']");
        private static readonly By PreviousPageButtonLocator = By.XPath("./li/a[@class='co_prev']");
        private static readonly By PaginationLinksLocator = By.XPath("./li/a[not(@class)]");
        private static readonly By NextPageButtonLocator = By.XPath("./li/a[@class='co_next']");
        private static readonly By LastPageButtonLocator = By.XPath("./li/a[@class='co_last']");

        /// <inheritdoc />
        protected override By ComponentLocator => By.XPath("//ul[@class='co_navFooter_pagination']");

        /// <summary>
        /// First page button
        /// </summary>
        public IButton FirstPageButton =>
            DriverExtensions.GetElements(this.ComponentLocator, FirstPageButtonLocator).Any()
                ? new Button(this.ComponentLocator, FirstPageButtonLocator)
                : null;

        /// <summary>
        /// Previous page button
        /// </summary>
        public IButton PreviousPageButton =>
            DriverExtensions.GetElements(this.ComponentLocator, PreviousPageButtonLocator).Any()
                ? new Button(this.ComponentLocator, PreviousPageButtonLocator)
                : null;

        /// <summary>
        /// Pagination links list
        /// </summary>
        public IReadOnlyCollection<ILink> PaginationLinksList =>
            DriverExtensions.GetElements(this.ComponentLocator, PaginationLinksLocator).Select(wbl => new Link(wbl))
                            .ToList();

        /// <summary>
        /// Next page button
        /// </summary>
        public IButton NextPageButton =>
            DriverExtensions.GetElements(this.ComponentLocator, NextPageButtonLocator).Any()
                ? new Button(this.ComponentLocator, NextPageButtonLocator)
                : null;

        /// <summary>
        /// Last page button
        /// </summary>
        public IButton LastPageButton =>
            DriverExtensions.GetElements(this.ComponentLocator, LastPageButtonLocator).Any()
                ? new Button(this.ComponentLocator, LastPageButtonLocator)
                : null;
    }
}