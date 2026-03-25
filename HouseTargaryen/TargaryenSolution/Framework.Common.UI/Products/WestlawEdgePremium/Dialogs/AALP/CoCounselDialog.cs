namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounsel dialog
    /// </summary>
    public class CoCounselDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_aiAssistantContainer']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2");
        private static readonly By CloseButonLocator = By.XPath(".//button[@class='co_overlayBox_closeButton']");
        private static readonly By CoCounselOpenButtonLocator = By.XPath(".//*[@id='coCounselOpenButton']");
        private static readonly By AiAssistedResearchLinkLocator = By.XPath(".//a[contains(@aria-label, 'AI-Assisted Research')]"); 
        private static readonly By AuAiAssistedResearchLinkLocator = By.XPath(".//a[contains(@aria-label, 'AI-Assisted Research opens in new tab')]");
        private static readonly By AiJurisdictionalSurveysLinklLocator = By.XPath(".//*[contains(@aria-label, 'AI Jurisdictional Surveys')]//*[not(contains(@class, 'launch'))]");
        private static readonly By ClaimsExplorerLinkLocator = By.XPath(".//a[contains(@aria-label, 'Claims Explorer')]");
        private static readonly By QuickCheckLinkLocator = By.XPath(".//a[contains(@aria-label, 'Quick Check')]");

        /// <summary>
        /// AI Jurisdictional Surveys Link
        /// </summary>
        public ILink AiJurisdictionalSurveysLink => new Link(ContainerLocator, AiJurisdictionalSurveysLinklLocator);

        /// <summary>
        /// AI-Assisted Research link
        /// </summary>
        public ILink AiAssistedResearchLink => new Link(ContainerLocator, AiAssistedResearchLinkLocator);

        /// <summary>
        /// Claims Explorer link
        /// </summary>
        public ILink ClaimsExplorerLink => new Link(ContainerLocator, ClaimsExplorerLinkLocator);

        /// <summary>
        /// Quick Check link
        /// </summary>
        public ILink QuickCheckLink => new Link(ContainerLocator, QuickCheckLinkLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButonLocator);

        /// <summary>
        /// 'CoCounsel' open button
        /// </summary>
        public IButton CoCounselOpenButton => new Button(ContainerLocator, CoCounselOpenButtonLocator);

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// ANZ AI-Assisted Research link
        /// </summary>
        public ILink AuAiAssistedResearchLink => new Link(ContainerLocator, AuAiAssistedResearchLinkLocator);
    }
}
