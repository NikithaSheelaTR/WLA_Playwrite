namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Footer component
    /// </summary>
    public class CanadaEdgeFooterComponent : EdgeFooterComponent
    {
        private static readonly By ContainerLocator = By.Id("co_footerLinks");
        private static readonly By NeedHelpTextLocator = By.XPath("//div[@class='Footer-info Footer-info-secondary']//li");
        private static readonly By CopyRightTextLocator = By.XPath("//li[@id='co_copyright']");
        private static readonly By TrDisclaimerLinkLocator = By.XPath("//div[@id='co_footer_disclaimer']/a");
        private static readonly By ThomsonReutersFooterLogoLocator = By.Id("co_trLogo_link");

        private EnumPropertyMapper<CanadaEdgeFooterLinks, WebElementInfo> footerLinksMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Indigo Footer Links Map
        /// </summary>
        protected new EnumPropertyMapper<CanadaEdgeFooterLinks, WebElementInfo> FooterLinksMap =>
            this.footerLinksMap = this.footerLinksMap ?? EnumPropertyModelCache.GetMap<CanadaEdgeFooterLinks, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Thomson Reuters Disclaimer Link
        /// </summary>
        public ILink TrDisclaimerLink = new Link(TrDisclaimerLinkLocator);

        /// <summary>
        /// Is Footer link displayed.
        /// </summary>
        /// <returns>True - if link is displayed, false - otherwise</returns>
        public bool IsFooterLinkDisplayed(CanadaEdgeFooterLinks footerLink) => DriverExtensions.IsDisplayed(By.Id(this.FooterLinksMap[footerLink].Id));

        /// <summary>
        /// Is Footer locale toggle link displayed.
        /// </summary>
        /// <returns>True - if link is displayed, false - otherwise</returns>
        public bool IsFooterLocaleToggleLinkDisplayed() => DriverExtensions.IsDisplayed(By.Id(this.FooterLinksMap[CanadaEdgeFooterLinks.LocaleToggle].Id));

        /// <summary>
        /// Click Link
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="footerLink">Indigo footer link</param>
        /// <returns>T</returns>
        public T ClickLink<T>(CanadaEdgeFooterLinks footerLink) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.Id(this.FooterLinksMap[footerLink].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Need help text
        /// </summary>
        /// <returns>Text of need help in footer</returns>
        public string GetNeedHelpText() => DriverExtensions.GetText(NeedHelpTextLocator);

        /// <summary>
        /// Gets the copy right text from the footer
        /// </summary>
        /// <returns>Get the text</returns>
        public string GetCopyRightText() => DriverExtensions.GetText(CopyRightTextLocator);

        /// <summary>
        /// Check if TR logo is displayed in the footer
        /// </summary>
        /// <returns> True if footer logo is displayed, false otherwise </returns>
        public bool IsThomsonReutersLogoDisplayed() => DriverExtensions.IsDisplayed(ThomsonReutersFooterLogoLocator);
    }
}
