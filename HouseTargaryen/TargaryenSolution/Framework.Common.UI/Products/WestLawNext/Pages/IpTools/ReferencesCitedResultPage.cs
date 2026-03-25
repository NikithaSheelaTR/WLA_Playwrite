namespace Framework.Common.UI.Products.WestLawNext.Pages.IpTools
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This class contains methods and elements related to the References Cited result page
    /// </summary>
    public class ReferencesCitedResultPage : CitingReferencesPage
    {
        private static readonly By HighlightedTermLocator = By.XPath("//span[@class='co_searchTerm co_locateTerm']");

        private static readonly By ReferencesCitedResultTableLocator = By.Id("co_relatedInfo_table_ipTools");

        private static readonly By DetailDropdownLocator = By.Id("co_docToolbarDetailWidget");

        /// <summary>
        /// Initializes a new instance of the <see cref="CitingReferencesPage"/> class. 
        /// </summary>
        public ReferencesCitedResultPage()
        {
            this.Toolbar.DetailDropdown = new DetailDropdown(DetailDropdownLocator);
        }

        /// <summary>
        /// Grid Component
        /// </summary>
        public ReferenceCitedGridComponent GridComponent { get; } = new ReferenceCitedGridComponent();

        /// <summary>
        /// Get all the highlighted elements.
        /// </summary>
        /// <returns>List of highlighted texts</returns>
        public IEnumerable<string> GetAllHighlightedTerms()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(ReferencesCitedResultTableLocator), HighlightedTermLocator)
                .Select(x => x.Text);
    }
}