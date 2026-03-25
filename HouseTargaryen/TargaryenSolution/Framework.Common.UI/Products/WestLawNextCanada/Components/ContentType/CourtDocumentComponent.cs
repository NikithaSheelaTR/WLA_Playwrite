namespace Framework.Common.UI.Products.WestLawNextCanada.Components.ContentType
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Instance of Canada Court document Content Types tab
    /// </summary>
    public class CourtDocumentComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_relatedInfo_mainContentWrapper");

        private static readonly By DocumentTitleLinksLocator = By.XPath(".//*[@class='co_detailsTable_content']//descendant::a");

        private static readonly By DateResultListLocator = By.XPath(".//tbody/tr/td[4]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// List of DocumentTitleLinkslist
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentTitleLinks => new ElementsCollection<Link>(ComponentLocator, DocumentTitleLinksLocator);

        /// <summary>
        /// List of Datelist
        /// </summary>
        public IReadOnlyCollection<ILabel> DateResultList => new ElementsCollection<Label>(ComponentLocator, DateResultListLocator);
    }
}
