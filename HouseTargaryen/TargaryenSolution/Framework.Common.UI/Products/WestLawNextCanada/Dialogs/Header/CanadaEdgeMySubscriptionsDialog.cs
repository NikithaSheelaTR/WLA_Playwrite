namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.Header
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The "My Subscriptions"pop-up dialog that is displayed when My Subscriptions menu item in header is clicked
    /// </summary>
    public class CanadaEdgeMySubscriptionsDialog : BaseEdgeHeaderDialog
    {
        private static readonly By CloseButtonLocator = By.CssSelector("button.co_overlayBox_closeButton");
        private static readonly By SubscriptionsLocator = By.CssSelector("ul.sourceSelector-columns>li>a");
        private static readonly By ContainerLocator = By.Id("co_sourceSelectorContainer");

        /// <summary>
        /// Subscription Links
        /// </summary>
        public IReadOnlyCollection<ILink> SubscriptionLinks =>
            new ElementsCollection<Link>(ContainerLocator, SubscriptionsLocator);

        /// <summary>
        /// Close Dialog button
        /// </summary>
        public IButton CloseDialogButton => new Button(this.Container, CloseButtonLocator);

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);
    }
}
