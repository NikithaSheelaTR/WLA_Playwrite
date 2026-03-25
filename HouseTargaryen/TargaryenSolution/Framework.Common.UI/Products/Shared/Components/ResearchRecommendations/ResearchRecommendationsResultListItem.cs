namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Research Recommendations item.
    /// </summary>
    public class ResearchRecommendationsResultListItem : ResultListItem
    {
        private const string LinkInPreviewLctMask =
            ".//button[contains(text(), '{0}')] | .//a[contains(text(), '{0}')]";

        private static readonly By SynopsisContentLocator =
            By.XPath(".//div[@class='co_searchResults_preview' or @class='co_searchResults_synopsis']/div");

        private static readonly By SynopsisLinkLocator =
            By.XPath(".//div[@class='co_searchResults_preview' or @class='co_searchResults_synopsis']/a | .//button[contains(@class, 'co_searchResults_synopsisToggle')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResearchRecommendationsResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public ResearchRecommendationsResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// True if synopsis or preview is displayed
        /// </summary>
        public bool IsSynopsisDisplayed => DriverExtensions.IsDisplayed(this.Container, SynopsisLinkLocator);

        /// <summary>
        /// True if synopsis or preview is expanded
        /// </summary>
        public bool IsSynopsisExpanded => this.IsSynopsisDisplayed && DriverExtensions.IsDisplayed(this.Container, SynopsisContentLocator);

        /// <summary>
        /// Gets a synopsis text
        /// </summary>
        public string SynopsisText => DriverExtensions.WaitForElement(this.Container, SynopsisContentLocator).Text;

        /// <summary>
        /// Click link within Synopsis content
        /// </summary>
        /// <param name="linkText">text of the link</param>
        /// <typeparam name="T">type of new instance to return</typeparam>
        /// <returns>new instance of specified class</returns>
        public T ClickLinkInPreview<T>(string linkText)
            where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(
                DriverExtensions.WaitForElement(this.Container, SynopsisContentLocator),
                By.XPath(string.Format(LinkInPreviewLctMask, linkText))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
          
        /// <summary>
        /// Expand or collapse synopsis or preview
        /// </summary>
        /// <param name="state">True if expand action is needed, false to collapse</param>
        public void ExpandPreviewByName(bool state = true)
        {
            if (!this.IsSynopsisExpanded == state)
            {
                DriverExtensions.GetElement(this.Container, SynopsisLinkLocator).Click();
                DriverExtensions.WaitForAnimation();
            }
        }
    }
}
