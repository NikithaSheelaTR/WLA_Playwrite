namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Query box component
    /// </summary>
    public class ParallelSearchQueryBoxComponent : BaseModuleRegressionComponent
    {
        private static readonly By QueryBoxContainerLocator = By.XPath("//div[contains(@class,'parallelSearchLandingSearch')]");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//button[@id='jurisdictionId_ParallelSearchJurisdictionSelector']");
        private static readonly By JurisdictionLabelLocator = By.XPath(".//span[@id='jurisdictionIdInner_ParallelSearchJurisdictionSelector']");
        private static readonly By SearchAreaLocator = By.XPath(".//saf-search-field");
        private const string SearchFieldScript = "return(arguments[0].shadowRoot.querySelector('input'));";
        private const string SearchButtonScript = "return(arguments[0].shadowRoot.querySelector('saf-button[id=search-button]'));";        
        private static readonly By SearchTipsButtonLocator = By.XPath(".//saf-button[@id='parallelSearchTipsButton']");
        private static readonly By ClearButtonLocator = By.Id("clear-button");

        /// <summary>
        /// Click search button to submit search query
        /// </summary>
        public void SubmitSearchQuery()
        {
            IWebElement searchAreaElement = DriverExtensions.GetElement(SearchAreaLocator);// Shadow host 
            IWebElement searchButton = (IWebElement)DriverExtensions.ExecuteScript(SearchButtonScript, searchAreaElement);
            searchButton.Click();
        }

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionButtonLocator);

        /// <summary>
        /// Search Tips button
        /// </summary>
        public IButton SearchTipsButton => new Button(this.ComponentLocator, SearchTipsButtonLocator);

        /// <summary>
        /// Enter query in search field
        /// </summary>
        /// /// <param name="query">query to run</param>
        public void EnterSearchQuery(string query)
        {
            IWebElement searchAreaElement = DriverExtensions.GetElement(SearchAreaLocator);// Shadow host 
            IWebElement searchField = (IWebElement)DriverExtensions.ExecuteScript(SearchFieldScript, searchAreaElement);// Shadow content
            searchField.SendKeys(query);
        }

        /// <summary>
        ///  Jurisdiction selected label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(this.ComponentLocator, JurisdictionLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => QueryBoxContainerLocator;

        /// <summary>
        /// Search Tip Component
        /// </summary>
        public ParallelSearchTipsComponent SearchTips { get; } = new ParallelSearchTipsComponent();

        /// <summary>
        /// Search query label
        /// </summary>
        public ILabel SearchQueryLabel => new Label(this.ComponentLocator, SearchAreaLocator);

        /// <summary>
        /// Clear button
        /// </summary>
        public IButton ClearButton => new Button(this.ComponentLocator, ClearButtonLocator);
    }
}
