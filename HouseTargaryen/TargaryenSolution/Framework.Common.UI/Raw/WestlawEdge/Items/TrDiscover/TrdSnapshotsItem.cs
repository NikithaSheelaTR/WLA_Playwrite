namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd snapshots item.
    /// </summary>
    public class TrdSnapshotsItem : TrdBaseCategoryItem
    {
        private static readonly By TextLocator = By.XPath(".//a/strong");
        private static readonly By DescriptionLocator = By.XPath(".//ul[contains(@class, 'co_inlineList')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdSnapshotsItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdSnapshotsItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// The link text.
        /// </summary>
        public override string Text => DriverExtensions.GetElement(this.Container, TextLocator)?.Text ?? string.Empty;
        

        /// <summary>
        /// The description text.
        /// </summary>
        public string Description
            => DriverExtensions.GetElement(this.Container, DescriptionLocator).Text ?? string.Empty;
    }
}