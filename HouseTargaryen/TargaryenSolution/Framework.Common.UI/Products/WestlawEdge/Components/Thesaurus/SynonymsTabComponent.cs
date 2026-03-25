namespace Framework.Common.UI.Products.WestlawEdge.Components.Thesaurus
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Thesaurus;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Synonyms Tab in Thesaurus dialog
    /// </summary>
    public class SynonymsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='panel_SynonymsTabId']//div[contains(@class,'Tab-container')]");
        private static readonly By LeftRailLocator = By.XPath(".//*[@class='Tab-list' and @aria-label='Terms']");
        private static readonly By TermLocator = By.XPath(".//*[contains(@id,'tab_')]");
        private static readonly By ListLocator = By.XPath(".//div[contains(@class, 'show')]//*[@class='co_thesaurusSynonymsList']");
        private static readonly By ItemLocator = By.XPath(".//div[@class='co_thesaurusSynonymGroup']");

        /// <summary>
        /// Related Concepts result list
        /// </summary>
        public ItemsCollection<ThesaurusResultListItem> SynonymsResultList =>
            new ItemsCollection<ThesaurusResultListItem>(new ByChained(this.ComponentLocator, ListLocator), ItemLocator);
        
        /// <summary>
        /// Left rail terms
        /// </summary>
        public IReadOnlyCollection<IButton> LeftRailTerms =>
            new ElementsCollection<Button>(this.ComponentLocator, LeftRailLocator, TermLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Synonyms";

        /// <summary>
        /// Get all terms from Left rail
        /// </summary>
        /// <returns>
        /// List with terms on the Left rail
        /// </returns>
        public List<string> GetTermsText() => this.LeftRailTerms.Select(term => term.Text).ToList();

         /// <summary>
        /// Is container Left Rail displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LeftRailComponentDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, LeftRailLocator);

        /// <summary>
        /// Check is Synonyms tab is enabled
        /// </summary>
        /// <returns>True - if the tab is enabled</returns>
        public bool IsSynonymsTabEnabled() => DriverExtensions
            .GetAttribute("aria-disabled", DriverExtensions.WaitForElement(this.ComponentLocator, 0)).Contains("false");
    }
}