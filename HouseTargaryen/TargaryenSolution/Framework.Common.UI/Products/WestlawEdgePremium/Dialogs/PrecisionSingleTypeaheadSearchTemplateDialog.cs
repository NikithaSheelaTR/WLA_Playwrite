namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.NewSearchTemplate;

    /// <summary>
    /// Precision single typeahead search template dialog on home page
    /// </summary>
    public class PrecisionSingleTypeaheadSearchTemplateDialog: BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='legalSimilaritySearchModal']");
        private static readonly By DialogTitleLocator = By.XPath(".//*[contains(@id, 'lightboxAriaLabel')]");
        private static readonly By DialogDescriptionLabelLocator = By.XPath(".//*[contains(@class, 'PrecisionSearchModal-subHeader')]/span[text()]");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//*[@id='jurisdictionId_athens']");
        private static readonly By ViewButtonLocator = By.XPath(".//div[@class='co_overlayBox_optionsBottom']//button[contains(@class,'co_primaryBtn')]");        
        private static readonly By CloseDialogButtonLocator = By.XPath(".//button[contains(@class, 'co_overlayBox_closeButton co_iconBtn')]");       

        /// <summary>
        /// Tab panel
        /// </summary>
        public PrecisionSingleTypeaheadSearchTemplateTabPanel TabPanel { get; } = new PrecisionSingleTypeaheadSearchTemplateTabPanel();

        /// <summary>
        /// Selections
        /// </summary>
        public PrecisionSelectionsComponent  Selections => new PrecisionSelectionsComponent ();

        /// <summary>
        /// Dialog title label
        /// </summary>
        public ILabel DialogTitleLabel => new Label(ContainerLocator, DialogTitleLocator);

        /// <summary>
        /// Dialog Description label
        /// </summary>
        public ILabel DescriptionLabel => new Label(ContainerLocator, DialogDescriptionLabelLocator);

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(ContainerLocator, JurisdictionButtonLocator);

        /// <summary>
        /// View X cases button
        /// </summary>
        public IButton ViewButton => new JsClickButton(ContainerLocator, ViewButtonLocator);

        /// <summary>
        /// Close Precision Research dialog
        /// </summary>
        public IButton CloseDialogButton => new Button(ContainerLocator, CloseDialogButtonLocator);      
    }
}
