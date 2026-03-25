namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics.OpportunityFinder
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Threading;

    /// <summary>
    /// Opportunity Finder Dialog
    /// </summary>
    public class OpportunityFinderFilterDialog : LitigationAnalyticsFacetDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class,'la-OpFinder-centerCol') and contains(@class,'la-OpFinder-scrollableColumn')]");
        private static readonly By SearchFieldLocator = By.XPath(".//input[contains(@class,'SearchFacet-inputText')]");
        private static readonly By AllAmountsDropdownLocator = By.ClassName("la-TotalDamagesFilter");
        private static readonly By AmountLabelLocator = By.XPath("//div[@class ='la-TotalDamagesFilter-minMax']/input");
        private static readonly By GoButtonLocator = By.ClassName("co_primaryBtn");
        private static string AllAmountsDropdownItemsMskLocator = "//option[./label[text()='{0}']]";

        /// <summary>
        /// Opportunity Finder Filter Dialog
        /// </summary>
        public OpportunityFinderFilterDialog()
        {

        }

        ///<summary>
        /// Select AmountLabel and click Go button
        ///</summary>
        public void SelectAmount(string amountCount)
        {
            var amountLabel = DriverExtensions.GetElement(this.ComponentLocator, AmountLabelLocator);
            amountLabel.Clear();
            amountLabel.SendKeys(amountCount);
            new Button(this.ComponentLocator, GoButtonLocator).Click();
        }

        /// <summary>
        /// Amount Label
        /// </summary>
        public ILabel AmountLabel => new Label(AmountLabelLocator);

        /// <summary>
        /// Gets the Damages filter enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AllAmountsDropdownItems, WebElementInfo> DamagesFilterMap =>
            EnumPropertyModelCache.GetMap<AllAmountsDropdownItems, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/LitigationAnalytics");

        /// <summary>
        /// Enter search query
        /// </summary>
        /// <param name="searchQuery">Search query</param>
        /// <returns>New instance of the page</returns>
        public new void EnterSearchQuery(string searchQuery)
        {
            DriverExtensions.WaitForElementPresent(this.ComponentLocator, SearchFieldLocator);
            DriverExtensions.SetTextField(searchQuery, this.ComponentLocator, SearchFieldLocator);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Select Dropdown Item
        /// </summary>
        public void SelectDropdownItem(AllAmountsDropdownItems item)
        {
            DriverExtensions.WaitForElementPresent(this.ComponentLocator, AllAmountsDropdownLocator);
            DriverExtensions.GetElement(this.ComponentLocator, AllAmountsDropdownLocator).Click();
            var dropdown = DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(AllAmountsDropdownItemsMskLocator, (this.DamagesFilterMap[item].Text))));
            dropdown.Click();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}