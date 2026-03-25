namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Jurisdictional Surveys Status component
    /// </summary>
    public class AiJurisdictionalSurveysStatusComponent : BaseModuleRegressionComponent
    {
        private static readonly By StatusContainerLocator = By.XPath("//div[@id='coid_lightboxOverlay']");
        private static readonly By QuestionLinkLocator = By.XPath(".//a[contains(@class,'surveyStatusQuestion')]");
        private static readonly By JurisdictionLabelLocator = By.XPath(".//li[contains(@class,'surveyStatusSection')]/ul/li[3]");

        /// <summary>
        /// Question Link
        /// </summary>
        public ILink QuestionLink => new Link(this.ComponentLocator, QuestionLinkLocator);

        /// <summary>
        /// Jurisdiction Label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(this.ComponentLocator, JurisdictionLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => StatusContainerLocator;
    }
}
