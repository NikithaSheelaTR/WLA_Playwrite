namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Law Firms Result List Item
    /// </summary>
    public class LawFirmsResultListItem : BaseItem
    {
        private static readonly By LawFirmResultListCheckBoxLocator = By.XPath("//input[@class = 'SearchFacet-inputCheckbox']");
        private static readonly By LawFirmResultListItemTitleLocator = By.XPath("//span[@class ='CompanyDetail-heading']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="LitigationAnalyticsTypeaheadItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public LawFirmsResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Nmae 
        /// </summary>
        public ILabel LawFirmResultListItemTitle => new Label(this.Container, LawFirmResultListItemTitleLocator);

        /// <summary>
        /// Nmae link
        /// </summary>
        public ICheckBox InputCheckBox => new CheckBox(this.Container, LawFirmResultListCheckBoxLocator);

    }
}