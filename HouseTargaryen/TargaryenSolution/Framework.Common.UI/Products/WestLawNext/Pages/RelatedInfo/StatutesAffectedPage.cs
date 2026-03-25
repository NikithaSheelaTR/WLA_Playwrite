namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Statutes Affected Page
    /// </summary>
    public class StatutesAffectedPage : TabPage
    {
        private static readonly By OrderedListLocator = By.XPath("//ol[@id='co_relatedInfo_orderedList']/li");

        private static readonly By AllResultsSelectedMessageLocator =
            By.XPath("//label[@*='coid_relatedinfo_results_select_all']");

        private static readonly By OrderedItemIsCheckedLocator =
            By.XPath(".//input[contains(@id, 'coid_relatedInfo_resultList_checkbox_')]");
        

        /// <summary>
        /// Check the checkbox in the list for the given the index.
        /// </summary>
        /// <param name="itemIndex">
        /// checkbox index
        /// </param>
        /// <param name="check">
        /// The check.
        /// </param>
        public void ClickOrderedListCheckbox(int itemIndex, bool check)
        {
            IWebElement container = DriverExtensions.GetElements(OrderedListLocator).ElementAt(itemIndex);
            DriverExtensions.SetCheckbox(check, container, OrderedItemIsCheckedLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get count of items in the ordered list.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetCountOrderedListItems() => DriverExtensions.GetElements(OrderedListLocator).Count;

        /// <summary>
        /// Is the all results selected message displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAllResultsSelectedMessageDisplayed() => DriverExtensions.IsDisplayed(AllResultsSelectedMessageLocator);
    }
}