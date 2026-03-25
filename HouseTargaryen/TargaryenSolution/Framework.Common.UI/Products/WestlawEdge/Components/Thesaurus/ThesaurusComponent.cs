namespace Framework.Common.UI.Products.WestlawEdge.Components.Thesaurus
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Thesaurus Component in NewsTypeAhead
    /// </summary>
    public class ThesaurusComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='co-Typeahead-section co-Typeahead-categoryThesaurus']");

        private static readonly By ThesaurusLinkLocator = By.Id("co_typeaheadThesaurusLink");

        private static readonly By ThesaurusTitleLocator = By.XPath(".//h3[@class='co-Typeahead-categoryHeading']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Text form title
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetThesaurusTitleText()
            => this.IsThesaurusTitleDisplayed() ? DriverExtensions.GetText(this.ComponentLocator, ThesaurusTitleLocator) : string.Empty;

        /// <summary>
        /// Get Text form link
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetThesaurusLinkText()
            => this.IsThesaurusLinkDisplayed() ? DriverExtensions.GetText(this.ComponentLocator, ThesaurusLinkLocator) : string.Empty;

        /// <summary>
        /// Click on the Thesaurus link
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public ThesaurusDialog ClickThesaurusLink()
        {
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator);
            DriverExtensions.Click(DriverExtensions.GetElement(this.ComponentLocator), ThesaurusLinkLocator);
            return new ThesaurusDialog();
        }

        /// <summary>
        /// Check is 'Thesaurus' title displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsThesaurusTitleDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, ThesaurusTitleLocator);

        /// <summary>
        /// Check is Thesaurus link displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsThesaurusLinkDisplayed() => DriverExtensions.IsDisplayed(new ByChained(this.ComponentLocator, ThesaurusLinkLocator), 15);
    }
}