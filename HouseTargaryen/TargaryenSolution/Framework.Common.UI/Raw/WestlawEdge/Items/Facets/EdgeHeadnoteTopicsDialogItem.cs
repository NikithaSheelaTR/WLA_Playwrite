namespace Framework.Common.UI.Raw.WestlawEdge.Items.Facets
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge HeadnoteTopics Item
    /// </summary>
    public class EdgeHeadnoteTopicsDialogItem : BaseItem
    {
        private static readonly By CountLocator = By.ClassName("HeadnoteNumber");
        private static readonly By HeaderLocator = By.ClassName("HeadnoteSubheader");
        private static readonly By CheckboxLocator = By.TagName("input");
        private static readonly By CheckboxTextLocator = By.ClassName("CitingReferencesAmount");

       /// <summary>
       /// Initializes a new instance of the <see cref="EdgeHeadnoteTopicsDialogItem"/> class. 
       /// </summary>
       /// <param name="container"> Container </param>
       public EdgeHeadnoteTopicsDialogItem(IWebElement container)
           : base(container)
       {
       }

       /// <summary>
        /// Count
        /// </summary>
        public int Count => DriverExtensions.GetElement(this.Container, CountLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Is checkbox displayed
        /// </summary>
        public bool IsCheckboxDisplayed => DriverExtensions.IsDisplayed(this.Container, CheckboxLocator);

        /// <summary>
        /// Checkbox text
        /// </summary>
        public string CheckboxText => DriverExtensions.GetElement(this.Container, CheckboxTextLocator).Text;

        /// <summary>
        /// Header
        /// </summary>
        public string Header => DriverExtensions.GetElements(this.Container, HeaderLocator)[0].Text;
    }
}