namespace Framework.Common.UI.Products.WestLawNext.Components.IphoneMode
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// MobileSearchHeader class
    /// </summary>
    public class MobileSearchHeader : BaseModuleRegressionPage, IWestlawNextHeaderComponent
    {
        private static readonly By SearchInputLocator = By.Id("searchInputId");

        private static readonly By SignOffLinkLocator = By.LinkText("Sign Off");

        private IWebElement SearchInput => DriverExtensions.WaitForElement(SearchInputLocator);

        /// <summary>
        /// Clicks the search button
        /// </summary>
        /// <typeparam name="T">
        /// Object
        /// </typeparam>
        /// <returns>
        /// A new instance of the search results page
        /// </returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            this.SearchInput.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the sign off.
        /// </summary>
        public void ClickSignOff() => DriverExtensions.Click(SignOffLinkLocator);


        /// <summary>
        /// Generic ClickSignOff method.
        /// </summary>
        /// <typeparam name="T">instance to return</typeparam>
        /// <returns>new instance</returns>
        public T ClickSignOff<T>() where T : ICommonSignOffPage
        {
            this.ClickSignOff();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enters the search and click search.
        /// </summary>
        /// <typeparam name="T">instance to return</typeparam>
        /// <param name="searchText">The search text.</param>
        /// <returns>new instance</returns>
        public T EnterSearchAndClickSearch<T>(string searchText) where T : ICreatablePageObject => this.Search<T>(searchText);

        /// <summary>
        ///  Enter Search query
        /// </summary>
        /// <param name="query"> Search query </param>
        /// <param name="sendSlow"> If true - send slow </param>
        /// <param name="clearFirst"> If true - clear input </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of he page </returns>
        public T EnterSearchQuery<T>(string query, bool sendSlow = false, bool clearFirst = true)
            where T : BaseModuleRegressionDialog
        {
            if (clearFirst)
            {
                this.SearchInput.Clear();
            }

            if (sendSlow)
            {
                this.SearchInput.SendKeysSlow(query);
            }
            else
            {
                this.SearchInput.SendKeys(query);
            }

            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The search.
        /// </summary>
        /// <param name="searchText"> The search text. </param>
        /// <typeparam name="T"> instance to return</typeparam>
        /// <returns> The new instance. </returns>
        private T Search<T>(string searchText) where T : ICreatablePageObject
        {
            this.SearchInput.SetTextField(searchText);
            this.SearchInput.SendKeys(Keys.Enter);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}