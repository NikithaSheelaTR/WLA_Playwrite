using System.Linq;
using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
using Framework.Common.UI.Products.Shared.Items;
using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage
{
    /// <summary>
    /// Advantage Tools Tab panel
    /// </summary>
    public class AdvantageToolsTabPanel : PrecisionToolsTabPanel
    {
        private static readonly By AdvantageToolsElementLocator = By.XPath(".//li");
        private static readonly By ContainerLocator = By.XPath("//*[@class='Athens-browse-tools' or contains(@class, 'Advantage-browse-tools')]/ancestor::*[contains(@id, 'panel')]");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Tools";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///  All Tools Items
        /// </summary>
        public ItemsCollection<AdvantageToolsTabItem> AdvantageToolsItems => new ItemsCollection<AdvantageToolsTabItem>(ComponentLocator, AdvantageToolsElementLocator);
        
        /// <summary>
        /// Checks if a widget is present by title.
        /// </summary>
        public bool IsWidgetPresent(string widgetTitle)
        {
            return AdvantageToolsItems.Any(item => item.HeaderLink.Text == widgetTitle);
        }
    }
}