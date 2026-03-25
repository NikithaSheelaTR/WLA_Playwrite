namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;    

    /// <summary>
    /// Breadcrumb component
    /// </summary>
    public class BreadcrumbComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class = 'co_researchOrganizerBreadCrumb']");
        private static readonly By ItemsLocator = By.XPath(".//div[@class = 'co_researchOrganizerBreadCrumbTrail']/nav/*");
        private const string TooltipLctMask = "//div[@class = 'a11yTooltip-content' and @id = '{0}']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get list of breadcrumbs links
        /// </summary>
        public IReadOnlyCollection<ILink> BreadcrumbLinks => DriverExtensions.GetElements(ContainerLocator, ItemsLocator)
            .Select(i => new BreadcrumbLink(i)).ToList();

        /// <summary>
        /// Click on the breadcrumb link by name
        /// </summary>
        /// <typeparam name="T">breadcrumb name</typeparam>
        /// <returns></returns>
        public T ClickBreadcrumbLink<T>(string itemName) where T : ICreatablePageObject
        {
           return this.BreadcrumbLinks.First(l => l.Text.Equals(itemName)).Click<T>();
        }

        /// <summary>
        /// Click on the breadcrumb link by index
        /// </summary>
        /// <typeparam name="T">breadcrumb index</typeparam>
        /// <returns></returns>
        public T ClickBreadcrumbLink<T>(int itemIndex) where T : ICreatablePageObject
        {
            return this.BreadcrumbLinks.ElementAt(itemIndex).Click<T>();
        }

        /// <summary>
        /// Tooltip for breadcrumb items
        /// </summary>
        public ILabel Tooltip(int itemIndex) 
            => new Label(By.XPath(string.Format(TooltipLctMask, BreadcrumbLinks.ElementAt(itemIndex).GetAttribute("aria-describedby"))));
    }
}
