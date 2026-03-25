namespace Framework.Common.UI.Products.WestlawEdge.Components.CompareText
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNext.Components;
    
    using OpenQA.Selenium;

    /// <summary>
    /// The base  compare text tab component.
    /// </summary>
    public abstract class BaseCompareTextTab : BaseTabComponent
    {
        private static readonly By CloseButtonLocator =
            By.XPath("//ul[@id = 'coid_redlineLightbox_footerButtons']//button[text() = 'Close']");

        private static readonly By NotificationsLocator =
            By.XPath(".//div[@class = 'co_redlineLightbox_notifications']/*");

        private static readonly By ItemsLocator =
            By.XPath(".//li[./div[contains(@class, 'co_redlineLightbox_tabItem')]]");

        private static readonly By UndoButtonLocator = By.XPath("//button[text() = 'Undo']"); 

        /// <summary>
        /// Notification labels
        /// </summary>
        public IReadOnlyCollection<ILabel> NotificationLabelList =>
            new ElementsCollection<Label>(this.ComponentLocator, NotificationsLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.ComponentLocator, CloseButtonLocator);

        /// <summary>
        ///  Get list of tab items
        /// </summary>
        /// <returns>list of tab items</returns>
        public IItemsCollection<TItem> TabItems<TItem>()
            where TItem : BaseItem =>
            new ItemsCollection<TItem>(this.ComponentLocator, ItemsLocator);

        /// <summary>
        /// Undo button
        /// </summary>
        public IButton UndoButton  => new Button(this.ComponentLocator, UndoButtonLocator);
    }
}