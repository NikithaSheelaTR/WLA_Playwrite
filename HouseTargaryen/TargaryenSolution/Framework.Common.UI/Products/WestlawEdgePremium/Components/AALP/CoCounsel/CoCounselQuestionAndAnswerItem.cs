namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements;
    using OpenQA.Selenium.Support.PageObjects;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// CoCounsel Question and Answer item
    /// </summary>
    public class CoCounselQuestionAndAnswerItem : BaseItem
    {
        private static readonly By QuestionLabelLocator = By.XPath("./preceding-sibling::saf-text");
        private static readonly By AnswerLabelLocator = By.XPath("//div/saf-text[@appearance='body-default-md']");
        private static readonly By DeliveryDropdownContainerLocator = By.XPath("./ancestor::*[@class='delphi-skill-response--summary']");
        private static readonly By LoadingAnswerLabelLocator = By.XPath(".//div[@aria-busy='true']");
        private static readonly By InlineTitlesLinkLocator = By.XPath(".//saf-anchor[contains(@href, 'Document')]");
        private static readonly By JumpLinkLocator = By.XPath(".//a[contains(@aria-label, 'Item No.')]");
        private static readonly By InlineTitlesKeyCiteFlagLocator = By.XPath(".//saf-anchor[contains(@href, 'RelatedInformation')]");
        private static readonly By ViewAllFootnotesButtonLocator = By.XPath(".//*[contains(@data-testid, 'view-footnotes-canvas')]");
        private static readonly By FootnoteLinkLocator = By.XPath(".//button[contains(@id,'flowId')]");
        private static readonly By AnswerWithinDocumentLabelLocator = By.XPath(".//div[contains(@id,'markdown-response-container')]");
        
        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="UserQuestionItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public CoCounselQuestionAndAnswerItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Tray dropdown.
        /// </summary>
        public CoCounselDeliveryDropdown CoCounselDeliveryDropdown => new CoCounselDeliveryDropdown(new ByChained(AnswerLabelLocator, DeliveryDropdownContainerLocator));

        /// <summary>
        /// View all footnotes button
        /// </summary>
        public IButton ViewAllFootnotesButton => new Button(this.Container, new ByChained(AnswerWithinDocumentLabelLocator, ViewAllFootnotesButtonLocator));

        /// <summary>
        /// Question label
        /// </summary>
        public ILabel QuestionLabel => new Label(this.Container, AnswerLabelLocator, QuestionLabelLocator);

        /// <summary>
        /// Answer label
        /// </summary>
        public ILabel AnswerLabel => new Label(this.Container, AnswerLabelLocator);

        /// <summary>
        /// Loading answer label
        /// </summary>
        public ILabel LoadingAnswerLabel => new Label(this.Container, LoadingAnswerLabelLocator);

        /// <summary>
        /// Inline titles links
        /// </summary>
        public IReadOnlyCollection<ILink> InlineTitlesLinks => new ElementsCollection<Link>(this.Container, new ByChained(AnswerLabelLocator, InlineTitlesLinkLocator));

        /// <summary>
        /// Jump links
        /// </summary>
        public IReadOnlyCollection<ILink> JumpLinks => new ElementsCollection<Link>(this.Container, new ByChained(AnswerLabelLocator, JumpLinkLocator));

        /// <summary>
        /// Inline titles key cite flags links
        /// </summary>
        public IReadOnlyCollection<ILink> InlineTitlesKeyCiteFlagsLinks => new ElementsCollection<Link>(this.Container, new ByChained(AnswerLabelLocator, InlineTitlesKeyCiteFlagLocator));

        /// <summary>
        /// Footnote links
        /// </summary>
        public IReadOnlyCollection<ILink> FootnoteLinks => new ElementsCollection<Link>(this.Container, new ByChained(AnswerWithinDocumentLabelLocator, FootnoteLinkLocator));
    }
}
