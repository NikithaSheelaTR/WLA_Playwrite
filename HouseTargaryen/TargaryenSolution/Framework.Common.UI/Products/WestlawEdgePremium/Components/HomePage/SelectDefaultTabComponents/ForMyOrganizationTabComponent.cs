namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.SelectDefaultTabComponents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Core.CommonTypes.Extensions;

    /// <summary>
    /// 'For my organization' select default tab dialog component
    /// </summary>
    public class ForMyOrganizationTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_firmTab");
        private static readonly By DescriptionLabelLocator = By.XPath(".//legend");
        private static readonly By SelectedTabLocator = By.XPath(".//div//input[@checked]//following-sibling::label");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "For my organization";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Description label
        /// </summary>
        public ILabel DescriptionLabel => new Label(this.ComponentLocator, DescriptionLabelLocator);

        /// <summary>
        /// Select Default tab
        /// </summary>
        /// <param name="option">Option to select</param>
        public void SelectDefaultTab(SelectDefaultTabOptions option) =>
            DriverExtensions.WaitForElement(By.Id(this.SelectTabMap[option].Id)).Click();

        /// <summary>
        /// Get selected default tab
        /// </summary>
        public SelectDefaultTabOptions GetSelectedTab() => 
            DriverExtensions.GetElement(this.ComponentLocator, SelectedTabLocator).
            Text.GetEnumValueByText<SelectDefaultTabOptions>(sourceFolder: @"Resources/EnumPropertyMaps/WestlawEdgePremium/HomePage");            

        /// <summary>
        /// Default options map
        /// </summary>
        protected EnumPropertyMapper<SelectDefaultTabOptions, WebElementInfo> SelectTabMap =>
            EnumPropertyModelCache.GetMap<SelectDefaultTabOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/HomePage");
    }
}
