namespace Framework.Common.UI.Products.Shared.Pages
{
    using System.Linq;
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Base class for page objects
    /// </summary>
    public abstract class BaseModuleRegressionPage : BaseWebObject, ICreatablePageObject
    {
        private static readonly By BlockedBoxLocator = By.Id("co_blockedBox");

        private static readonly By ClosePrivacyPolicyButtonLocator = By.XPath("//button[@id='coid_website_privacyPolicyAcknowledged']");

        private static readonly By Error404MessageBoxLocator = By.Id("co_404Box");

        private static readonly By EnvironmentErrorMessageLocator = By.XPath(
            "//*[contains(text(), 'An error has occurred. Please try again.')] | //*[contains(text(), 'We’re sorry')] | //*[contains(text(), 'An error occurred')]");

        private static readonly By PrivacyPolicyContainerLocator = By.Id("co_privacyPolicyContainer");

        private static readonly By SpinnerLocator = By.ClassName("co_search_ajaxLoading");

        private static readonly By WebSiteErrorLocator = By.Id("co_website_errorSummaryToggleLink");

        private static readonly By SearchErrorLocator = By.Id("co_infoBox_searchErrors");

        private static readonly By ResponsiveDesktopLocator = By.XPath("//body[contains(@class,'Responsive-desktop')]");

        private static readonly By ResponsiveBlacklistedLocator = By.XPath("//body[contains(@class,'Responsive-blacklisted')]");

        private EnumPropertyMapper<Dialogs, WebElementInfo> dialogsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModuleRegressionPage"/> class. 
        /// </summary>
        protected BaseModuleRegressionPage()
        {
            DriverExtensions.WaitForElementNotDisplayed(90000, SpinnerLocator);
        }

        /// <summary>
        /// Dialogs  Map
        /// </summary>
        public EnumPropertyMapper<Dialogs, WebElementInfo> DialogsMap
            => this.dialogsMap = this.dialogsMap ?? EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>();

        /// <summary>
        /// Determines if the page is sitting on an error page (404, WLN blocked, or other error page)
        /// </summary>
        /// <value>True if we got an error page, false otherwise</value>
        public virtual bool IsErrorPage
        {
            get
            {
                DriverExtensions.WaitForJavaScript();

                var errorPageTitles = new[]
                                          {
                                              "404",
                                              "not found",
                                              "Runtime Error",
                                              "Problem loading",
                                              "Application Error"
                                          };

                return DriverExtensions.IsDisplayed(Error404MessageBoxLocator)
                       || DriverExtensions.IsDisplayed(BlockedBoxLocator)
                       || DriverExtensions.IsDisplayed(WebSiteErrorLocator)
                       || DriverExtensions.IsDisplayed(SearchErrorLocator)
                       || errorPageTitles.Any(errorTitle => BrowserPool.CurrentBrowser.Title.Contains(errorTitle));
            }
        }

        /// <summary>
        /// Get current page title that 
        /// Displayed on the browser window or tab
        /// </summary>
        /// <returns>Browser Title</returns>
        public virtual string Title => BrowserPool.CurrentBrowser.Title;

        /// <summary>
        /// Get Current Url for page
        /// </summary>
        /// <returns>Current Url</returns>
        public string Url => BrowserPool.CurrentBrowser.Url;

        /// <summary>
        /// Checks whether environment error message is displayed on page
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsEnvironmentErrorMessageDisplayed() => DriverExtensions.IsDisplayed(EnvironmentErrorMessageLocator);

        /// <summary>
        /// Determines if a red error box is present, and returns text if true
        /// </summary>
        /// <returns>Returns the error text if present, empty string if not found</returns>
        public string GetRedErrorText()
        {
            string errorText = string.Empty;

            try
            {
                errorText = DriverExtensions.GetElement(By.CssSelector(".co_systemErrorBox p")).Text;
            }
            catch (NoSuchElementException)
            {
            }

            return errorText;
        }

        /// <summary>
        /// Checks if the 404 box error message displays.
        /// </summary>
        /// <returns>Boolean value</returns>
        public bool Is404BoxPresent() => DriverExtensions.IsDisplayed(Error404MessageBoxLocator);

        /// <summary>
        /// Close Privacy Policy Message If Displayed
        /// </summary>
        public void ClosePrivacyPolicyMessageIfDisplayed()
        {
            if (DriverExtensions.IsDisplayed(PrivacyPolicyContainerLocator))
            {
                DriverExtensions.WaitForElement(ClosePrivacyPolicyButtonLocator).Click();
            }
        }

        /// <summary>
        /// Verify that dialog is displayed
        /// </summary>
        /// <param name="dialog"> Dialog to check </param>
        /// <param name="timeoutInSeconds"> Time in seconds to wait before timeout </param>
        /// <returns> True if dialog is displayed, false otherwise </returns>
        public bool IsDisplayed(Dialogs dialog, int timeoutInSeconds = 5)
            => DriverExtensions.IsDisplayed(By.XPath(this.DialogsMap[dialog].LocatorString), timeoutInSeconds);

        /// <summary>
        /// Scroll page to bottom
        /// </summary>
        public void ScrollPageToBottom() => DriverExtensions.ScrollPageToBottom();

        /// <summary>
        /// Scroll page to top
        /// </summary>
        public void ScrollPageToTop() => DriverExtensions.ScrollToTop();

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="elementToDrag">The element to drag.</param>
        /// <param name="copyOrMoveEnum">The drag And Drop Enum.</param>
        protected void DragAndDropToFolder(
            IWebElement targetFolder,
            IWebElement elementToDrag,
            CopyOrMoveEnum copyOrMoveEnum)
        {
            this.DragAndDropOrHold(targetFolder, elementToDrag, true);

            if (this.IsDisplayed(Dialogs.DragAndDropMoveOrCopyDialog))
            {
                var copyOrMoveDialog = new DragAndDropMoveOrCopyDialog();
                if (copyOrMoveEnum == CopyOrMoveEnum.Copy)
                {
                    copyOrMoveDialog.ClickCopyButton();
                }

                if (copyOrMoveEnum == CopyOrMoveEnum.Move)
                {
                    copyOrMoveDialog.ClickMoveButton();
                }
            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verify is page scrolled to the top of the page
        /// </summary>
        /// <returns>true if scrolled, false otherwise</returns>
        public bool IsPageScrolledToTop() => DriverExtensions.IsPageScrolledToTop();

        /// <summary>
        /// IsResponsiveDesktopPage
        /// </summary>
        public bool IsResponsiveDesktopPage => DriverExtensions.IsDisplayed(ResponsiveDesktopLocator, 5);

        /// <summary>
        /// IsResponsiveBlacklistedPage
        /// </summary>
        public bool IsResponsiveBlacklistedPage => DriverExtensions.IsDisplayed(ResponsiveBlacklistedLocator, 5);

        /// <summary>
        /// Drag and hold item over folder in recent folders dialog
        /// </summary>
        /// <param name="targetFolder">The folder Name.</param>
        /// <param name="elementToDrag">The element to drag.</param>
        protected void DragAndHoldOverElement(IWebElement targetFolder, IWebElement elementToDrag)
            => this.DragAndDropOrHold(targetFolder, elementToDrag, false);

        private void DragAndDropOrHold(IWebElement targetFolder, IWebElement elementToDrag, bool drop = true)
        {
            if (drop)
            {
                DriverExtensions.DragAndDrop(targetFolder, elementToDrag);
            }
            else
            {
                DriverExtensions.DragAndHold(targetFolder, elementToDrag);
            }

            DriverExtensions.WaitForJavaScript();
        }
    }
}