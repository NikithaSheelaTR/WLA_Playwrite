namespace Framework.Common.UI.Products.GovernmentWeblinks.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Weblinks header component for module regression suites
    /// </summary>
    public class WeblinksFooterComponent : BaseModuleRegressionComponent
    {
        private static readonly By LinksLocator = By.XPath("//div[@id='co_footerLinks']/a");

        private static readonly By LogoLocator = By.XPath("//div[@id='co_trLogo']/a[@id='co_trLogo_link']");

        private static readonly By ContainerLocator = By.Id("co_footerContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets links count
        /// </summary>
        /// <returns>The <see cref="int"/> count of links.</returns>
        public Dictionary<string, string> GetLinks() => DriverExtensions.GetElements(LinksLocator).ToDictionary(e => e.Text, e => e.GetAttribute("href"));

        /// <summary>
        /// Verifies is logo displayed
        /// </summary>
        /// <returns>True if logo displayed, false otherwise</returns>
        public bool IsLogoDisplayed() => DriverExtensions.IsDisplayed(LogoLocator, 5);
    }
}