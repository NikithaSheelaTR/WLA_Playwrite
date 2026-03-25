namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;

    using OpenQA.Selenium;

    /// <summary>
    /// Heading component of an omitted by both case
    /// </summary>
    public class OmittedByBothHeadingComponent : BaseItem
    {
        private static readonly By HeadingExpanderLocator = By.XPath(".//button");

        private static readonly By SnippetTextLocator = By.XPath(".//li[@class='DA-RelevantItem']/a");

        private static readonly By RelatedCaseLocator = By.XPath(".//ul[@class='DA-RecommendationOverruling--links']//li");

        /// <summary>
        /// Initializes a new instance of the <see cref="OmittedByBothHeadingComponent"/> class
        /// </summary>
        /// <param name="container"></param>
        public OmittedByBothHeadingComponent(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The Is Expanded
        /// </summary>
        public bool IsExpanded => this.HeadingExpanderLink.GetAttribute("aria-expanded").Equals("true");

        /// <summary>
        /// Gets the heading expander link
        /// </summary>
        public ILink HeadingExpanderLink => new Link(this.Container, HeadingExpanderLocator);

        /// <summary>
        /// Gets the snippet text from a heading
        /// </summary>
        public ILink SnippetLink => new Link(this.Container, SnippetTextLocator);

        /// <summary>
        /// Thumbnails items list. 
        /// </summary>
        public ItemsCollection<RelatedCitationItem> RelatedCases => new ItemsCollection<RelatedCitationItem>(this.Container, RelatedCaseLocator);        

        /// <summary>
        /// Toggles a heading expander.
        /// </summary>
        /// <param name="state">
        /// Expander state
        /// </param>
        public void ToggleHeading(bool state)
        {
            if (this.IsExpanded != state)
            {
                this.HeadingExpanderLink.Click();
            }
        }
    }
}
