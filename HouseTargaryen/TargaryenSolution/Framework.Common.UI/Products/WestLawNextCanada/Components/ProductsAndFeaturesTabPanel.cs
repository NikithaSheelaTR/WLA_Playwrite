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
    /// Products and Features Tab Panel
    /// </summary>
    public class ProductsAndFeaturesTabPanel : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_browseTabContents']//div[contains(@class,'Tab-panel') and @aria-hidden='false']");

        private EnumPropertyMapper<ProductsAndFeaturesLinks, WebElementInfo> productFeatureLinkMap;

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Products and Features";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<ProductsAndFeaturesLinks, WebElementInfo> ProductFeatureMap
            => this.productFeatureLinkMap = this.productFeatureLinkMap ?? EnumPropertyModelCache
                                                .GetMap<ProductsAndFeaturesLinks, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Clicks the product feature link in the Products and Features tab
        /// </summary>
        /// <typeparam name="T">the type of the page to return</typeparam>
        /// <param name="productFeatureLink">the product feature link to clicks</param>
        /// <returns>a browse page for the specified content type</returns>
        public T ClickProductFeatureLink<T>(ProductsAndFeaturesLinks productFeatureLink) where T : ICreatablePageObject
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(By.XPath(this.ProductFeatureMap[productFeatureLink].LocatorString)));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}