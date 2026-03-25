namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Facets;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Raw.WestlawEdge.Models.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Previously Viewed facet
    /// </summary>
    public class PreviouslyViewedFacetComponent : EdgeBaseFacetComponent
    {
        private static readonly By ApplyButtonLocator = By.XPath("//button[.='Apply']");
        private static readonly By AppliedItemLocator = By.XPath(".//div[@class='SearchFacet-output']//span[@class = 'SearchFacet-outputTextValue']");
        private static readonly By ContainerLocator = By.XPath("//section[contains(@class, 'SearchFacetPreviouslyViewed')]");
        private static readonly By FacetOptionLocator = By.XPath(".//div[@role = 'listitem']");
        private static readonly By RemoveButtonLocator = By.XPath("//span[(contains(text(), 'Remove') or contains(text(), 'Retirer'))]//parent::button[contains(@class, 'tertiary')]");
        private static readonly By UndoButtonLocator = By.XPath("//span[contains(text(), 'Undo')]//parent::button[contains(@class, 'tertiary')]");

        private EnumPropertyMapper<PreviouslyViewedOption, WebElementInfo> previouslyViewedOptionsMap;

        /// <summary>
        /// Facet radiobutton
        /// </summary>
        public IRadiobutton Radiobutton(PreviouslyViewedOption option) => new Radiobutton(this.ComponentLocator, By.XPath(string.Format(this.PreviouslyViewedOptionMap[option].LocatorString)));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
        
        /// <summary>
        /// Previously viewed options map
        /// </summary>
        private EnumPropertyMapper<PreviouslyViewedOption, WebElementInfo> PreviouslyViewedOptionMap =>
            this.previouslyViewedOptionsMap = this.previouslyViewedOptionsMap
                                              ?? EnumPropertyModelCache.GetMap<PreviouslyViewedOption, WebElementInfo>(
                                                  string.Empty,
                                                  @"Resources/EnumPropertyMaps/WestlawEdge/Facets");
        
        /// <summary>
        /// Apply facet
        /// </summary>
        /// <typeparam name="T">type of the page</typeparam>
        /// <param name="option">Option</param>
        /// <returns>New instance of the page</returns>
        public T ApplyFacet<T>(PreviouslyViewedOption option) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(this.PreviouslyViewedOptionMap[option].LocatorString)))
                            .Click();

            if (DriverExtensions.IsDisplayed(ApplyButtonLocator, 5))
            {
                DriverExtensions.GetElement(ApplyButtonLocator).Click();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets applying query text 
        /// </summary>
        /// <returns>The list of output items</returns>
        public List<string> GetAppliedOptions() => 
            DriverExtensions.GetElements(this.ComponentLocator, AppliedItemLocator).Select(item => item.Text).ToList();

        /// <summary>
        /// The get options as models.
        /// </summary>
        /// <returns>
        /// The collection of facet option models
        /// </returns>
        public IEnumerable<FacetOptionModel> GetOptionsAsModels() =>
            this.GetOptionItems().Select(item => item.ToModel<FacetOptionModel>());

        /// <summary>
        /// The Get option item
        /// </summary>
        /// <param name="option">The desired option</param>
        /// <returns>The <see cref="FacetOptionItem"/> class</returns>
        public FacetOptionModel GetOptionAsModel(PreviouslyViewedOption option) => this
            .GetOptionItems()
            .First(item => item.Title.ToLower().Contains(this.PreviouslyViewedOptionMap[option].Text.ToLower()))
            .ToModel<FacetOptionModel>();
        
        /// <summary>
        /// The get option items.
        /// </summary>
        /// <returns>
        /// The list of facet options
        /// </returns>
        private IEnumerable<FacetOptionItem> GetOptionItems()
        {
            this.ExpandFacet();
            return DriverExtensions.GetElements(this.ComponentLocator, FacetOptionLocator)
                                   .Select(item => new FacetOptionItem(item, "PreviouslyViewed"));
        }

        /// <summary>
        /// Is 'Remove' button displayed
        /// </summary>
        /// <returns>True if the button is displayed, false otherwise.</returns>
        public bool IsRemoveButtonDisplayed() => DriverExtensions.IsDisplayed(RemoveButtonLocator);

        /// <summary>
        /// Is 'Undo' button displayed
        /// </summary>
        /// <returns>True if the button is displayed, false otherwise.</returns>
        public bool IsUndoButtonDisplayed() => DriverExtensions.IsDisplayed(UndoButtonLocator);

        /// <summary>
        /// Click 'Remove' button
        /// </summary>
        public void ClickRemoveButton() => DriverExtensions.GetElement(RemoveButtonLocator).Click();

        /// <summary>
        /// Click 'Undo' button
        /// </summary>
        public void ClickUndoButton() => DriverExtensions.GetElement(UndoButtonLocator).Click();
    }
}