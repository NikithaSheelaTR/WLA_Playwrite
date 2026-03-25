namespace Framework.Common.UI.Raw.WestlawEdge.Items.RelatedDocument
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Secondary Sources Item in the Related Documents component
    /// </summary>
    public class SecondarySourcesItem : BaseItem
    {
        private static readonly By LinkLocator = By.XPath(".//a");

        private static readonly By HighlightedSearchTermLocator = By.XPath(".//span[@class='co_searchTerm']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="SecondarySourcesItem"/> class. 
        /// </summary>
        /// <param name="secondarySourcesContainer"> The Secondary Sources Item Container. </param>
        public SecondarySourcesItem(IWebElement secondarySourcesContainer): base(secondarySourcesContainer)
        {
        }

        /// <summary>
        /// Name of document link
        /// </summary>
        public string LinkName => this.DocumentLink.Text;

        /// <summary>
        /// Verifies that Highlighted Search Term is displayed
        /// </summary>
        /// <returns> True if Highlighted Search Term is displayed </returns>
        public bool IsHighlightedSearchTermDisplayed => this.HighlightedSearchTerm.IsDisplayed();

        /// <summary>
        /// Link in the notification
        /// </summary>
        private IWebElement DocumentLink => DriverExtensions.SafeGetElement(this.Container, LinkLocator);

        /// <summary>
        /// Link in the notification
        /// </summary>
        private IWebElement HighlightedSearchTerm => DriverExtensions.SafeGetElement(this.Container, HighlightedSearchTermLocator);

        /// <summary>
        /// The click title link.
        /// </summary>
        /// <typeparam name="T">
        /// The ICreatable PO
        /// </typeparam>
        /// <returns>
        /// The new item of T class
        /// </returns>
        internal T ClickTitleLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(this.DocumentLink);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}