namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.SelectDefaultTabComponents
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Interfaces.Elements;

    /// <summary>
    /// 'For me' select default tab dialog component
    /// </summary>
    public class ForMeTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel_userTab']");
        private static readonly By DescriptionLabelLocator = By.XPath(".//legend");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "For me";

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
        /// <param name="option"></param>
        public void SelectDefaultTab(SelectDefaultTabOptions option) =>
            DriverExtensions.WaitForElement(By.Id(this.SelectTabMap[option].Id)).Click();

        /// <summary>
        /// Default options map
        /// </summary>
        protected EnumPropertyMapper<SelectDefaultTabOptions, WebElementInfo> SelectTabMap =>
            EnumPropertyModelCache.GetMap<SelectDefaultTabOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/HomePage");
    }
}
