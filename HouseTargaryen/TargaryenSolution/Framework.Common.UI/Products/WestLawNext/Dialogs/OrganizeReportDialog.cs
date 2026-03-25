namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Describe Organize Report dialog on the Research Report Page (appears after clicking on the 'Organize Report' link)
    /// </summary>
    public class OrganizeReportDialog : BaseModuleRegressionDialog
    {
        private static readonly By DialogTitleLocator = By.XPath("//div[@class = 'co_overlayBox_headline']");

        private static readonly By SaveButtonLocator = By.XPath("//div[@class='co_overlayBox_optionsBottom']//*[text() ='Save']");

        private static readonly By PageBreakWarningMessageLocator = By.XPath("//div[contains(@ng-show , 'PageBreakWarning')]");

        private static readonly By OrganizeReportListItemLocator = By.XPath("//ul[@id='draggableSortContainer']/li");

        private static readonly By OrganizeReportDropdownLocator = By.XPath("//select[@name='sortOptions']");

        private static readonly By MoveToTopButtonLocator = By.XPath("//button[@ng-click='moveToTop()']");

        private static readonly By MoveUpButtonLocator = By.XPath("//button[@ng-click='moveUp()']");

        private static readonly By MoveDownButtonLocator = By.XPath("//button[@ng-click='moveDown()']");

        private static readonly By MoveToBottomButtonLocator = By.XPath("//button[@ng-click='moveToBottom()']");

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizeReportDialog"/> class. 
        /// </summary>
        public OrganizeReportDialog()
        {
            DriverExtensions.WaitForElement(DialogTitleLocator);
        }

        /// <summary>
        /// Font Size DropDown
        /// </summary>
        public IDropdown<OrganizeReportSortOptions> OrganizeReportDropdown { get; } = new Dropdown<OrganizeReportSortOptions>(OrganizeReportDropdownLocator);

        /// <summary>
        /// The click move to top.
        /// </summary>
        public void ClickMoveToTop() => this.ClickElement(MoveToTopButtonLocator);

        /// <summary>
        /// The click move up.
        /// </summary>
        public void ClickMoveUp() => this.ClickElement(MoveUpButtonLocator);

        /// <summary>
        /// The click move to bottom.
        /// </summary>
        public void ClickMoveToBottom() => this.ClickElement(MoveToBottomButtonLocator);

        /// <summary>
        /// The click move down.
        /// </summary>
        public void ClickMoveDown() => this.ClickElement(MoveDownButtonLocator);

        /// <summary>
        /// Click on the 'Save' button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(SaveButtonLocator);

        /// <summary>
        /// Verify that page break warning message is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPageBreakWarningMessageDisplayed() => DriverExtensions.IsDisplayed(PageBreakWarningMessageLocator);

        /// <summary>
        /// TODO make this method private, and don't use Items in tests
        /// The get organize report list items.
        /// </summary>
        /// <returns>The <see cref="OrganizeReportListItem"/>.</returns>
        public List<OrganizeReportListItem> GetOrganizeReportListItems()
        {
            DriverExtensions.WaitForElementDisplayed(OrganizeReportListItemLocator);
            return DriverExtensions.GetElements(OrganizeReportListItemLocator)
                                   .Select(item => new OrganizeReportListItem(item)).ToList();
        }

        /// <summary>
        /// Click on first item
        /// </summary>
        /// <param name="index"> The index. (start from 0) </param>
        /// <returns>
        /// The <see cref="OrganizeReportDialog"/>. 
        /// </returns>
        public OrganizeReportDialog ClickOrganizeReportItem(int index = 0)
        {
            this.GetOrganizeReportListItems()[index].Click();
            return this;
        }
    }
}