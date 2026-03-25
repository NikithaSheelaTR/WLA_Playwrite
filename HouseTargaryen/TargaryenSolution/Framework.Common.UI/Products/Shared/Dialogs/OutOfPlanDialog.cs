namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when you view an out of plan document
    /// </summary>
    public class OutOfPlanDialog : BaseModuleRegressionDialog
    {
        private static readonly By AdditionalChargeTextLocator = By.XPath("//p/strong");

        private static readonly By CancelButtonLocator = By.Id("coid_deliveryOutOfPlan_cancelLink");

        private static readonly By CancelLinkLocator =
            By.XPath(
                "//div[contains(@class,'co_overlayBox_optionsBottomLeft') and not(contains(@class,'co_hideState'))]//a[text()='Cancel']");

        private static readonly By ChangeClientIdLinkLocator = By.XPath("//div[@id='co_warnLightbox']//button[@class='co_clientIDInline_change']");

        private static readonly By ClientIdLabelLocator = By.CssSelector(".co_clientIDInline_label");

        private static readonly By ClientIdTextLocator = By.CssSelector(".co_clientIDInline_recent");

        private static readonly By ClientIdTextBoxLocator = By.Id("co_clientIDOOPTextbox");

        private static readonly By DeliverAllItemsButtonLocator = By.Id("coid_deliveryOutOfPlanMessage_allItems_btn");

        private static readonly By DialogTitleLocator = By.Id("co_warnLightboxTitle");

        private static readonly By DialogParagraphsTextLocator = By.XPath("//div[@id = 'co_docWarning_text']/p");

        private static readonly By DocPriceLinkLocator = By.Id("co_doc_price_link");

        private static readonly By DocPriceLocator = By.CssSelector("#co_doc_price");

        private static readonly By DocPriceMessageLocator = By.CssSelector(".co_pricingInfo");

        private static readonly By FootnoteTextLocator = By.ClassName("co_pricingInfo_footnote");

        private static readonly By MatterIdTextBoxLocator = By.Id("co_matterIDOOPTextbox");

        private static readonly By SelectItemsToDeliverButtonLocator =
            By.Id("coid_deliveryOutOfPlanMessage_selectItems_btn");

        private static readonly By UnselectPricesLocator = By.ClassName("co_detailsTable_chargeAmount");

        private static readonly By ViewItemButtonLocator =
            By.XPath(
                "//div[contains(@class,'co_overlayBox_optionsBottomLeft') and not(contains(@class,'co_hideState'))]//input");

        private static readonly By WarningDialogMessageByLocator = By.Id("co_warningDialogMessage");

        private static readonly By LoadingSpinnerLocator = By.Id("co_deliveryWaitProgress");

        /// <summary>
        /// Gets dialog paragraphs text
        /// </summary>
        public IList<string> GetDialogParagraphsText =>
            DriverExtensions.GetElements(DialogParagraphsTextLocator).Select(p => p.Text).ToList();

        /// <summary>
        /// hits cancel link and returns to previous page
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The T </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// hits cancel button and returns to previous page
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The T</returns>
        public T ClickCancelLink<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(CancelLinkLocator);

        /// <summary>
        /// Clicks Deliver All Items Button
        /// </summary>
        public void ClickDeliverAllItemsButton() => this.ClickElement(DeliverAllItemsButtonLocator);

        /// <summary>
        /// Clicks Deliver All Items Button
        /// </summary>
        /// <typeparam name="T"> T page </typeparam>
        /// <returns> New Instance of the page </returns>
        public T ClickDeliverAllItemsButton<T>() where T : BaseModuleRegressionDialog
        {
            this.ClickElement(DeliverAllItemsButtonLocator);
            return this.WaitForUpdateComplete<T>(400000, LoadingSpinnerLocator);
        }

        /// <summary>
        /// Clicks doc price link
        /// </summary>
        public void ClickDocPriceLink() => DriverExtensions.WaitForElement(DocPriceLinkLocator).Click();

        /// <summary>
        /// Clicks Select Items To Download Button
        /// </summary>
        public void ClickSelectItemsToDownloadButton() => this.ClickElement(SelectItemsToDeliverButtonLocator);

        /// <summary>
        /// hits view document/results/etc button and leads to the page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The next page </returns>
        public T ClickViewDocumentButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(ViewItemButtonLocator);

        /// <summary>
        /// Gets the additional charge text
        /// </summary>
        /// <returns>The additional charge text</returns>
        public string GetAdditionalChargeText() => DriverExtensions.GetText(AdditionalChargeTextLocator);

        /// <summary>
        /// Gets the text of the client id label
        /// </summary>
        /// <returns>The text of the client id label</returns>
        public string GetClientIdLabel() => DriverExtensions.GetText(ClientIdLabelLocator);
       
        /// <summary>
        /// Gets the text of the client id text box
        /// </summary>
        /// <returns>The text of the client id text box</returns>
        public string GetClientIdText() => DriverExtensions.GetAttribute("value", ClientIdTextBoxLocator);

        /// <summary>
        /// Gets the dialog title text
        /// </summary>
        /// <returns>the dialog title text</returns>
        public string GetDialogTitleText() => DriverExtensions.GetText(DialogTitleLocator);

        /// <summary>
        /// Gets the OOP doc price
        /// </summary>
        /// <returns>The OOP doc price</returns>
        public string GetDocPrice() => DriverExtensions.GetText(DocPriceLocator);

        /// <summary>
        /// Gets the OOP doc price message
        /// </summary>
        /// <returns>The OOP doc price message</returns>
        public string GetDocPriceMessage() => DriverExtensions.GetText(DocPriceMessageLocator);

        /// <summary>
        /// Gets the footnote text
        /// </summary>
        /// <returns>The footnote text</returns>
        public string GetFootnoteText() => DriverExtensions.GetText(FootnoteTextLocator);

        /// <summary>
        /// Gets the text of the matter id text box
        /// </summary>
        /// <returns>The text of the matter id text box</returns>
        public string GetMatterIdText() => DriverExtensions.GetAttribute("value", MatterIdTextBoxLocator);

        /// <summary>
        /// Gets List of prices
        /// </summary>
        /// <returns> List of prices </returns>
        public List<string> GetOutOfPlanUnselectPrices()
            => DriverExtensions.GetElements(UnselectPricesLocator).ToList().Select(price => price.Text).ToList();

        /// <summary>
        /// Gets the footnote text
        /// </summary>
        /// <returns>The footnote text</returns>
        public string GetWarningText() => DriverExtensions.GetText(WarningDialogMessageByLocator);

        /// <summary>
        /// Checks if the change client id link is visible
        /// </summary>
        /// <returns> True if change client id link is displayed </returns>
        public bool IsChangeClientIdLinkDisplayed() => DriverExtensions.IsDisplayed(ChangeClientIdLinkLocator);

        /// <summary>
        /// Checks if the Continue/View button visible
        /// </summary>
        /// <returns> True if Continue/View button are displayed </returns>
        public bool IsContinueButtonDisplayed() => DriverExtensions.IsDisplayed(ViewItemButtonLocator);

        /// <summary>
        /// Checks if doc price link is visible
        /// </summary>
        /// <returns> True If the doc price link is visible</returns>
        public bool IsDocPriceLinkVisible() => DriverExtensions.IsDisplayed(DocPriceLinkLocator);

        /// <summary>
        /// Verifies that Select Items To Download Button is Displayed
        /// </summary>
        /// <returns> True if Select Items To Download Button is Displayed </returns>
        public bool IsSelectItemsToDownloadButtonDisplayed()
            => DriverExtensions.IsDisplayed(SelectItemsToDeliverButtonLocator);

        /// <summary>
        /// Waits for the dialog to appear
        /// </summary>
        /// <param name="timeToWaitInSeconds">How long to wait</param>
        public void WaitForDialog(int timeToWaitInSeconds)
            => DriverExtensions.WaitForElement(CancelButtonLocator, timeToWaitInSeconds);

        /// <summary>
        /// Gets the text of the client id label
        /// </summary>
        /// <returns>The text of the client id value label</returns>
        public string GetClientIdTextLabel() => DriverExtensions.GetText(ClientIdTextLocator);
    }
}