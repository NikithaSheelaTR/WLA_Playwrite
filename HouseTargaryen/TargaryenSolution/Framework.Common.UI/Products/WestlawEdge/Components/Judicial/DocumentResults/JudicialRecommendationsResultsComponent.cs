namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial.DocumentResults
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial recommendations result list
    /// </summary>
    public class JudicialRecommendationsResultsComponent : RecommendationsResultsComponent
    {
        private static readonly By CheckboxLocator = By.XPath(".//div[contains(@class, 'DA-DocumentHeader')]/input");
        private static readonly By TitleLocator = By.XPath(".//*[@class='DA-RecHeader']");

        /// <summary>
        /// Initializes a new instance of the <see cref="JudicialRecommendationsResultsComponent"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public JudicialRecommendationsResultsComponent(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Gets the checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckboxLocator);

        /// <summary>
        /// Gets the document section title
        /// </summary>
        public ILabel Title => new Label(this.Container, TitleLocator);
    }
}
