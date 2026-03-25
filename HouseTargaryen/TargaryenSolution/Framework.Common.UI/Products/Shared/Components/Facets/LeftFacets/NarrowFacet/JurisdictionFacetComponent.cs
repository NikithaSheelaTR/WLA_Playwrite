namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Jurisdiction Facet (Filter)
    /// </summary>
    public class JurisdictionFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask
            = "//li[@role='treeitem']//div[./label[text()=\"{0}\"]]/input[@type='checkbox'] | //*[./*[text()=\"{0}\"]]/*[(local-name()='a' and @role='checkbox') or (local-name()='input' and @type='checkbox')] | //li[@role='treeitem']//div[./label[text()=\"{0}\"]]/span[@class='co_facetCount']";

        private static readonly By ChildItemLocator = By.XPath(".//../div/ul/li/a[@role='checkbox']");
        private static readonly By CanadaChildItemLocator = By.XPath(".//../div/ul/li/label");

        private static readonly By ContainerLocator
           = By.CssSelector("#facet_div_jurisdiction,#facet_div_Jurisdiction,#facet_div_AdminJurisdiction,#facet_div_trd_jurisdiction,#facet_div_MetaDataJurisdictionFacet,#facet_div_wlncJurisdiction");

        private const string ExpandButtonLctMask = ".//*[contains(@class,'co_facet_expand') and contains(text(),'{0}')]";

        private static readonly By ExpandButtonLocator = By.XPath(".//a[contains(@class,'co_facet_expand')]");

        private EnumPropertyMapper<Jurisdiction, BaseTextModel> jurisdictionsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        protected EnumPropertyMapper<Jurisdiction, BaseTextModel> JurisdictionsMap
            => this.jurisdictionsMap = this.jurisdictionsMap ?? EnumPropertyModelCache.GetMap<Jurisdiction, BaseTextModel>();

        /// <summary>
        /// GetChildItemsList of the specific checkbox
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction</param>
        /// <returns>The list of checkbox names</returns>
        public List<string> GetChildItemsList(Jurisdiction jurisdiction)
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text));
            By expandButtonLocator = By.XPath(string.Format(ExpandButtonLctMask, this.JurisdictionsMap[jurisdiction].Text));

            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            if (DriverExtensions.IsElementPresent(this.ComponentLocator, expandButtonLocator))
            {
                DriverExtensions.GetElement(this.ComponentLocator, expandButtonLocator).CustomClick();
            }
            return DriverExtensions.GetElements(this.ComponentLocator, checkboxLocator, ChildItemLocator).Select(ch => ch.GetText()).ToList();
        }

        /// <summary>
        /// GetItemsList
        /// </summary>
        /// <returns>The list of checkbox names</returns>
        public List<string> GetItemsList()
            => DriverExtensions.GetElements(this.ComponentLocator, ChildItemLocator).Select(ch => ch.GetText()).ToList();

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(Jurisdiction jurisdiction)
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator));
        }

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(string jurisdiction)
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, jurisdiction));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator));
        }

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxDisplayed(Jurisdiction jurisdiction)
            => this.IsCheckboxDisplayed(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text))));

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(Jurisdiction jurisdiction)
            => base.GetCheckboxCount(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text));

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public new int GetCheckboxCount(string jurisdiction)
            => base.GetCheckboxCount(string.Format(CheckboxLctMask, jurisdiction));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(Jurisdiction jurisdiction, bool setTo = true) where T : ICreatablePageObject
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator), setTo);
        }

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string jurisdiction, bool setTo = true) where T : ICreatablePageObject
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, jurisdiction));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator), setTo);
        }

        /// <summary>
        /// Verify that the given facet is of the checkbox type
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckbox(Jurisdiction jurisdiction)
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.IsCheckbox(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator));
        }

        /// <summary>
        /// GetChildItemsList of the specific checkbox
        /// </summary>
        /// <param name="jurisdiction">The jurisdiction</param>
        /// <returns>The list of checkbox names</returns>
        public List<string> GetCanadaChildItemsList(Jurisdiction jurisdiction)
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.JurisdictionsMap[jurisdiction].Text));
            By expandButtonLocator = By.XPath(string.Format(ExpandButtonLctMask, this.JurisdictionsMap[jurisdiction].Text));

            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            if (DriverExtensions.IsElementPresent(this.ComponentLocator, expandButtonLocator))
            {
                DriverExtensions.GetElement(this.ComponentLocator, expandButtonLocator).CustomClick();
            }
            return DriverExtensions.GetElements(this.ComponentLocator, checkboxLocator, CanadaChildItemLocator).Select(ch => ch.GetText()).ToList();
        }
    }
}

    

    
