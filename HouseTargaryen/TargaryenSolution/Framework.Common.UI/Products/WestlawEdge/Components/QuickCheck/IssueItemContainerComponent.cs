namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Document analyzer=>Recommendations page=>Recommendations tab=>Results pane to the right.
    /// Issue items container. Everything that's under the blue collapsible header.
    /// </summary>
    public class IssueItemContainerComponent : BaseItem
    {
        private static readonly By HeadingCheckboxLocator = By.XPath(".//input[contains(@id, 'co_DAHeaderCheckbox')]");

        private static readonly By HeaderLocator = By.XPath(".//*[@class='DA-RecHeader']");

        private static readonly By ExpanderLocator = By.XPath(".//button[@class='DA-RecHeaderButton']");

        private static readonly By SectionLocator = By.XPath("//*[@class='DA-HeadingCases']/span");

        private static readonly By IssueItemLocator = By.XPath(".//li[@class='co_issueItemBody']");

        private static readonly By CasesListLocator = By.XPath(".//div[contains(.,'Cases') or contains(.,'cases')]/following-sibling::ul");

        private static readonly By BriefsAndMemorandaListLocator = By.XPath(".//div[contains(.,'Briefs & Memoranda') or contains(.,'briefs')]/following-sibling::ul");

        private static readonly By SecondarySourcesListLocator = By.XPath(".//div[contains(.,'Secondary Sources') or contains(.,'secondarySources')]/following-sibling::ul");

        private static readonly By SeeAdditionalCasesLinkLocator = By.XPath(".//a[contains(., 'See additional cases')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueItemContainerComponent"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public IssueItemContainerComponent(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Gets the checkbox.
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, HeadingCheckboxLocator);

        /// <summary>
        /// Gets the additional cases link.
        /// </summary>
        public ILink AdditionalCasesLink => new Link(this.Container, SeeAdditionalCasesLinkLocator);

        /// <summary>
        /// Gets the expand checkbox.
        /// </summary>
        public ICheckBox ExpandCheckbox => new SectionExpandCheckbox(this.Container, ExpanderLocator);

        /// <summary>
        /// Gets the header label.
        /// </summary>
        public ILabel TitleLabel => new Label(this.Container, HeaderLocator);

        /// <summary>
        /// Section labels
        /// </summary>
        public IReadOnlyCollection<ILabel> SectionsLabels => new ElementsCollection<Label>(this.Container, SectionLocator);

        /// <summary>
        /// Cases
        /// </summary>
        public ItemsCollection<RecommendationItem> Cases => new ItemsCollection<RecommendationItem>(this.Container, new ByChained(CasesListLocator, IssueItemLocator));

        /// <summary>
        /// Briefs and Memoranda
        /// </summary>
        public ItemsCollection<QuickCheckBaseItem> BriefsAndMemoranda => new ItemsCollection<QuickCheckBaseItem>(this.Container, new ByChained(BriefsAndMemorandaListLocator, IssueItemLocator));

        /// <summary>
        /// Secondary Sources
        /// </summary>
        public ItemsCollection<QuickCheckBaseItem> SecondarySources => new ItemsCollection<QuickCheckBaseItem>(this.Container, new ByChained(SecondarySourcesListLocator, IssueItemLocator));

        /// <summary>
        /// All Recommendations
        /// </summary>
        public ItemsCollection<RecommendationItem> AllRecommendations => new ItemsCollection<RecommendationItem>(this.Container, IssueItemLocator); 
    } 
}