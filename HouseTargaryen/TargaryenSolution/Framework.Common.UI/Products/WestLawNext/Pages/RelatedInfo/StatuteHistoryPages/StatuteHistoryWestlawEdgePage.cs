namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.RI;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Statute History Page for Westlaw Edge and Precision
    /// </summary>
    public class StatuteHistoryWestlawEdgePage : TabPage
    {
        private static readonly EnumPropertyMapper<HistorySections, WebElementInfo> HistorySectionsMap =
            EnumPropertyModelCache.GetMap<HistorySections, WebElementInfo>("WestlawEdge");

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
    }
}