namespace Framework.Common.UI.Products.Concourse.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Header
    /// </summary>
    public class HeaderComponent : BaseModuleRegressionComponent
    {
        private const string ProductLinkLctMask = "//div[@id='appActiveContent']//a[@title={0}]";

        private static readonly By ShowProductsButtonLocator = By.XPath("//div[@id='appName']//a[@class='icon icon_appToggleDown']");

        private static readonly By LogoLocator = By.Id("logo");

        private static readonly By ContainerLocator = By.Id("header");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Select Product
        /// </summary>
        /// <typeparam name="T">Type of page to return</typeparam>
        /// <param name="productName">The name of the product</param>
        /// <returns>Instance of the T Page</returns>
        public T SelectProduct<T>(string productName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ShowProductsButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ProductLinkLctMask, productName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks for the concourse logo.
        /// </summary>
        /// <returns>True if element is displayed, otherwise - false</returns>
        public bool IsLogoDisplayed() => DriverExtensions.IsDisplayed(LogoLocator, 5);
    }
}