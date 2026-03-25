namespace Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    /// <summary>
    /// The quick check base page.
    /// </summary>
    public class QuickCheckBasePage : EdgeCommonAuthenticatedWestlawNextPage
    {
        /// <summary>
        /// Gets the header.
        /// </summary>
        public new QuickCheckReportHeaderComponent Header { get; } = new QuickCheckReportHeaderComponent();

        /// <summary>
        /// The delivery tray.
        /// </summary>
        public QuickCheckReportTrayComponent ReportTray => new QuickCheckReportTrayComponent();
    
        /// <summary>
        /// Gets the tour component.
        /// </summary>
        public QuickCheckTourComponent TourCardComponent => new QuickCheckTourComponent();

        /// <summary>
        /// The is error page.
        /// </summary>
        public override bool IsErrorPage => base.IsErrorPage || this.IsTextPresented("There was a system error.");

        /// <summary>
        /// The close welcome video dialog if displayed.
        /// </summary>
        public void CloseWelcomeVideoDialogIfDisplayed()
        {
            if (this.IsDisplayed(Dialogs.QuickCheckWelcomeVideo))
            {
                var welcomeVideoDialog = new QuickCheckWelcomeToQuickCheckDialog();
                welcomeVideoDialog.CloseButton.Click();
            }
        }
    }
}