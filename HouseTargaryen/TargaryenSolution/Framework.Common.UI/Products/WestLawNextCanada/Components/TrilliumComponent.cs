namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Document Trillium Features
    /// </summary>
    public class TrilliumComponent : BaseModuleRegressionComponent
    {
        private static readonly By TrilliumContainerLocator = By.XPath(".//div[@class='co_resultPageSectionWidget']");
        private static readonly By AddToFavouritesButtonLocator = By.XPath(".//a[@id='co_foldering_categoryPage']");
        private static readonly By PublicationsResultsLocator = By.XPath(".//a[contains(@id,'cobalt_result_commentaryLibrary_title')]");
        private static readonly By LibraryIsFilteredTextLocator = By.XPath(".//h2[@id='co_libraryIsFilteredText']");
        private static readonly By ProViewResultsListLocator = By.XPath(".//span[@class='co_result_info co_proviewIcon']");
        private static readonly By ViewOnProViewLinkLocator = By.XPath(".//a[@id='coid_view_on_proview']");
        private static readonly By CheckBoxResultsListLocator = By.XPath(".//li[contains(@id,'cobalt_search_results_commentaryLibrary')]");
        private static readonly By ScopeIconListLocator = By.XPath(".//button[@class='co_result_info co_scopeIcon']");
        private static readonly By ScopeIconLocator = By.XPath(".//button[@class='co_result_info co_scopeIcon']/preceding-sibling::a");
        private static readonly By ScopeHeaderLocator = By.XPath(".//div[@class='co_overlayBox_headline']//descendant::h2");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='co_overlayBox_closeButton co_iconBtn']");
        private static readonly By OrganizeLinkLocator = By.XPath(".//a[@id='co_foldering_favorites_editLink']");
        private static readonly By DeleteButtonLocator = By.XPath("//*[@aria-label='My Favourites']//button[@class='co_favoriteDel co_linkBlue' and contains(text(),'Delete')]");
        private static readonly By ElooseleafsHeaderLocator = By.XPath(".//*[@id='cobalt_search_can_ereference_results_header' and contains(text(),'eLooseleafs on ProView')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TrilliumContainerLocator;

        /// <summary>
        /// List of Publication items
        /// </summary>
        public ItemsCollection<TrilliumResultListItem> PublicationsResultList => new ItemsCollection<TrilliumResultListItem>(this.ComponentLocator, CheckBoxResultsListLocator);

        /// <summary>
        /// Add To MyFavourites Button
        /// </summary>
        public IButton AddToFavouritesButton => new Button(AddToFavouritesButtonLocator);

        /// <summary>
        /// Library Filter Label
        /// </summary>
        public ILabel LibraryIsFilteredLabel => new Label(this.ComponentLocator, LibraryIsFilteredTextLocator);

        /// <summary>
        /// Get ProView ResultsList
        /// </summary>
        public IReadOnlyCollection<ILabel> GetProViewFromPublications => new ElementsCollection<Label>(this.ComponentLocator, ProViewResultsListLocator);

        /// <summary>
        /// ViewOnProView Link
        /// </summary>
        public ILink ViewOnProViewLink => new Link(ViewOnProViewLinkLocator);

        /// <summary>
        /// Scope Icon Buttons
        /// </summary>
        public IReadOnlyCollection<IButton> ScopeIconButtons => new ElementsCollection<Button>(this.ComponentLocator, ScopeIconListLocator);

        /// <summary>
        /// Scope Icon Labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ScopeIconLabels => new ElementsCollection<Label>(this.ComponentLocator, ScopeIconLocator);

        /// <summary>
        /// Scope Header Label
        /// </summary>
        public ILabel ScopeHeaderLabel => new Label(ScopeHeaderLocator);

        /// <summary>
        /// Scope Close Button 
        /// </summary>
        public IButton ScopeCloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Publications ResultList
        /// </summary>
        public IReadOnlyCollection<ILink> GetPublicationsResultList => new ElementsCollection<Link>(this.ComponentLocator, PublicationsResultsLocator);

        /// <summary>
        /// Click on a specific document based on the documents index
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="docIndex"> Document index to open </param>
        /// <returns> New instance of the page </returns>
        public T ClickOnSearchResultDocumentByIndex<T>(int docIndex) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(PublicationsResultsLocator);
            GetPublicationsResultList.ElementAt(docIndex).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteButton => new Button(DeleteButtonLocator);

        /// <summary>
        /// Delete All MyFavourites
        /// </summary>
        public void DeleteAllMyFavourites()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(OrganizeLinkLocator).ClickUsingJavaScriptAsync();
            if (DeleteButton.Present)
            {
                DriverExtensions.WaitForElement(DeleteButtonLocator).ClickUsingJavaScriptAsync();
            }
            DriverExtensions.ScrollToTop();
        }

        /// <summary>
        /// Elooseleafs Header
        /// </summary>
        public ILabel ElooseleafsHeader => new Label(ElooseleafsHeaderLocator);
    }
}
