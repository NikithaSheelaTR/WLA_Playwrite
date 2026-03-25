namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.OpportunityFinderComponents
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// OpportunityFinderCustomizeAndViewInTheReportComponent
    /// </summary>
    public class OpportunityFinderCustomizeAndViewInTheReportComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='la-OpFinder-customize']");
        private static string ColumnItemMaskLocator = "//input[@id = '{0}']";

        /// <summary>
        /// Gets the Litigation Analytics tabs enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<OpportunityFinderColumnsToDisplay, WebElementInfo> TabsMap =>
            EnumPropertyModelCache.GetMap<OpportunityFinderColumnsToDisplay, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        ///<summary> 
        /// Column Item checkbox
        ///</summary>
        public ICheckBox ColumnItem(OpportunityFinderColumnsToDisplay column) => new CheckBox(By.XPath(string.Format(ColumnItemMaskLocator, this.TabsMap[column].Id)));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}