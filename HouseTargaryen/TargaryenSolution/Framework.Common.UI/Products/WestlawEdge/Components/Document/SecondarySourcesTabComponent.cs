namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Items.RelatedDocument;
    using Framework.Common.UI.Raw.WestlawEdge.Models.RelatedDocuments;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Secondary sources tab component
    /// </summary>
    public class SecondarySourcesTabComponent : RelatedDocumentsBaseTabComponents
    {
        private static readonly By SecondarySourcesItemContainer = By.XPath("//div[@id='panel_secondarySourcesTabId']//li");

        private static readonly By ContainerLocator = By.Id("tab_secondarySourcesTabId");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Secondary Sources";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets list of Secondary sources documents
        /// </summary>
        /// <returns> list of Secondary sources documents  </returns>
        public List<RelatedDocumentsBaseItemModel> GetListOfDocuments() => this.GetListOfDocumentItems()
            .Select(item => item.ToModel<RelatedDocumentsBaseItemModel>())
            .ToList();

        private List<SecondarySourcesItem> GetListOfDocumentItems()
            => DriverExtensions.GetElements(SecondarySourcesItemContainer)
                               .Select(item => new SecondarySourcesItem(item)).ToList();

        /// <summary>
        /// Click on title link by index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The new item of T class</returns>
        public T ClickTitleLinkByIndex<T>(int index)
            where T : ICommonDocumentPage => this.GetListOfDocumentItems().ElementAt(index).ClickTitleLink<T>();
    }
}
