namespace Framework.Common.UI.Products.WestLawNextCanada.Components.HomePage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// International Tab Panel
    /// </summary>
    public class InternationalTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class='co_tabs']//a[text()= 'International']");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "International";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;       

    }
}