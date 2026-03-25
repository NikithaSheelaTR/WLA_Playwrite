namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Quotation type facet
    /// </summary>
    public class QuotationTypeFacetComponent: BaseFacetComponent
    {
        private const string FacetCountLctMask = "//div[@id='co_contentTypeLinksBox']/ul/li[.//*[text()='{0}']]";

        private static readonly By FacetLabelLocator = By.XPath("//*[@class='co_genericBoxHeader']");
        private static readonly By ActiveQuotationTypeItemLocator = By.XPath(".//li[contains(@class, 'co_contentTypeDefault')]");
        private static readonly By QuotationTypeItemLocator = By.XPath(".//li");
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_contentTypeLinksBox']");

        /// <summary>
        /// Sort Quotations Map
        /// </summary>
        private EnumPropertyMapper<QuotationType, WebElementInfo> QuotationTypeMap =>
            EnumPropertyModelCache.GetMap<QuotationType, WebElementInfo>("", @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// Facet label
        /// </summary>
        public ILabel FacetLabel => new Label(FacetLabelLocator);

        /// <summary>
        /// Click quotation type link
        /// </summary>
        /// <param name="type">Quotation type</param>
        /// <returns>The <see cref="QuickCheckRecommendationsPage"/></returns>
        public QuickCheckRecommendationsPage ClickQuotationTypeLink(QuotationType type)
        {
            DriverExtensions.WaitForElement(By.XPath(this.QuotationTypeMap[type].LocatorString)).Click();
            return new QuickCheckRecommendationsPage();
        }

        /// <summary>
        /// Is quotation type selected
        /// </summary>
        /// <param name="type">Quotation type</param>
        /// <returns>True - if it is selected, false - otherwise</returns>
        public bool IsQuotationTypeSelected(QuotationType type) =>
            DriverExtensions.GetText(this.ComponentLocator, ActiveQuotationTypeItemLocator)
                            .Contains(this.QuotationTypeMap[type].Text);

        /// <summary>
        /// Is quotation type enabled
        /// </summary>
        /// <param name="type">Quotation type</param>
        /// <returns>True - if it is enabled, false - otherwise</returns>
        public bool IsQuotationTypeEnabled(QuotationType type) =>
            !DriverExtensions.GetElement(By.XPath(this.QuotationTypeMap[type].LocatorString)).GetAttribute("class").Contains("co_disabled");

        /// <summary>
        /// Get number near quotation type
        /// </summary>
        /// <param name="type">Quotations type</param>
        /// <returns>The number</returns>
        public int GetNumberOfResultsForQuotationType(QuotationType type)
        {
            By facetCountLocator = By.XPath(string.Format(FacetCountLctMask, this.QuotationTypeMap[type].Text));
            return DriverExtensions.GetElement(this.ComponentLocator, facetCountLocator).Text.ConvertCountToInt();
        }

        /// <summary>
        /// Get quotation type options
        /// </summary>
        /// <returns>List of options</returns>
        public List<QuotationType> GetQuotationTypeItems()
        {
            var listOfOptions = new List<QuotationType>();
            var optionsTextList = DriverExtensions.GetElements(this.ComponentLocator, QuotationTypeItemLocator).Select(el => el.Text).ToList();
            foreach(string optionText in optionsTextList)
            {
                string newOptionText = optionText.Replace("results", string.Empty).Replace("New", string.Empty);
                listOfOptions.Add(Regex.Replace(newOptionText, @"[\d-]", string.Empty).Trim().GetEnumValueByText<QuotationType>("", @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck"));
            }                               
                        
            return listOfOptions;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
