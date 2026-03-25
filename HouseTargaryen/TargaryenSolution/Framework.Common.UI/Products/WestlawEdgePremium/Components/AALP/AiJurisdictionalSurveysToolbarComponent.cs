namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Jurisdictional Surveys toolbar component
    /// </summary>
    public class AiJurisdictionalSurveysToolbarComponent : Toolbar
    {
        private static readonly By ToolbarContainerLocator = By.XPath("//div[@id='fiftyStatesToolbar']");
        private static readonly By NewSurveyButtonLocator = By.XPath(".//saf-button[contains(text(),'New Survey')]");
        private static readonly By SurveyStatusButtonLocator = By.XPath(".//button/*[contains(text(),'Survey Status')]");
        private static readonly By CopyLinkButtonLocator = By.XPath(".//button[contains(@class, 'linkBuilder-button icon_link co_tbButton')]");

        /// <summary>
        /// Survey Status Component
        /// </summary>
        public AiJurisdictionalSurveysStatusComponent SurveyStatus { get; } = new AiJurisdictionalSurveysStatusComponent();

        /// <summary>
        /// Copy Link Button
        /// </summary>
        public IButton ResearchCopyLinkButton => new Button(this.ComponentLocator, CopyLinkButtonLocator);

        /// <summary>
        /// New survey button
        /// </summary>
        public IButton NewSurveyButton => new Button(this.ComponentLocator, NewSurveyButtonLocator);

        /// <summary>
        /// Survey status button
        /// </summary>
        public IButton SurveyStatusButton => new Button(this.ComponentLocator, SurveyStatusButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;
    }
}



