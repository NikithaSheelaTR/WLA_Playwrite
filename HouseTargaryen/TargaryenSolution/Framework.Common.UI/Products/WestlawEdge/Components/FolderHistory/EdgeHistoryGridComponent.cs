namespace Framework.Common.UI.Products.WestlawEdge.Components.FolderHistory
{
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    /// <summary>
    /// History Grid Component
    /// </summary>
    public class EdgeHistoryGridComponent : HistoryGridComponent
    {
        private const string ImpliedOverrulingMask = "//a[@class='co_impliedOverrulingsFlagSm' and contains(@href, {0})]";

        /// <summary>
        /// Checks if the Implied overruling flag is displayed for the item with the given guid
        /// </summary>
        /// <param name="itemGuid">
        /// The item Guid.
        /// </param>
        /// <returns>
        /// True if flag is displayed, false otherwise. 
        /// </returns>
        public bool IsImpliedOverrulingFlagDisplayedForItem(string itemGuid) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ImpliedOverrulingMask, itemGuid), 5);
    }
}
