namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements;

    /// <summary>
    /// AI Deep Research left column component
    /// </summary>
    public class AiDeepResearchLeftColumnComponent : BaseModuleRegressionComponent
    {
        private static readonly By LeftColumnContainerLocator = By.XPath("//div[@data-testid='left-column']");
        private static readonly By ResearchStepsHeadingButtonLocator = By.XPath(".//div[@data-testid='message-disclosure-heading' and text()='Research steps']");
        private static readonly By ResearchStepsExpandedLabelLocator = By.XPath(".//saf-disclosure-v3[@data-testid='message-disclosure' and @expanded][div[@data-testid='message-disclosure-heading' and text()='Research steps']]");
        private static readonly By ResearchStepsListLocator = By.XPath(".//ul[contains(@class,'ResearchPlanDisclosure-module__researchPlan')]/li");
        private static readonly By ResearchSectionLinksLocator = By.XPath(".//saf-disclosure-v3[contains(@class,'MessageDisclosure-module__messageDisclosure')]");
        private static readonly By ResearchContentsHeadingButtonLocator = By.XPath(".//saf-disclosure-v3[@data-testid='message-disclosure'][div[@data-testid='message-disclosure-heading' and text()='Research contents']]");
        private static readonly By ResearchContentsExpandedLabelLocator = By.XPath(".//saf-disclosure-v3[@data-testid='message-disclosure' and @summary='Research contents' and @expanded]");
        private static readonly By ResearchContentsListLocator = By.XPath(".//saf-list-v3[contains(@class,'TableOfContent-module__tableOfContentList')]/saf-list-item-v3/a/span");
        private const string TocTitleLinkLctMask = ".//saf-list-v3[contains(@class,'TableOfContent-module__tableOfContentList')]//span[text()='{0}']";
        private static readonly By QueryLabelLocator = By.XPath(".//div[contains(@class, 'messageContent')]");
        private static readonly By ReportTimeLabelLocator = By.XPath(".//saf-message-box-v3[@data-testid='message-1-Thinking']//saf-metadata-item-v3[@data-testid= 'timestamp-item']/time");
        private static readonly By FollowUpQuestionTextareaLocator = By.XPath(".//saf-text-area-v3[@data-testid='input-textarea']");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('textarea[part=control]'));";
        private static readonly By FollowUpAnswerLabelLocator = By.XPath(".//div[contains(@class, 'answerMessage')]");
        private static readonly By AlertMessageLabelLocator = By.XPath(".//saf-alert-v3[@data-testid='message-alert']");
        private static readonly By ReportTypeLabelLocator = By.XPath(".//saf-metadata-item-v3[@data-testid= 'metadata-item-0']");
        private static readonly By ProgressBarLabelLocator = By.XPath("//saf-progress-v3[@data-testid='thinking-progress-bar']");
        private static readonly By JurisdictionResolutionMessageLocator = By.XPath("//saf-alert-v3[@data-testid='information-alert']");
        private static readonly By SelectedJurisdictionLabelLocator = By.XPath(".//saf-metadata-item-v3[@data-testid='metadata-item-1']");
        private static readonly By EnhanceMessageContentLabelLocator = By.XPath("//saf-message-box-v3[@data-testid='message-2-Question']//div[contains(@class, 'Message-module__messageContent')]/div");
        private static readonly By V2ReportButtonLocator = By.XPath("//saf-message-box-v3[@data-testid='message-3-Thinking']//div[@aria-label= 'Open report']");
        private static readonly By V2ReportResearchContentsLabelLocator = By.XPath("//saf-message-box-v3[@data-testid='message-3-Thinking']//saf-disclosure-v3[1]");
        private static readonly By V2ReportResearchStepsLabelLocator = By.XPath("//saf-message-box-v3[@data-testid='message-3-Thinking']//saf-disclosure-v3[2]");

        /// <summary>
        /// Research Steps heading button - click to expand/collapse the section
        /// </summary>
        public IButton ResearchStepsHeadingButton => new Button(ResearchStepsHeadingButtonLocator);

        /// <summary>
        /// Progress bar label
        /// </summary>
        public ILabel ProgressBarLabel => new Label(ProgressBarLabelLocator);

        /// <summary>
        /// Selected Jurisdiction Label
        /// </summary>
        public ILabel SelectedJurisdictionLabel => new Label(SelectedJurisdictionLabelLocator);

        /// <summary>
        /// List Research Section links
        /// </summary>
        public IReadOnlyCollection<ILink> ResearchSectionLinks => new ElementsCollection<Link>(this.ComponentLocator, ResearchSectionLinksLocator);

        /// <summary>
        /// Research Steps Expanded Label
        /// </summary>
        public ILabel ResearchStepsExpandedLabel => new Label(ResearchStepsExpandedLabelLocator);

        /// <summary>
        /// Research steps list
        /// </summary>
        public IList<IWebElement> ResearchStepsList => (IList<IWebElement>)DriverExtensions.GetElements(ResearchStepsListLocator);

        /// <summary>
        /// Research Contents heading button - click to expand/collapse the section
        /// </summary>
        public IButton ResearchContentsHeadingButton => new Button(ResearchContentsHeadingButtonLocator);

        /// <summary>
        /// Research Contents expanded Label
        /// </summary>
        public ILabel ResearchContentsExpandedLabel => new Label(ResearchContentsExpandedLabelLocator);

        /// <summary>
        /// Research Contents section: list of TOC title links
        /// </summary>
        public IList<IWebElement> ResearchContentsList => (IList<IWebElement>)DriverExtensions.GetElements(ResearchContentsListLocator);

        /// <summary>
        /// TOC title at index
        /// </summary>
        /// <param name="title">position of TOC title</param>
        public IReadOnlyCollection<ILink> TocTitleLink(string title) => new ElementsCollection<Link>(ComponentLocator, By.XPath(string.Format(TocTitleLinkLctMask, title)));

        /// <summary>
        /// Query Label
        /// </summary>
        public ILabel QueryLabel => new Label(ComponentLocator, QueryLabelLocator);

        /// <summary>
        /// Report Time Label
        /// </summary>
        public ILabel ReportTimeLabel => new Label(ComponentLocator, ReportTimeLabelLocator);

        /// <summary>
        /// Follow up Answer Label
        /// </summary>
        public ILabel FollowUpAnswerLabel => new Label(ComponentLocator, FollowUpAnswerLabelLocator);

        /// <summary>
        /// Alert Message Label
        /// </summary>
        public ILabel AlertMessageLabel => new Label(AlertMessageLabelLocator);

        /// <summary>
        /// Report Type Label
        /// </summary>
        public ILabel ReportTypeLabel => new Label(ReportTypeLabelLocator);

        /// <summary>
        /// Jurisdiction Resolution Message label
        /// </summary>
        public ILabel JurisdictionResolutionMessageLabel => new Label(JurisdictionResolutionMessageLocator);

        /// <summary>
        /// Enhance Message Content Label
        /// </summary>
        public ILabel EnhanceMessageContentLabel => new Label(EnhanceMessageContentLabelLocator);

        /// <summary>
        /// V2 Report Button
        /// </summary>
        public IButton V2ReportButton => new Button(V2ReportButtonLocator);

        /// <summary>
        /// V2 Report Research contents Label
        /// </summary>
        public ILink V2ReportResearchContentsLabel => new Link(V2ReportResearchContentsLabelLocator);

        /// <summary>
        /// V2 Report Research Steps Label
        /// </summary>
        public ILink V2ReportResearchStepsLabel => new Link(V2ReportResearchStepsLabelLocator);

        /// <summary>
        /// Enter question in the follow up TextArea
        /// </summary>
        /// <param name="followUpQuestion">followup question</param>
        public void EnterFollowUpQuestion(string followUpQuestion)
        {
            IWebElement followupQuestionTextArea = DriverExtensions.GetElement(ComponentLocator, FollowUpQuestionTextareaLocator);
            IWebElement inputArea = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, followupQuestionTextArea);
            inputArea.SendKeys(followUpQuestion);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => LeftColumnContainerLocator;
    }
}


