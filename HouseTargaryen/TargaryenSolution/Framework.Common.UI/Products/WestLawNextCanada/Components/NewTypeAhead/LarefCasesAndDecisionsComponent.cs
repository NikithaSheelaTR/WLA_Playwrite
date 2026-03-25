namespace Framework.Common.UI.Products.WestLawNextCanada.Components.NewTypeAhead
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// LaRef Cases and Decisions Type A head component
    /// </summary>
    public class LarefCasesAndDecisionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_autoCompleteSection_elastic_canada_fr_laref");
        private static readonly By SuggestionItemsLocator = By.XPath("//li[@class='co_typeAheadItem co_typeAheadScroll']");
        private static readonly By CitationsLocator = By.XPath(".//ul[@class='co_typeAheadInfoList co_inlineList']/li");
        private static readonly By ShowSuggestionsLabelLocator = By.XPath("//label[@for='typeAhead_checkboxelastic_canada_fr_laref']");
        private static readonly By ShowSuggestionsCheckboxLocator = By.Id("typeAhead_checkboxelastic_canada_fr_laref");
        private static readonly By SuggestionTitleLabelLocator = By.Id("heading_typeAhead_checkboxelastic_canada_fr_laref");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Show suggestions label
        /// </summary>
        public ILabel ShowSuggestionsLabel => new Label(ShowSuggestionsLabelLocator);

        /// <summary>
        /// Show suggestions check box
        /// </summary>
        public ICheckBox ShowSuggestionsCheckbox => new CheckBox(ShowSuggestionsCheckboxLocator);

        /// <summary>
        /// Cases and Decision suggestion title
        /// </summary>
        public ILabel CasesAndDecisionSuggestionTitleLabel => new Label(SuggestionTitleLabelLocator);

        /// <summary>
        /// Get Suggestions from Type A head which are only shown
        /// </summary>
        /// <returns>List of titles shown on dialog</returns>
        public List<string> GetSuggestionTitlesShown() =>
            this.GetSuggestionItemsShown().Select(item => item.Text).ToList();

        /// <summary>
        /// Get the citations list for the items shown in suggestions
        /// </summary>
        /// <returns>List of citations</returns>
        public List<List<string>> GetCitationsList()
            => this.GetSuggestionItemsShown().Select(item => item.FindElements(CitationsLocator).Select(sub => sub.Text.Trim()).ToList()).ToList();
        

        /// <summary>
        /// Get Suggestion items which are shown
        /// </summary>
        /// <returns>List of webelements</returns>
        protected IEnumerable<IWebElement> GetSuggestionItemsShown() =>
            DriverExtensions.GetElements(SuggestionItemsLocator)
                .Where(item => item.GetAttribute("style") != "display: none;");
    }
}