namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Cases on both sides Tab
    /// </summary>
    public class CasesOnBothSidesTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-casesonbothsidestab']");
        private static readonly By COBSTitleLabelLocator = By.XPath("//h3[contains(@class, 'CasesOnBothSides-module__casesOnBoth_title')]");
        private static readonly By CasesInFavorButtonLocator = By.XPath("(//div[contains(@class, 'CasesOnBothSides-module__chipContainer')]//saf-chip-v3)[1]");
        private static readonly By CasesAgainstButtonLocator = By.XPath("(//div[contains(@class, 'CasesOnBothSides-module__chipContainer')]//saf-chip-v3)[2]");
        private static readonly By COBSColumnsLabelLocator = By.XPath("//div[contains(@class, 'CasesOnBothSides-module__casesOnBoth_column')]/h4");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('saf-button-v3'));";

        /// <summary>
        /// COBS Title label
        /// </summary>
        public ILabel COBSTitleLabel => new Label(ComponentLocator, COBSTitleLabelLocator);

        /// <summary>
        /// Cases in favor button
        /// </summary>
        public IButton CasesInFavorButton => new Button(ComponentLocator, CasesInFavorButtonLocator);

        /// <summary>
        /// Cases in against button
        /// </summary>
        public IButton CasesAgainstButton => new Button(ComponentLocator, CasesAgainstButtonLocator);

        /// <summary>
        /// COBS Columns label
        /// </summary>
        public ElementsCollection<Label> COBSColumnsLabel => new ElementsCollection<Label>(this.ComponentLocator, COBSColumnsLabelLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Cases on both sides";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;

        /// <summary>
        /// Click the cross button to close the tab
        /// </summary>
        public void ClickXButton()
        {
            IWebElement COBSFilterCasesInFavor = DriverExtensions.GetElement(ComponentLocator, CasesInFavorButtonLocator);
            IWebElement crossButton = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, COBSFilterCasesInFavor);
            crossButton.Click();
        }
    }
}