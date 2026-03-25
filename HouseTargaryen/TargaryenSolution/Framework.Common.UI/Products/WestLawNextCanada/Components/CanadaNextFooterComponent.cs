namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Next Footer Component
    /// </summary>
    public class CanadaNextFooterComponent : WestlawNextFooterComponent
    {
        private static readonly By ContainerLocator = By.ClassName("Footer-info");
        private static readonly By NeedHelpContactNumberLocator = By.XPath("//ul[@id='co_footerCopyrightLine2']/li[@id='Span1']");

        private EnumPropertyMapper<CanadaNextFooterLinks, WebElementInfo> footerLinksMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Indigo Footer Links Map
        /// </summary>
        protected new EnumPropertyMapper<CanadaNextFooterLinks, WebElementInfo> FooterLinksMap =>
            this.footerLinksMap = this.footerLinksMap ?? EnumPropertyModelCache.GetMap<CanadaNextFooterLinks, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Is Footer link displayed.
        /// </summary>
        /// <returns>True - if link is displayed, false - otherwise</returns>
        public bool IsFooterLinkDisplayed(CanadaNextFooterLinks footerLink) => DriverExtensions.IsDisplayed(By.XPath(this.FooterLinksMap[footerLink].LocatorString));

        /// <summary>
        /// Gets Need help text from footer
        /// </summary>
        /// <returns>Text of Need help</returns>
        public string GetNeedHelpText() => DriverExtensions.GetElement(NeedHelpContactNumberLocator).Text;
    }
}