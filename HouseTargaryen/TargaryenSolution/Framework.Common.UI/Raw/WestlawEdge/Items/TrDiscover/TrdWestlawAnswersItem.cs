namespace Framework.Common.UI.Raw.WestlawEdge.Items.TrDiscover
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The westlaw analytics answers item.
    /// </summary>
    public class TrdWestlawAnswersItem : TrdBaseCategoryItem
    {
        private static readonly By JurisdictionLocator = By.XPath(".//span[contains(@class,'co_unlink')]");
        private static readonly By TextLocator = By.XPath("./span[not(contains(@class,'co_unlink'))]");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrdWestlawAnswersItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public TrdWestlawAnswersItem(IWebElement container)
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
            => DriverExtensions.GetElement(this.Container, JurisdictionLocator)?.Text ?? string.Empty;
    }
}
