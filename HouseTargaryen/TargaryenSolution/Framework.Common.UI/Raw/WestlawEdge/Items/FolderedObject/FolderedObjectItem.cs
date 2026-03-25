namespace Framework.Common.UI.Raw.WestlawEdge.Items.FolderedObject
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Items;

    /// <summary>
    /// Foldered object item
    /// </summary>
    public class FolderedObjectItem : BaseItem
    {
        private static readonly By FolderNameLocator = By.XPath("./li[1]");

        private static readonly By DateLocator = By.XPath("./li[2]");

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="containerElement"></param>
        public FolderedObjectItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Date 
        /// </summary>
        public string Date => DriverExtensions.WaitForElement(this.Container, DateLocator).Text;

        /// <summary>
        /// Folder name
        /// </summary>
        public string FolderName => DriverExtensions.WaitForElement(this.Container, FolderNameLocator).Text;
    }
}
