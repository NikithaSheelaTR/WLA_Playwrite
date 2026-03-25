namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// FoldersComponent Widget on homepage
    /// </summary>
    public class FoldersComponent : BaseModuleRegressionComponent
    {
        private static readonly By FolderComponentLocator = By.XPath(".//*[@id= 'co_dockContainer']");
        private static readonly By FolderComponentTitleLocator = By.Id("co_dockTitle");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => FolderComponentLocator;

         /// <summary>
         /// Folders widget
         /// </summary>
        public IWebElement FoldersWidget => DriverExtensions.GetElement(this.ComponentLocator);

        /// <summary>
        /// Folder Component Title Label
        /// </summary>
        public ILabel FolderComponentTitle { get; } = new Label(FolderComponentTitleLocator);
    }
}
