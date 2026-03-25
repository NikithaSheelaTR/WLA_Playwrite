namespace Framework.Common.UI.Products.WestlawEdge.Components.Folders
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.TourComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Tours;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Tour on the new Folders redesign page
    /// </summary>
    public class FoldersTourComponent : BaseModuleRegressionComponent
    {
        private static readonly By TourCardLocator = By.XPath("//*[contains(@class,'tour-FoldersPageStep')]");
        
        /// <summary>
        /// Folders tour card component
        /// </summary>
        public TourCardComponent<FoldersTourCards> TourCardComponent => new TourCardComponent<FoldersTourCards>();

        /// <summary>
        /// Folders page tour take the tour component options: 
        /// Take the tour, Maybe later, Don't show me this
        /// </summary>
        public TakeTheTourComponent TakeTheTourComponent { get; set; } = new TakeTheTourComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TourCardLocator;

        /// <summary>
        /// Is the Tour card displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 30);
    }
}
