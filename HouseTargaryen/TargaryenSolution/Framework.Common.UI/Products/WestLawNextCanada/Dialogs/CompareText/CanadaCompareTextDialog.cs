namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.CompareText;
    using Framework.Common.UI.Products.WestLawNextCanada.Items;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Label = Shared.Elements.Labels.Label;
    using Button = Shared.Elements.Buttons.Button;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.TabPanel;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.CompareText;

    /// <summary>
    /// Canada Compare Text Dialog
    /// </summary>
    public class CanadaCompareTextDialog : CompareTextDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_redlineLightbox");
        private static readonly By ComparsionItemsLocator = By.XPath("//li[@class='co_redlineLightbox_tabItem']");
        private static readonly By NotificationsLocator = By.ClassName("co_redlineLightbox_notifications");
        private static readonly By ItemsCountLocator = By.Id("coid_redlineLightbox_snippetCountMessage");
        private static readonly By UndoButtonLocator = By.XPath("//button[@title='Undo']");
        private static readonly By SaveButtonLocator = By.XPath("//div[@class='co_overlayBox_optionsBottom']//button[contains(text(),'Save')]");

        /// <summary>
        /// Compare list items
        /// </summary>
        /// <returns>List of compare list items</returns>
        public ItemsCollection<CompareTextItem> ComparsionItems =>
            new ItemsCollection<CompareTextItem>(ContainerLocator, ComparsionItemsLocator);

        /// <summary>
        /// Notification labels
        /// </summary>
        public IReadOnlyCollection<ILabel> NotificationLabelList =>
            new ElementsCollection<Label>(ContainerLocator, NotificationsLocator);

        /// <summary>
        /// Returns number of snippets 
        /// </summary>
        public ILabel SnippetCount => new Label(ItemsCountLocator);

        /// <summary>
        /// Undo button
        /// </summary>
        public ILink UndoButton => new Link(UndoButtonLocator);

        /// <summary>
        /// TabPanel
        /// </summary>
        public CanadaCompareTextReportTabPanel ReportTabPanel { get; set; } = new CanadaCompareTextReportTabPanel();


        /// <summary>
        /// TabPanel
        /// </summary>
        public new CompareTextTabPanel TabPanel { get; set; } = new CompareTextTabPanel();

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);
    }
}
