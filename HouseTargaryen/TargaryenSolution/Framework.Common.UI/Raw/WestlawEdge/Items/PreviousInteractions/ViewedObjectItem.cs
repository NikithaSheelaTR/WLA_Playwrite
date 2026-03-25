namespace Framework.Common.UI.Raw.WestlawEdge.Items.PreviousInteractions
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Viewed object item
    /// </summary>
    public class ViewedObjectItem : BaseItem
    {
        private static readonly By DateLocator = By.XPath("./span[1]");

        private static readonly By TimeLocator = By.XPath("./span[2]");

        private static readonly By ClientIdLocator = By.XPath("./span[3]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="ViewedObjectItem"/> class. 
        /// </summary>
        /// <param name="containerElement">
        /// The Annotated Item Container.
        /// </param>
        public ViewedObjectItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Date
        /// </summary>
        public string Date => DriverExtensions.WaitForElement(this.Container, DateLocator).Text;

        /// <summary>
        /// Time
        /// </summary>
        public string Time => DriverExtensions.WaitForElement(this.Container, TimeLocator).Text;

        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId => DriverExtensions.WaitForElement(this.Container, ClientIdLocator).Text;
    }
}
