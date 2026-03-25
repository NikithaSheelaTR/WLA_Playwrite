namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd legislation item.
    /// </summary>
    public class TrdLegislationItem : TrdBaseCategoryItem
    {
        private static readonly By TextLocator = By.XPath(".//span");
        private static readonly By JurisdictionLocator = By.XPath(".//li[contains(@ng-if,'court')]");
        private static readonly By DescriptionLocator = By.XPath(".//li[contains(@ng-repeat,'cite in')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdLegislationItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdLegislationItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// The text.
        /// </summary>
        public override string Text => DriverExtensions.GetElement(this.Container, TextLocator)?.Text ?? string.Empty;

        /// <summary>
        /// The jurisdiction text.
        /// </summary>
        public string Jurisdiction
            => DriverExtensions.SafeGetElement(this.Container, JurisdictionLocator)?.Text ?? string.Empty;

        /// <summary>
        /// The jurisdiction text.
        /// </summary>
        public string Description
            => DriverExtensions.SafeGetElement(this.Container, DescriptionLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Is displayed
        /// </summary>
        public bool IsDisplayed => this.Container.IsDisplayed();
    }
}
