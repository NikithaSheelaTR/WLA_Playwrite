namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Start Page Component (for example on the News Category Page)
    /// </summary>
    public class StartPageComponent : BaseModuleRegressionComponent
    {
        private static readonly By MakeThisMyStartPageLinkLocator = By.XPath("//*[@class='co_website_browsePageAddHomepage'] | //*[@id='coid_setAsHomePageElement']");

        private static readonly By RemoveAsMyStartPageLinkLocator =
            By.XPath("//*[@class='co_website_browsePageRemoveAsHomepage'] | //*[@id='coid_setAsHomePageElement']");

        private static readonly By ContainerLocator = By.Id("coid_website_browsePageSelectAsHomepage");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Make This My Start Page
        /// </summary>
        /// <typeparam name="T"> Current Page </typeparam>
        /// <returns> New instance of the page</returns>
        public T ClickMakeThisMyStartPage<T>()
            where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(MakeThisMyStartPageLinkLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Remove As My Start Page
        /// </summary>
        /// <typeparam name="T"> Current Page </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickRemoveAsMyStartPage<T>()
            where T : ICreatablePageObject =>
            this.ClickByLink<T>(RemoveAsMyStartPageLinkLocator);

        /// <summary>
        /// Is Make This MyStart Page Link Displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsMakeThisMyStartPageLinkDisplayed() =>
            DriverExtensions.IsDisplayed(MakeThisMyStartPageLinkLocator, 5);

        /// <summary>
        /// Is Remove As My Start Page Link Displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsRemoveAsMyStartPageLinkDisplayed() =>
            DriverExtensions.IsDisplayed(RemoveAsMyStartPageLinkLocator);

        private T ClickByLink<T>(By linkLocator)
            where T : ICreatablePageObject
        {
            if (DriverExtensions.IsDisplayed(linkLocator, 5))
            {
                DriverExtensions.GetElement(linkLocator).Click();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}