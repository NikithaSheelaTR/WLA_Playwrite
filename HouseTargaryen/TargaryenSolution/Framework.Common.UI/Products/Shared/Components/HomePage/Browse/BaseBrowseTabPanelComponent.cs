namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Browse Tab Component
    /// </summary>
    public abstract class BaseBrowseTabPanelComponent : BaseTabComponent
    {
        private static readonly By OpenedBrowseContentAreaLocator = By.ClassName("co_tabShow");
        private static readonly By BrowseTabPanelItemLocator = By.XPath("//div[@id='co_browseWidget' or @class = 'Browse-widget']");

        /// <summary>
        /// Determines if the category link is present
        /// </summary>
        /// <param name="category">The text of the category link</param>
        /// <returns>True if present, false otherwise</returns>
        public bool IsCategoryDisplayed(string category)
            => DriverExtensions.IsDisplayed(DriverExtensions.WaitForElement(OpenedBrowseContentAreaLocator), By.LinkText(category));

        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="category">The category.</param>
        /// <returns>New instance of T page.</returns>
        public T ClickBrowseCategory<T>(string category) where T : ICreatablePageObject
        {
            //DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(BrowseTabPanelItemLocator), By.LinkText(category)).Click();
            //return DriverExtensions.CreatePageInstance<T>();

            var containerElement = DriverExtensions.WaitForElement(BrowseTabPanelItemLocator);
            var linkElement = DriverExtensions.WaitForElement(containerElement, By.LinkText(category));

            // Use the custom click extension which handles intercepted clicks
            DriverExtensions.Click(linkElement);

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}