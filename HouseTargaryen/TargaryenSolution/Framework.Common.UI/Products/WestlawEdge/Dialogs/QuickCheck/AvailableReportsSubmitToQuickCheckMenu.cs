namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// Submit To Quick Check Menu With Available reports
    /// </summary>
    public class AvailableReportsSubmitToQuickCheckMenu : BaseModuleRegressionDialog
    {
        private static readonly By TrayItemLocator = By.XPath(".//li[contains(@class,'DA-Dropdown-item')]");

        private static readonly By AvailableReportsForCurrentDocumentListLocator =
            By.XPath("//*[@class='DA-Dropdown-section DA-Dropdown-availableReports DA-Reports-section']");

        private static readonly By OtherAvailableReportsListLocator =
            By.XPath("//*[contains(@class,'DA-Dropdown-otherAvailableReports')] | //h3[text()='Other available report(s)']");

        private static readonly By InfoMessageLocator =
            By.XPath("//*[@class='DA-Dropdown-section DA-Dropdown-availableReports DA-Reports-section']//div[@class='co_infoBox_message']");

        /// <summary>
        /// Available reports for current document
        /// </summary>
        public ItemsCollection<QuickCheckTrayItem> AvailableReportsForCurrentDocument =>
            new ItemsCollection<QuickCheckTrayItem>(AvailableReportsForCurrentDocumentListLocator, TrayItemLocator);
        
        /// <summary>
        /// others available reports
        /// </summary>
        public ItemsCollection<QuickCheckTrayItem> OtherAvailableReports =>
            new ItemsCollection<QuickCheckTrayItem>(OtherAvailableReportsListLocator, TrayItemLocator);

        /// <summary>
        /// Info box message label
        /// </summary>
        public ILabel InfoBoxMessageLabel =>
            new Label(AvailableReportsForCurrentDocumentListLocator, InfoMessageLocator);
    }
}
