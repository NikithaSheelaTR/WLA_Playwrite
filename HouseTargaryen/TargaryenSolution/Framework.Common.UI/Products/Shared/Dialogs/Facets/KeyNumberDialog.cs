namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents the modal that pops up after click on Select button in the Key Number facet
    /// </summary>
    public class KeyNumberDialog : BaseModuleRegressionDialog
    {
        private const string AddItemButtonLocatorLctMask = "//div[@class='co_listItem '][contains(.,{0})]/a[@role='checkbox']";

        private const string KeyNumberItemLinkLctMask = "//a[@linktype='item_expand']//span[contains(.,{0})]";

        private static readonly By FilterButtonLocator = By.Id("co_facet_keynumber_filterButton");

        /// <summary>
        /// This method is used to click on the Add button near the specified Key Number item
        /// </summary>
        /// <param name="itemToAdd">item to click the Add button for</param>
        /// <returns>This object for fluent interfaces</returns>
        public KeyNumberDialog ClickAddKeyNumber(string itemToAdd)
        {
            DriverExtensions.WaitForElementDisplayed(SafeXpath.BySafeXpath(AddItemButtonLocatorLctMask, itemToAdd)).Click();
            return this;
        }

        /// <summary>
        /// This method is used to click on the specified Key Number item
        /// </summary>
        /// <param name="item">item to click on</param>
        /// <returns>This object for fluent interfaces</returns>
        public KeyNumberDialog ClickKeyNumberItem(string item) =>
            this.ClickElement<KeyNumberDialog>(SafeXpath.BySafeXpath(KeyNumberItemLinkLctMask, item));

        /// <summary>
        /// This method is used to click Filter button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickFilterButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(FilterButtonLocator);
    }
}