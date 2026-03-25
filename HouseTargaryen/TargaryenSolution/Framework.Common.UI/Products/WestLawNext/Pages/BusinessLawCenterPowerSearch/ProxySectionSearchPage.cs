namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    /// <summary>
    /// Proxy Section Search Page
    /// </summary>
    public class ProxySectionSearchPage : CommonAdvancedSearchPage
    {
        private const string AreaCheckboxLctMask = "//label[text() = {0}]/input | //label[text() = {0}]";

        /// <summary>
        /// Selects Area of Expertise checkbox
        /// </summary>
        /// <param name="area">Area to select</param>
        /// <param name="setTo">The select</param>
        public new void SelectAreaOfExpertise(string area, bool setTo = true)
        {
            DriverExtensions.GetElement(SafeXpath.BySafeXpath(AreaCheckboxLctMask, area)).SetCheckbox(setTo);
            DriverExtensions.WaitForJavaScript();
        }
    }
}
