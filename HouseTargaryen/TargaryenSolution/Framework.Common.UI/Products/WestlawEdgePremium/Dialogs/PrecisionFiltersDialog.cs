namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters;

    /// <summary>
    /// Precision Filters dialog
    /// </summary>
    public class PrecisionFiltersDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("legalSimilaritySearchModal");
        private static readonly By TitleLabelLocator = By.XPath(".//*[contains(@id, 'coid_lightboxAriaLabel')]");
        private static readonly By DescriptionLabelLocator = By.XPath(".//div[@class='PrecisionSearchModal-subHeader']");
        private static readonly By ViewCasesButtonLocator = By.XPath(".//button[contains(@class,'Button-primary')] | .//button[contains(@class,'co_primaryBtn')]");
        private static readonly By CancelButtonLocator = By.XPath(".//button[text()='Cancel']");
        private static readonly By XButtonLocator = By.XPath(".//button[text()='Close']");        

        /// <summary>
        /// Tab panel
        /// </summary>
        public PrecisionFiltersTabPanel TabPanel { get; } = new PrecisionFiltersTabPanel();

        /// <summary>
        /// Selections
        /// </summary>
        public PrecisionSelectionsComponent  Selections => new PrecisionSelectionsComponent();

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Description label
        /// </summary>
        public ILabel DescriptionLabel => new Label(ContainerLocator, DescriptionLabelLocator);

        /// <summary>
        /// View cases button
        /// </summary>
        public IButton ViewCasesButton => new JsClickButton(ContainerLocator, ViewCasesButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(ContainerLocator, CancelButtonLocator);

        /// <summary>
        /// X button
        /// </summary>
        public IButton XButton => new Button(ContainerLocator, XButtonLocator);
    }
}
