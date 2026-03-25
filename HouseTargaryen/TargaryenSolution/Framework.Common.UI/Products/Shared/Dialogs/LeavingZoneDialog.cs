namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when you leave non billable zone
    /// </summary>
    public class LeavingZoneDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.XPath("//*[@id='coid_deliveryOutOfPlan_cancelLink' or @id='co_WarnViewCancelButton']");

        private static readonly By ClientIdErrorLabelLocator = By.XPath("//div[contains(@id,'co_clientIDErrorMsgPlaceholder')]");

        private static readonly By SaveButtonLocator = By.XPath("//*[contains(@value,'Save')]");

        private static readonly By InfoMessageLocator = By.XPath(".//div[@id='co_deliveryWaitMessageTitle' or @id='co_deliveryWaitMessageTitle'] | //div[@id='co_docWarning_text']/p[1]/strong");

        private static readonly By ProceedAsNonBillableLabelLocator = By.XPath("//div[contains(@id,'coid_eLibraryClientBillingOff')]//label");

        private static readonly By ProceedAsNonBillableCheckboxLocator = By.XPath("//div[contains(@id,'coid_eLibraryClientBillingOff')]//input");

        private static readonly By SetClientIDLocator = By.XPath("//input[@id='co_WarnViewSaveButton' and @value='Save Client'] | //input[@id='Button1' and @value='Save']");

        private static readonly By ContinueButtonLocator = By.CssSelector("#co_WarnViewContinueButton");

        /// <summary>
        /// Client Id drop down
        /// </summary>
        public ClientIdDropdown ClientIdDropdown => new ClientIdDropdown();

        /// <summary>
        /// Matter Id drop down
        /// </summary>
        public MatterIdDropdown MatterIdDropdown => new MatterIdDropdown();

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Error message label
        /// </summary>
        public ILabel ErrorMessageLabel => new Label(ClientIdErrorLabelLocator);

        /// <summary>
        /// Info message label
        /// </summary>
        public ILabel InfoMessageLabel => new Label(InfoMessageLocator);

        /// <summary>
        /// Proceed as non-billable label
        /// Accessible only with LeavingNonBillableZoneCheckbox FAC
        /// </summary>
        public ILabel ProceedAsNonBillableLabel => new Label(ProceedAsNonBillableLabelLocator);

        /// <summary>
        /// Checkbox to proceed as non-billable
        /// Accessible only with LeavingNonBillableZoneCheckbox FAC
        /// </summary>
        public ICheckBox ProceedAsNonBillableCheckbox => new CheckBox(ProceedAsNonBillableCheckboxLocator);

        /// <summary>
        /// Set a ClientID button in Leaving as non-billable Zone
        /// </summary>
        public IButton SetClientIDButton => new Button(SetClientIDLocator);

        /// <summary>
        /// Continue Button
        /// </summary>
        public IButton ContinueButton => new Button(ContinueButtonLocator);
    }
}