using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Components;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents
{
    /// <summary>
    /// OpportunityFinderFiltersPanel
    /// </summary>
    public class OpportunityFinderFiltersPanel : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='la-OpFinder-filter']");
        private static string FilterItemMaskLocator = "//div[contains(@class,'la-OpFinder-filterGroup-item')]/button[./span[text()='{0}']]";

        ///<summary>
        ///Filter Item
        /// </summary>
        public IButton FilterItemButton(string filterName) => new Button(By.XPath(string.Format(FilterItemMaskLocator, filterName)));

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}