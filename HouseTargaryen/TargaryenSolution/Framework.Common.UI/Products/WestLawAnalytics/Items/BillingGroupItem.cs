namespace Framework.Common.UI.Products.WestLawAnalytics.Items
{
    using System;

    using Framework.Common.UI.Interfaces.Models;
    using Framework.Common.UI.Utils.Mapper;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Account Location Item
    /// </summary>
    public class BillingGroupItem : IMappable
    {
        private static readonly By RemoveOrAddLinkLocator = By.ClassName("wa_billingGroupLink");

        /// <summary>
        /// Instance of AccountLocationItem
        /// </summary>
        /// <param name="containerElement"></param>
        public BillingGroupItem(IWebElement containerElement)
        {
            this.Container = containerElement;
        }

        private IWebElement Container { get; set; }

        /// <summary>
        /// Billing Group
        /// </summary>
        /// <returns></returns>
        public string BillingGroupName => this.Container.Text;

        /// <summary>
        /// Billing Group State (Add/Remove)
        /// </summary>
        /// <returns></returns>
        public string BillingGroupState => DriverExtensions.GetElement(this.Container, RemoveOrAddLinkLocator).Text;

        /// <summary>
        /// Is the Billing Group currently displayed
        /// </summary>
        public bool IsBillingGroupDisplayed =>
            !DriverExtensions.GetElement(this.Container).GetAttribute("style").Equals("display: none;");

        /// <summary>
        /// Click on Remove / Add button
        /// </summary>
        public void ClickRemoveOrAddButton() =>
            DriverExtensions.GetElement(this.Container, RemoveOrAddLinkLocator).Click();

        /// <inheritdoc />
        /// <summary>
        /// The to model.
        /// </summary>
        /// <typeparam name="TModel">
        /// the desired type
        /// </typeparam>
        /// <returns>
        /// The TModel
        /// </returns>
        public TModel ToModel<TModel>() => MapperService.Map<TModel>(this);

        /// <summary>
        /// ToModel
        /// </summary>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public object ToModel(Type destinationType) => MapperService.Map(this, this.GetType(), destinationType);

    }
}
