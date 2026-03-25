namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Class describes Key Numbers Results section in the Research Recommendations Slider
    /// </summary>
    public class KeyNumbersComponent : BaseModuleRegressionComponent
    {
        private static readonly By HierarchyInfoBoxLocator =
            By.XPath("//div[@class='co_keyNumberHierarchyHoverContainers co_infoBox bottom']");

        private static readonly By ContainerLocator = By.Id("coid_keynum_recom_results");

        private static readonly By KeyNumberHierarchyLocator =
            By.XPath("//div[@id='coid_keynum_recom_results']//a[@class='co_keyNumberHierarchyIcon']");

        private static readonly By KeyNumberItemLocator =
            By.XPath("//div[@id='coid_keynum_recom_results']//a[@class='co_keyNumberResultItemLink']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the Hierarchy icon for the specified Key Number item
        /// </summary>
        /// <param name="itemNumber">number of key number item with hierarchy icon. Starts from zero</param>
        public void ClickHierarchyForKeyNumberItem(int itemNumber)
        {
            DriverExtensions.GetElements(KeyNumberHierarchyLocator).ElementAt(itemNumber).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click the key number link specified by number in the list
        /// </summary>
        /// <param name="itemNumber">number of item to click. Starts from zero</param>
        /// <returns>The <see cref="CategorySearchResultPage"/>.</returns>
        public CategorySearchResultPage ClickKeyNumberItemByNumber(int itemNumber)
        {
            DriverExtensions.GetElements(KeyNumberItemLocator).ElementAt(itemNumber).Click();
            return new CategorySearchResultPage();
        }

        /// <summary>
        /// Get number of key number result items present
        /// </summary>
        /// <returns>Number of key number result items</returns>
        public int GetKeyNumberItemsNumber() => DriverExtensions.GetElements(KeyNumberItemLocator).Count;

        /// <summary>
        /// Get list of Key Number items text
        /// </summary>
        /// <returns>list of Key Number items text</returns>
        public List<string> GetKeyNumberItemsText() => DriverExtensions.GetElements(KeyNumberItemLocator).Select(i => i.Text).ToList();

        /// <summary>
        /// Verify info box containing the key number hierarchy appears
        /// </summary>
        /// <returns> true if hierarchy info box appeared and contains some text, false otherwise</returns>
        public bool IsHierarchyInfoBoxDisplayed() =>
            DriverExtensions.IsDisplayed(HierarchyInfoBoxLocator, 5) && !string.IsNullOrEmpty(DriverExtensions.WaitForElementDisplayed(HierarchyInfoBoxLocator).Text);

        /// <summary>
        /// Verify that at least one Key Number Result item is present
        /// </summary>
        /// <returns>true if at least one Key Number Result item is present, false otherwise</returns>
        public bool IsKeyNumberItemDisplayed() => DriverExtensions.IsDisplayed(KeyNumberItemLocator, 5);

        /// <summary>
        /// Verify the recommended key numbers section is present on the slider
        /// </summary>
        /// <returns>true if recommended key numbers section is present, false otherwise</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Verify Key Number Result list item specified by number has some text
        /// </summary>
        /// <param name="itemNumber">number of item to verify. Starts from zero</param>
        /// <returns>true if specified Key Number item has some text, true otherwise</returns>
        public bool IsTextInKeyNumberItemDisplayed(int itemNumber) =>
            DriverExtensions.GetElements(KeyNumberItemLocator).Count > 0 && !string.IsNullOrEmpty(DriverExtensions.GetElements(KeyNumberItemLocator).ElementAt(itemNumber).Text);
    }
}