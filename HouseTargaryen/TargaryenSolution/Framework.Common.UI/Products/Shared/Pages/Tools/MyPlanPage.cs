namespace Framework.Common.UI.Products.Shared.Pages.Tools
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The My Content page in Tools
    /// </summary>
    public class MyPlanPage : BaseModuleRegressionPage
    {
        private static readonly By SelectedSortOptionLocator = By.Id("jumpAnchorControl_disabled");

        private static readonly By CompanyNameTitleLocator = By.XPath("//h1");

        private static readonly By DownloadPdfLinkLocator = By.Id("pdfDownloadContainer");

        private static readonly By PageDescriptionLocator = By.XPath("//div[@class='co_innertube']/p");

        private static readonly By PlanSearchResultInfoLocator = By.Id("plansSearchResultInfo");

        private static readonly By SortByContentTypeDropdownLocator = By.Id("jumpDropDownControl");

        private static readonly By StaticTextLocator = By.XPath("//div[@class='co_textCenter']/p");

        private static readonly By MyPlanSortDropdownLocator = By.Id("sortingControl_select");

        private EnumPropertyMapper<MyPlanDeliveryOption, WebElementInfo> myPlanDeliveryMap;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MyPlanPage"/> class. 
        /// </summary>
        /// <param name="driver">driver </param>
        public MyPlanPage(IWebDriver driver)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyPlanPage"/> class. 
        /// </summary>
        public MyPlanPage()
        {
        }

        /// <summary>
        /// Override the current header with a more in depth one
        /// </summary>
        public WestlawNextHeaderComponent Header { get; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Sort By dropdown
        /// </summary>
        public IDropdown<MyPlanSortOptions> SortByDropdown { get; } = new Dropdown<MyPlanSortOptions>(MyPlanSortDropdownLocator);

        /// <summary>
        /// Gets the MyPlanDeliveryOption enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<MyPlanDeliveryOption, WebElementInfo> MyPlanDeliveryMap
            =>
                this.myPlanDeliveryMap =
                    this.myPlanDeliveryMap ?? EnumPropertyModelCache.GetMap<MyPlanDeliveryOption, WebElementInfo>();

        /// <summary>
        /// Checks if the sort option given is currently a link or not
        /// </summary>
        /// <param name="sortOptionText">The sort option to check</param>
        /// <returns>If the option is a link</returns>
        public bool IsSortOptionSelected(string sortOptionText)
        {
            IWebElement sortOptionElement = DriverExtensions.GetElement(SelectedSortOptionLocator);
            return sortOptionElement.Displayed && sortOptionElement.Text.Equals(sortOptionText);
        }

        /// <summary>
        /// Clicks the item in the sort bar when set to By Name, usually a letter.
        /// </summary>
        /// <param name="sortOptionText"> The sort Option Text </param>
        public void ClickAlphaSortOption(string sortOptionText) => DriverExtensions.GetElement(By.LinkText(sortOptionText)).Click();

        /// <summary>
        /// Clicks the download PDF link, which brings up a delivery light-box
        /// </summary>
        /// <returns>The deliveryDialog that pops up</returns>
        public ReadyForDeliveryDialog ClickDownloadPdf()
        {
            DriverExtensions.GetElement(DownloadPdfLinkLocator).Click();
            return new ReadyForDeliveryDialog();
        }

        /// <summary>
        /// Clicks the download PDF link, which selects the input option (after a search) and brings up a downloading light-box
        /// </summary>
        /// <param name="downloadOption"> The download Option </param>
        /// <returns>
        /// The delivery dialog that pops up
        /// </returns>
        public ReadyForDeliveryDialog ClickDownloadPdf(MyPlanDeliveryOption downloadOption)
        {
            DriverExtensions.GetElement(DownloadPdfLinkLocator).Click();
            DriverExtensions.GetElement(By.Id(this.MyPlanDeliveryMap[downloadOption].Id)).Click();
            return new ReadyForDeliveryDialog();
        }

        /// <summary>
        /// Clicks the specified content link
        /// </summary>
        /// <typeparam name="T">content Link Name</typeparam>
        /// <param name="contentLinkName">The content name to click</param>
        /// <returns>new page</returns>
        public T ClickMyContentLink<T>(string contentLinkName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(By.LinkText(contentLinkName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Whether the sorting dropdown for content types is on the page
        /// </summary>
        /// <returns>if the dropdown exists</returns>
        public bool IsContentTypeSortDropdownDisplayed() => DriverExtensions.IsDisplayed(SortByContentTypeDropdownLocator);

        /// <summary>
        /// Whether the sorting by content type dropdown option is in the dropdown
        /// </summary>
        /// <param name="option">The option to check for</param>
        /// <returns>if the dropdown exists</returns>
        public bool ContentTypeSortDropdownOptionExists(string option)
            =>
                DriverExtensions.GetDropdownOptionElements(SortByContentTypeDropdownLocator)
                                .Any(elem => elem.Text.Equals(option));

        /// <summary>
        /// Selects the given content type dropdown option to sort by
        /// </summary>
        /// <param name="option">The option to select</param>
        public void ClickContentTypeSortOption(string option) => DriverExtensions.SetDropdown(option, SortByContentTypeDropdownLocator);

        /// <summary>
        /// Gets the text of the page title which represents the company name
        /// </summary>
        /// <returns>The company name string</returns>
        public string GetBusinessTitleName() => DriverExtensions.GetElement(CompanyNameTitleLocator).Text;

        /// <summary>
        /// Gets the text of the page description
        /// </summary>
        /// <returns>The description text</returns>
        public string GetPageDescriptionText() => DriverExtensions.GetElement(PageDescriptionLocator).Text;

        /// <summary>
        /// Gets the search result info for searches within the content
        /// </summary>
        /// <returns>The search result info</returns>
        public string GetSearchResultInfoText() => DriverExtensions.GetElement(PlanSearchResultInfoLocator).Text;

        /// <summary>
        /// Checks if the static text is visible
        /// </summary>
        /// <returns>If the static text is visible</returns>
        public bool IsStaticTextDisplayed() => DriverExtensions.IsDisplayed(StaticTextLocator);

        /// <summary>
        /// Checks if the static text is visible and equal to the text
        /// </summary>
        /// <returns>If the static text is visible and equal to the input</returns>
        public string GetStaticText() => DriverExtensions.IsDisplayed(StaticTextLocator)
                                             ? DriverExtensions.GetElement(StaticTextLocator).Text
                                             : string.Empty;

        /// <summary>
        /// Checks if the sort option given is currently the visible page 'title'
        /// </summary>
        /// <param name="sortOptionText">The sort option to check</param>
        /// <returns>If the sort option given is currently the visible page 'title'</returns>
        public bool IsSortOptionHeaderDisplayed(string sortOptionText)
            => DriverExtensions.IsDisplayed(By.Id($"planListContainer{sortOptionText}"));
    }
}