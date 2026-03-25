namespace Framework.Common.UI.Products.WestlawEdge.Components.NewTypeAhead
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Raw.WestlawEdge.Enums.NewTypeAhead;
    using Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover;
    using Framework.Common.UI.Raw.WestlawEdge.Models.TrDiscover;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Government and Regulatory Materials component
    /// </summary>
    public class GovRegComponent : ContentTypeDetailsBaseComponent
    {
        private static readonly By MoreLinkLocator = By.Id("co_typeaheadGovReg_moreSuggestions_button");

        private static readonly By ContainerLocator = By.XPath("//div[@class= 'co-TRDiscover-govRegView']");

        private static readonly By GovRegItemContainerLocator = By.XPath(".//div[contains(@class,'co-TRDiscover-result')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="GovRegComponent"/> class.
        /// </summary>
        public GovRegComponent()
        {
            this.InitializeSupportedCategories();
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on More Link in category
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        public override void ClickMoreLink(NewTypeAheadCategories category)
            => DriverExtensions.GetElement(this.ComponentLocator, MoreLinkLocator).Click();

        /// <summary>
        /// Get suggestion elements list by name.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The <see cref="T:IList"/>.</returns>
        protected override IList<IWebElement> GetSuggestionElementsListByName(NewTypeAheadCategories category)
            =>
                category.Equals(NewTypeAheadCategories.GovernmentAndRegulatoryMaterials)
                    ? this.GetItems(this.ComponentLocator, GovRegItemContainerLocator)
                    : base.GetSuggestionElementsListByName(category);

        /// <summary>
        /// Map items to models by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The <see cref="T:List"/>.</returns>
        protected override List<TrdSuggestionModel> MapItemsToModelsByCategory(NewTypeAheadCategories category)
        {
            List<TrdSuggestionModel> resultList;

            switch (category)
            {
                case NewTypeAheadCategories.GovernmentAndRegulatoryMaterials:
                    resultList = this.GetMappedModelsList<TrdSuggestionModel, TrdGovRegItem>(category);
                    break;
                default:
                    resultList =
                        this.GetSuggestionElementsListByName(category)
                            .Select(x => new TrdSuggestionModel { Text = x.Text })
                            .ToList();
                    break;
            }

            return resultList;
        }

        /// <summary>
        /// Initialize supported categories.
        /// </summary>
        protected override sealed void InitializeSupportedCategories()
            =>
                this.SupportedCategories =
                    new List<NewTypeAheadCategories>
                        {
                            NewTypeAheadCategories.GovernmentAndRegulatoryMaterials,
                            NewTypeAheadCategories.ContentPages
                        };
    }
}
