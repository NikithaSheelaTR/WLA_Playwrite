namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.FolderHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using System.Threading;

    /// <summary>
    /// Indigo Folder Page
    /// </summary>
    public class EdgeResearchOrganizerPage : ResearchOrganizerPage
    {
        private static readonly By QuickAccessPanelLocator = By.XPath("//div[@class = 'co_quickAccess']");
        private static readonly By QuickAccessToggleLocator = By.XPath(".//button[contains(@class,'QuickAccess-titleButton')]");
        private static readonly By TreeViewPanelLocator = By.XPath("//div[@id = 'co_leftColumn']");
        private static readonly By TreeViewToggleLocator = By.XPath(".//div[@id = 'co_collapseButtonLeft']");
        private static readonly By RevokeWarningMessageLocator = By.XPath("//div[@id = 'co_itemRevoked_warningMessage']//div[@class = 'co_infoBox_message']");
        private static readonly By SuccessMessageLocator = By.XPath("//div[contains(@class,'co_infoBox success')]");
        private static readonly By UndoDeletionButtonLocator = By.XPath("//button[@id = 'co_ro_detail_trash_undo']");
        private static readonly By FolderAnalysisButtonLocator = By.XPath("//button[@class = 'FolderAnalysisToggleSlider']");
        private static readonly By FolderAnalysisGreenIndicatorLocator = By.XPath("//div[@id = 'coid_smartFolders_slider']//span[@class = 'NotificationMenuIndicator']");
        private static readonly By TreeViewPaneCollapseLocator = By.XPath("//button[@id = 'co_collapseActionLeft']");
        private static readonly By SearchQueryinFolder = By.XPath("//input[@name='SearchFacetSearchWithin-inputKeyword']");
        private static readonly By SearchQueryinFolderinDialogBox = By.XPath("//label[text()='Search Within Query']//following-sibling::input[@id='«r1»']");
        private static readonly By ContinueButton = By.XPath("//button[text()='Continue' or text()='Search']");
        private static readonly By LoadingSpinnerLocator = By.XPath("//div[@class='co_progressIndicator']");
        private static readonly By RemoveSearchButtonLocator = By.XPath("//button[text()='Remove search']");

        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Gets the Folder Grid
        /// </summary>
        public new EdgeFolderGridComponent FolderGrid { get; } = new EdgeFolderGridComponent();

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Gets the doc toolbar.
        /// </summary>
        public EdgeToolbarComponent EdgeToolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// Breadcrumb widget
        /// </summary>
        public BreadcrumbComponent BreadcrumbWidget { get; } = new BreadcrumbComponent();

        /// <summary>
        /// Quick access
        /// </summary>
        public QuickAccessComponent QuickAccess => new QuickAccessComponent();

        /// <summary>
        /// Quick access toggle
        /// </summary>
        public IToggle QuickAccessToggle => new Toggle(DriverExtensions.GetElement(QuickAccessPanelLocator), QuickAccessToggleLocator, "aria-expanded", "true");

        /// <summary>
        /// Tree view panel toggle
        /// </summary>
        public IToggle TreeViewPanelToggle => new ToggleWithText(DriverExtensions.GetElement(TreeViewPanelLocator), TreeViewToggleLocator, "Collapse");

        /// <summary>
        /// Folder analysis button
        /// </summary>
        public IButton FolderAnalysisButton => new Button(FolderAnalysisButtonLocator);

        /// <summary>
        ///  Green dot indicating the presence of recommendations in the FA slider
        /// </summary>
        public ILabel FolderAnalysisGreenIndicator => new Label(FolderAnalysisGreenIndicatorLocator);

        /// <summary>
        /// Onboarding tour component
        /// </summary>
        /// <returns>
        /// The <see cref="FoldersTourComponent"/>.
        /// </returns>
        public FoldersTourComponent TourComponent => new FoldersTourComponent();

        /// <summary>
        /// Left folder
        /// </summary>
        public new EdgeLeftFolderComponent LeftFolder { get; } = new EdgeLeftFolderComponent();

        /// <summary>
        /// Folder Header component
        /// </summary>
        public EdgeFolderHeaderComponent FolderHeader { get; } = new EdgeFolderHeaderComponent();

        /// <summary>
        /// The New Negative treatment box (appears only when a document in the folder has new negative treatment)
        /// </summary>
        public EdgeNegativeTreatmentComponent EdgeNegativeTreatment { get; private set; } = new EdgeNegativeTreatmentComponent(); 

        /// <summary>
        /// Revoke warning message label
        /// </summary>
        public ILabel RevokeWarningMessage => new Label(RevokeWarningMessageLocator);

        /// <summary>
        /// Success message label
        /// </summary>
        public ILabel SuccessMessage => new Label(SuccessMessageLocator);

        /// <summary>
        /// Undo button for a warning message about moving item to Trash
        /// </summary>
        public IButton UndoDeletionButton => new Button(UndoDeletionButtonLocator);

        /// <summary>
        /// Remove Search button to remove search within filter
        /// </summary>
        public IButton RemoveSearchButton => new Button(RemoveSearchButtonLocator);

        /// <summary>
        /// Save document to another folder (needed for move and copy methods)
        /// </summary>
        /// <param name="gridModel"> Grid item model </param>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <returns>The <see cref="SaveToFolderDialog"/></returns>
        public T SelectGridItemAndOpenSaveToFolderDialog<T>(FolderGridModel gridModel, string documentFolder) where T : SaveToFolderDialog
        {
            this.LeftFolder.FolderTree.SelectFolderByName(documentFolder);
            this.FolderGrid.SelectItemByName(gridModel.Title);
            return this.Toolbar.ClickToolbarElement<T>(ToolbarElements.SaveToFolder);
        }

        /// <summary>
        /// Saves document range to folder (needed for copy and move methods)
        /// </summary>
        /// <param name="documentFolder">The place of document (folder)</param>
        /// <param name="lastItemNumber">last Items number</param>
        /// <param name="firstItemNumber">first item number</param>
        /// <returns>The <see cref="SaveToFolderDialog"/> instance </returns>
        public override SaveToFolderDialog SelectGridItemRangeAndOpenSaveToFolderDialog(string documentFolder, int lastItemNumber, int firstItemNumber)
        {
            this.LeftFolder.FolderTree.SelectFolderByName(documentFolder);
            this.FolderGrid.SelectGridItemsRange(lastItemNumber, firstItemNumber);
            return this.EdgeToolbar.ClickToolbarElement<EdgeSaveToFolderDialog>(EdgeToolbarElements.SaveToFolder);
        }

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="gridItemTitle">The element to drag.</param>
        /// <param name="copyOrMoveEnum">The drag And Drop Enum.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public override string DragAndDropGridItemToRecentFolder(
            string targetFolder,
            string gridItemTitle,
            CopyOrMoveEnum copyOrMoveEnum = CopyOrMoveEnum.Copy)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)));

            this.DragAndDropToFolder(
                new EdgeRecentFoldersDialog().GetFolderElement(targetFolder),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)),
                copyOrMoveEnum);

            return this.Header.GetInfoMessage();
        }

        /// <summary>
        /// Drag grid item to a folder
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <param name="gridItemTitle"></param>
        /// <returns>Drag drop box text.</returns>
        public string DragGridItemToFolder(
           string targetFolder,
           string gridItemTitle)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)));

            var recentFoldersDialog = new EdgeRecentFoldersDialog();
         
            DriverExtensions.DragAndHold(
              recentFoldersDialog.GetFolderElement(targetFolder),
               DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)));

            return recentFoldersDialog.GetDragDropBoxText();       
        }

        /// <summary>
        /// This method waits for the New link found in the left pane
        /// and allows the user to click the New link to bring up the 
        /// New Folder Dialog
        /// </summary>
        /// <param name="folderName"> Folder name </param>
        public override void CreateNewFolder(string folderName)
        {
            this.EdgeToolbar.ClickToolbarElement<EdgeNewFolderDialog>(EdgeToolbarElements.NewFolder).CreateNewFolder(folderName);
        }

        /// <summary>
        /// This method is used to expand the collpase arrow of treepane in folders 
        /// </summary>
       
        public void TreepaneExpand()
        {
            string ToogleText = DriverExtensions.GetText(TreeViewPaneCollapseLocator);
            if(ToogleText.Equals("Expand filters panel"))
            {
                TreeViewPanelToggle.ToggleState<EdgeResearchOrganizerPage>(true);
            }
        }

        /// <summary>
        /// Hover over smart terms information icon displayed
        /// </summary>
        /// <returns>
        /// The <see cref="EdgeResearchOrganizerPage"/>.
        /// </returns>
        public EdgeResearchOrganizerPage HoverOverQuickAccessPanel()
        {
            DriverExtensions.Hover(QuickAccessPanelLocator);
            return this;
        }

        /// <summary>
        /// This method performs search within query in folders 
        /// </summary>
        public void searchAndClickEnterinDailogBox(string Query)
        {
            DriverExtensions.Click(SearchQueryinFolder);
            // new Textbox(SearchQueryinFolder).SendKeys(Query);
            // new Textbox(SearchQueryinFolderinDialogBox).SendKeys(Query);
            DriverExtensions.SetTextField(Query, SearchQueryinFolderinDialogBox);
            Thread.Sleep(5000);
            DriverExtensions.Click(ContinueButton);
            DriverExtensions.WaitForElementNotDisplayed(50000, LoadingSpinnerLocator);
            //DriverExtensions.WaitForElementDisplayed(CasesCount, 50000);
        }
    }    
}