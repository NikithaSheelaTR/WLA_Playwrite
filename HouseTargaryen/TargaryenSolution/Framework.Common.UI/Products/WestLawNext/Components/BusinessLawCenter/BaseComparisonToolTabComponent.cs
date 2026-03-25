namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Tab component for ViewSavedComparisonsTabComponent and SelectToCompareTabComponent
    /// </summary>
    public abstract class BaseComparisonToolTabComponent : BaseTabComponent
    {
        private static readonly By ItemLocator = By.XPath(".//li[contains(@class,'co_redlineLightbox')]");

        private static readonly By CloseLinkLocator = By.XPath(".//a[text()='Close']");

        private static readonly By ItemsCountTextLocator = By.Id("coid_redlineLightbox_itemCount");

        /// <summary>
        /// GetItemDate
        /// </summary>
        /// <param name="itemIndex"> Item index </param>
        /// <returns> Date </returns>
        public string GetItemDate(int itemIndex = 0) => this.GetItem(itemIndex).Date;

        /// <summary>
        ///  Click Close link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCloseLink<T>() where T : ICreatablePageObject
        {
            IWebElement closeLink = DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), CloseLinkLocator);
            closeLink.WaitForElementDisplayed();
            closeLink.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get items count
        /// </summary>
        /// <returns> Items count </returns>
        public int GetItemsCount()
            => DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), ItemsCountTextLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Remove all selected items
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <returns> New Instance of the page </returns>
        public T RemoveAllSelectedItems<T>() where T : BaseComparisonToolTabComponent
        {
            this.GetItems().ForEach(item => item.ClickDeleteButton());
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get item title
        /// </summary>
        /// <param name="itemIndex"> Item index </param>
        /// <returns> Title </returns>
        public string GetItemTitle(int itemIndex) => this.GetItem(itemIndex).Title;

        /// <summary>
        /// Get Redline Comparison Item list
        /// </summary>
        /// <returns> List of items </returns>
        protected List<RedlineComparisonItem> GetItems()
            =>
                DriverExtensions.GetElements(this.ComponentLocator, ItemLocator)
                                .Select(elem => new RedlineComparisonItem(elem))
                                .ToList();

        /// <summary>
        /// Get Redline Comparison Item
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> Item </returns>
        protected RedlineComparisonItem GetItem(int index)
            => new RedlineComparisonItem(DriverExtensions.GetElements(this.ComponentLocator, ItemLocator).ElementAt(index));
    }
}