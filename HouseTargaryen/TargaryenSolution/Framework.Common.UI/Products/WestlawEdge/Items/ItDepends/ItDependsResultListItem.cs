namespace Framework.Common.UI.Products.WestlawEdge.Items.ItDepends
{
    using System;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// 
    /// </summary>
    public class ItDependsResultListItem : BaseItem
    {
        private static readonly By DocumentTitleLocator = By.XPath(".//a[contains(@class,'tableTitleLink')]");
        private static readonly By CourtLocator = By.XPath(".//td[@class='ItDepends-tableCourt']");
        private static readonly By OutcomeLocator = By.XPath(".//td[@class='ItDepends-tableOutcome']");
        private static readonly By DateLocator = By.XPath(".//td[@class='ItDepends-tableDate']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ItDependsResultListItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// </param>
        public ItDependsResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Document name
        /// </summary>
        public string DocumentName => DriverExtensions.SafeGetElement(this.Container, DocumentTitleLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Court
        /// </summary>
        public string Court => DriverExtensions.SafeGetElement(this.Container, CourtLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Outcome
        /// </summary>
        public string Outcome => DriverExtensions.SafeGetElement(this.Container, OutcomeLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date => DateTime.Parse(DriverExtensions.SafeGetElement(this.Container, DateLocator).Text);
    }
}