using OpenQA.Selenium;

namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Jurisdiction component
    /// </summary>
    public class TrademarksJurisdictionFilterComponent : BaseModuleRegressionComponent
    {
        private static readonly By NarrowJurisdictionLabelLocator = By.XPath("./h4");
        private static readonly By JurisdictionNarrowTextBoxLocator = By.XPath(".//input[@id='jurisdictions-search']");
        private static readonly By FilterItemsLocator = By.XPath(".//ul[@id='jurisdictions-tree']/li");
        private static readonly By ClearAllJurisdictionsButtonLocator = By.XPath(".//button[@id='tmis_clear_all_jurisdictions']");
        private static readonly By RegionGroupLocator = By.XPath(".//li[contains(@class,'tf-child-true')]//label");

        private static readonly By RegionGroupCheckboxLocator = By.XPath("./input[contains(@class,'jurisdictionGroupInputCheckbox')]");
        private static readonly By ExpandJurisdictionButtonLocator = By.XPath(".//button[@class='TrademarkImageSearch-buttonSortTree']/span[contains(@class,'addBox')]");
        private static readonly By CollapseJurisdictionButtonLocator = By.XPath(".//button[@class='TrademarkImageSearch-buttonSortTree']/span[contains(@class,'removeBox')]");

        private IReadOnlyCollection<IWebElement> regionElements => DriverExtensions.GetElements(this.ComponentLocator, RegionGroupLocator).ToList();

        private IReadOnlyCollection<IButton> ExpandJurisdictionButtonsList => new ElementsCollection<Button>(this.ComponentLocator, ExpandJurisdictionButtonLocator);

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//div[contains(@class,'TrademarkImageSearch-container')]");

        /// <summary>
        /// List of Jurisdiction Collapse button's list
        /// </summary>
        public IReadOnlyCollection<IWebElement> CollapseJurisdictionButtonsList => DriverExtensions.GetElements(this.ComponentLocator, CollapseJurisdictionButtonLocator).ToList();


        /// <summary>
        /// Jurisdiction Narrow Label
        /// </summary>
        public ILabel JurisdictionNarrowLabel => new Label(this.ComponentLocator, NarrowJurisdictionLabelLocator);

        /// <summary>
        /// RegionGroupLabel
        /// </summary>
        public List<Label> RegionGroupLabelList => regionElements?.Select(wl => new Label(wl)).ToList();

        /// <summary>
        /// Set Region Group Checkbox
        /// </summary>
        /// <param name="region"></param>
        /// <param name="state"></param>
        public void SetRegionGroupCheckbox(string region, bool state = true) =>
           this.regionElements?.Where(el => el.Text.Contains(region))
               .Select(el => new CheckBox(el, RegionGroupCheckboxLocator)).FirstOrDefault().Set(state);

        /// <summary>
        /// Jurisdiction Narrow textbox
        /// </summary>
        public ITextbox JurisdictionNarrowTextbox => new Textbox(this.ComponentLocator, JurisdictionNarrowTextBoxLocator);

        /// <summary>
        /// Clear Jurisdictions Button
        /// </summary>
        public IButton ClearAllJurisdictionsButton => new Button(this.ComponentLocator, ClearAllJurisdictionsButtonLocator);

        /// <summary>
        /// Jurisdiction Filter root item
        /// </summary>
        public JurisdictionFilterItem RootFilterItem =>
            new JurisdictionFilterItem(DriverExtensions.GetElement(this.ComponentLocator, FilterItemsLocator));

        /// <summary>
        /// Expand Parent Jurisdictions method
        /// </summary>
        public void ExpandParentJurisdictions() => this.ExpandJurisdictionButtonsList.FirstOrDefault().Click();

        /// <summary>
        /// Clear previous Jurisdcition filter and set US Jurisdiction
        /// </summary>
        /// <param name="jurisdiction"></param>
        public void ClearAndSetJurisdiction(string jurisdiction)
        {
            this.ClearAllJurisdictionsButton.Click();
            this.ExpandParentJurisdictions();
            this.EnterCountryCode(jurisdiction);
            this.RootFilterItem.ChildrenFilterItems()
                          .FirstOrDefault(x => x.FilterLabel.Text == jurisdiction)
                          .JurisdictionFilterCheckbox.Set(true);
        }

        /// <summary>
        /// Enter Country code in the Jurisdiction text box
        /// </summary>
        /// <param name="countryCode"></param>
        public void EnterCountryCode(string countryCode)
        {
            this.ClearAllJurisdictionsButton.Click();
            this.JurisdictionNarrowTextbox.SetText(countryCode);
        }
    }
}