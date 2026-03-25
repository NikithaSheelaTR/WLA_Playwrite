namespace Framework.Common.UI.Products.Shared.Components.Delivery
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// DeliveryQueue
    /// </summary>
    public class DeliveryQueueComponent : BaseModuleRegressionComponent
    {
        private static readonly By MinimizeButtonLocator = By.Id("dq_expand_collapse");

        private static readonly By QueueCountLocator = By.Id("dq_status_message");

        private static readonly By QueueItemLocator = By.CssSelector("#co_deliveryQueueList li");

        private static readonly By QueueListLocator = By.Id("co_deliveryQueueList");

        private static readonly By QueueStatusLocator = By.Id("dq_msgbar");

        private static readonly By ContainerLocator = By.Id("co_deliveryQueue");

        private static readonly By SuccessfulItemLocator =
            By.CssSelector(".co_deliveryQueue_downloadItem,.co_deliveryQueue_printAgainItem,.co_deliveryQueue_printItem");

        private static readonly By QueueItemDocumentLocator = By.XPath(".//*[contains(@class,'co_deliveryQueue_item')]/span[1]/strong");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get a list of document names from the delivery queue
        /// </summary>
        /// <returns> List of documents </returns>
        public List<string> GetListOfDocuments() =>
        DriverExtensions.GetElements(QueueItemLocator).Select(item => item.FindElement(QueueItemDocumentLocator)).Select(strongElement => strongElement.Text.Trim()).ToList();
        /// <summary>
        /// Get the item count displayed to the user by the minimized queue
        /// </summary>
        /// <returns> Count of the delivered items </returns>
        public string GetStatusMessageCount() => DriverExtensions.GetText(QueueCountLocator).Trim();

        /// <summary> Verify item in the delivery queue component is shown as a successful delivery </summary>
        /// <param name="index"> The index of the item to verify </param>
        /// <returns> True if delivery successful, false otherwise </returns>
        public bool IsDeliveryOfItemSuccessful(int index) =>
            DriverExtensions.GetElements(SuccessfulItemLocator)
                            .Contains(DriverExtensions.GetElements(QueueItemLocator).ToList()[index]);

        /// <summary>
        /// Verify the delivery queue is open
        /// </summary>
        /// <returns> True if open, false otherwise </returns>
        public bool IsQueueOpen() => DriverExtensions.IsDisplayed(QueueListLocator);

        /// <summary>
        /// Minimizes or opens the delivery queue
        /// </summary>
        /// <param name="open"> True to open, false to minimized. </param>
        public void SetQueueOpenedState(bool open)
        {
            if (open && !this.IsQueueOpen())
            {
                DriverExtensions.Click(QueueStatusLocator);
            }
            else if (!open && this.IsQueueOpen())
            {
                DriverExtensions.Click(MinimizeButtonLocator);
            }

            DriverExtensions.WaitForAnimation();
        }
    }
}