namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using Framework.Common.UI.Enums.RI;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.RI;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// StatuteHistoryPage
    /// </summary>
    public class StatuteHistoryPage : TabPage
    {
        private static readonly EnumPropertyMapper<HistorySections, WebElementInfo> HistorySectionsMap =
            EnumPropertyModelCache.GetMap<HistorySections, WebElementInfo>();

        private static readonly By HistorySectionTitlesLocator = By.XPath("./div/div/h3");

        /// <summary>
        /// Click On History Section
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="historySection">The history Section.</param>
        /// <returns>The new instance of T page.</returns>
        public T ClickOnkHistorySection<T>(HistorySections historySection) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.Id(HistorySectionsMap[historySection].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets RegulatoryHistorySectionText
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRegulatoryHistorySectionText()
            => DriverExtensions.WaitForElement(By.Id(HistorySectionsMap[HistorySections.RegulatoryHistory].Id)).Text;

        /// <summary>
        /// Get the section header text.
        /// </summary>
        /// <param name="historySection">
        /// The history Section.
        /// </param>
        /// <returns>Section Header as string</returns>
        public string GetSectionHeader(HistorySections historySection)
            => DriverExtensions.WaitForElement(
                    DriverExtensions.WaitForElement(By.Id(HistorySectionsMap[historySection].Id)),
                    HistorySectionTitlesLocator).Text;

        /// <summary>
        /// Verifies if category section is clickable
        /// </summary>
        /// <param name="historySection"> The history Section. </param>
        /// <returns>true or false</returns>
        public bool IsHistorySectionClickable(HistorySections historySection)
            => DriverExtensions.WaitForElement(By.Id(HistorySectionsMap[historySection].Id)).InnerHtml().Contains("href");

        /// <summary>
        /// Is Regulatory History Section Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRegulatoryHistorySectionDisplayed()
            => DriverExtensions.IsDisplayed(By.Id(HistorySectionsMap[HistorySections.RegulatoryHistory].Id));
    }
}