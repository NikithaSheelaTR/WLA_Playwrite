namespace Framework.Common.UI.Products.WestlawEdge.Items.ItDepends
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Factor Item
    /// </summary>
    public class ItDependsFactorItem : BaseItem
    {
        private static readonly By PositiveCheckboxLocator = By.XPath(".//input[@value='positive']");
        private static readonly By NegativeCheckboxLocator = By.XPath(".//input[@value='negative']");
        private static readonly By NeutralCheckboxLocator = By.XPath(".//input[@value='neutral']");
        private static readonly By FactorNameLocator = By.XPath("td[1]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ItDependsFactorItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// </param>
        public ItDependsFactorItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Factor Name
        /// </summary>
        public string FactorName => DriverExtensions.SafeGetElement(this.Container, FactorNameLocator)?.Text ?? string.Empty;

        /// <summary>
        /// Set Positive Favor
        /// </summary>
        public void SetPositiveFavor() => DriverExtensions.GetElement(this.Container, PositiveCheckboxLocator).CustomClick();

        /// <summary>
        /// Set Negative Favor
        /// </summary>
        public void SetNegativeFavor() => DriverExtensions.GetElement(this.Container, NegativeCheckboxLocator).CustomClick();

        /// <summary>
        /// Set Neutral Favor
        /// </summary>
        public void SetNeutralFavor() => DriverExtensions.GetElement(this.Container, NeutralCheckboxLocator).CustomClick();
    }
}