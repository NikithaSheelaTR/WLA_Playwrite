namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// VersionsPage
    /// </summary>
    public class VersionsPage : TabPage
    {
        private const string PriorVersionDocumentLctMask =
            "(//*[@id = 'co_relatedInfo_resultList_otherVersions']//a[contains(@id,'coid_relatedInfo_resultList_documentLink')])[{0}]";

        private static readonly By LatestLegislationOrActionElementLocator =
            By.XPath("//div[@class='coid_relatedInfo_contentResult_container']/div/h3");

        private static readonly By PriorVersionsGridLocator =
            By.XPath("//div[@id='co_relatedInfo_resultList_otherVersions'][last()]");

        private static readonly By PriorVersionsGridMinimizeExpandButtonLocator =
            By.Id("coid_relatedInfo_Prior_Versions");

        private static readonly By PriorVersionsListLocator =
            By.XPath("//div[contains(@id, 'co_relatedInfo_resultList_otherVersions')][last()]//ul[@id = 'co_relatedInfo_resultList_currentVersions']");

        private static readonly By TitleLinkLocator =
            By.XPath(".//a[contains(@class,'co_relatedInfo_HistoryItem_Title')]");

        private static readonly By CurrentVersionLinkLocator =
            By.XPath("//ul[@id='co_relatedInfo_resultList_currentVersions']//a[contains(@id,'coid_relatedInfo_resultList_documentLink_')]");

        private static readonly By EmptyResultContainerLocator = By.ClassName("co_relatedInfo_contentResult_containerEmpty");

        private static readonly By CurrentVersionCheckboxLocator =
            By.XPath("//ul[@id='co_relatedInfo_resultList_currentVersions']//input[@type='checkbox']");

        /// <summary>
        /// Tools And Resources Widget
        /// </summary>
        public ToolsAndResourcesFacetComponent ToolsAndResourcesComponent { get; set; } = new ToolsAndResourcesFacetComponent();

        /// <summary>
        /// Clicks on prior version by index.
        /// </summary>
        /// <typeparam name="T">T
        /// </typeparam>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// New instance of T page.
        /// </returns>
        public T ClickOnPriorVersionByIndex<T>(int index) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(string.Format(PriorVersionDocumentLctMask, index)));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets Prior Versions Grid Minimize Expand Button
        /// </summary>
        public void ClickPriorVersionsGridMinimizeExpandButton()
            => DriverExtensions.Click(PriorVersionsGridMinimizeExpandButtonLocator);

        /// <summary>
        /// Get count of checkbox in the prior versions grid
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetCountOfPriorVersionsCheckboxes()
            => this.GetChildCheckboxes(DriverExtensions.WaitForElement(PriorVersionsGridLocator)).Count;

        /// <summary>
        /// Get Prior Versions Grid Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetPriorVersionsGridText() => DriverExtensions.WaitForElement(PriorVersionsGridLocator).Text;

        /// <summary>
        /// Gets all title links elements in prior versions grid
        /// </summary>
        /// <returns>List of link's elements</returns>
        public List<string> GetPriorVersionsTitleLinks()
            =>
                DriverExtensions.GetElements(
                    DriverExtensions.WaitForElement(PriorVersionsGridLocator),
                    TitleLinkLocator).Select(li => li.Text).ToList();

        /// <summary>
        /// Is all checkbox in the prior versions grid selected
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAllPriorVersionsCheckboxesSelected()
            => this.GetChildCheckboxes(DriverExtensions.WaitForElement(PriorVersionsGridLocator))
               .TrueForAll(c => c.Selected);

        /// <summary>
        /// Gets LatestLegislationOrActionText
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetLatestLegislationOrActionText()
            => DriverExtensions.WaitForElement(LatestLegislationOrActionElementLocator).Text;

        /// <summary>
        /// Determine if the prior versions grid is collapsed.
        /// </summary>
        /// <returns>True if collapsed</returns>
        public bool IsPriorVersionsCollapsed()
            => DriverExtensions.WaitForElement(PriorVersionsListLocator).GetAttribute("class").Contains("co_hideState");

        /// <summary>
        /// Is Prior Versions Grid Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPriorVersionsGridDisplayed() => DriverExtensions.IsDisplayed(PriorVersionsGridLocator);

        /// <summary>
        /// Determines if the versions page is displayed
        /// </summary>
        /// <returns>True if it is</returns>
        public bool IsVersionsPageDisplayed() => this.Url.ToLower().Contains("riversions.html");

        /// <summary>
        /// Verifies if Latest Legislation Or Action Text exists under select all items checkbox
        /// </summary>
        /// <returns>True or false</returns>
        public bool IsLatestLegislationOrActionTextDisplayed()
            => DriverExtensions.IsDisplayed(LatestLegislationOrActionElementLocator);

        /// <summary>
        /// Determines if the minimize button for the prior versions grid has a plus sign to expand or a minus sign to collapse
        /// </summary>
        /// <returns>True if its a plus sign</returns>
        public bool IsPriorVersionsCollapseButtonHasPlus()
            => DriverExtensions.WaitForElement(PriorVersionsGridMinimizeExpandButtonLocator)
               .GetAttribute("class")
               .Contains("co_genericExpand");

        /// <summary>
        /// Click on Current Version Link
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <returns> The <see cref="CommonDocumentPage"/>.  </returns>
        public T ClickCurrentVersionLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(CurrentVersionLinkLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Current Version Link text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCurrentVersionLinkText() => DriverExtensions.WaitForElement(CurrentVersionLinkLocator).Text;

        /// <summary>
        /// Gets EmptyResultContainerText
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetEmptyResultContainerText() => DriverExtensions.WaitForElement(EmptyResultContainerLocator).Text;

        /// <summary>
        /// Is Current Version Checkbox Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCurrentVersionCheckboxDisplayed() => DriverExtensions.IsDisplayed(CurrentVersionCheckboxLocator);
    }
}