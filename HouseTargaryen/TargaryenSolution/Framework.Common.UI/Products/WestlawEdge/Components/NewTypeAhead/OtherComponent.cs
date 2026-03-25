namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System.Collections.Generic;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The other component.
    /// </summary>
    public class OtherComponent : ContentTypeDetailsBaseComponent
    {
        private const string MoreLinkLctMask = "//h2[text()='{0}']//following-sibling::div//*[contains(@id,'moreSuggestions_button')]";

        private static readonly By ContainerLocator = By.Id("typeAheadContentTypesDiv");
        private static readonly By OtherDocumentItemContainerLocator = By.XPath(".//div[@class='co-Typeahead-result']//li/a");

        /// <summary>
        /// Initializes a new instance of the <see cref="OtherComponent"/> class.
        /// </summary>
        public OtherComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is More link displayed
        /// </summary>
        /// <param name="category">
        /// The category
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsMoreLinkDisplayed(NewTypeAheadCategories category)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text)));

        /// <summary>
        /// Click on More Link in category
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public override void ClickMoreLink(NewTypeAheadCategories category)
            => DriverExtensions.GetElement(By.XPath(string.Format(MoreLinkLctMask, this.CategoriesMap[category].Text))).Click();

        /// <summary>
        /// The map items to models by category.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="T:List"/>.
        /// </returns>
        protected override List<TrdSuggestionModel> MapItemsToModelsByCategory(NewTypeAheadCategories category)
           => this.GetMappedModelsList<TrdSuggestionModel, TrdOtherItem>(category);

        
        /// <summary>
        /// The initialize supported categories.
        /// </summary>
        protected override sealed void InitializeSupportedCategories()
            => this.SupportedCategories = new List<NewTypeAheadCategories>
                                              {
                                                  NewTypeAheadCategories.AdministrativeDecisionsAndGuidance,
                                                  NewTypeAheadCategories.Briefs,
                                                  NewTypeAheadCategories.Dockets,
                                                  NewTypeAheadCategories.ContentPages,
                                                  NewTypeAheadCategories.ExpertMaterials,
                                                  NewTypeAheadCategories.JuryVerdicts,
                                                  NewTypeAheadCategories.OtherDocuments,
                                                  NewTypeAheadCategories.Tools,
                                                  NewTypeAheadCategories.TrialCourtDocuments,
                                                  NewTypeAheadCategories.TrialCourtOrders
                                              };
    }
}