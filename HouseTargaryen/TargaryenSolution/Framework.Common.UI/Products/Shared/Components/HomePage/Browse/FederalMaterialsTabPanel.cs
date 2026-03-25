namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Homepage;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Federal Materials Tab Panel
    /// </summary>
    public class FederalMaterialsTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("co_browseWidgetTabPanel2");

        private EnumPropertyMapper<FederalMaterials, BaseTextModel> federalMaterialsMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Federal Materials";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<FederalMaterials, BaseTextModel> FederalMaterialsMap
            => this.federalMaterialsMap = this.federalMaterialsMap ?? EnumPropertyModelCache.GetMap<FederalMaterials, BaseTextModel>();

        /// <summary>
        /// Clicks the category page link for a specified content type
        /// </summary>
        /// <typeparam name="T">the type of the page to return</typeparam>
        /// <param name="federalMaterials">the content type to navigate to</param>
        /// <returns>a browse page for the specified content type</returns>
        public T ClickBrowseCategory<T>(FederalMaterials federalMaterials) where T : ICreatablePageObject
            => this.ClickBrowseCategory<T>(this.FederalMaterialsMap[federalMaterials].Text);
    }
}