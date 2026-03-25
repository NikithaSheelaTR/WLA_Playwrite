namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Products Tab Panel
    /// </summary>
    public class ProductsTabPanel : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_browseWidgetTabPanel5");

        private EnumPropertyMapper<ProductsLinks, WebElementInfo> ProductslLinksMap;

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Products";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the International type enumeration to InternationalTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ProductsLinks, WebElementInfo> productsMap
            => this.ProductslLinksMap = this.ProductslLinksMap ?? EnumPropertyModelCache.GetMap<ProductsLinks, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Clicks the product feature link in the Products and Features tab
        /// </summary>
        /// <typeparam name="T">the type of the page to return</typeparam>
        /// <param name="ProductsLinks">the product feature link to clicks</param>
        /// <returns>a browse page for the specified content type</returns>
        public T ClickproductsLinks<T>(ProductsLinks ProductsLinks) where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(By.XPath(this.productsMap[ProductsLinks].Text)));
            return DriverExtensions.CreatePageInstance<T>();
        }

    }
}
