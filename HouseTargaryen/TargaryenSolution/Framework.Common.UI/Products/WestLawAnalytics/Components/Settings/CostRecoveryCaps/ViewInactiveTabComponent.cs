namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// View Inactive Tab Component
    /// </summary>
    public class ViewInactiveTabComponent : BaseTabComponent
    {
        private static readonly By ChexboxLocator = By.XPath("//tbody[@id='wa_manageCapsTableBody']//td[@class='wa_manageCapsTable_checkbox']/input");

        private static readonly By EditLinkLocator = By.XPath("//tr[descendant::td]//td[@class='wa_manageCapsTable_edit']");

        private static readonly By DeleteButtonLocator = By.Id("wa_manageCapsDeleteCapsButton");

        private static readonly By TabNameTextLocator = By.XPath("//div[@id='wa_manageCaps']/h2");

        private static readonly By InformationTextLocator = By.ClassName("co_formInline");

        private static readonly By ContainerLocator = By.CssSelector("#wa_pricingTabsMainContent, #wa_pricingTabsMainFooter");

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "View Inactive Caps";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verifies that the delete button is present.
        /// </summary>
        /// <returns> True if the delete button is present </returns>
        public bool IsDeleteButtonPresent() => DriverExtensions.IsElementPresent(DeleteButtonLocator);

        /// <summary>
        /// Gets Tab Name text
        /// </summary>
        /// <returns> Tab Name text </returns>
        public string GetTabNameText() => DriverExtensions.GetText(TabNameTextLocator);

        /// <summary>
        /// Gets Information text
        /// </summary>
        /// <returns> Information text </returns>
        public string GetInformationText() => DriverExtensions.GetText(InformationTextLocator);

        /// <summary>
        /// Verifies that the Edit link is not present
        /// </summary>
        /// <returns> True if the Edit link is not present </returns>
        public bool IsEditLinkNotPresent() => DriverExtensions.GetElements(EditLinkLocator).Count == 0;

        /// <summary>
        /// Verifies that the cap checkbox is not present
        /// </summary>
        /// <returns> True if the cap checkbox is not present </returns>
        public bool IsCapCheckboxNotPresent() => DriverExtensions.GetElements(ChexboxLocator).Count == 0;
    }
}