namespace Framework.Common.UI.Products.WestLawNextCanada.Components.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.CompareText;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Linq;


    /// <summary>
    /// Canada Compare Text Side By Side View Tab
    /// </summary>
    public class CanadaSideBySideViewTab : SideBySideViewTab
    {
        private static readonly By HighlightedTextLocator = By.XPath("//div[contains(@class,'co_statutesCompare_viewHighlight')]//mark");
        private static readonly By KeyLabelTextLocator = By.XPath(".//li[@class = 'co_statutesCompare_key']/strong");
        private static readonly By KeyValueLabelLocator = By.XPath(".//li[@class = 'co_statutesCompare_key']//mark");
        private static readonly By KeyInfoLabelLocator = By.ClassName("co_moreInfo");

        /// <summary>
        /// Navigation component
        /// </summary>
        public CompareTextNavigationComponent NavigationComponent => new CompareTextNavigationComponent(this.ComponentLocator);

        /// <summary>
        /// Verifies that texts highlighted to compare are highlighted.
        /// </summary>
        /// <returns>true if highlighted else false</returns>
        public bool AreAllSnippetTextHighlighted(string HighlightedCssValue) => AreElementsHighlighted(HighlightedTextLocator, HighlightedCssValue);

        /// <summary>
        /// Verifies that Key label is highlighted
        /// </summary>
        /// <returns>true if highlighted else false</returns>
        public bool IsKeyValueHighlighted(string HighlightedCssValue) => AreElementsHighlighted(KeyValueLabelLocator, HighlightedCssValue);

        /// <summary>
        /// Gets the Label of the Key
        /// </summary>
        public ILabel KeyLabel => new Label(DriverExtensions.GetElement(ComponentLocator), KeyLabelTextLocator);

        /// <summary>
        /// Gets the Key Value label
        /// </summary>
        public ILabel KeyValueLabel => new Label(DriverExtensions.GetElement(ComponentLocator), KeyValueLabelLocator);

        /// <summary>
        /// Gets the Key Info label
        /// </summary>
        public ILabel KeyInfoLocator => new Label(DriverExtensions.GetElement(ComponentLocator), KeyInfoLabelLocator);

        private bool AreElementsHighlighted(By Locator, string HighlightedCssValue) => DriverExtensions
              .GetElements(DriverExtensions.GetElement(ComponentLocator), Locator)
              .ToList().TrueForAll(text => text.GetCssValue("background-color").Contains(HighlightedCssValue));
    }
}