namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Company Result List Item
    /// </summary>
    public class CompanyResultListItem : BaseItem
    {
        private static readonly By CompanyResultListCheckBoxLocator = By.XPath("//*[@id= 'co_la_casePrecedent_resultItemundefined_container']//input[@class = 'SearchFacet-inputCheckbox']");
        private static readonly By CompanyResultSelectAllCheckBoxLocator = By.XPath("//input[@class = 'SearchFacet-inputCheckbox ng-untouched ng-pristine ng-valid']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="LitigationAnalyticsTypeaheadItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public CompanyResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Nmae link
        /// </summary>
        public ICheckBox InputCheckBox => new CheckBox(this.Container, CompanyResultListCheckBoxLocator);

        /// <summary>
        /// Select All CheckBox
        /// </summary>
        public ICheckBox SelectAllInputCheckBox => new CheckBox(this.Container, CompanyResultSelectAllCheckBoxLocator);

    }
}