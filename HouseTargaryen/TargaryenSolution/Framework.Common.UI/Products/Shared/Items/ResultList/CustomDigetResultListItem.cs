namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Result Item
    /// Part of search result list
    /// In addition to items from ResultListItems contains:
    /// todo: make this class internal when Search Manager is implemented
    /// - Number Of Cases that Cites The Issue Link
    /// </summary>
    public sealed class CustomDigetResultListItem : ResultListItem
    {
        /// <summary>
        /// The cases that cite this legal issue link.
        /// </summary>
        private static readonly By CasesThatCiteThisLegalIssueLinkLocator =
            By.XPath(".//div[@class='co_searchResults_headnote']/div/a");

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDigetResultListItem"/> class. 
        ///  </summary>
        /// <param name="containerElement"> container </param>
        public CustomDigetResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// The value of casedocguid attribute.
        /// </summary>
        public string CaseDocGuid
            => DriverExtensions.SafeGetElement(this.Container, GuidLocator)?.GetAttribute("casedocguid");

        /// <summary>
        /// This method clicks on Cases That Cite This Legal Issue Link in search result item
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCasesThatCiteThisLegalIssueLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, CasesThatCiteThisLegalIssueLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}