namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Within Facet
    /// </summary>
    public class EdgeSearchWithinFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'SearchFacetSearchWithin')]");
        private static readonly By CrossedOutPreviousSearchesLocator = By.XPath(".//del[contains(@class, 'searchTermItem co_inline')]");
        private static readonly By PreviousSearchesLocator = By.XPath(".//*[@class = 'searchWithin-list']//li");
        private static readonly By RemoveSearchLinkLocator = By.XPath(".//button[contains(@class, 'SearchFacet-inputTextRemove')]");
        private static readonly By RemoveAllPreviousSearchesButtonLocator = By.Id("searchWithinRemoveButton");
        private static readonly By SearchButtonLocator = By.XPath(".//button[contains(@class, 'SearchFacet-buttonSearch--secondary')]");
        private static readonly By TextboxLocator = By.XPath("//input[contains(@name, 'SearchFacetSearchWithin')] | .//*[@id='SearchFacetSearchWithin-inputKeyword']");
        private static readonly By SearchWithinResultsLocator = By.XPath("//button[@class='SearchFacet-button co_secondaryBtn']");


        /// <summary>
        /// Remove all previous searches button
        /// </summary>
        public IButton RemoveAllPreviousSearchesButton => new Button(this.ComponentLocator, RemoveAllPreviousSearchesButtonLocator);

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(this.ComponentLocator, SearchButtonLocator);

        /// <summary>
        /// Remove search link
        /// </summary>
        public ILink RemoveSearchLink => new Link(this.ComponentLocator, RemoveSearchLinkLocator);

        /// <summary>
        /// Search Within textbox
        /// </summary>
        public ITextbox SearchWithinTextbox => new Textbox(this.ComponentLocator, TextboxLocator);

        /// <summary>
        /// Previous searches labels
        /// </summary>
        public IReadOnlyCollection<ILabel> PreviousSearchesLabels => new ElementsCollection<Label>(this.ComponentLocator, PreviousSearchesLocator);

        /// <summary>
        /// Crossed out previous searches labels
        /// </summary>
        public IReadOnlyCollection<ILabel> CrossedOutPreviousSearchesLabels => new ElementsCollection<Label>(this.ComponentLocator, CrossedOutPreviousSearchesLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Opens Search within dialog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T OpenSearchWithinDialog<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(SearchWithinResultsLocator).Click();
            DriverExtensions.GetElement(TextboxLocator).Click();
            return DriverExtensions.CreatePageInstance<T>("Facet");
        }

        /// <summary>
        /// Open Multiple Search Within dialog
        /// </summary>
        /// <returns>New instance of the dialog</returns>
        public MultipleSearchWithinDialog OpenMultipleSearchWithinDialog()
        {
            DriverExtensions.GetElement(SearchWithinResultsLocator).Click();
            DriverExtensions.GetElement(TextboxLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<MultipleSearchWithinDialog>();
        }

        /// <summary>
        /// Open Search Within this folder dialog
        /// </summary>
        /// <returns>New instance of the dialog</returns>
        public EdgeSearchWithinThisFolder OpenSearchWithinThisFolderDialog()
        {
            DriverExtensions.GetElement(SearchWithinResultsLocator).CustomClick();
            return new EdgeSearchWithinThisFolder();
        }

        /// <summary>
        /// Apply a Search within facet
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="query">Query to search for</param>
        /// <returns> New instance of the page</returns>
        public T ApplyFacet<T>(string query) where T : ICreatablePageObject
        {
            //Please do not change this locator.  
            //If SearchWithin facet opens as Dialog, please use appropriate method from EdgeSearchWithinDialog
            DriverExtensions.WaitForElement(TextboxLocator).SetTextField(query);
            return this.SearchButton.Click<T>();
        }
    }
}