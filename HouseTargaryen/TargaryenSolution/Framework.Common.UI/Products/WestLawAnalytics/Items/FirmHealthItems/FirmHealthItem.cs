namespace Framework.Common.UI.Products.WestLawAnalytics.Items.FirmHealthItems
{
    using System;

    using Framework.Common.UI.Interfaces.Models;
    using Framework.Common.UI.Utils.Mapper;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The firm heqlth item.
    /// </summary>
    /// 
    class FirmHealthItem : IMappable
    {
        private static readonly By GridItemLinkLocator = By.XPath(".//div[contains(@class,'wa_gridColumnDescription')]/div/a");

        private static readonly By InPlanColumnLocator = By.CssSelector("div.wa_gridColumnActualChargeInPlan");

        private static readonly By OutOfPlanColumnLocator = By.CssSelector("div.wa_gridColumnActualChargeOutOfPlan");

        private static readonly By PercentOutOfPlanLocator = By.CssSelector("div.wa_gridColumnActualChargePercentOutOfPlan");

        private static readonly By StandardChargeLocator = By.CssSelector("div.wa_gridColumnActualCharge");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="FirmHealthItem"/> class. 
        /// </summary>
        /// <param name="firmHealthContainer"> The Experiment Item Container. </param>
        public FirmHealthItem(IWebElement firmHealthContainer)
        {
            this.FirmHealthContainer = firmHealthContainer;
        }

        /// <summary>
        /// Description: Office Location/User/Client/Practice Area/Product Platform
        /// </summary>
        public string Link => DriverExtensions.WaitForElement(this.FirmHealthContainer, GridItemLinkLocator).Text;

        /// <summary>
        /// In Plan
        /// </summary>
        public string InPlan => DriverExtensions.WaitForElement(this.FirmHealthContainer, InPlanColumnLocator).Text;

        /// <summary>
        /// Out of Plan
        /// </summary>
        public string OutOfPlan => DriverExtensions.WaitForElement(this.FirmHealthContainer, OutOfPlanColumnLocator).Text;

        /// <summary>
        /// % Out of Plan
        /// </summary>
        public string PercentOutOfPlan => DriverExtensions.WaitForElement(this.FirmHealthContainer, PercentOutOfPlanLocator).Text;

        /// <summary>
        /// Standard Charge
        /// </summary>
        public string StandardCharge => DriverExtensions.WaitForElement(this.FirmHealthContainer, StandardChargeLocator).Text;

        /// <summary>
        /// Firm Health Container
        /// </summary>
        protected IWebElement FirmHealthContainer { get; set; }

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