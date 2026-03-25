namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The recent research list item.
    /// </summary>
    public class ChartContentItem : BaseItem
    {
        private static readonly By ItemLocator = By.XPath("//*[@class = 'stackedBlock']");
        private static readonly By StackedButtonChartViewLocator = By.XPath(".//*[@class='la-Chart-cover']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartContentItem"/> class.
        /// </summary>
        /// <param name="rootElement">
        /// The root element.
        /// </param>
        public ChartContentItem(IWebElement rootElement) : base(rootElement)
        {

        }

        /// <summary>
        /// Select colors
        /// </summary>
        public List<string> ColorsList() =>
            DriverExtensions.GetElements(ItemLocator).ToList()
                            .Select(element => element.GetCssValue("fill"))
                            .ToList();

        /// <summary>
        /// Stacked Button Chart Click
        /// </summary>
        public void StackedButtonChartClick()
        {
            DriverExtensions.GetElement(this.Container, StackedButtonChartViewLocator).Click();
        }
    }
}