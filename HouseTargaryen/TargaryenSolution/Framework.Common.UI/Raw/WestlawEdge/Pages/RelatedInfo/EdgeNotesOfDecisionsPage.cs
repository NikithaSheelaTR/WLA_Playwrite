namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Items.NotesOfDecisions;
    using Framework.Common.UI.Raw.WestlawEdge.Models.NotesOfDecisions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Notes of Decision tab Page
    /// </summary>
    public class EdgeNotesOfDecisionsPage : EdgeTabPage
    {
        private const string HeadingTitleLctMask = "//div[contains(@class,'co_relatedInfo_nod_heading')]//*[contains(text(), '{0}') and not(contains(@class,'co_accessibilityLabel'))]";

        private const string DraggableDocumentTitleLctMask = "//div[contains(@id, 'relatedInfo')]//a[contains(@title, '{0}')]";

        private static readonly By SelectedCategoryPathLocator =
            By.XPath("//div[@class='TocEntryWrapper TocEntryHighlight']//span");

        private static readonly By NoteOfDecisionsTopicLocator = By.XPath("//div[@class='TocEntryContent']//span");

        private static readonly By SortDropDownLocator =
            By.XPath("//div[@id='co_docToolbar']//select[@name='coid_relatedInfo_NOD_SortElement']");

        private static readonly By SelectedCategoryTitlesFromBodyLocator =
            By.XPath("//div[@id='coid_relatedInfo_NOD_MainContent']//h4");

        private static readonly By LeftTopicListLocator = By.Id("co_leftColumn");

        private static readonly By CategoryTableContainerLocator = By.Id("co_contentWrapper");

        private static readonly By NotesOfDecisionsHeaderLocator = By.Id("co_categoryLabelContainer");

        private static readonly By SelectIndexMessageLocator = By.XPath("//div[@id='coid_relatedInfo_NOD_MainContent']/h3");

        private static readonly By SelectAllCheckboxLocator = By.XPath("//input[@id='co_nod_selectAll']");

        private static readonly By SelectAllCheckboxLabelLocator = By.XPath("//label[@for='co_nod_selectAll']");

        private static readonly By SelectedItemLabelLocator = By.XPath("//span[@id='co_nod_itemsSelected' or @class='co_nod_noItemsSelected']");

        private static readonly By ClearSelectedLinkLocator = By.XPath("//span[@id='co_nod_clearSelected']");

        private static readonly By NodContainerLocator = By.XPath(".//div[contains(@id,'co_relatedInfo_Heading')]");

        private static readonly By NodListContainerLocator = By.XPath("//div[@class='co_relatedInfo_annotations']");

        private static readonly By FocusedCategoryTitleLocator = By.ClassName("FocusableItem");

        private static readonly By UndoFiltersMessageLocator = By.XPath("//div[@class='co_relatedInfo_contentResult_containerEmpty']");

        private static readonly By RestoredPreviousFiltersMessageLocator = By.CssSelector("#co_reapplyFacets_infoBox_NOD .co_infoBox_message");

        private EnumPropertyMapper<NotesOfDesicionsLinks, WebElementInfo> notesOfDesicionsLinksMap;
        
        /// <summary>
        /// Gets the Notes of Decisions toolbar.
        /// </summary>
        public NotesOfDecisionsToolbarComponent NodToolbar { get; } = new NotesOfDecisionsToolbarComponent();

        /// <summary>
        /// New Notes of Decisions Narrow Pane
        /// </summary>
        public EdgeNodNarrowPaneComponent NodNarrowPane => new EdgeNodNarrowPaneComponent();

        /// <summary>
        /// Gets the notification type enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<NotesOfDesicionsLinks, WebElementInfo> NotesOfDesicionsLinksMap =>
            this.notesOfDesicionsLinksMap = this.notesOfDesicionsLinksMap
                                            ?? EnumPropertyModelCache.GetMap<NotesOfDesicionsLinks, WebElementInfo>();

        /// <summary>
        /// Gets the KeyCiteFlag enumeration to WebElementInfo map.
        /// </summary>
        protected override EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap
            =>  EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document"); 

        /// <summary>
        /// Check if RestorePreviousFiltersMessage Displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsRestorePreviousFiltersMessageDisplayed()
            => DriverExtensions.IsDisplayed(RestoredPreviousFiltersMessageLocator);

        /// <summary>
        /// Returns notes of decisions header text
        /// </summary>
        public ILabel NodHeader => new Label(NotesOfDecisionsHeaderLocator);

        /// <summary>
        /// GetRestorePreviousFiltersMessage
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetRestorePreviousFiltersMessage()
            => DriverExtensions.GetText(RestoredPreviousFiltersMessageLocator);

        /// <summary>
        /// Is SortDropDown Displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if SortDropDown is Displayed </returns>
        public bool IsSortDropDownDisplayed() => DriverExtensions.IsDisplayed(SortDropDownLocator);

        /// <summary>
        /// Verifies that Selected Heading is in view
        /// </summary>
        /// <param name="name"> The name of heading. </param>
        /// <returns> The <see cref="bool"/>. True if that Selected Heading is in view. </returns>
        public bool IsSelectedHeadingInView(string name)
            => DriverExtensions.GetElement(By.XPath(string.Format(HeadingTitleLctMask, name))).IsElementInView();

        /// <summary>
        /// Is left topic list displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>.</returns>
        public bool IsLeftTopicListDisplayed() => DriverExtensions.IsDisplayed(LeftTopicListLocator, 5);

        /// <summary>
        /// Is category table container displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>.</returns>
        public bool IsCategoryTableContainerDisplayed() => DriverExtensions.IsDisplayed(CategoryTableContainerLocator, 5);

        /// <summary>
        /// Is NOD header with count displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>.</returns>
        public bool IsHeaderWithTitleAndCountDisplayed() => DriverExtensions.IsDisplayed(NotesOfDecisionsHeaderLocator, 5) && DriverExtensions.GetElement(NotesOfDecisionsHeaderLocator).Text.ConvertCountToInt() > 0;

        /// <summary>
        /// Verifies that select index message is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSelectFromIndexMessageDisplayed() => DriverExtensions.IsDisplayed(SelectIndexMessageLocator, 5);

        /// <summary>
        /// Verifies that undo filters message is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUndoFiltersMessageDisplayed() => DriverExtensions.IsDisplayed(UndoFiltersMessageLocator);

        /// <summary>
        /// Click link
        /// </summary>
        /// <param name="link">The link.</param>
        public void ClickLink(NotesOfDesicionsLinks link)
        {
            DriverExtensions.Click(DriverExtensions.GetElement(By.Id(this.NotesOfDesicionsLinksMap[link].Id)));
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Returns the value of the selected category link.
        /// </summary>
        /// <returns>Category name</returns>
        public string GetSelectedCategoryName() => DriverExtensions.WaitForElement(SelectedCategoryPathLocator).Text;

        /// <summary>
        /// Gets collection of categories in NodToc.
        /// </summary>
        /// <returns>Collection of categories in NodToc.</returns>
        public List<string> GetCategoriesHeadersFromNodToc() =>
            DriverExtensions
                .GetElements(NoteOfDecisionsTopicLocator).Where(a => !a.Text.Equals(string.Empty)).Select(s => s.Text.Trim()).ToList();

        /// <summary>
        ///  Gets collection of categories in text body
        /// </summary>
        /// <returns>Collection of categories in text body</returns>
        public List<string> GetCategoriesHeadersFromBody() => DriverExtensions
            .GetElements(SelectedCategoryTitlesFromBodyLocator).Select(a => a.Text.Trim().Replace("\r\n", " ")).ToList();

        /// <summary>
        /// The get focused category header from body.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFocusedCategoryHeaderFromBody() => DriverExtensions.GetText(FocusedCategoryTitleLocator);

        /// <summary>
        /// Verifies that the select all checkbox is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the select all checkbox is displayed. </returns>
        public bool IsSelectAllCheckboxDisplayed() => DriverExtensions.IsDisplayed(SelectAllCheckboxLocator);

        /// <summary>
        /// Gets the select all checkbox text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The select all checkbox text. </returns>
        public string GetSelectAllCheckboxName() => DriverExtensions.GetText(SelectAllCheckboxLabelLocator);

        /// <summary>
        /// Click on a Flag in the doc.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public T ClickKeyCiteFlag<T>(KeyCiteFlag flag) where T : EdgeCommonDocumentPage
        {
            DriverExtensions.WaitForElement(By.ClassName(this.KeyCiteFlagsMap[flag].ClassName), 5).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Sets select all checkbox.
        /// </summary>
        /// <param name="selected"> The selected. </param>
        /// <returns> The <see cref="EdgeNotesOfDecisionsPage"/>. </returns>
        public EdgeNotesOfDecisionsPage SetSelectAllCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(SelectAllCheckboxLocator, selected);
            return this;
        }

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="title">The element to drag.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string DragAndDropTitleElementToRecentFolder(
            string targetFolder,
            string title)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
              this.GetDraggableDocumentTitleElement(title));

            this.DragAndDropToFolder(new EdgeRecentFoldersDialog().GetFolderElement(targetFolder), this.GetDraggableDocumentTitleElement(title), CopyOrMoveEnum.Move);

            return this.Header.GetInfoMessage();
        }

        /// <summary>
        /// Verifies that the Select all checkbox is selected.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the Select all checkbox is selected. </returns>
        public bool IsSelectAllCheckboxSelected() => DriverExtensions.IsCheckboxSelected(SelectAllCheckboxLocator);

        /// <summary>
        /// Verifies that the selected item text is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the selected item text is displayed. </returns>
        public bool IsSelectedItemTextDisplayed() => DriverExtensions.IsDisplayed(SelectedItemLabelLocator);

        /// <summary>
        /// Gets the selected item text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The selected item text. </returns>
        public string GetSelectedItemsText() => DriverExtensions.GetText(SelectedItemLabelLocator);

        /// <summary>
        /// Verifies that the clear selected link is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the clear selected link is displayed. </returns>
        public bool IsClearSelectedLinkDisplayed() => DriverExtensions.IsDisplayed(ClearSelectedLinkLocator);

        /// <summary>
        /// Gets the clear selected link text.
        /// </summary>
        /// <returns> The <see cref="string"/>. The clear selected link text. </returns>
        public string GetClearSelectedLinkText() => DriverExtensions.GetText(ClearSelectedLinkLocator);

        /// <summary>
        /// Clicks the clear selected link.
        /// </summary>
        /// <returns> The <see cref="EdgeNotesOfDecisionsPage"/>. </returns>
        public EdgeNotesOfDecisionsPage ClickClearSelectedLink()
        {
            DriverExtensions.Click(ClearSelectedLinkLocator);
            return this;
        }

        /// <summary>
        /// Gets list of Nod
        /// </summary>
        /// <returns> list of Nods </returns>
        public List<NotesOfDecisionsItemModel> GetListOfNodModels() => this.GetListOfNodItems()
                                                                       .Select(item => item.ToModel<NotesOfDecisionsItemModel>()).ToList();

        /// <summary>
        /// Checks Nod by name.
        /// </summary>
        /// <param name="selected"> The selected. </param>
        /// <param name="nodName"> The Nod name. </param>
        /// <returns> The <see cref="EdgeNotesOfDecisionsPage"/>. </returns>
        public EdgeNotesOfDecisionsPage CheckNodByName(bool selected, params string[] nodName)
        {
            var listOfNod = this.GetListOfNodItems();

            nodName
                .Select(
                    name => listOfNod.First(
                        item => item.NodHeading.Equals(name)))
                .ToList()
                .ForEach(elem => elem.SelectNodCheckbox(selected));

            return this;
        }

        /// <summary>
        /// Scroll to the NOD content heading by name.
        /// Note: Offset number is added to aviod the case 
        /// related to minified header issue 
        /// when the NOD heading is hidden by the global header
        /// but is focused in the TOC (approved by NPD).
        /// </summary>
        /// <param name="name">
        /// The name of heading. 
        /// </param>
        public void ScrollToNodContentHeadingByName(string name) =>
            DriverExtensions.ScrollIntoView(By.XPath(string.Format(HeadingTitleLctMask, name)), 300);

        /// <summary>
        /// Get text of select index message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSelectIndexMessageText() => DriverExtensions.GetText(SelectIndexMessageLocator);

        /// <summary>
        /// Get text of undo filters message
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetUndoFiltersMessageText() => DriverExtensions.GetText(UndoFiltersMessageLocator);

        /// <summary>
        /// Get count of NOD paragraphs
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>
        /// </returns>
        public int GetNodParagraphCount() =>
            DriverExtensions.GetElements(NodContainerLocator).Count;

        /// <summary>
        /// Get draggable document title element
        /// </summary>
        /// <returns>List of IWebElements</returns>
        private IWebElement GetDraggableDocumentTitleElement(string title) =>
         DriverExtensions.GetElement(By.XPath(string.Format(DraggableDocumentTitleLctMask, title)));

        private List<NotesOfDecisionsItem> GetListOfNodItems()
            => DriverExtensions.GetElements(NodListContainerLocator, NodContainerLocator)
                               .Select(item => new NotesOfDecisionsItem(item)).ToList();
    }
}
