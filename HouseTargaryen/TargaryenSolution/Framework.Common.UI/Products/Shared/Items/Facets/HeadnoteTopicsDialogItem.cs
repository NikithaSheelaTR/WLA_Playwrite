namespace Framework.Common.UI.Products.Shared.Items.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Headnote Item element into Headnote Topics Dialog
    /// TODO: Remove using Item from tests
    /// </summary>
    public class HeadnoteTopicsDialogItem : BaseItem
    {
        private static readonly By HeadnoteCheckboxLocator = By.XPath("./span/input[@type='checkbox']");

        private static readonly By HeadnoteCheckboxTextLocator =
            By.XPath(".//span[contains(@id, 'co_relatedInfo_count_checkbox_')]");

        private static readonly By HeadnoteCountLocator = By.XPath("./div[@class='co_headnoteNumber']");

        private static readonly By HeadnoteItemLocator = By.XPath("./ol");

        private static readonly By HeadnoteTopicDetailsLocator = By.XPath(
            "./span[@class='co_relatedInfo_topicDetails']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="HeadnoteTopicsDialogItem"/> class. 
        /// </summary>
        /// <param name="container"> Element container </param>
        public HeadnoteTopicsDialogItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Count
        /// </summary>
        public int Count
            => DriverExtensions.GetElement(this.Container, HeadnoteCountLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Head Note Check box
        /// </summary>
        public IWebElement HeadNoteCheckbox
            => DriverExtensions.GetElement(this.Container, HeadnoteCheckboxLocator);

        /// <summary>
        /// Head Note Check box Text
        /// </summary>
        public string HeadNoteCheckboxText
            => DriverExtensions.GetElement(this.Container, HeadnoteCheckboxTextLocator).Text;

        /// <summary>
        /// Head notes
        /// </summary>
        public string Headnotes => DriverExtensions.GetElement(this.Container, HeadnoteItemLocator).Text;

        /// <summary>
        /// Topic Detail value
        /// </summary>
        public string TopicDetail
            => DriverExtensions.GetElement(this.Container, HeadnoteTopicDetailsLocator).Text;
    }
}