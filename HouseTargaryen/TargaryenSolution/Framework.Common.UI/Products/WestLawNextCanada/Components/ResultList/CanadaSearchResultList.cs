namespace Framework.Common.UI.Products.WestLawNextCanada.Components.ResultList
{
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary> The searchResultList, adapted for Canada</summary>
    /// <typeparam name="TResultListItem"> </typeparam>
    public class CanadaSearchResultList<TResultListItem> : SearchResultList<TResultListItem>
        where TResultListItem : ResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly By CanadaItemLocator = By.XPath("./li");

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaSearchResultList{TResultListItem}"/> class. 
        /// The constructor
        /// </summary>
        /// <param name="container"> The component container
        /// </param>
        public CanadaSearchResultList(IWebElement container)
            : base(container, CanadaItemLocator)
        {
        }
    }
}