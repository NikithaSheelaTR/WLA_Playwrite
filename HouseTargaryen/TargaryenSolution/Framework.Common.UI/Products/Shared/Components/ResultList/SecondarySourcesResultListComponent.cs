namespace Framework.Common.UI.Products.Shared.Components.ResultList
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Result List Component for trillium pages
    /// todo: implement using ISearchResultGrid approach
    /// </summary>
    public class SecondarySourcesResultListComponent : LegacyBaseResultListComponent
    {
        private const string PublicationOutOfPlanLabelLctMask = "//input[@class='co_linkCheckBox_checkBox' and contains(@value,'{0}')]/following-sibling::div[@class='co_outOfPlanLabel']";

        private const string ScopeIconPubInTheListLctMask =
            "//div[@id='cobalt_search_commentaryLibrary_results']//button[contains(@class,'co_scopeIcon')][contains(@scopetitle, '{0}')]";

        private const string TitleLctMask = "//a[contains(@id,'Library_title') and contains(text(),'{0}')]";

        private static readonly By OutOfPlanBannerLocator = By.XPath("//div[@class='co_outOfPlanLabel']");

        private static readonly By ContainerLocator = By.Id("cobalt_search_commentaryLibrary_results");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns count of Out Of Plan banners
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetOutOfPlanBannerCount() => DriverExtensions.GetElements(OutOfPlanBannerLocator).Count;

        /// <summary>
        /// Checks whether the OutOfPlan banner is displayed on a specific document
        /// </summary>
        /// <param name="publicationName">The document title</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsOutOfPlanLabelPresentForPublication(string publicationName)
            => DriverExtensions.IsElementPresent(By.XPath(string.Format(PublicationOutOfPlanLabelLctMask, publicationName)));

        /// <summary>
        /// Clicks on Scope Icon for a specific document
        /// </summary>
        /// <param name="pubName">The document title</param>
        /// <returns>The <see cref="ScopeDialog"/>.</returns>
        public ScopeDialog ClickScopeIconForPublication(string pubName)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ScopeIconPubInTheListLctMask, pubName))).Click();
            return new ScopeDialog();
        }

        /// <summary>
        /// Checks whether the Scope Icon is displayed on a specific document
        /// </summary>
        /// <param name="pubName">The document title</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsScopeIconDisplayedForPublication(string pubName)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(ScopeIconPubInTheListLctMask, pubName)));

        /// <summary>
        /// Check whether the desired document exists in list
        /// </summary>
        /// <param name="publicationTitle">Document Title</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPublicationDisplayed(string publicationTitle)
            => this.GetListOfDocumentsTitles().Any(title => title.Contains(publicationTitle));

        /// <summary>
        /// Checks the the publication list is sorted alphabetically
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreSearchResultsSortedByName()
        {
            IEnumerable<string> documentTitlesToCompare = this.GetListOfDocumentsTitles()
                .Select(title => title.NormalizeDocumentName().RemoveArticlesFromDocumentName());

            return documentTitlesToCompare.SequenceEqual(documentTitlesToCompare.OrderBy(u => u));
        }

        /// <summary>
        /// Checks if publication title is into view after scrolling 
        /// </summary>
        /// <param name="title">Publication title.</param>
        /// <returns>True if title is scrolled into view, false otherwise</returns>
        public bool IsTitleInView(string title)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(TitleLctMask, title))).IsElementInView();
    }
}