namespace Framework.Common.UI.Products.Shared.Pages.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document page with headnotes.
    /// </summary>
    public class DocumentPageWithHeadnotes : CommonDocumentPage
    {
        private const string CustomKeyNumberLinkLctMask = "//span[@class='co_headnoteRefNumber']/a[text()='{0}']";

        private static readonly By HeadnotesHeaderLocator = By.Id("co_headnoteHeader");

        private static readonly By HeadnotesLocator = By.Id("co_headnotes");

        private static readonly By InternalHeadnotesLocator = By.XPath("//a[contains(@id, 'co_pp_HNF') and contains(@class,'co_internalLink co_headnoteLink')]");

        private static readonly By KeyNumberLinkLocator = By.XPath("//span[@class='co_headnoteRefNumber']/a");

        private static readonly By ExpandedHeadNotesLocator = By.Id("co_expandedHeadnotes");

        private static readonly By ChangeViewButton = By.Id("co_headnotesShowOrHideFancy");

        private static readonly By GridView = By.XPath("//button[contains(@class,'Headnote-grid-view')]");

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

        private IWebElement HeadnotesHeader => DriverExtensions.WaitForElement(HeadnotesHeaderLocator);

        /// <summary>
        /// Determines whether Fancy Headnotes are displayed or not
        /// </summary>
        /// <returns>True if displayed, false otherwise </returns>
        public bool AreFancyHeadnotesDisplayed()
            => DriverExtensions.WaitForElementDisplayed(HeadnotesLocator).GetAttribute("class").Contains("co_fancyHeadnotes");

        /// <summary>
        /// Determines whether West Headnotes are opened or not
        /// </summary>
        /// <returns> True if opened, false otherwise </returns>
        public bool AreWestHeadnotesExpanded() => !this.HeadnotesHeader.GetAttribute("class").Contains("co_headnoteInactive");

        /// <summary>
        /// Clicks the internal headnotes link
        /// </summary>
        public void ClickInternalHeadnotesLink() => DriverExtensions.WaitForElement(InternalHeadnotesLocator).Click();

        /// <summary>
        /// Get count of west headnotes
        /// </summary>
        /// <returns> Count value </returns>
        public int GetWestHeadnotesCount() => HeadnotesHeader.Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Turns off Fancy Headnotes but only if they are shown
        /// </summary>
        public void HideFancyHeadnotes() => this.ChangeFancyHeadnotesView(false);

        /// <summary>
        /// Turns on Fancy Headnotes but only if they are not already shown
        /// </summary>
        public void ShowFancyHeadnotes() => this.ChangeFancyHeadnotesView(true);

        /// <summary>
        /// The is key number link present
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsKeyNumberLinkDisplayed() => DriverExtensions.IsDisplayed(KeyNumberLinkLocator);

        /// <summary>
        /// Show West Headnotes
        /// </summary>
        public void ShowWestHeadnotes()
        {
            if (!this.AreWestHeadnotesExpanded())
            {
                this.ToggleWestHeadnotes();
            }
        }

        /// <summary>
        /// Closes the West Headnotes section
        /// </summary>
        public void ToggleWestHeadnotes()
        {
            if (this.HeadnotesHeader.Displayed)
            {
                this.HeadnotesHeader.Click();
            }

            DriverExtensions.WaitForElement(ExpandedHeadNotesLocator);
        }

        /// <summary>
        /// The change fancy headnotes view.
        /// </summary>
        /// <param name="display">
        /// The display.
        /// </param>
        private void ChangeFancyHeadnotesView(bool display)
        {
            if (this.AreFancyHeadnotesDisplayed() != display)
            {
                DriverExtensions.WaitForElement(ChangeViewButton).Click();
            }
        }

        /// <summary>
        /// Clicks on KeyNumber in HeadNoteson Document
        /// </summary>
        /// <typeparam name="T">Page we'll end up on</typeparam>
        /// <param name="keyNumber">KeyNumber to click</param>
        /// <returns>New instance of a document page</returns>
        public T ClickKeyNumberInHeadnotes<T>(string keyNumber) where T : ICreatablePageObject
        {
            if (!this.IsGridViewSelected())
            {
                DriverExtensions.WaitForElement(GridView).Click();
            }

            DriverExtensions.WaitForElement(By.XPath(string.Format(CustomKeyNumberLinkLctMask, keyNumber))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks whther the Grid view is Slected or Not
        /// </summary>
        /// <returns></returns>
        public bool IsGridViewSelected()
        {
            return DriverExtensions.WaitForElement(GridView).GetAttribute("class").Contains("co_selected");
        }
    }
}