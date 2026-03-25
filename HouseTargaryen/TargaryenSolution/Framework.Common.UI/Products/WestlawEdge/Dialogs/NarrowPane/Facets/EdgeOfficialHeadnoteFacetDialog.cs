namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Official Headnote Facet Dialog
    /// </summary>
    public class EdgeOfficialHeadnoteFacetDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.Id("co_facet_OfficialHeadnotes_popup");

        private static readonly By HeadnotesLocator = By.XPath(".//div[@class='HeadnoteTopicContent']");

        /// <summary>
        /// Element Container of dialog
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// Head Notes
        /// </summary>
        private List<OfficialHeadnoteDialogItem> HeadNotes
            => DriverExtensions.GetElements(this.Container, HeadnotesLocator).Select(hn => new OfficialHeadnoteDialogItem(hn)).ToList();

        /// <summary>
        /// Set a checkbox for a headnote
        /// </summary>
        /// <param name="headnoteNumber"></param>
        public void SetCheckboxForHeadnoteByNumber(int headnoteNumber)
            => this.HeadNotes.Find(h => h.Count == headnoteNumber).SetHeadnoteCheckbox(true);

        /// <summary>
        /// Gets CitingReferences Count 
        /// </summary>
        /// <param name="headnoteNumber"></param>
        /// <returns></returns>
        public int GetCitingReferencesCountForHeadnoteByNumber(int headnoteNumber) => this.HeadNotes.Find(h => h.Count == headnoteNumber).CitingReferencesCount;
    }
}