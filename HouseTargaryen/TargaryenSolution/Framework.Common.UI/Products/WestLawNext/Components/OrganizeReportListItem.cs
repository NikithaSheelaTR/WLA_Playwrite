namespace Framework.Common.UI.Products.WestLawNext.Components
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The organize report list item
    /// </summary>
    public class OrganizeReportListItem : BaseItem
    {
        private static readonly By ItemInfoLocator = By.XPath(".//div[not (contains(@ng-if,'judges'))]/span[@class='ng-binding']");

        private static readonly By JudgeLocator = By.XPath(".//*[contains(@ng-if,'judges')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResearchReportListItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container Element.
        /// </param>
        public OrganizeReportListItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// The Item Info.
        /// </summary>
        public string ItemInfo => DriverExtensions.GetElement(this.Container, ItemInfoLocator).GetText();

        /// <summary>
        /// The Item Id.
        /// </summary>
        public string ItemId => DriverExtensions.GetElement(this.Container).GetAttribute("id");

        /// <summary>
        /// Gets or sets the judge.
        /// </summary>
        public string Judge
        {
            get
            {
                IWebElement webElement;
                DriverExtensions.TryGetElement(this.Container, JudgeLocator, out webElement);
                return webElement?.Text;
            }
        }

        /// <summary>
        /// Click on Item
        /// </summary>
        public void Click() => this.Container.Click();
    }
}