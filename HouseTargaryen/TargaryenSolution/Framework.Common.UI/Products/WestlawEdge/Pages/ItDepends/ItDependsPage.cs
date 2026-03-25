namespace Framework.Common.UI.Products.WestlawEdge.Pages.ItDepends
{
    using Framework.Common.UI.Products.Shared.Pages;

    using OpenQA.Selenium;

    using Framework.Common.UI.Products.WestlawEdge.Components.ItDepends;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// It Depends Page
    /// </summary>
    public class ItDependsPage : BaseModuleRegressionPage
    {
        private static readonly By HeaderLocator = By.XPath("//h1[@class='ItDepends-heading']");

        /// <summary>
        /// Header label
        /// </summary>
        public ILabel HeaderLabel => new Label(HeaderLocator);

        /// <summary>
        /// Topic Section
        /// </summary>
        public ItDependsMultiFactorTestComponent ItDependsMultiFactorTestSection => new ItDependsMultiFactorTestComponent();

        /// <summary>
        /// Jurisdiction Section
        /// </summary>
        public ItDependsJurisdictionComponent ItDependsJurisdictionSection => new ItDependsJurisdictionComponent();

        /// <summary>
        /// ItDependsFactorModel Section
        /// </summary>
        public ItDependsFactorsComponent ItDependsFactorsSection => new ItDependsFactorsComponent();

        /// <summary>
        /// Outcome Section
        /// </summary>
        public ItDependsOutcomeComponent ItDependsOutcomeComponent => new ItDependsOutcomeComponent();  
    }
}