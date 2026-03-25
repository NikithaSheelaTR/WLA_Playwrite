namespace Framework.Common.UI.Products.WestlawEdge.Components.Thesaurus
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Thesaurus;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Related concepts tab
    /// </summary>
    public class RelatedConceptsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='panel_relatedConceptsTabId']//ancestor-or-self::div[contains(@class,'Tab-container ')]");
        private static readonly By ItemLocator = By.XPath(".//div[@class='co_thesaurusRelatedConceptGroup']");
        private static readonly By ResultListLocator = By.XPath(".//div[@class= 'co_thesaurusRelatedConcepts']");
        private static readonly By LeftRailLocator = By.XPath("//*[@class='Tab-list' and @aria-label='Terms']");

        /// <summary>
        /// Related Concepts result list
        /// </summary>
        public ItemsCollection<ThesaurusResultListItem> RelatedConceptsResultList =>
            new ItemsCollection<ThesaurusResultListItem>(new ByChained(this.ComponentLocator, ResultListLocator), ItemLocator);

         /// <summary>
         /// Component locator
         /// </summary>
         protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Related concepts";

        /// <summary>
        /// Is container Left Rail displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LeftRainComponentDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, LeftRailLocator);
    }
}