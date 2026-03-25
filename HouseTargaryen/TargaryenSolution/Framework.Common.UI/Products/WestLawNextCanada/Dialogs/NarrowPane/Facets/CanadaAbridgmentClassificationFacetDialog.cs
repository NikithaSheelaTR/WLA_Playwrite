namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Canada Abridgment Classification Facet Component
    /// </summary>
    public class CanadaAbridgmentClassificationFacetDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private static readonly By ContainerLocator = By.Id("co_facet_keynumber_popup");
        private static readonly By AbridgmentClassificationOptionsLocator = By.XPath("//ul[@id='co_facet_keynumber_availableOptions']/li//a[@role='checkbox']");
        private static readonly By FilterButtonLocator = By.Id("co_facet_keynumber_filterButton");

        /// <summary>]
        /// Container
        /// </summary>
        protected override IWebElement Container =>
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Clicks random Abridgment Classification option in the dialog
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>new page instance</returns>
        public T ClickRandomAbridgmentClassificationOption<T>() where T : ICreatablePageObject
        {
            this.GetAbridgmentClassificationOptions().ElementAt(new Random().Next(0, 7)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on Filter button in Abridgment Classification dialog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>new page instance</returns>
        public T ClickAbridgmentClassificationFilterButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(FilterButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        private IReadOnlyCollection<IWebElement> GetAbridgmentClassificationOptions() => DriverExtensions.GetElements(AbridgmentClassificationOptionsLocator);
    }
}