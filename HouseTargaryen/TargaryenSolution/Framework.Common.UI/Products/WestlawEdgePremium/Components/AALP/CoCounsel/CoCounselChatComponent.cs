namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounsel Chat component
    /// </summary>
    public class CoCounselChatComponent : BaseModuleRegressionComponent
    {
        private static readonly By ChatContainerLocator = By.XPath("//*[@class='chat-scrollable-container' or contains(@class, 'landingPageContainer')] | //*[contains(@class, 'logoWrapper')]/following-sibling::div");
        private static readonly By QuestionTextboxLocator = By.XPath(".//*[@id='cocounsel-prompt' or @data-testid='query-textarea']");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//*[@id='jurisdictionsButton']");
        private static readonly By SubmitButtonLocator = By.XPath(".//*[@id='send' or @data-testid='submit-button']");
        private static readonly By UploadDocumentButtonLocator = By.XPath(".//*[@data-testid='file-upload-button' and @appearance='secondary']");
        private static readonly By AiAssistedResearchRadiobuttonLocator = By.XPath(".//*[@id='ai_assisted_legal_research']//span");
        private static readonly By StartAiAssistedResearchButtonLocator = By.XPath(".//*[@type='submit']");
        private static readonly By AnswerItemLocator = By.XPath(".//*[@appearance='agent' and @data-flow-id]");
        private static readonly By QuickCheckAnswerItemLocator = By.XPath(".//*[contains(@class, 'ResultsList-module__quoteListItem')]");
        private static readonly By CoCounselQuickCheckFileChipItemLocator = By.XPath(".//span[contains(@class, 'uploadChip_')]");
        private static readonly By ViewAllCitedSourcesButtonLocator = By.XPath(".//*[contains(@data-testid, 'view-footnotes-ai_assisted_legal_research')]");
        private static readonly By LearnAboutDataRetenionLinkLocator = By.XPath(".//button[@id='coid_security_message']");
        private static readonly By DateTimeLocator = By.XPath(".//saf-metadata[@class='message-box-metadata']//time");
        private static readonly By QuickCheckProgressSpinnerLabelLocator = By.XPath(".//saf-progress-ring[@data-testid='conversation-progress']");
        private static readonly By QuickCheckProgressLinesLabelLocator = By.XPath(".//*[contains(@class, 'EstimatedProgressSkeleton')]");
        private static readonly By ProgressSpinnerLabelLocator = By.XPath("//div[@data-testid='progress-info']");
        private static readonly By QuickCheckFileErrorStatusLabelLocator = By.XPath(".//saf-status[@role='status']");
        private static readonly By AiAssistedResearchSkillLocator = By.XPath(".//button[contains(@data-testid,'rec-action-card-ai-assisted research')]");
        private static readonly By DisclaimerInformationLocator = By.XPath(".//p[@data-testid='claims-results-disclaimer']");
        private static readonly By ResearchActionButtonLocator = By.XPath(".//div[@data-testid='action-research']");
        private static readonly By SelectedJurisdictionLocator = By.XPath(".//span[@data-testid='selected-jurisdictions']");
        private static readonly By QuickCheckReportLinkLocator = By.XPath("//saf-icon[contains(@class,'FullReportLink-module')]");
        private static readonly By QuickCheckUploadErrorLabelLocator = By.XPath("//saf-alert[@data-testid='file-upload-alert']");
        private static readonly By ComplaintAnalysisProgressLinesLabelLocator = By.XPath(".//*[contains(@class, 'Skeleton-module__progressBody')]");
        private static readonly By AiDeepResearchRadioButtonLocator = By.XPath(".//*[@id='deep_research']//span");
        private static readonly By StartAiDeepResearchButtonLocator = By.XPath(".//*[@type='submit']");
        private static readonly By ViewAllDRCitedSourcesButtonLocator = By.XPath(".//*[contains(@data-testid, 'view-footnotes-deep_research')]");
        private static readonly By DRProgressBarLabelLocator = By.XPath("//div[@data-testid='skill-progress-bar']");
        private static readonly By CitationLinksLocator = By.XPath("//div[contains(@class, 'delphi-skill-result-summary-container')]//a[contains(@href,'Document')]");
        private static readonly By DRSourcesLabelLocator = By.XPath("//div[@id='supportingSourcesId']//h3[1][text()='Relevant resources and snippets']");
        private static readonly By SourcesFlagLinksLocator = By.XPath("//ol[@class = 'delphi-skill--ai-assisted-legal-research__extra-scroll-container']//a[contains(@href,'RelatedInformation')]");
        private static readonly By StartAnalysisButtonLocator = By.XPath("//*[contains(@class,'Landing-module__startAnalysis')]");
        private static readonly By ZeroStateMessageLabelLocator = By.XPath("//*[contains(text(),'No complaint details found')]");
        private static readonly By DownloadButtonLocator = By.XPath(".//saf-button[@data-testid='delivery-button']");
        private static readonly By ComplaintAnalysisProgressLabelLocator = By.XPath(".//*[contains(@saf, 'progress-ring')]");

        private const string UploadPathLctMask = "//saf-radio[@data-testid='{0}']";
        private const string UploadPathRadiobuttonLocator = "return(arguments[0].shadowRoot.querySelector('input[id=control]'));";

        private EnumPropertyMapper<QuickCheckOptions, WebElementInfo> quickCheckOptionsMap;

        /// <summary>
        /// Gets the QuickCheckOptions enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<QuickCheckOptions, WebElementInfo> QuickCheckOptionsMap
            => this.quickCheckOptionsMap = this.quickCheckOptionsMap ?? EnumPropertyModelCache.GetMap<QuickCheckOptions, WebElementInfo>(
                string.Empty, @"Resources/EnumPropertyMaps/WestlawEdgePremium/CoCounsel");

        /// <summary>
        /// Question textbox
        /// </summary>
        public ITextbox QuestionTextbox => new Textbox(this.ComponentLocator, QuestionTextboxLocator);

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionButtonLocator);

        /// <summary>
        /// Submit button
        /// </summary>
        public IButton SubmitButton => new Button(this.ComponentLocator, SubmitButtonLocator);

        /// <summary>
        /// Start AI-Assisted Research button
        /// </summary>
        public IButton StartAiAssistedResearchButton => new Button(this.ComponentLocator, StartAiAssistedResearchButtonLocator);

        /// <summary>
        /// Start Westlaw AI Deep Research button
        /// </summary>
        public IButton StartAiDeepResearchButton => new Button(this.ComponentLocator, StartAiDeepResearchButtonLocator);

        /// <summary>
        /// View all Deep Research cited sources button
        /// </summary>
        public IButton ViewAllDRCitedSourcesButton => new Button(this.ComponentLocator, ViewAllDRCitedSourcesButtonLocator);

        /// <summary>
        /// View all cited sources button
        /// </summary>
        public IButton ViewAllCitedSourcesButton => new Button(this.ComponentLocator, ViewAllCitedSourcesButtonLocator);

        /// <summary>
        /// AI Assisted Research Skill button
        /// </summary>
        public IButton AiAssistedResearchSkillButton => new Button(this.ComponentLocator, AiAssistedResearchSkillLocator);

        /// <summary>
        /// Research skills button
        /// </summary>
        public IButton ResearchActionButton => new Button(this.ComponentLocator, ResearchActionButtonLocator);

        /// <summary>
        /// Upload document button
        /// </summary>
        public IButton UploadDocumentButton => new Button(this.ComponentLocator, UploadDocumentButtonLocator);

        /// <summary>
        /// Start Analysis button
        /// </summary>
        public IButton StartAnalysisButton => new Button(this.ComponentLocator, StartAnalysisButtonLocator);

        /// <summary>
        /// Download button
        /// </summary>
        public IButton DownloadButton => new Button(this.ComponentLocator, DownloadButtonLocator);

        /// <summary>
        /// Learn about CoCounsel data retention link
        /// </summary>
        public ILink LearnAboutDataRetenionLink => new Link(this.ComponentLocator, LearnAboutDataRetenionLinkLocator);

        /// <summary>
        /// Progress label
        /// </summary>
        public ILabel ProgressLabel => new Label(this.ComponentLocator, ProgressSpinnerLabelLocator);

        /// <summary>
        /// Deep Research Progress bar label
        /// </summary>
        public ILabel DRProgressBarLabel => new Label(this.ComponentLocator, DRProgressBarLabelLocator);

        /// <summary>
        /// Deep Research sources label
        /// </summary>
        public ILabel DRSourcesLabel => new Label(this.ComponentLocator, DRSourcesLabelLocator);

        /// <summary>
        /// Progress spinner label (Quick Check)
        /// </summary>
        public ILabel QuickCheckProgressSpinnerLabel => new Label(this.ComponentLocator, QuickCheckProgressSpinnerLabelLocator);

        /// <summary>
        /// Progress spinner lines (Quick Check)
        /// </summary>
        public ILabel QuickCheckProgressLinesLabel => new Label(this.ComponentLocator, QuickCheckProgressLinesLabelLocator);

        /// <summary>
        /// Date Time label
        /// </summary>
        public ILabel DateTimeLabel => new Label(this.ComponentLocator, DateTimeLocator);

        /// <summary>
        /// Disclaimer Information label
        /// </summary>
        public ILabel DisclaimerInformationLabel => new Label(this.ComponentLocator, DisclaimerInformationLocator);

        /// <summary>
        /// Selected Jurisdiction label
        /// </summary>
        public ILabel SelectedJurisdictionLabel => new Label(this.ComponentLocator, SelectedJurisdictionLocator);

        /// <summary>
        /// Quick Check file status error label
        /// </summary>
        public ILabel QuickCheckFileErrorStatusLabel => new Label(this.ComponentLocator, QuickCheckFileErrorStatusLabelLocator);

        /// <summary>
        /// Error message on upload dialog (Quick Check)
        /// </summary>
        public ILabel QuickCheckUploadErrorLabel => new Label(this.ComponentLocator, QuickCheckUploadErrorLabelLocator);

        /// <summary>
        /// Progress spinner lines (Complaint Analysis)
        /// </summary>
        public ILabel ComplaintAnalysisProgressLabel => new Label(this.ComponentLocator, ComplaintAnalysisProgressLabelLocator);

        /// <summary>
        /// Zero state message label for complaint analyser
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(this.ComponentLocator, ZeroStateMessageLabelLocator);

        /// <summary>
        /// List of citation links
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLinks => new ElementsCollection<Link>(this.ComponentLocator, CitationLinksLocator);

        /// <summary>
        /// List of Sources Flag Links
        /// </summary>
        public IReadOnlyCollection<ILink> SourcesFlagLinks => new ElementsCollection<Link>(this.ComponentLocator, SourcesFlagLinksLocator);

        /// <summary>
        /// Quick Check report link
        /// </summary>
        public ILink QuickCheckReportLink => new Link(this.ComponentLocator, QuickCheckReportLinkLocator);

        /// <summary>
        /// All views Radiobutton
        /// </summary>
        public IRadiobutton AiAssistedResearchRadiobutton => new Radiobutton(DriverExtensions.GetElement(ChatContainerLocator), AiAssistedResearchRadiobuttonLocator);

        /// <summary>
        /// Westlaw AI Deep Research Radio button
        /// </summary>
        public IRadiobutton AiDeepResearchRadioButton => new Radiobutton(DriverExtensions.GetElement(ChatContainerLocator), AiDeepResearchRadioButtonLocator);

        /// <summary>
        /// CoCounsel Answer items list
        /// </summary>
        /// <returns>List of answers</returns>
        public ItemsCollection<CoCounselQuestionAndAnswerItem> CoCounselQuestionAndAnswerItems => new ItemsCollection<CoCounselQuestionAndAnswerItem>(this.ComponentLocator, AnswerItemLocator);

        /// <summary>
        /// CoCounsel Answer items list
        /// </summary>
        /// <returns>List of answers</returns>
        public ItemsCollection<CoCounselQuickCheckAnswerItem> CoCounselQuickCheckAnswerItems => new ItemsCollection<CoCounselQuickCheckAnswerItem>(this.ComponentLocator, QuickCheckAnswerItemLocator);

        /// <summary>
        /// CoCounsel Quick Check file chip items
        /// </summary>
        /// <returns>List of file chipps</returns>
        public ItemsCollection<CoCounselQuickCheckFileChipItem> CoCounselQuickCheckFileChipItems => new ItemsCollection<CoCounselQuickCheckFileChipItem>(this.ComponentLocator, CoCounselQuickCheckFileChipItemLocator);

        /// <summary>
        /// Claims Explorer Question and Answer items list
        /// </summary>
        /// <returns>List of user questions and answers</returns>
        public AiClaimsExplorerQuestionAndAnswerItem ClaimsExplorerQuestionAndAnswerItem => new AiClaimsExplorerQuestionAndAnswerItem(DriverExtensions.GetElement(this.ComponentLocator));

        /// <summary>
        /// CoCounsel Complaint Analysis Result Item items list
        /// </summary>
        /// <returns>List of results</returns>
        public CoCounselComplaintAnalysisResultItem CoCounselComplaintAnalysisResultItem => new CoCounselComplaintAnalysisResultItem(DriverExtensions.GetElement(this.ComponentLocator));

        /// <summary>
        /// Waits for Quick Check results to be fully rendered after the spinners disappear.
        /// Both the ring spinner and the skeleton lines must be gone, and at least one answer
        /// item must be present before the method returns.
        /// </summary>
        /// <param name="timeoutFromSec">Maximum seconds to wait for spinners to clear. Defaults to 300.</param>
        public void WaitForQuickCheckResultsLoaded(int timeoutFromSec = 300)
        {
            SafeMethodExecutor.WaitUntil(() => !this.QuickCheckProgressSpinnerLabel.Displayed, timeoutFromSec: timeoutFromSec);
            SafeMethodExecutor.WaitUntil(() => !this.QuickCheckProgressLinesLabel.Displayed, timeoutFromSec: 60);
            SafeMethodExecutor.WaitUntil(() => this.CoCounselQuickCheckAnswerItems.Any(), timeoutFromSec: 30);
        }

        /// <summary>
        /// Waits for AI-Assisted Research chat results to be fully rendered.
        /// The progress spinner must be gone and at least one answer item must be present.
        /// </summary>
        /// <param name="timeoutFromSec">Maximum seconds to wait for the spinner to clear. Defaults to 300.</param>
        public void WaitForAiResultsLoaded(int timeoutFromSec = 300)
        {
            SafeMethodExecutor.WaitUntil(() => !this.ProgressLabel.Displayed, timeoutFromSec: timeoutFromSec);
            SafeMethodExecutor.WaitUntil(() => this.CoCounselQuestionAndAnswerItems.Any(), timeoutFromSec: 30);
        }

        /// <summary>
        /// Select upload path
        /// </summary>
        public void SelectUploadPath(QuickCheckOptions quickCheckOption)
        {
            var uploadPathElement = DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(UploadPathLctMask, this.QuickCheckOptionsMap[quickCheckOption].Id)));
            var uploadPathRadiobutton = (IWebElement)DriverExtensions.ExecuteScript(UploadPathRadiobuttonLocator, uploadPathElement);
            uploadPathRadiobutton.Click();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ChatContainerLocator;
    }
}
