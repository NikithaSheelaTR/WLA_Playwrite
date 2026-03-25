
namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  Describe  dialog which appears after clicking on the 'Select' button  on the Narrow pane
    /// </summary>
    public class TsdrInternationalClassDialog: BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='co_facet_tsdrClassInternational_popup']");
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
