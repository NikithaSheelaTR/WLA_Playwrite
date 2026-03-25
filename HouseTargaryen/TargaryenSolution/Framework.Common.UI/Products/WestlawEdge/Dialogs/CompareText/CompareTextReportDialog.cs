namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.CompareText
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.TabPanel;
    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that shows similarities and differences of snippets
    /// </summary>
    public class CompareTextReportDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id= 'coid_redlineCompare']");
        private static readonly By CloseButtonLocator = By.XPath(".//button[text() ='Close']");
        private static readonly By SaveButtonLocator = By.XPath(".//button[contains(@class, 'co_primaryBtn')]");
        private static readonly By InfoMessageLocator = By.XPath(".//div[@class = 'co_infoBox_message']");
        private static readonly By EditSavedComparisonsButtonLocator = By.XPath(".//button[text() = 'Edit saved comparisons']");

        /// <summary>
        /// TabPanel
        /// </summary>
        public CompareTextReportTabPanel TabPanel { get; set; } = new CompareTextReportTabPanel();

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(ContainerLocator, SaveButtonLocator);

        /// <summary>
        /// Edit saved comparisons button
        /// </summary>
        public IButton EditSavedComparisonsButton => new Button(ContainerLocator, EditSavedComparisonsButtonLocator);

        /// <summary>
        /// Info message label
        /// </summary>
        public ILabel InfoMessage => new Label(ContainerLocator, InfoMessageLocator);
    }
}