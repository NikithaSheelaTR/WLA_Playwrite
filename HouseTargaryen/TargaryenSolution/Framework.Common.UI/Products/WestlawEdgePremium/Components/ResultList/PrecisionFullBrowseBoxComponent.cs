namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Precision full browse box component
    /// </summary>
    public class PrecisionFullBrowseBoxComponent : PrecisionBaseBlueBoxComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[contains(@class,'Athens-browseBox')]");
        private static readonly By MaterialFactsLabelLocator = By.XPath(".//*[contains(@class, 'Athens-browseBox-materialFacts-list')]//li");
        private static readonly By MaterialFactsAllLabelsLocator = By.XPath(".//*[contains(@class, 'Athens-browseBox-materialFacts-list')]");        
        private static readonly By SearchTermLocator = By.XPath(".//span[contains(@class,'co_search_term')]");
        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionFullBrowseBoxComponent"/> class.
        /// </summary>
        /// <param name="containerElement"></param>
        public PrecisionFullBrowseBoxComponent(IWebElement containerElement) : base(containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// List of material facts
        /// </summary>
        public IReadOnlyCollection<ILabel> MaterialFactLabels => new ElementsCollection<Label>(this.ContainerElement, ComponentLocator, MaterialFactsLabelLocator);

        /// <summary>
        /// Get all search words within material facts
        /// </summary>
        /// <returns>IEnum of search words within material facts</returns>
        public IEnumerable<string> GetAllSearchWordsWithinMaterialFacts() =>     
            DriverExtensions.GetElements(this.ContainerElement, this.ComponentLocator, MaterialFactsAllLabelsLocator, SearchTermLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
