namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// AI JurisdictionalSurvey result item
    /// </summary>
    public class AiJurisdictionalurveysResultItem : BaseItem
    {
        private static readonly By JurisdictionNameLocator = By.XPath(".//h4");
        private static readonly By JurisdictionSummaryLocator = By.XPath(".//p");

        /// <summary>
        /// Initializes a new instance of the <see cref="AiJurisdictionalurveysResultItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container locator.
        /// </param>
        public AiJurisdictionalurveysResultItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Jurisdiction Name label
        /// </summary>
        public ILabel JurisdictionNameLabel => new Label(this.Container, JurisdictionNameLocator);

        /// <summary>
        /// Jurisdiction Summary label
        /// </summary>
        public ILabel JurisdictionSummaryLabel => new Label(this.Container, JurisdictionSummaryLocator);

        /// <summary>
        /// Jurisdiction Responce text label
        /// </summary>
        public ILabel JurisdictionResponceLabel => new Label(this.Container);
    }
}
