namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// The header component. Common for Recommendations, Warnings for cited authority and Table if authorities tabs.
    /// </summary>
    public sealed class QuickCheckReportHeaderComponent : EdgeHeaderComponent
    {
        private static readonly By DocAnalyzerLogoLocator = By.XPath("//div[@class='HeaderBrand-subLogo']/a");

        /// <summary>
        /// The logo link.
        /// </summary>
        public ILink LogoLink => new Link(DocAnalyzerLogoLocator);
    }
}