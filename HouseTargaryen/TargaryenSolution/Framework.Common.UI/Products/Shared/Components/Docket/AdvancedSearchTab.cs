namespace Framework.Common.UI.Products.Shared.Components.Docket
{
    using Framework.Common.UI.Products.WestLawNext.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Dockets advanced search tab
    /// </summary>
    public class AdvancedSearchTab : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_advancedSearchContent']//div[@id='OD']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Advanced search";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}