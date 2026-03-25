namespace Framework.Common.UI.Products.Shared.Pages
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// PageObject for Offline Portlal Find&amp;Print HTML page.
    /// Represent the PO of the offline HTML page.
    /// </summary>
    public class FindAndPrintOfflinePage : BaseModuleRegressionPage
    {
        private const string DeliveryOptionRadiobuttonLctMask =
            "//div[@id='portalDeliveryOptions']//label[contains(.,'{0}')]/input";

        private static readonly By FindAndPrintHeaderLocator = By.XPath("//h1[@id='wln_H1' and text()='Find & Print']");

        private static readonly By CitationTextBoxLocator = By.XPath("//textarea[@id='wln_citations']");

        private static readonly By SubmitButtonLocator = By.Id("submitBtn");

        private EnumPropertyMapper<DeliveryMethod, WebElementInfo> deliveryMethodMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAndPrintOfflinePage"/> class. 
        /// FindAndPrintOfflinePage
        /// </summary>
        public FindAndPrintOfflinePage()
        {
            DriverExtensions.WaitForElementDisplayed(FindAndPrintHeaderLocator);
        }

        /// <summary>
        /// Gets the Delivery Method Map.
        /// </summary>
        private EnumPropertyMapper<DeliveryMethod, WebElementInfo> DeliveryMethodMap =>
            this.deliveryMethodMap =
                this.deliveryMethodMap ?? EnumPropertyModelCache.GetMap<DeliveryMethod, WebElementInfo>();

        /// <summary>
        /// click submit button
        /// </summary>
        /// <param name="delegatedAction"> The delegated Action.</param>
        /// <typeparam name="TPage"> Page type.</typeparam>
        /// <returns> The type of page.</returns>
        public TPage ClickSubmit<TPage>(Action delegatedAction)
            where TPage : ICreatablePageObject
        {
            DriverExtensions.ScrollTo(SubmitButtonLocator);
            DriverExtensions.Click(SubmitButtonLocator);
            delegatedAction.Invoke();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Enter citation
        /// </summary>
        /// <param name="citations">Citations</param>
        public void EnterCitations(params string[] citations) =>
            DriverExtensions.GetElement(CitationTextBoxLocator).SendKeys(string.Join(";", citations));

        /// <summary>
        /// This method selects the given delivery option. If the delivery option is Email, user's email id is added to To Field
        /// </summary>
        /// <param name="deliveryMethod"> The delivery Method. </param>
        public void SelectDeliveryOption(DeliveryMethod deliveryMethod)
        {
            DriverExtensions.Click(
                DriverExtensions.WaitForElement(
                    By.XPath(
                        string.Format(DeliveryOptionRadiobuttonLctMask, this.DeliveryMethodMap[deliveryMethod].Text))));
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }
    }
}