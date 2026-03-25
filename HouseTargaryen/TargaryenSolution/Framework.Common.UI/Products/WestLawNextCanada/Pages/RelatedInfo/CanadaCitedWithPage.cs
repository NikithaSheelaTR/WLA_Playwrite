namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNextCanada.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Co-cite Page
    /// </summary>
    public class CanadaCitedWithPage : EdgeTabPage
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id = 'co_pageContainer']");
        private static readonly By CoCitedItemsLocator = By.XPath(".//li[@class = 'ResultItem']");
        private static readonly By CitedWithTitleTextLocator = By.ClassName("co_relatedInfo_subHeading");

        /// <summary>
        /// Canada CoCites Toolbar component
        /// </summary>
        public new CanadaToolbarComponent Toolbar { get; set; } = new CanadaToolbarComponent(ContainerLocator);

        /// <summary>
        /// Left Narrow Pane component in Canada
        /// </summary>
        public LeftNarrowPaneComponent LeftNarrowPane { get; } = new LeftNarrowPaneComponent();

        /// <summary>
        /// List of co-cite items
        /// </summary>
        public ItemsCollection<CanadaCoCitationsItem> CoCiteList => new ItemsCollection<CanadaCoCitationsItem>(ContainerLocator, CoCitedItemsLocator);

        /// <summary>
        /// Get the text present in Cited with title
        /// </summary>
        /// <returns></returns>
        public string GetCitedWithTitleText() => DriverExtensions.GetElement(CitedWithTitleTextLocator).Text;
        
    }
}