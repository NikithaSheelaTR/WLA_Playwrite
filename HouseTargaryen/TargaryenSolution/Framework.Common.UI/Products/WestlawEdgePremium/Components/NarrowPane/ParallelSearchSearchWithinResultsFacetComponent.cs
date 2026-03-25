namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Parallel Search Within Result Facet
    /// </summary>
    public class ParallelSearchSearchWithinResultsFacetComponent : NewEdgeRecentFiltersFacetComponent
    {
        private static readonly By SearchWithinComponentLocator = By.XPath("//div[contains(@class, '__searchWithinContainer')]");
        private static readonly By SearchWithinResultsLocator = By.Id("search-within-input");
        private static readonly By SearchWithinErrorLocator = By.XPath("//div[@id='search-within-error-message']//span");
        private const string SearchWithinResultsLabelScript = "return(arguments[0].shadowRoot.querySelector('label[id=label-search-within-input]'));";
        private const string SearchWithinInputScript = "return(arguments[0].shadowRoot.querySelector('input[id=search-within-input]'));";
        private const string SearchButtonScript = "return(arguments[0].shadowRoot.querySelector('saf-button[class=search-button]'));";
        private const string ClearButtonScript = "return(arguments[0].shadowRoot.querySelector('saf-button[id=clear-button]'));";
        
        ///<Summary>
        ///Search within error
        ///</Summary>
        public ILabel SearchWithinError => new Label(this.ComponentLocator, SearchWithinErrorLocator);

        ///<Summary>
        ///Component locator
        ///</Summary>
        protected override By ComponentLocator => SearchWithinComponentLocator;

        /// <summary>
        /// Search within results label
        /// </summary> 
        /// <result>
        /// Get search within results label
        /// </result>
        public string GetSearchWithinResultsLabel()
        {
            IWebElement searchWithinResultsElement = DriverExtensions.GetElement(this.ComponentLocator, SearchWithinResultsLocator);
            IWebElement searchWithinLabel = (IWebElement)DriverExtensions.ExecuteScript(SearchWithinResultsLabelScript, searchWithinResultsElement);
            return searchWithinLabel.Text;
        }

        /// <summary>
        /// Search within results input
        /// </summary>  
        /// <param name="searchTerm">        
        /// pass the search term as parameter
        /// </param>
        /// <param name="isClear">
        /// pass the clear input as parameter
        /// </param>
        public void EnterSearchWithinResultTerm(string searchTerm, bool isClear=false)
        {
            IWebElement searchWithinResultsElement = DriverExtensions.GetElement(this.ComponentLocator, SearchWithinResultsLocator);
            
            if (isClear) {
                IWebElement clearButton = (IWebElement)DriverExtensions.ExecuteScript(ClearButtonScript, searchWithinResultsElement);
                clearButton.Click();
            }

            IWebElement searchWithinInput = (IWebElement)DriverExtensions.ExecuteScript(SearchWithinInputScript, searchWithinResultsElement);
            searchWithinInput.SendKeys(searchTerm);
        }

        /// <summary>
        /// Click on search button
        /// </summary>     
        public void ClickSearchButton()
        {
            IWebElement searchWithinResultsElement = DriverExtensions.GetElement(this.ComponentLocator, SearchWithinResultsLocator);
            IWebElement searchButton = (IWebElement)DriverExtensions.ExecuteScript(SearchButtonScript, searchWithinResultsElement);
            searchButton.Click();
        }
    }
}