namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestLawNext.Enums.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Intellectual property page browse widget component
    /// </summary>
    public class IpBrowseSelectorComponent : BaseModuleRegressionComponent
    {
        private EnumPropertyMapper<IntellectualPropertyCategories, WebElementInfo> ipCategoryMap;

        private static readonly By ContainerLocator = By.Id("coid_website_browseMainColumn");

        /// <summary>
        /// Intellectual Properties selector component
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the DeliveryMethod enumeration to WebElementInfo map for pdfs options.
        /// </summary>
        protected EnumPropertyMapper<IntellectualPropertyCategories, WebElementInfo> IpCategoryMap =>
            this.ipCategoryMap = this.ipCategoryMap
                                 ?? EnumPropertyModelCache.GetMap<IntellectualPropertyCategories, WebElementInfo>();

        /// <summary>
        /// Select Ip Category
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ipCategory"></param>
        /// <returns></returns>
        public T SelectIpCategory<T>(IntellectualPropertyCategories ipCategory) where T : IntellectualPropertyCategoryPage
        {
            DriverExtensions.WaitForElement(By.CssSelector(this.IpCategoryMap[ipCategory].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
