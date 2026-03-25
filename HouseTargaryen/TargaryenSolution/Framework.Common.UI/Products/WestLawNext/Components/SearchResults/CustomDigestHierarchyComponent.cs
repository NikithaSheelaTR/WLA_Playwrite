namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CustomDigestHierarchyComponent
    /// </summary>
    public class CustomDigestHierarchyComponent : BaseModuleRegressionComponent
    {
        private static readonly By TopicForDigestLocator = By.XPath("//ul[@class='co_customDigestSecondary']//a[@id='cobalt_result_search_hierarchy']");

        private static readonly By ContainerLocator = By.ClassName("co_customDigestSearchResult_tree");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// This method is used to verify that the Sort By dropdown exists on the key number search results page
        /// </summary>
        /// <returns>true if search results appear on the page</returns>
        public int GetDigestTopicsCount() => DriverExtensions.GetElements(TopicForDigestLocator).Count;

        /// <summary>
        /// This method is used to click on digest hierarchy item on basis of index - starts from 0
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="index"> item index </param>
        /// <returns>A new page</returns>
        public T ClickOnDigestHierarchyItemByIndex<T>(int index) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(TopicForDigestLocator).ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}