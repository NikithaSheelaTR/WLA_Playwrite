namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Jurisdiction Filter
    /// </summary>
    public class JurisdictionFilterItem : BaseItem
    {
        private static readonly By ExpandCollapseButtonLocator =
            By.XPath(".//button[contains(@class,'TrademarkImageSearch-buttonSortTree')]");

        private static readonly By ParentFilterLabelLocator = By.XPath(".//div[contains(@class,'TrademarkImageSearch-parent')]//label");

        private static readonly By SingleFilterItemLocator = By.XPath(".//label[contains(@class,'TrademarkImageSearch-label')]");

        private static readonly By CheckboxLocator = By.XPath("./input[contains(@class,'TrademarkImageSearch-inputCheckbox')]");

        private static readonly By CheckboxLabelLocator = By.XPath("./span[contains(@class,'TrademarkImageSearch-labelText')]");

        private static readonly By ChildrenFilterItemsLocator =
            By.XPath(".//ul[contains(@class,'TrademarkImageSearch-tree')]//li");

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="container"></param>
        public JurisdictionFilterItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Defines whether filter has children filters
        /// </summary>
        public bool HasChildrenJurisdictions => this.Container.GetAttribute("class").Contains("tf-child-true");

        /// <summary>
        /// Filter label 
        /// </summary>
        public ILabel FilterLabel =>
            this.HasChildrenJurisdictions ? new Label(this.Container, ParentFilterLabelLocator) : new Label(this.Container, SingleFilterItemLocator, CheckboxLabelLocator);

        /// <summary>
        /// Expand collapse button
        /// </summary>
        public IButton ExpandCollapseButton =>
            this.HasChildrenJurisdictions ? new Button(this.Container, ExpandCollapseButtonLocator) : null;

        /// <summary>
        /// Jurisdiction filter checkbox
        /// </summary>
        public ICheckBox JurisdictionFilterCheckbox =>
            new CheckBox(this.Container, SingleFilterItemLocator, CheckboxLocator);

        /// <summary>
        /// Jurisdiction Children items
        /// </summary>
        /// <returns></returns>
        public List<JurisdictionFilterItem> ChildrenFilterItems() =>
            this.HasChildrenJurisdictions
                ? DriverExtensions.GetElements(this.Container, ChildrenFilterItemsLocator)
                                  .Select(webElem => new JurisdictionFilterItem(webElem)).ToList()
                : null;
    }
}