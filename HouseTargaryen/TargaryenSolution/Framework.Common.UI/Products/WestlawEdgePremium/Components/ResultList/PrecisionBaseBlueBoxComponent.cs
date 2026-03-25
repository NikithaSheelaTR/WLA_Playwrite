namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Precision base blue box conponent
    /// </summary>
    public abstract class PrecisionBaseBlueBoxComponent
    {
        private const string MotionTypeByGuidLocator = ".//div[@data-docguid='{0}']//span[text()='Motion Type']/following-sibling::span/span";
        private const string MotionTypeHoverByGuidLocator = ".//div[@data-docguid='{0}']//span[text()='Motion Type']/ancestor::div[@class='Athens-browseBox-secondaryContent']/div";
        private const string CauseOfActionByGuidLocator = ".//div[@data-docguid='{0}']//span[text()='Causes of Action']/following-sibling::span/span";
        private const string CauseOfActionHoverByGuidLocator = ".//div[@data-docguid='{0}']//span[text()='Causes of Action']/ancestor::div[@class='Athens-browseBox-secondaryContent']/div";

        private static readonly By CauseOfActionLabelLocator = By.XPath(".//div//span[text()='Causes of Action']/following-sibling::span/span");
        private static readonly By BestHeadnoteTextLocator = By.XPath(".//div[(@class='Athens-bestHeadnote-content')]//p");
        private static readonly By BestHeadnoteNumberLocator = By.XPath(".//*[(@class='Athens-browseBox-headingLinkWrapper') or (@class='Athens-bestHeadnote-contentHeading')]//a");
        private static readonly By HeadingLabelLocator = By.XPath(".//*[@class='Athens-browseBox-question']");
        private static readonly By LegalIssueLabelLocator = By.XPath(".//div[@class='Athens-narrow-legal-issue']");
        private static readonly By MoreCasesOnThisIssueLinkLocator = By.XPath(".//a[@class='Athens-browseBox-question-link']");
        private static readonly By MotionTypeLabelLocator = By.XPath(".//span[text()='Motion Type']/following-sibling::span/span/span");
        private static readonly By SearchTermLocator = By.XPath(".//span[contains(@class,'co_searchTerm')]");    
        private static readonly By SearchWithinHighlightedTermLocator = By.XPath(".//span[contains(@class,'co_keyword')]");

        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionBaseBlueBoxComponent"/> class.
        /// </summary>
        /// <param name="containerElement">Container element</param>
        public PrecisionBaseBlueBoxComponent(IWebElement containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Heading label (legal issue + outcome)
        /// </summary>
        public ILabel HeadingLabel => new Label(this.ContainerElement, this.ComponentLocator, HeadingLabelLocator);

        /// <summary>
        /// Best headnote label
        /// </summary>
        public ILabel BestHeadnoteLabel => new Label(this.ContainerElement, this.ComponentLocator, BestHeadnoteTextLocator);

        /// <summary>
        /// Best headnote number
        /// </summary>
        public ILabel BestHeadnoteNumberLabel => new Label(this.ContainerElement, this.ComponentLocator, BestHeadnoteNumberLocator);

        /// <summary>
        /// Legal issue
        /// </summary>
        public ILabel LegalIssueLabel => new Label(this.ContainerElement, this.ComponentLocator, LegalIssueLabelLocator);

        /// <summary>
        /// Causes of action labels
        /// </summary>
        public IReadOnlyCollection<ILabel> CausesOfActionLabels => new ElementsCollection<Label>(this.ContainerElement, this.ComponentLocator, CauseOfActionLabelLocator);

        /// <summary>
        /// Motion type labels
        /// </summary>
        public IReadOnlyCollection<ILabel> MotionTypeLabels => new ElementsCollection<Label>(this.ContainerElement, this.ComponentLocator, MotionTypeLabelLocator);

        /// <summary>
        /// List of search within highlighted words in a headnote
        /// </summary>
        public IReadOnlyCollection<ILabel> SearchWithinHighlightedWordsListLabels => new ElementsCollection<Label>(this.ContainerElement, SearchWithinHighlightedTermLocator);

        /// <summary>
        /// Best headnote link
        /// </summary>
        public ILink BestHeadnoteLink => new Link(this.ContainerElement, this.ComponentLocator, BestHeadnoteNumberLocator);

        /// <summary>
        /// More cases on this issue link
        /// </summary>
        public ILink MoreCasesOnThisIssueLink => new Link(this.ContainerElement, this.ComponentLocator, MoreCasesOnThisIssueLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected abstract By ComponentLocator { get; }

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                String.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        /// <summary>
        /// The headnote element
        /// </summary>
        private IWebElement HeadnoteElement => DriverExtensions.SafeGetElement(this.ContainerElement, new ByChained(this.ComponentLocator, BestHeadnoteTextLocator));

        /// <summary>
        /// Return search words in headnote
        /// </summary>
        /// <returns>IEnum of search words in headnote</returns>
        public IEnumerable<string> GetAllSearchWords() =>
            this.HeadnoteElement != null ?
            DriverExtensions.GetElements(this.HeadnoteElement, SearchTermLocator).Select(e => e.Text).ToList() : new List<string>();

        /// <summary>
        /// Is blue box displayed
        /// </summary>
        /// <returns>True - if blue box is displayed, false - otherwise</returns>
        public bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ContainerElement, this.ComponentLocator);

        /// <summary>
        /// Get hover text for Motion Type section
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// Return hover text for motion type 
        /// </returns>
        public string GetMotionTypeHoverText(string guid)
        {
            DriverExtensions.Hover(By.XPath(string.Format(MotionTypeByGuidLocator, guid)));
            return DriverExtensions.GetElement(By.XPath(string.Format(MotionTypeHoverByGuidLocator, guid))).Text;
        }

        /// <summary>
        /// Get hover text for Cause of Action section
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// Return hover text for cause of action
        /// </returns>
        public string GetCauseOfActionHoverText(string guid)
        {
            DriverExtensions.Hover(By.XPath(string.Format(CauseOfActionByGuidLocator, guid)));
            return DriverExtensions.GetElement(By.XPath(string.Format(CauseOfActionHoverByGuidLocator, guid))).Text;
        }

        /// <summary>
        /// Gets terms colors
        /// </summary>
        /// <returns>List of terms colors</returns>
        public List<TermColors> GetTermsColors() =>
                this.SearchWithinHighlightedWordsListLabels.Select(term => term.GetCssValue("background-color"))
                .Select(termCode => this.GetColorTypeByCode(termCode)).ToList();

        /// <summary>
        /// Get color type by a code
        /// </summary>
        /// <param name="termCode">Term color rgb code</param>
        /// <returns>Term color</returns>
        protected TermColors GetColorTypeByCode(string termCode) =>
            Enum.GetValues(typeof(TermColors))
            .Cast<TermColors>()
            .First(color => TermColorMap[color].BackgroundColorCode.Equals(termCode));
    }
}
