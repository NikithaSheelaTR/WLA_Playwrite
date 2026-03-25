namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.SelectAll;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.SearchQuestionAnswer;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Canada Common Search Result Page
    /// </summary>
    public class CanadaCommonSearchResultPage : EdgeCommonSearchResultPage
    {
        private static readonly By SelectAllComponentLocator = By.XPath("//ul[@class='co_navOptions']");
        private static readonly By ApplyButtonLocator = By.XPath("//button[@id='co_multifacet_selector_1_applyFacetFilter']");
        private static readonly By DateResultListLocator = By.XPath("//div[contains(@id,'co_searchResults_citation')]/span[4]");

        /// <summary>
        /// Date dropdown map
        /// </summary>
        protected EnumPropertyMapper<JudicialConsiderationSortByOptions, WebElementInfo> JcSortDropdownMap
            => EnumPropertyModelCache.GetMap<JudicialConsiderationSortByOptions, WebElementInfo>(
                string.Empty);

        /// <summary>
        /// The Question and Answer Result list component in Canada
        /// </summary>
        public CanadaSearchQuestionAnswerComponent CanadaQnAComponent { get; } = new CanadaSearchQuestionAnswerComponent();

        /// <summary>
        /// Gets the select all component.
        /// </summary>
        public SelectAllComponent SelectAllComponent { get; } = new SelectAllComponent(SelectAllComponentLocator);

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new CustomClickButton(ApplyButtonLocator);

        /// <summary>
        /// List of Datelist
        /// </summary>
        public IReadOnlyCollection<ILabel> DateResultList => new ElementsCollection<Label>(DateResultListLocator);

        /// <summary>
        /// Click on Apply filter button if displayed
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>Creates Page instance</returns>
        public T ClickApplyFilterButton<T>()
            where T : ICreatablePageObject
        {
            if (this.ApplyButton.Displayed)
            {
                return this.ApplyButton.Click<T>();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Selects the option from the SortBy dropdown.
        /// </summary>
        /// <param name="option"></param>
        public T SelectOptionFromSortByDropdown<T>(JudicialConsiderationSortByOptions option) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(this.JcSortDropdownMap[option].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
