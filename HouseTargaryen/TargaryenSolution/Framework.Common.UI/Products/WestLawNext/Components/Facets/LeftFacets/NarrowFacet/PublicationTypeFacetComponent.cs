namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.WestLawNext.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// PublicationTypeFacetComponent
    /// </summary>
    public class PublicationTypeFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = "//*[normalize-space(text())='{0}']";

        private static readonly By ExpandButtonLocator = By.XPath(".//a[@class='co_facet_expand']");

        private static readonly By PublicationTypesLocator = By.ClassName("co_multiple_xboxes_link_publicationType");

        private static readonly By ContainerLocator = By.CssSelector("#facet_div_publicationType, #facet_div_MetaDataPublicationTypeFacet, #facet_div_topic");

        private EnumPropertyMapper<PublicationType, BaseTextModel> publicationTypeMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// PublicationTypeMap
        /// </summary>
        protected EnumPropertyMapper<PublicationType, BaseTextModel> PublicationTypeMap
            => this.publicationTypeMap = this.publicationTypeMap ?? EnumPropertyModelCache.GetMap<PublicationType, BaseTextModel>();

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="publicationType">The publicationType</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(PublicationType publicationType)
        {
            this.ExpandParentFacet(By.XPath(string.Format(CheckboxLctMask, this.PublicationTypeMap[publicationType].Text)), ExpandButtonLocator);
            return this.GetCheckboxCount(string.Format(CheckboxLctMask, this.PublicationTypeMap[publicationType].Text));
        }

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="publicationType">The publicationType</param>
        /// <returns>True if the facet is a checkbox</returns>
        public bool IsCheckboxDisplayed(PublicationType publicationType)
        {
            IWebElement checkbox
                = DriverExtensions.SafeGetElement(DriverExtensions.GetElement(this.ComponentLocator), By.XPath(string.Format(CheckboxLctMask, this.PublicationTypeMap[publicationType].Text)));
            return checkbox == null ? false : checkbox.IsDisplayed();
        }

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="publicationType">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(PublicationType publicationType, bool setTo = true) where T : ICreatablePageObject
        {
            if (DriverExtensions.IsDisplayed(PublicationTypesLocator))
            {
                DriverExtensions.GetElement(PublicationTypesLocator).CustomClick();
            }

            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.PublicationTypeMap[publicationType].Text));
            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            return this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, checkboxLocator), setTo);
        }

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="publicationType">The publicationType.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(PublicationType publicationType)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, this.PublicationTypeMap[publicationType].Text))));

        /// <summary>
        /// Gets the list of Topic facets
        /// </summary>
        /// <returns>The list of facet names</returns>
        public List<string> GetPubTypeSubFacetsList()
            => DriverExtensions.GetElements(this.ComponentLocator, By.TagName("label")).Select(s => s.Text).ToList();
    }
}