namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    ///  Describe  dialog which appears after clicking on the 'Select' button  on the Tsdr Status Facet
    /// </summary>
    public class TsdrStatusDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_facet_tsdrStatus_popup']");
        private static readonly By SortByDropdownLocator = By.XPath("//select[@class='co_RI_SortBy']");

        /// <summary>
        /// Gets the Sort By dropdown
        /// </summary>
        public virtual IDropdown<SortByIpFacetDialogDropDownOptions> SortBy { get; } = new Dropdown<SortByIpFacetDialogDropDownOptions>(SortByDropdownLocator);

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
    }
}
