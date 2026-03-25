namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd other document item.
    /// </summary>
    public class TrdOtherItem : TrdBaseCategoryItem
    {
        private static readonly By TextLocator = By.XPath(".//span");
        private static readonly By JurisdictionLocator = By.XPath(".//li[contains(@ng-if,'court')]");
        private static readonly By EntityLocator = By.XPath(".//*[contains(@ng-if,'DocType') or @key]");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdOtherItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdOtherItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// The text.
        /// </summary>
        public override string Text => DriverExtensions.GetElement(this.Container, TextLocator)?.Text ?? string.Empty;

        /// <summary>
        /// The jurisdiction.
        /// </summary>
        public string Jurisdiction
            => DriverExtensions.SafeGetElement(this.Container, JurisdictionLocator)?.Text ?? string.Empty;

        /// <summary>
        /// The entity.
        /// </summary>
        public string Entity => DriverExtensions.SafeGetElement(this.Container, EntityLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Is displayed
        /// </summary>
        public bool IsDisplayed => this.Container.IsDisplayed();
    }
}