namespace Framework.Common.UI.Raw.WestlawEdge.Items.GradingTool
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.GradingTool;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The assignment item on the Assignment page.
    /// </summary>
    public class AssignmentItem: BaseItem
    {
        private static readonly By QueryNameLinkLocator = By.XPath(".//a[@class='gradingToolAssignmentLink']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="AssignmentItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> The Assignment Item. </param>
        public AssignmentItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// The query name.
        /// </summary>
        public string QueryName => DriverExtensions.GetElement(this.Container, QueryNameLinkLocator).Text;

        /// <summary>
        /// Clicks Query Name Link.
        /// </summary>
        /// <returns> The <see cref="EdgeGradingToolSearchResultPage"/>. </returns>
        public EdgeGradingToolSearchResultPage ClickQueryNameLink()
        {
            DriverExtensions.Click(this.Container, QueryNameLinkLocator);
            return new EdgeGradingToolSearchResultPage();
        }
    }
}