namespace Framework.Common.UI.Products.Shared.Components.Facets.RightFacets
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Suggested titles facet component
    /// </summary>
    public class SuggestedTitlesFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_suggestedTitlesContainer");
        private static readonly By TitleLocator = By.XPath(".//h3");
        private static readonly By SuggestedItemLocator = By.XPath(".//li[@class = 'co_suggestedTitle_item']/a");
        private static readonly By ViewMoreLinkLocator = By.XPath(".//a[@id = 'co_moreSuggestedTitles']");

        /// <summary>
        /// Dialog title
        /// </summary>
        public string Title => DriverExtensions.GetText(this.ComponentLocator, TitleLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get number of suggested titles
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetNumberOfSuggestedTitles() => DriverExtensions.GetElements(this.ComponentLocator, SuggestedItemLocator).Count;

        /// <summary>
        /// Is 'View More' link displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise.</returns>
        public bool IsViewMoreLinkDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, ViewMoreLinkLocator);

        /// <summary>
        /// Click 'View all' link
        /// </summary>
        /// <returns>New instance of 'Suggested titles' dialog</returns>
        public SuggestedTitlesDialog ClickViewAllLink()
        {
            DriverExtensions.GetElement(this.ComponentLocator, ViewMoreLinkLocator).Click();
            return new SuggestedTitlesDialog();
        }

        /// <summary>
        /// Click Link By Text
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> Link text </param>
        /// <returns> New instance of the page </returns>
        public override T ClickLinkByText<T>(string linkText)
        {
            DriverExtensions.GetElements(this.ComponentLocator, SuggestedItemLocator)
                 .FirstOrDefault(link => link.Text.Trim().ToLower().Equals(linkText.Trim().ToLower())).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}