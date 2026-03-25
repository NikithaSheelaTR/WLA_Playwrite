namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Each item represents item in the list of Outlines
    /// </summary>
    public class OutlineItem : BaseItem
    {
        private static readonly By RemoveButtonLocator = By.CssSelector("button.OutlineBuilder-outlineList--delete");
        private static readonly By ItemNameLocator = By.CssSelector("button.OutlineBuilder-outlineListLink.co_linkBlue");
        private static readonly By ConfirmDeletionLocator = By.CssSelector("button#OutlineBuilder-confirmDeleteButton");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="OutlineItem"/> class. 
        /// </summary>
        /// <param name="container">Container</param>
        public OutlineItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Remove button
        /// </summary>
        private IButton DeleteButton => new Button(this.Container, RemoveButtonLocator);        

        /// <summary>
        /// Confirm deletion button
        /// </summary>
        private IButton ConfirmDeleteButton => new Button(this.Container, ConfirmDeletionLocator);

        /// <summary>
        /// Title button
        /// </summary>
        public IButton TitleButton => new Button(this.Container, ItemNameLocator);

        /// <summary>
        /// Click Delete and Confirm deletion button
        /// </summary>
        public TWebObject DeleteOutline<TWebObject>() where TWebObject : ICreatablePageObject
        {
            this.DeleteButton.Click<EdgeCommonDocumentPage>();
            ConfirmDeleteButton.Click<EdgeCommonDocumentPage>();
            return DriverExtensions.CreatePageInstance<TWebObject>();
        }
    }
}