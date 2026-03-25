namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.DropDowns;

    /// <summary>
    /// Toolbar
    /// </summary>
    public class Toolbar : BaseModuleRegressionComponent
    {
        private static readonly By CategoryPageLabelLocator = By.XPath(".//*[@aria-controls='saf-tooltip']");
        private static readonly By HeadingLabelLocator = By.XPath(".//*[@class='CS-main-content-heading']");
        private static readonly By HowAiWorksButtonLocator = By.XPath(".//*[@class='Conversational-search-formWrapper-infoButton' and contains(text(), 'How')]");
        private static readonly By JurisdictionInfoBoxMessageLocator = By.XPath(".//*[contains(@id,'popover-panel') and not(contains(@class, 'co_hideState'))] | .//*[contains(@id,'popover-panel') and (contains(@class, 'co_hideState'))]");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//button[@class='Jurisdiction-selector-button'] | .//div[@class='Jurisdiction-selector-wrapper']//button[contains(@class, 'Conversational-search')]");
        private static readonly By JurisdictionInfoIconButtonLocator = By.XPath(".//*[contains(@class, 'CS-jurisdiction-info')]");
        private static readonly By JurisdictionLabelLocator = By.XPath(".//span[@class='AALP-settings-jurisdiction-label']");
        private static readonly By NewResearchButtonLocator = By.XPath(".//button[contains(@class, 'CS-main-start-new-button')]");
        private static readonly By ToolbarContainerLocator = By.XPath("//div[@class='CS-main-content-heading-container']");                
        private static readonly By TipsBestResultsButtonLocator = By.XPath(".//*[@class='Conversational-search-formWrapper-infoButton' and contains(text(), 'Tips')]");
        private static readonly By SaveToFolderButtonLocator = By.XPath(".//button[contains(@class, 'co_dropdownTitle')]");
        private static readonly By CopyLinkButtonLocator = By.XPath(".//button[contains(@class, 'linkBuilder-button icon_link co_tbButton')]");
        private static readonly By CopiedLinkSuccessLabelLocator = By.XPath(".//div[contains(@class,'saf-alert__content')]");

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown { get; } = new DeliveryDropdown();

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionButtonLocator);

        /// <summary>
        /// New research button
        /// </summary>
        public IButton NewResearchButton => new Button(this.ComponentLocator, NewResearchButtonLocator);

        /// <summary>
        /// How AI works button
        /// </summary>
        public IButton HowAiWorksButton => new Button(this.ComponentLocator, HowAiWorksButtonLocator);

        /// <summary>
        /// Tips for best results button
        /// </summary>
        public IButton TipsBestResultsButton => new Button(this.ComponentLocator, TipsBestResultsButtonLocator);

        /// <summary>
        /// Jurisdiction Info icon button
        /// </summary>
        public IButton JurisdictionInfoIconButton => new Button(this.ComponentLocator, JurisdictionInfoIconButtonLocator);

        /// <summary>
        /// Save To Folder Button
        /// </summary>
        public IButton SaveToFolderButton => new Button(this.ComponentLocator, SaveToFolderButtonLocator);

        /// <summary>
        /// Copy Link Button
        /// </summary>
        public IButton CopyLinkButton => new Button(this.ComponentLocator, CopyLinkButtonLocator);

        /// <summary>
        /// Copied Link Label 
        /// </summary>
        public ILabel CopiedLinkSuccessLabel => new Label(this.ComponentLocator, CopiedLinkSuccessLabelLocator);

        /// <summary>
        ///  Jurisdiction info icon infobox
        /// </summary>
        public IInfoBox JurisdictionInfoBox => new InfoBox(DriverExtensions.GetElement(JurisdictionInfoBoxMessageLocator));

        /// <summary>
        /// Jurisdiction label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(this.ComponentLocator, JurisdictionLabelLocator);

        /// <summary>
        /// Category page label 
        /// </summary>
        public ILabel CategoryPageLabel => new Label(this.ComponentLocator, CategoryPageLabelLocator);

        /// <summary>
        /// Landing page heading label
        /// </summary>
        public ILabel HeadingLabel => new Label(this.ComponentLocator, HeadingLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ToolbarContainerLocator;
    }
}
