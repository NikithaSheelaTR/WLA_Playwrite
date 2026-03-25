namespace Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Cited Authority Component for Quick Check feature in WestlawNext Canada.
    /// </summary>
    public class CitedAuthorityComponent: BaseModuleRegressionComponent
    {
        private static readonly By TitleLabelLocator = By.XPath("//div[@id='coid_website_briefAnalyzer']//div[contains(@class,'DA-ColumnRight')]//h4");
        private static readonly By LegalIssueLabelLocator = By.XPath("//label[@for='legalIssueInput_general']");
        private static readonly By PlainTextLabelLocator = By.Id("coid_plainTextInputLabel");
        private static readonly By EditFullViewButtonLocator = By.XPath("//button[contains(@class,'DA-EditDetailsButton')]");
        private static readonly By StartAnalysisButtonLocator = By.XPath("//button[text()='Start analysis']");
        private static readonly By LegalIssueMaxCharactersLabelLocator = 
            By.XPath("//label[@for='legalIssueInput_general']//following-sibling::span[@class='co_maxCharacters']");
        private static readonly By PlainTextMaxCharactersLabelLocator = 
            By.XPath("//label[@id='coid_plainTextInputLabel']//following-sibling::span[@class='co_maxCharacters']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CitedAuthorityComponent"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public CitedAuthorityComponent(By container)
        {
            this.ComponentLocator = container;
        }

        /// <summary>
        /// Cited Tile Title Label for Quick Check page.
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLabelLocator);

        /// <summary>
        /// Legal Issue Label for Quick Check page.
        /// </summary>
        public ILabel LegalIssueLabel => new Label(LegalIssueLabelLocator);

        /// <summary>
        /// Legal Issue Max Characters Label for Quick Check page.
        /// </summary>
        public ILabel LegalIssueMaxCharactersLabel => new Label(LegalIssueMaxCharactersLabelLocator);

        /// <summary>
        /// Gets the plain text label associated with the specified locator.
        /// </summary>
        public ILabel PlainTextLabel => new Label(PlainTextLabelLocator);

        /// <summary>
        /// Gets the label that displays the maximum number of characters allowed for plain text input.
        /// </summary>
        public ILabel PlainTextMaxCharactersLabel => new Label(PlainTextMaxCharactersLabelLocator);

        /// <summary>
        /// Edit Full View Button for Quick Check page.
        /// </summary>
        public IButton EditFullViewButton => new Button(EditFullViewButtonLocator);

        /// <summary>
        /// Start Analysis Button for Quick Check page.
        /// </summary>
        public IButton StartAnalysisButton => new Button(StartAnalysisButtonLocator);

        /// <summary>
        /// Gets the upload file component for Quick Check page.
        /// </summary>
        protected override By ComponentLocator { get; }
    }
}
