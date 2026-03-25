namespace Framework.Common.UI.Products.Shared.Components.BreadCrumb
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Bread Crumb Component
    /// </summary>
    public class BreadCrumbComponent : BaseModuleRegressionComponent
    {
        private static readonly By BreadCrumbItemLocator = By.XPath(".//*[contains(@class, 'BreadCrumbItem')]");

        private static readonly By ContainerLocator = By.XPath("//*[contains(@class,'co_website_browseBreadCrumbTrail')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets Breadcrumb Items
        /// </summary>
        public IList<BreadCrumbItem> BreadCrumbItems
            => DriverExtensions.GetElements(DriverExtensions.WaitForElementDisplayed(this.ComponentLocator), BreadCrumbItemLocator)
                .Select(rootElement => new BreadCrumbItem(rootElement)).ToList();

        /// <summary>
        /// The click breadcrumb link.
        /// </summary>
        /// <param name="linkText">The Link text</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickBreadcrumbLink<T>(string linkText) where T : ICreatablePageObject
            => this.BreadCrumbItems.FirstOrDefault(item => item.GetBreadCrumbItemText() == linkText).Click<T>();

        /// <summary>
        /// The is breadcrumb contains links.
        /// </summary>
        /// <param name="linksText">
        /// The links text.
        /// </param>
        /// <returns>
        /// The links text<see cref="bool"/>.
        /// </returns>
        public bool IsBreadcrumbContainsLinks(params string[] linksText)
            => linksText.ToList().TrueForAll(item => this.BreadCrumbItems.Select(link => link.GetBreadCrumbItemText()).ToList().Contains(item));

        /// <summary>
        /// The is breadcrumb component is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// The is breadcrumb component is string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Join(" > ", this.BreadCrumbItems.Select(a => a.Title));
        }
    }
}