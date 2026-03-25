namespace Framework.Common.UI.Raw.WestlawEdge.Items.Facets
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The official Headnote dialog item.
    /// </summary>
    public class OfficialHeadnoteDialogItem : BaseItem
    {
        private static readonly By HeadnoteCheckboxLocator = By.XPath(".//input[@type='checkbox']");
        private static readonly By HeadnoteCountLocator = By.XPath(".//span[@class='HeadnoteNumber']");
        private static readonly By CitingReferencesCountLocator = By.XPath(".//label[@class='CitingReferencesAmount']/span");

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficialHeadnoteDialogItem"/> class. 
        /// </summary>
        /// <param name="container"> Element container </param>
        public OfficialHeadnoteDialogItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Count
        /// </summary>
        public int Count => DriverExtensions.GetElement(this.Container, HeadnoteCountLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Sets a checkbox for a headnote 
        /// </summary>
        /// <param name="selected"></param>
        public void SetHeadnoteCheckbox(bool selected)
            => DriverExtensions.SetCheckbox(selected, this.Container, HeadnoteCheckboxLocator);

        /// <summary>
        /// Gets CitingReferences Count 
        /// </summary>
        public int CitingReferencesCount => DriverExtensions.GetElement(this.Container, CitingReferencesCountLocator).Text.ConvertCountToInt();
    }
}
