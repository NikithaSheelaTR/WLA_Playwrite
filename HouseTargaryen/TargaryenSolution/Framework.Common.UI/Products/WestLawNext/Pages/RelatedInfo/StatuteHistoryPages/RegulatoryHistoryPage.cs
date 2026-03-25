namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RegulatoryHistoryPage
    /// </summary>
    public class RegulatoryHistoryPage : TabPage
    {
        private static readonly By RegulatoryHistoryMaterialRankLocator = By.ClassName("co_relatedInfo_resultList_rank");
        

        /// <summary>
        /// Return the number of items in the body of the page.
        /// </summary>
        /// <returns>Number of items</returns>
        public int GetRegulatoryHistoryMaterialsItemCount()
            => DriverExtensions.GetElements(this.ContentResultContainer, RegulatoryHistoryMaterialRankLocator).Count;
    }
}