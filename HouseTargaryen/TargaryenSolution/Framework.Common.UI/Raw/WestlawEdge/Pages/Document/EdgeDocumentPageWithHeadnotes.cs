namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document page with headnotes.
    /// </summary>
    public class EdgeDocumentPageWithHeadnotes : EdgeCommonDocumentPage
    {
        private static readonly By WestHeadnotesHeaderLocator = By.ClassName("co_headnoteHeader");

        private static readonly By DisplayFancyHeadnotesButtonLocator =
            By.XPath("//button[contains(@class,'Headnote-list-view')]");

        private static readonly By HideFancyHeadnotesButtonLocator =
            By.XPath("//button[contains(@class,'Headnote-grid-view')]");

        private static readonly By InternalLinkLocator = By.XPath(
            "//a[contains(@id, 'co_pp_HNF') and contains(@class,'co_internalLink co_headnoteLink')]");

        private static readonly By HeadnoteTopicCellLocator = By.ClassName("co_headnoteTopicsCell");

        private static readonly By KeyNumberLinkLocator =
            By.CssSelector("div.co_fancyKeyciteContainer div.co_headnoteKeyPair a[id^='co_link_']");

        private static readonly By SingleHeadnotesNoteIcon =
            By.XPath("//a[contains(@class, 'HeadnoteSingleNote') and @title = 'Maximize Note']");

        private static readonly By MultipleHeadnotesNoteIcon =
            By.XPath("//a[contains(@class, 'HeadnoteMultipleNotes') and @title = 'Maximize Note']");

        /// <summary>
        /// Internal Users Only Block
        /// </summary>
        public InternalUsersOnlyComponent InternalUsersOnlyBlock { get; } = new InternalUsersOnlyComponent();

        /// <summary>
        /// Key Number Hierarchy Block
        /// </summary>
        public KeyNumberHierarchyComponent KeyNumberHierarchyBlock { get; } = new KeyNumberHierarchyComponent();

        /// <summary>
        /// West Headnotes Component
        /// </summary>
        public WestHeadnotesComponent WestHeadnotesComponent { get; } = new WestHeadnotesComponent();

        /// <summary>
        /// Checks whether FancyHeadnotes are present in the document
        /// </summary>
        /// <returns>True if headnotes are present</returns>
        public bool AreFancyHeadnotesDisplayed => !DriverExtensions.IsDisplayed(HeadnoteTopicCellLocator, 5);

        /// <summary>
        /// Checks if WestHeadnotes are opened
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AreWestHeadnotesOpen =>
            !DriverExtensions.GetAttribute("class", WestHeadnotesHeaderLocator).Contains("co_headnoteInactive");

        /// <summary>
        /// The is key number link present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsKeyNumberLinkDisplayed() => DriverExtensions.IsDisplayed(KeyNumberLinkLocator);

        /// <summary>
        /// Shows Fancy Headnotes
        /// </summary>
        public void ShowFancyHeadnotes() =>
            DriverExtensions.WaitForElementDisplayed(DisplayFancyHeadnotesButtonLocator).CustomClick();

        /// <summary>
        /// Hides Fancy Headnotes
        /// </summary>
        public void HideFancyHeadnotes() =>
            DriverExtensions.WaitForElementDisplayed(HideFancyHeadnotesButtonLocator).Click();

        /// <summary>
        /// Clicks the internal headnotes link
        /// </summary>
        public void ClickInternalHeadnotesLink() => DriverExtensions.GetElement(InternalLinkLocator).CustomClick();

        /// <summary>
        /// Is single headnotes note icon displayed
        /// </summary>
        /// <returns>True if icon is displayed, false otherwise.</returns>
        public bool IsSingleHeadnotesNoteIconDisplayed() => DriverExtensions.IsDisplayed(SingleHeadnotesNoteIcon, 5);

        /// <summary>
        /// Is multiple headnotes note icon displayed
        /// </summary>
        /// <returns>True if icon is displayed, false otherwise.</returns>
        public bool IsMultipleHeadnotesNoteIconDisplayed() => DriverExtensions.IsDisplayed(MultipleHeadnotesNoteIcon, 5);

        /// <summary>
        /// Is the List View toggle button present.
        /// </summary>
        /// <returns>True if List View button is displayed, false otherwise.</returns>
        public bool IsListViewToggleButtonDisplayed() => DriverExtensions.IsDisplayed(DisplayFancyHeadnotesButtonLocator, 5);

        /// <summary>
        /// Is the Grid View toggle button present.
        /// </summary>
        /// <returns>True if Grid View is displayed, false otherwise.</returns>
        public bool IsGridViewToggleButtonDisplayed() => DriverExtensions.IsDisplayed(HideFancyHeadnotesButtonLocator, 5);

        /// <summary>
        /// Clicks single headnotes note icon
        /// </summary>
        public void ClickSingleHeadnotesNoteIcon()
        {       
            DriverExtensions.Click(SingleHeadnotesNoteIcon);
            DriverExtensions.WaitForJavaScript();
        }
        

        /// <summary>
        /// Clicks multiple headnotes note icon
        /// </summary>
        public void ClickMultipleHeadnotesNoteIcon()
        {
            DriverExtensions.Click(MultipleHeadnotesNoteIcon);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Toggle West headnotes
        /// </summary>
        /// <param name="toggle">True to expand headnotes, false to collapse.</param>
        public void ToggleWestHeadnotes(bool toggle)
        {
            if (toggle != this.AreWestHeadnotesOpen)
            {
                DriverExtensions.GetElement(WestHeadnotesHeaderLocator).CustomClick();
            }

            DriverExtensions.WaitForJavaScript();
        }   
    }
}