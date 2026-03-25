namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestlawEdge.Components.TabPanel;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;    

    /// <summary>
    /// Compare text dialog
    /// </summary>
    public class CompareTextDialog : BaseModuleRegressionDialog
    {
        private static readonly By HeadingLocator = By.XPath("//div[@class = 'co_overlayBox_headline']//h2");
        private static readonly By CloseButtonLocator = By.XPath("//button[@id = 'coid_closeLightbox'] | //button[@id='co_snippetCompare_closeImage']");
        private static readonly By CompareButtonLocator = By.XPath("//button[text() ='Compare']");
        private static readonly By DeleteButtonLocator = By.XPath("//button[@class='co_redlineLightbox_tabItem_deleteButton' and @title='Delete']");

        /// <summary>
        /// TabPanel
        /// </summary>
        public CompareTextTabPanel TabPanel { get; set; } = new CompareTextTabPanel();

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton { get; } = new Button(CloseButtonLocator);

        /// <summary>
        /// Compare button
        /// </summary>
        public IButton CompareButton { get; } = new Button(CompareButtonLocator);

        /// <summary>
        /// Heading label
        /// </summary>
        public ILabel Heading { get; } = new Label(HeadingLocator);

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteButton { get; } = new Button(DeleteButtonLocator);
    }
}