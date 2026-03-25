namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer.Toolbar;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer Skill Landing Page
    /// </summary>
    public class ComplaintAnalyzerSkillLandingPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='coid_website_aiSandboxSkillsContainer'] | //*[@id='coid_website_complaintAnalyzerSkillLandingPage']");
        private static readonly By ProgressBarLabelLocator =By.XPath(".//div[@class='Progress-module__heading__Axeaz']");
        private static readonly By AnalyzeNewComplaintButtonLocator = By.XPath(".//saf-button-v3[@data-testid='analyze-new-complaint-button'] | .//saf-button[@data-testid='analyze-new-complaint-button']");
        private static readonly By ProgressBarLabelTextLocator = By.XPath("//saf-progress-v3[contains(@label,'Analyzing complaint')]");

        /// <summary>
        /// Get the Complaint Analyzer Result Tab Panel 
        /// </summary>
        public ComplaintAnalyzerResultTabPanel ComplaintAnalyzerResultTabPanel => new ComplaintAnalyzerResultTabPanel(DriverExtensions.GetElement(ContainerLocator));

        /// <summary>
        /// Toolbar
        /// </summary>
        public ComplaintAnalyzerToolbar Toolbar { get; } = new ComplaintAnalyzerToolbar();  

        /// <summary>
        /// Progress Bar label
        /// </summary>
        public ILabel ProgressBarLabel => new Label(ContainerLocator, ProgressBarLabelLocator);

        /// <summary>
        /// Progress Bar label text
        /// </summary>
        public ILabel ProgressBarLabelText => new Label(ContainerLocator, ProgressBarLabelTextLocator);

        /// <summary>
        /// Analyze New Complaint button
        /// </summary>
        public IButton AnalyzeNewComplaintButton => new Button(ContainerLocator, AnalyzeNewComplaintButtonLocator);

        /// <summary>
        /// Complaint Analyzer Delivery Dialog
        /// </summary>
        public ComplaintAnalyzerDeliveryDialog ComplaintAnalyzerDeliveryDialog => new ComplaintAnalyzerDeliveryDialog();
    }
}
