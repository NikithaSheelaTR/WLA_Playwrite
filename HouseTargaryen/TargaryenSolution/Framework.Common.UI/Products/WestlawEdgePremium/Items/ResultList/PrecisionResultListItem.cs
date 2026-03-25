namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.ResultList
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList;
    using Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Precision result list item.
    /// </summary>
    public class PrecisionResultListItem : EdgeResultListItem
    {
        private static readonly By HideButtonLocator = By.XPath(".//button//span[contains(@class, 'upCaret')]");
        private static readonly By ShowButtonLocator = By.XPath(".//button//span[contains(@class, 'downCaret')]");
        private static readonly By AddToKeepListButtonLocator = By.XPath(".//input[contains(@class, 'Keeplist-checkbox') and @aria-checked='false']");
        //private static readonly By AddToKeepListButtonLocator = By.XPath(".//input[@aria-checked='false' and contains(@class, 'Keeplist-checkbox-input')]");
        private static readonly By AddToKeepListLabelLocator = By.XPath(".//span[@class='KeepList-addTo-title']");
        private static readonly By RemoveFromKeepListButtonLocator = By.XPath(".//input[@aria-checked='true']/following-sibling::*[contains(@class, 'KeepList-addTo')] | .//input[@aria-checked='true' and contains(@class, 'KeepList-addTo')]");
        private static readonly By MltBrowseBoxLocator = By.XPath(".//div[@class='Athens-browseBox' or @class='Athens-browseBox Athens-browseBox-mlt--collapsed' or @class='Athens-bestHeadnote']");
        private static readonly By CitingTagButtonsLocator = By.XPath(".//span[contains(@class,'premium24_')]/parent::div/span[2]");
        private static readonly By InfoBoxLocator = By.XPath("//div[contains(@class, 'top co_messageBoxContainer co_infoBox')]");
        private static readonly By FolderedIconButtonLocator = By.XPath(".//li[@class = 'co_document_icon_foldered']//a");
        private static readonly By CitingTagsLabelLocator = By.XPath(".//*[@class='Athens-citing-tags-label']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public PrecisionResultListItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Browse box component
        /// </summary>
        public PrecisionFullBrowseBoxComponent BrowseBox => new PrecisionFullBrowseBoxComponent(this.Container);

        /// <summary>
        /// Best headnote box component
        /// </summary>
        public BestHeadnoteBoxComponent BestHeadnoteBox => new BestHeadnoteBoxComponent(this.Container);

        /// <summary>
        /// List MLT blue boxes components
        /// </summary>
        public List<PrecisionMltFullBrowseBoxComponent> MltFullBrowseBox => DriverExtensions.GetElements(this.Container, MltBrowseBoxLocator).Select(item => new PrecisionMltFullBrowseBoxComponent(item)).ToList();

        /// <summary>
        /// Athens Hide button
        /// </summary>
        public IButton HideButton => new Button(this.Container, HideButtonLocator);

        /// <summary>
        /// Athens Show button
        /// </summary>
        public IButton ShowButton => new Button(this.Container, ShowButtonLocator);

        /// <summary>
        /// Athens Add to Keep List button
        /// </summary>
        public IButton AddToKeepListButton => new Button(this.Container, AddToKeepListButtonLocator);

        /// <summary>
        /// Athens Remove from Keep List button
        /// </summary>
        public IButton RemoveFromKeepListButton => new Button(this.Container, RemoveFromKeepListButtonLocator);

        /// <summary>
        /// Athens Add to Keep List label
        /// </summary>
        public ILabel AddToKeepListLabel => new Label(this.Container, AddToKeepListLabelLocator);

        /// <summary>
        /// Similarities Buttons
        /// </summary>
        public IReadOnlyCollection<IButton> SimilaritiesButtons => new ElementsCollection<Button>(this.Container, CitingTagButtonsLocator);
        
        /// <summary>
        /// Info box when we click to any button like Legal Issue, Motion Type and so on
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(InfoBoxLocator);

        /// <summary>
        /// Athens Foldered button
        /// </summary>
        public IButton FolderedIconButton => new Button(this.Container, FolderedIconButtonLocator);

        /// <summary>
        /// Athens Citing Tags Label
        /// </summary>
        public ILabel CitingTagsLabel => new Label(this.Container, CitingTagsLabelLocator);

        /// <summary>
        /// Scroll to list item
        /// </summary>
        public void ScrollToListItem() => this.Container.ScrollToElement();
    }
}
