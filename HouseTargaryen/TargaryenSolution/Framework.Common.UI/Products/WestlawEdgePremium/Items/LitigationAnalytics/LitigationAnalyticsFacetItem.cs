namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Litigation Analytics Facet Item
    /// </summary>
    public class LitigationAnalyticsFacetItem : BaseItem
    {
        private readonly By SearchFacetCheckboxLocator = By.XPath(".//input[contains(@class, 'SearchFacet-inputCheckbox')]");
        private readonly By SearchFacetLabelTextLocator = By.XPath(".//span[@class = 'SearchFacet-labelText']");
        private readonly By SearchFacetOutputTextValueLocator = By.XPath(".//span[@class = 'SearchFacet-outputTextValue']");
        private readonly By SearchFacetOutputFederalStateTextValueLocator = By.XPath(".//ul[@class = 'SearchFacet-labelData']/li");

        /// <summary>
        ///  Litigation Analytics Facet Item
        /// </summary>
        /// <param name="container"></param>
        public LitigationAnalyticsFacetItem(IWebElement container) : base(container)
        {          
        }

        /// <summary>
        /// Search facet checkbox.
        /// </summary>
        public ICheckBox SearchFacetCheckbox => new CheckBox(this.Container, SearchFacetCheckboxLocator);

        /// <summary>
        /// Search facet label text. 
        /// </summary>
        public ILabel SearchFacetLabelText => new Label(DriverExtensions.WaitForElement(this.Container, SearchFacetLabelTextLocator));

        /// <summary>
        /// Search facet output text value. 
        /// </summary>
        public ILabel SearchFacetOutputTextValue => new Label(this.Container, SearchFacetOutputTextValueLocator);

        /// <summary>
        /// Search facet output federal text value. 
        /// </summary>
        public IReadOnlyCollection<ILabel> SearchFacetOutputStateFederalTextValue => new ElementsCollection<Label>(Container, SearchFacetOutputFederalStateTextValueLocator);

        ///<summary>
        ///Gets facet label text
        /// </summary>
        public string SelectedFiltertext => DriverExtensions.GetElement(SearchFacetOutputTextValueLocator).GetCssValue("aria-label");

        /// <summary>
        /// Is current item displayed.
        /// </summary>
        public bool IsCurrentItemDisplayed() =>
            DriverExtensions.IsDisplayed(this.Container);
    }
}