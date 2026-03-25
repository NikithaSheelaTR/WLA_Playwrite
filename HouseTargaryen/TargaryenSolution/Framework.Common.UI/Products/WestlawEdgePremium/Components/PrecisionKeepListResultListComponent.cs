namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using System;

    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Items;

    /// <summary>
    /// Precision keep list result list
    /// </summary>
    public class PrecisionKeepListResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By ResultListItemLocator = By.ClassName("ResultItem");
        private static readonly By ResultListContainerLocator = By.ClassName("ResultList");
        private const string GuidItemLctMask = ".//h3/a[contains(@href, '{0}')]/ancestor::li";

        /// <summary>
        /// The get result list items.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<T> GetItems<T>() where T: BaseItem
        {
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), ResultListItemLocator);
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.GetElements(this.ComponentLocator, ResultListItemLocator)
                                   .Select(item => (T)Activator.CreateInstance(typeof(T), item));
        }

        /// <summary>
        /// Gets item from result list by document guid
        /// </summary>
        /// <param name="guid">document guid</param>
        /// <returns>TItem instance</returns>
        public T GetEdgeResultListItemByGuid<T>(string guid) where T : BaseItem
        {
            var item = DriverExtensions.GetElement(this.ComponentLocator,
                By.XPath(string.Format(GuidItemLctMask, guid)));
            return (T)Activator.CreateInstance(typeof(T), item);
        }

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => ResultListContainerLocator;
    }
}