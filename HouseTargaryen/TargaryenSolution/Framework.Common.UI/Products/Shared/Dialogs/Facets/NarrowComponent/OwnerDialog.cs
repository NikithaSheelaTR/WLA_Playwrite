namespace Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent
{
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  Describe  dialog which appears after clicking on the 'Select' button  on the Narrow pane
    /// </summary>
    public class OwnerDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.Id("co_facet_ipOwner_popup");

        private static readonly By SearchResults = By.XPath(".//li[not (contains(@class, 'co_hideState'))]//span[contains(@id,'matchedWord')]");
        private static readonly By SortByDropdownLocator = By.XPath("//select[@class='co_RI_SortBy']");

        /// <summary>
        /// Gets the Sort By dropdown
        /// </summary>
        public virtual IDropdown<SortByIpFacetDialogDropDownOptions> SortBy { get; } = new Dropdown<SortByIpFacetDialogDropDownOptions>(SortByDropdownLocator);
        
        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);
        
        /// <summary>
        /// Are search results displayed
        /// </summary>
        public bool AreSearchResultsDisplayed() => DriverExtensions.GetElements(ContainerLocator, SearchResults).ToList().TrueForAll(elem => elem.Displayed);
    }
}
