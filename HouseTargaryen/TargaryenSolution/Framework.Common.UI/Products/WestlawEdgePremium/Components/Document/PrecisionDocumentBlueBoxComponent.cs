namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.Document
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision document blue box component
    /// </summary>
    public class PrecisionDocumentBlueBoxComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[starts-with(@id, 'legalSimilaritiesHeadnotesSection_')]");
        private static readonly By ShowButtonLocator = By.XPath(".//span[contains(@class, 'icon_downCaret')]");
        private static readonly By QuestionLocator = By.XPath(".//div[@class='Athens-browseBox-question']");
        private static readonly By MaterialFactsLocator = By.XPath(".//div[@class='Athens-browseBox-materialFacts']");
        private static readonly By CausesOfActionLocator = By.XPath(".//div[@class='Athens-browseBox-secondaryContent']");

        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionDocumentBlueBoxComponent"/> class.
        /// </summary>
        /// <param name="containerElement"></param>
        public PrecisionDocumentBlueBoxComponent(IWebElement containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Precision blue box show button
        /// </summary>
        public IButton ShowButton => new Button(this.ContainerElement, ShowButtonLocator);

        /// <summary>
        /// Blue box question or title
        /// </summary>
        public ILabel BlueBoxTitleLabel => new Label(this.ContainerElement, QuestionLocator);

        /// <summary>
        /// Blue box material facts
        /// </summary>
        public ILabel BlueBoxMaterialFactsLabel => new Label(this.ContainerElement, MaterialFactsLocator);

        /// <summary>
        ///  Blue box causes of action
        /// </summary>
        public ILabel BlueBoxCausesOfActionLabel => new Label(this.ContainerElement, CausesOfActionLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
