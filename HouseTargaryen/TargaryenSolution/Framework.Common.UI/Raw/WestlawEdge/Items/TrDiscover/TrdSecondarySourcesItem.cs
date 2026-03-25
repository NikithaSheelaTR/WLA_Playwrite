namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd secondary sources item.
    /// </summary>
    public class TrdSecondarySourcesItem : TrdBaseCategoryItem
    {
        private static readonly By TextLocator = By.XPath(".//span");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdSecondarySourcesItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdSecondarySourcesItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// The text.
        /// </summary>
        public override string Text => DriverExtensions.GetElement(this.Container, TextLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Is displayed
        /// </summary>
        public bool IsDisplayed => this.Container.IsDisplayed();
    }
}