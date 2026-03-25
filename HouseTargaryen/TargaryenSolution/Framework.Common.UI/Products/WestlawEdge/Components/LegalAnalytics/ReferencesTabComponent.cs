namespace Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics
{
    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ReferencesTabComponent
    /// </summary>
    public class ReferencesTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_la_references_tab");

        /// <summary>
        /// NarrowPane
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane => new EdgeNarrowPaneComponent();

        /// <summary>
        /// ResultList
        /// </summary>
        public ReferenceCitedGridComponent ResultList => new ReferenceCitedGridComponent();

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "References";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
