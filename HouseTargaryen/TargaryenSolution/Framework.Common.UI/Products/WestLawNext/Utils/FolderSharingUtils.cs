namespace Framework.Common.UI.Products.WestLawNext.Utils
{
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// has methods to take care of the folder sharing util methods.
    /// </summary>
    public class FolderSharingUtils
    {
        // todo retrieve //div[@id='co_shareAccessApproval'] into separate container or use css locator.
        // todo emplement in the foldering PO refactoring scope.
        private static readonly By AcceptSaoInvitationButtonLocator =
            By.XPath("//div[@id='co_shareAccessApproval']//input[@value='Accept']");

        private static readonly By CloseNoPendingSaoRequestButtonLocator =
            By.XPath("//div[@id='coid_lightboxOverlay']//input[@value='Close']|//a[text()='Close']");

        private static readonly By ConfirmSaoInvitationButtonLocator =
            By.XPath("//div[@id='co_shareAccessApproval']//input[@value='Confirm']");

        private static readonly By DeclineOrDenySaoInvitationButtonLocator =
            By.XPath("//div[@id='co_shareAccessApproval']//a[@class='co_overlayBox_buttonCancel']");

        private static readonly By NoPendingSaoRequestsMessageLocator = By.XPath("//div[@class='co_overlayBox_content']");

        private static readonly By SaoAcceptDenyMessageLocator = By.XPath("//div[@id='co_shareAccessApproval']//p");

        private static readonly By SaoTitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h3");

        /// <summary>
        /// Click on the Accept link for SAO Invitation.
        /// </summary>
        public void AcceptSaoInvitation()
        {
            DriverExtensions.Click(AcceptSaoInvitationButtonLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// closing the pending SAO Approval / Sharing request display page
        /// </summary>
        public void CloseNoPendingSaoRequestPage()
        {
            DriverExtensions.Click(CloseNoPendingSaoRequestButtonLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Confirm the SAO invitation as owner
        /// </summary>
        public void ConfirmSaoInvitation()
        {
            DriverExtensions.Click(ConfirmSaoInvitationButtonLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on the Accept link for SAO Invitation.
        /// </summary>
        public void DeclineOrDenySaoInvitation()
        {
            DriverExtensions.Click(DeclineOrDenySaoInvitationButtonLocator);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get the message displayed on the SAO Accept and Deny Widget page
        /// </summary>
        /// <returns>text displayed on the widget</returns>
        public string GetMessageFromSaoAcceptDenyWidget() => DriverExtensions.GetText(SaoAcceptDenyMessageLocator);

        /// <summary>
        /// no pending SAO requests are available for this user. This method can be used for pending approval / sharing.
        /// </summary>
        /// <returns>text displayed on the page</returns>
        public string GetNoPendingSaoRequestsMessage() => DriverExtensions.GetText(NoPendingSaoRequestsMessageLocator);

        /// <summary>
        /// GetSaoLightboxTitle
        /// </summary>
        /// <returns>Lightbox Title</returns>
        public string GetSaoLightboxTitle() => DriverExtensions.GetText(SaoTitleLocator);

        /// <summary>
        /// Open a Share Across Organization response
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="token">The token.</param>
        /// <param name="isOwner">The is Owner.</param>
        public void OpenSaoResponse(string url, string token, bool isOwner)
        {
            BrowserPool.CurrentBrowser.GoToUrl(
            $"{url}/ExternalSharing/{(isOwner ? "Initiator" : "Recipient")}Approval?sharedToken={token}&transitionType=Default&contextData=(sc.Default)&VR=3.0&RS=cblt1.0&firstPage=true");
            DriverExtensions.WaitForPageLoad();
        }
    }
}