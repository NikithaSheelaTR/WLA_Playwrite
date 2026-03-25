namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The trd jurisdiction item.
    /// </summary>
    public class TrdStateAndFederalItem : TrdBaseCategoryItem
    {
        private static readonly By JurisdictionLocator = By.XPath(".//li[contains(@ng-if,'court')]");
        private static readonly By TextLocator = By.XPath(".//span");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdStateAndFederalItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdStateAndFederalItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// The link text.
        /// </summary>
        public override string Text => DriverExtensions.GetElement(this.Container, TextLocator)?.Text ?? string.Empty;

        /// <summary>
        /// The jurisdiction text.
        /// </summary>
        public string Jurisdiction
            => DriverExtensions.SafeGetElement(this.Container, JurisdictionLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Is displayed
        /// </summary>
        public bool IsDisplayed => this.Container.IsDisplayed();
    }
}