namespace Framework.Common.UI.Products.WestlawEdge.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Footer;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Footer component
    /// </summary>
    public class EdgeFooterComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_footerLinks");
        private static readonly By ThomsonReutersCopyRightLocator = By.Id("co_copyright");
        private static readonly By CopyRightPolicyLinkLocator = By.XPath("//li[@id='co_copyright']/a");
        private static readonly By FooterPhoneNumberLocator = By.XPath("//*[contains(@class, 'Footer-info-secondary')]//span");

        private EnumPropertyMapper<EdgeFooterLinks, WebElementInfo> footerLinksMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Indigo Footer Links Map
        /// </summary>
        protected EnumPropertyMapper<EdgeFooterLinks, WebElementInfo> FooterLinksMap =>
            this.footerLinksMap = this.footerLinksMap ?? EnumPropertyModelCache.GetMap<EdgeFooterLinks, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Footer");

        /// <summary>
        /// Copyright policy link
        /// </summary>
        public ILink CopyRightPolicyLink => new Link(CopyRightPolicyLinkLocator);

        /// <summary>
        /// CopyRight label
        /// </summary>
        public ILabel ThomsonReutersCopyRightLabel => new Label(ThomsonReutersCopyRightLocator);

        /// <summary>
        /// Footer phone number label
        /// </summary>
        public ILabel FooterPhoneNumberLabel => new Label(FooterPhoneNumberLocator);

        /// <summary>
        /// Is Footer link displayed.
        /// </summary>
        /// <returns>True - if link is displayed, false - otherwise</returns>
        public bool IsFooterLinkDisplayed(EdgeFooterLinks footerLink) => DriverExtensions.IsDisplayed(By.LinkText(this.FooterLinksMap[footerLink].Text));

        /// <summary>
        /// Click Link
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="footerLink">Indigo footer link</param>
        /// <returns>T</returns>
        public T ClickLink<T>(EdgeFooterLinks footerLink) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.LinkText(this.FooterLinksMap[footerLink].Text)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}