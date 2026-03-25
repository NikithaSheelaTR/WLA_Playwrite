namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The search result grid.
    /// </summary>
    /// <typeparam name="TSearchResultItem">
    /// The type of search result item
    /// </typeparam>
    public class SearchResultList<TSearchResultItem>
        : ItemsCollection<TSearchResultItem>, ISearchResultList<TSearchResultItem>
        where TSearchResultItem : ResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private const string DocGuidLctMask = ".//a[@docguid='{0}']";

        private static readonly By SpellCheckLinkLocator = By.Id("co_search_spellCheck");

        private static readonly By MoreInfoLinkLocator = By.Id("co_moreInfoLink");

        private static readonly By ResultHeaderLocator = By.XPath("//div[@class='co_search_result_heading_content']//h1");

        private static readonly By SearchResultOutOfPlanLocator = By.XPath(".//div[@class='co_outOfPlanLabel']/following-sibling::div[starts-with(@class,'co_searchContent')]");

        private static readonly By InfoMessageLocator = By.XPath(".//div[contains(@class,'co_searchMoreInfoTooltip')]");

        private static readonly By ResultItemLocator = By.XPath(".//li[starts-with(@id, 'cobalt_search_result')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultList{TSearchResultItem}"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public SearchResultList(IWebElement container)
            : base(container, ResultItemLocator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultList{TSearchResultItem}"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="itemLocator">
        /// The item locator.
        /// </param>
        public SearchResultList(IWebElement container, By itemLocator)
            : base(container, itemLocator)
        {
        }

        /// <inheritdoc />
        public string SpellCheckText =>
            DriverExtensions.IsDisplayed(this.Container, SpellCheckLinkLocator)
                ? DriverExtensions.GetElement(this.Container, SpellCheckLinkLocator).Text
                : string.Empty;

        /// <inheritdoc />
        public string ResultsHeader => DriverExtensions.WaitForElement(ResultHeaderLocator).Text;

        /// <inheritdoc />
        public string MoreInfoText => this.MoreInfoMessage.Text.Trim();

        /// <inheritdoc />
        public int TotalCount =>
            DriverExtensions.WaitForElement(ResultHeaderLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// The more info icon element.
        /// </summary>
        protected IWebElement MoreInfoIcon => DriverExtensions.SafeGetElement(MoreInfoLinkLocator);

        /// <summary>
        /// Gets the more info message element.
        /// </summary>
        protected IWebElement MoreInfoMessage
        {
            get
            {
                this.MoreInfoIcon.SeleniumHover();
                return DriverExtensions.WaitForElementDisplayed(InfoMessageLocator);
            }
        }

        /// <summary>
        /// Gets item by name or guid
        /// </summary>
        /// <param name="nameOrGuid">
        /// The name.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        public override TSearchResultItem this[string nameOrGuid]
        {
            get
            {
                DriverExtensions.WaitForElementDisplayed(this.Container, this.ItemLocator);
                return nameOrGuid.IsWestlawGuid() ? this.GetItemByGuid(nameOrGuid) : this.GetItemByName(nameOrGuid);
            }
        }

        /// <inheritdoc />
        public TPage ClickSpellCheckLink<TPage>() where TPage : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(SpellCheckLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <inheritdoc />
        public TSearchResultItem GetRandomItem(bool onlyOutOfPlan = true)
        {
            IList<IWebElement> outOfPlanItems = DriverExtensions.GetElements(this.Container, SearchResultOutOfPlanLocator);

            var randomObj = new Random();

            return onlyOutOfPlan
                       ? (TSearchResultItem)Activator.CreateInstance(
                           typeof(TSearchResultItem),
                           outOfPlanItems.ElementAt(randomObj.Next(0, outOfPlanItems.Count - 1)).GetParentElement("li"))
                       : this[randomObj.Next(0, this.Count - 1)];
        }

        /// <inheritdoc />
        public bool IsMoreInfoIconDisplayed() =>
            this.MoreInfoIcon != null && this.MoreInfoIcon.IsDisplayed();

        /// <summary>
        /// The get item by guid.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        protected virtual TSearchResultItem GetItemByGuid(string guid) => (TSearchResultItem)Activator.CreateInstance(
            typeof(TSearchResultItem),
            DriverExtensions.GetElement(this.Container, this.ItemLocator, By.XPath(string.Format(DocGuidLctMask, guid))).GetParentElement("li"));

        /// <summary>
        /// Gets item
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        protected virtual TSearchResultItem GetItemByName(string name) =>
            (TSearchResultItem)Activator.CreateInstance(
                typeof(TSearchResultItem),
                DriverExtensions.GetElements(this.Container, this.ItemLocator).First(item => item.Text.Contains(name)));
    }
}