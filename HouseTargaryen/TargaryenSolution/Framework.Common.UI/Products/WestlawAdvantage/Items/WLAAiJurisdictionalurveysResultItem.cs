namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// AI JurisdictionalSurvey result item
    /// </summary>
    public class WLAAiJurisdictionalSurveysResultItem : AiJurisdictionalurveysResultItem
    {
        private static readonly By ResultsJurisdictionCasesLabelLocator = By.XPath(".//h5");
        private static readonly By ResultsSummaryStatutesAndRegulationsLabelLocator = By.XPath("//*[contains(@class, '__resultsSummarySubHeading')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="AiJurisdictionalurveysResultItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public WLAAiJurisdictionalSurveysResultItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// AJS results cases label
        /// </summary>
        public ILabel ResultsJurisdictionCasesLabel => new Label(this.Container, ResultsJurisdictionCasesLabelLocator);

        /// <summary>
        /// AJS results statutes and regulations label
        /// </summary>
        public ILabel ResultsSummaryStatutesAndRegulationsLabel => new Label(this.Container, ResultsSummaryStatutesAndRegulationsLabelLocator);

    }
}