namespace Framework.Common.UI.Raw.WestlawEdge.Items.PreviousInteractions
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Annotated Item 
    /// </summary>
    public class AnnotatedItem : BaseItem
    {
        private static readonly By HighlightedTextLocator = By.XPath(".//div[contains(@class, 'co_hl')]");

        private static readonly By NoteTextLocator = By.XPath(".//div[@class = 'co_viewNoteText']");

        private static readonly By DateLocator = By.XPath(".//span[@class = 'co_noteBody_date']");

        private static readonly By TimeLocator = By.XPath(".//span[@class = 'co_noteBody_time']");

        private static readonly By ClientIdLocator = By.XPath(".//span[@class = 'co_noteHeader_clientID']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="AnnotatedItem"/> class. 
        /// </summary>
        /// <param name="containerElement">
        /// The Annotated Item Container.
        /// </param>
        public AnnotatedItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Highlighted text
        /// </summary>
        public string HighlightedText => DriverExtensions.IsDisplayed(this.Container, HighlightedTextLocator)?DriverExtensions.WaitForElement(this.Container, HighlightedTextLocator).Text : string.Empty;

        /// <summary>
        /// Note text
        /// </summary>
        public string NoteText => DriverExtensions.WaitForElement(this.Container, NoteTextLocator).Text;

        /// <summary>
        /// Annotation date
        /// </summary>
        public string Date => DriverExtensions.WaitForElement(this.Container, DateLocator).Text;

        /// <summary>
        /// Annotation time
        /// </summary>
        public string Time => DriverExtensions.WaitForElement(this.Container, TimeLocator).Text;

        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId => DriverExtensions.WaitForElement(this.Container, ClientIdLocator).Text;
    }
}
