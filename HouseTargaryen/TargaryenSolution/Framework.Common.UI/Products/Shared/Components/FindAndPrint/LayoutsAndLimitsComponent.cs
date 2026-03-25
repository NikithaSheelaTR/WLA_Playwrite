
namespace Framework.Common.UI.Products.Shared.Components.FindAndPrint
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;

    using OpenQA.Selenium;

    /// <summary>
    /// LayoutComponent in Delivery download
    /// </summary>
    public class LayoutsAndLimitsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_website_generatedLayoutAndLimitsTab");

        private static readonly By CoverPageLocator = By.Id("//input[@id='coid_chkDdcLayoutCoverPage']");

        private static readonly By FooterNotesLocator = By.Id("co_delivery_footnotes");

        private static readonly By LinksLocator = By.Id("co_delivery_linkColor");

        private static readonly By FontSizeLocator = By.Id("co_delivery_fontSize");

        private static readonly By DualColumnLayoutLocator = By.Id("coid_chkDdcLayoutUseDualColumnsForCases");

        private static readonly By UnderLineLocator = By.Id("co_delivery_linkUnderline");

        private static readonly By DeliveryAsDropDownLocator = By.XPath("//select[@id= 'co_delivery_fileContainer' or @name = 'co_delivery_fileContainer']");

        private static readonly By DeliveryFormatDropdownLocator
            = By.XPath("//div[not(contains(@style, 'display: none'))]/select[contains(@id, 'co_delivery_format')]");

        private static readonly By LayoutLinkLocator = By.Id("co_deliveryOptionsTab2");

        /// <summary>
        /// Dual Column Layout checkbox
        /// </summary>
        public ICheckBox DualColumnLayoutCheckbox => new CheckBox(DualColumnLayoutLocator);

        /// <summary>
        /// Cover page checkbox
        /// </summary>
        public ICheckBox CoverPageCheckbox => new CheckBox(CoverPageLocator);

        /// <summary>
        /// Footer notes dropdown
        /// </summary>
        public IDropdown<string> FooterNotesDropdown => new Dropdown(FooterNotesLocator);

        /// <summary>
        /// Footer notes dropdown
        /// </summary>
        public IDropdown<string> LinksDropdown => new Dropdown(LinksLocator);

        /// <summary>
        /// Footer notes dropdown
        /// </summary>
        public IDropdown<string> FontSizeDropdown => new Dropdown(FontSizeLocator);

        /// <summary>
        /// Underline checkbox
        /// </summary>
        public ICheckBox UnderlineCheckbox => new CheckBox(UnderLineLocator);

        /// <summary>
        /// Deliver as dropdown
        /// </summary>
        public IDropdown<string> DeliveryAsDropdown => new Dropdown(DeliveryAsDropDownLocator);

        /// <summary>
        /// Gets the format dropdown.
        /// </summary>
        public IDropdown<DeliveryFormat> FormatDropdown => new Dropdown<DeliveryFormat>(DeliveryFormatDropdownLocator);

        /// <summary>
        /// Get a Layout tab
        /// </summary>
        /// <returns>A list of treatment links</returns>
        public ILink LayoutLink => new Link(LayoutLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
