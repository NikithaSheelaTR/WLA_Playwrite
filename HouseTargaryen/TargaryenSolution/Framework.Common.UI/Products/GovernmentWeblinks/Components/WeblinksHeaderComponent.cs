namespace Framework.Common.UI.Products.GovernmentWeblinks.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.GovernmentWeblinks.Enums;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks header component for module regression suites
    /// </summary>
    public class WeblinksHeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_header");

        private static readonly By HeaderTextLocator = By.XPath("//div[@id='co_logo']//a[@class='applinknavigation']");

        private static readonly By LogoLocator = By.XPath("//div[@id='co_logo']/a[@class='logoImage']");

        private EnumPropertyMapper<StandardHeaderLinks, WebElementInfo> standardHeaderLinksMap;

        /// <summary>
        /// Gets the TermsFrequency enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<StandardHeaderLinks, WebElementInfo> StandardHeaderLinksMap
            => this.standardHeaderLinksMap = this.standardHeaderLinksMap ?? EnumPropertyModelCache.GetMap<StandardHeaderLinks, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the weblinks header link
        /// </summary>
        /// <typeparam name="T">ICreatablePageObject</typeparam>
        /// <param name="link">WeblinksHeaderLinks</param>
        /// <returns>The instance of a page</returns>
        public T ClickLink<T>(StandardHeaderLinks link) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(this.StandardHeaderLinksMap[link].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets header href
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetHeaderLink() => DriverExtensions.WaitForElement(HeaderTextLocator).GetAttribute("href");

        /// <summary>
        /// Gets header text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetHeaderText() => DriverExtensions.GetText(HeaderTextLocator);

        /// <summary>
        /// Gets logo link (href)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetLogoLink() => DriverExtensions.GetAttribute("href", LogoLocator);

        /// <summary>
        /// Verifies is header text displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHeaderTextDisplayed() => DriverExtensions.IsDisplayed(HeaderTextLocator);

        /// <summary>
        /// Verifies is logo displayed
        /// </summary>
        /// <returns>True if logo displayed, false otherwise</returns>
        public bool IsLogoDisplayed() =>
            DriverExtensions.IsDisplayed(LogoLocator, 5);
    }
}
