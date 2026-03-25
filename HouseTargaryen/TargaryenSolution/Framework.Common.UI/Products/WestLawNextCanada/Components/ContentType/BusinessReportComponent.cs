namespace Framework.Common.UI.Products.WestLawNextCanada.Components.ContentType
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// BusinessReport Component
    /// </summary>
    public class BusinessReportComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_search_advancedSearchDocumentFieldsBox_1");

        private static readonly By ChildResultListLocator = By.XPath(".//*[@class='co_search_advancedSearch_verticalList co_2Column']//descendant::input");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// List of DocumentLinkslist
        /// </summary>
        public IReadOnlyCollection<ILink> ChildResultList => new ElementsCollection<Link>(ComponentLocator, ChildResultListLocator);
    }
}
