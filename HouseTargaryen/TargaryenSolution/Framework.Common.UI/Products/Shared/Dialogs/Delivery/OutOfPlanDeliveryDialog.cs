
namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Delivery out of plan Dialog
    /// </summary>
    public class OutOfPlanDeliveryDialog : BaseModuleRegressionDialog
    {
        private static readonly By DeliveryOutOfPlanMessageLocator = By.XPath("//*[@id ='co_deliveryWaitMessageTitle'] | //*[@id= 'co_headerMessage']");

        private static readonly By CancelButtonLocator = By.Id("coid_deliveryOutOfPlan_cancelLink");

        private static readonly By ContainerLocator = By.XPath("//*[@id ='outOfPlanCitationsLightbox'] | //*[@id ='blockedCitationsLightbox']");

        private static readonly By CitationsListLocator = By.XPath(".//li//label");

        private static readonly By CitationsCheckboxLocator = By.XPath(".//input");

        private static readonly By NextButtonLocator = By.Id("co_next");

        private static readonly By HelpLineNumberLocator = By.XPath("//p[contains(text(),'For more information')]");

        private static readonly By AdditionalMessageForLLULocator = By.XPath("//*[@id='co_outOfPlanMessage']//following-sibling::p[2]");

        private static readonly By AdditionalMessageForLLUWarnLocator = By.XPath("//*[@id='co_outOfPlanMessage']//following-sibling::p[2]");

        private static readonly By OutOfPlanMessageLocator = By.Id("co_outOfPlanMessage");

        private static readonly By EmptySelectionReminderLocator = By.Id("co_outOfPlanNoneSelectedWarning");

        private static readonly By CitationLinkLocator = By.XPath(".//ul//a");

        private static readonly By CitationBlockLocator = By.XPath(".//div[@class='co_overlayBox_content']");

        /// <summary>
        /// Clicks cancel button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Get the out of plan delivery message after selecting delivery option 
        /// </summary>
        /// <returns> Delivery message text </returns>
        public string GetOutOfPlanDeliveryMessage() => DriverExtensions.GetText(DeliveryOutOfPlanMessageLocator);

        /// <summary>
        /// Get list of Citations in the dialog
        /// </summary>
        public IReadOnlyCollection<Label> CitationsList =>
            new ElementsCollection<Label>(ContainerLocator, CitationsListLocator);

        /// <summary>
        /// Citation Links
        /// </summary>
        public IReadOnlyCollection<ILink> CitationLinks =>
            new ElementsCollection<Link>(ContainerLocator, CitationBlockLocator, CitationLinkLocator);

        /// <summary>
        /// Get list of Citations in the dialog
        /// </summary>
        public IReadOnlyCollection<CheckBox> CitationsListCheckbox =>
            new ElementsCollection<CheckBox>(ContainerLocator, CitationsListLocator, CitationsCheckboxLocator);

        /// <summary>
        ///Get helpline number text label 
        /// </summary>
        public ILabel HelpLineNumber => new Label(HelpLineNumberLocator);

        /// <summary>
        /// NextButton
        /// </summary>
        public IButton NextButton => new Button(NextButtonLocator);

        /// <summary>
        /// Additional message for LLU
        /// </summary>
        public ILabel AdditionalMessageForLLU => new Label(AdditionalMessageForLLULocator);

        /// <summary>
        /// Additional message for LLU
        /// </summary>
        public ILabel AdditionalMessageForLLUWarn => new Label(AdditionalMessageForLLUWarnLocator);

        /// <summary>
        /// Out of plan message
        /// </summary>
        public ILabel OutOfPlanMessage => new Label(OutOfPlanMessageLocator);

        /// <summary>
        /// Empty selection reminder
        /// </summary>
        public IInfoBox EmptySelectionReminder => new InfoBox(EmptySelectionReminderLocator);
    }
}
