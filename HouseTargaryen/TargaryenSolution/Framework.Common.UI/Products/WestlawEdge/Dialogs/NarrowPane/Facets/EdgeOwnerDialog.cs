
namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;

    using OpenQA.Selenium;

    /// <summary>
    ///  Edge owner dialog
    /// </summary>
    public class EdgeOwnerDialog : OwnerDialog
    {
        private static readonly By SortByDropdownLocator = By.XPath("//select[@class='co_RI_SortBy']");

        /// <summary>
        /// Gets the Sort By dropdown
        /// </summary>
        public override IDropdown<SortByIpFacetDialogDropDownOptions> SortBy { get; } =
            new Dropdown<SortByIpFacetDialogDropDownOptions>(SortByDropdownLocator)
                {
                    AdditionalInfo = "Edge",
                    SourceFolder = @"Resources/EnumPropertyMaps/WestlawEdge"
                };
    }
}
