namespace Framework.Common.UI.Products.WestLawAnalytics.Items.BillingResultGridItems
{
    using System;
    using System.Globalization;
    using Framework.Common.UI.Interfaces.Models;
    using Framework.Common.UI.Utils.Mapper;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents one row in billing results table
    /// </summary>
    public class GridRowItem : IMappable
    {
        private const string ContainerMask = "//div[@id='{0}']";

        private static readonly By GridColumnChargeableLocator = By.XPath(".//div[contains(@class,'Chargeable')]/div");

        private static readonly By GridColumnClientIdLocator = By.XPath(".//div[contains(@class,'ClientId')]/div");

        private static readonly By GridColumnDateLocator = By.XPath(".//div[contains(@class,'Date')]/div");

        private static readonly By GridColumnEditedLocator = By.XPath(".//div[contains(@class,'Edited')]/div");

        private static readonly By GridColumnSessionTypeLocator = By.XPath(".//div[contains(@class,'SessionType')]/div");

        private static readonly By GridColumnTotalActualCostLocator =
            By.XPath(".//div[contains(@class,'TotalActualCost')]/div");

        private static readonly By GridColumnUserLocator = By.XPath(".//div[contains(@class,'ColumnUser')]/div");

        private static readonly By GridColumnExpandDetailsLocator =
            By.XPath(".//a[@class='wa_sessionViewHideDetails wa_collapsed']");

        private static readonly By GridColumnCollapseDetailsLocator =
            By.XPath(".//a[@class='wa_sessionViewHideDetails wa_expanded']");

        private readonly By containerLocator;

        /// <summary>
        /// The partial id of the divId.
        /// </summary>
        private readonly string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridRowItem"/> class.
        /// </summary>
        /// <param name="id"> correspondent div id </param>
        public GridRowItem(string id)
        {
            DriverExtensions.WaitForPageLoad();
            this.id = id;
            if (id.Equals(string.Empty))
            {
                throw new NoSuchElementException("Empty div partialId");
            }

            this.containerLocator = By.XPath(string.Format(ContainerMask, this.id));
        }

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientId => DriverExtensions.GetText(this.containerLocator, GridColumnClientIdLocator);

        /// <summary>
        /// Billing _date
        /// </summary>
        public DateTime Date => DateTime.Parse(DriverExtensions.GetText(this.containerLocator, GridColumnDateLocator));

        /// <summary>
        /// Initializes a new instance of the <see cref="GridRowDetailItem"/> class.
        /// </summary>
        public GridRowDetailItem DetailItem => new GridRowDetailItem(this.id);

        /// <summary>
        /// Was billing item edited
        /// </summary>
        public bool IsEdited => DriverExtensions.GetText(this.containerLocator, GridColumnEditedLocator).Contains("Y");

        /// <summary>
        /// Chargeable/Non chargeable option
        /// </summary>
        public bool IsChargeable => DriverExtensions.GetElement(this.containerLocator, GridColumnChargeableLocator).Text.Contains("C");

        /// <summary>
        /// Session type (hourly, etc..)
        /// </summary>
        public string SessionType => DriverExtensions.GetText(this.containerLocator, GridColumnSessionTypeLocator);

        /// <summary>
        /// Total cost in USD
        /// </summary>
        public decimal TotalCost => decimal.Parse(
            DriverExtensions.GetText(this.containerLocator, GridColumnTotalActualCostLocator),
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);

        /// <summary>
        /// User name
        /// </summary>
        public string User => DriverExtensions.GetText(this.containerLocator, GridColumnUserLocator);

        /// <summary>
        /// Expand GridRowItem
        /// </summary>
        /// <returns>The <see cref="GridRowDetailItem"/> </returns>
        public GridRowDetailItem ClickExpandButton()
        {
            DriverExtensions.GetElement(this.containerLocator, GridColumnExpandDetailsLocator).Click();
            return this.DetailItem;
        }

        /// <summary>
        /// Collapse GridRowItem
        /// </summary>
        public void ClickCollapseButton()
        {
            DriverExtensions.GetElement(this.containerLocator, GridColumnCollapseDetailsLocator).Click();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// The to model.
        /// </summary>
        /// <typeparam name="TModel"> The type of model </typeparam>
        /// <returns> The <see cref="!:TModel" />. </returns>
        public TModel ToModel<TModel>() => MapperService.Map<TModel>(this);

        /// <summary>
        /// The to model.
        /// </summary>
        /// <param name="destinationType"> The destination type. </param>
        /// <returns> The <see cref="T:System.Object" />. </returns>
        public object ToModel(Type destinationType)
            => MapperService.Map(this, this.GetType(), destinationType);
    }
}