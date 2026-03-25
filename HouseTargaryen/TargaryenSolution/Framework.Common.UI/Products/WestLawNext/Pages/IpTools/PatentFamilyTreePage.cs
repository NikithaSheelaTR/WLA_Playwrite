namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The patent family tree page.
    /// </summary>
    public class PatentFamilyTreePage : TabPage
    {
        private static readonly By PatentGraphItemLocator = By.XPath("//*[name()='g' and contains(@id,'group')]");

        private static readonly By PatentListSelectedItemLocator = By.XPath("//a[@class='co_listItemGroup co_checkbox_selected']");

        private static readonly By PatentListItemLocator = By.XPath("//ul[@id='co_patentList']/li/div/a");

        private static readonly By CitationArrayLocator =
            By.XPath("./*[name()='text' and ./*[name()='tspan' and @font-weight='bold']]/*[name()='tspan' and position() < 3]");

        private static readonly By PatentListItemCitationLocator = By.XPath(".//div/span[contains(@id, 'cite')]");
        
        /// <summary>
        /// Gets citation of graph patent item
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHighlightedGraphPatentItemCitation()
        {
            const string CoordinateQuery = "return $('path[stroke=#f89b2f]')[0].getBBox()";
            float highlightedPatentX = Convert.ToSingle(DriverExtensions.ExecuteScript($"{CoordinateQuery}.x"));
            float highlightedPatentY = Convert.ToSingle(DriverExtensions.ExecuteScript($"{CoordinateQuery}.y"));

            IWebElement highlitedPatentGraphItem
                = DriverExtensions.WaitForElement(By.XPath($"//*[name()='g' and ./*[name()='rect' and @x='{highlightedPatentX}' and @y='{highlightedPatentY}']]"));

            return string.Concat(DriverExtensions.GetElements(highlitedPatentGraphItem, CitationArrayLocator).Select(x => x.GetAttribute("textContent")));
        }

        /// <summary>
        /// Clicks using JavaScript on Patent Graph Item
        /// </summary>
        /// <param name="index">Item's index</param>
        public void ClickPatenGraphItem(int index)
            => DriverExtensions.ExecuteScript("$(arguments[0]).click();", DriverExtensions.GetElements(PatentGraphItemLocator).ElementAt(index));

        /// <summary>
        /// Clicks on Patent Graph Item
        /// </summary>
        /// <param name="index">Item's index</param>
        public void ClickPatentListItem(int index) => DriverExtensions.GetElements(PatentListItemLocator).ElementAt(index).Click();

        /// <summary>
        /// Gets citation of the selected patent list item
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetPatentListSelectedItemCitation()
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(PatentListSelectedItemLocator), PatentListItemCitationLocator).Text;
    }
}