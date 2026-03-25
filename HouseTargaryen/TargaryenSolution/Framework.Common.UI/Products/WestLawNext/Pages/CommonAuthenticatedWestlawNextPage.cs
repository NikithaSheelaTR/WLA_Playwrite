namespace Framework.Common.UI.Products.WestLawNext.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.Shared.Components.Delivery;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CommonAuthenticatedWestlawNextPage can be used as a base page for CommonSearchHome and other WestlawNext pages
    /// NOTE: use CommonAuthenticatedWestlawNextPage for pages with the more narrow header
    /// </summary>
    public abstract class CommonAuthenticatedWestlawNextPage : BaseModuleRegressionPage
    {
        private static readonly By BrowsePageTitleLocator = By.Id("co_browsePageLabel");

        private static readonly By BrowsePageTitleImgLocator = By.XPath("//*[@id='co_browsePageImageLabel'] | //a[@id='coid_website_logo']/img");

        private static readonly By OutOfPlanBrowsePageTitleLocator = By.Id("title");

        private static readonly By CategoryPageScopeContainerLocator = By.Id("categoryPageScope");

        private static readonly By SmartBreadcrumbTrailContainerLocator = By.CssSelector("#cp_homeLink>div, #coid_website_breadCrumbTrail, .co_breadcrumbs");

        private static readonly By SuccessMessageContainerLocator = By.XPath("//div[contains(@class,'co_infoBox success')]");

        private static readonly By FailureMessageContainerLocator = By.XPath("//div[contains(@class,'co_infoBox failure') and @style='display: block;']");

        private static readonly By CopyLinkLocator = By.Id("co_linkBuilder");

        private static readonly By BrowsePageHeaderLocator = By.XPath("//body[contains(@class,'Header-scrolled')]");

        private static readonly By SystemErrorMessageLocator = By.XPath("//h1");

        /// <summary>
        /// WestlawNext Header
        /// </summary>
        public WestlawNextHeaderComponent Header { get; protected set; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Gets Delivery Queue
        /// </summary>
        public DeliveryQueueComponent DeliveryQueue { get; protected set; } = new DeliveryQueueComponent();

        /// <summary>
        /// WestlawNext footer
        /// </summary>
        public WestlawNextFooterComponent Footer { get; protected set; } = new WestlawNextFooterComponent();

        /// <summary>
        /// IsBrowsePageTitlePresent
        /// </summary>
        public bool IsBrowsePageTitleDisplayed => DriverExtensions.IsDisplayed(BrowsePageTitleLocator, 5);

        /// <summary>
        /// The is category page scope displayed.
        /// </summary>
        public bool IsCategoryPageScopeDisplayed => DriverExtensions.IsDisplayed(CategoryPageScopeContainerLocator, 5);

        /// <summary>
        /// Is header minimized or not
        /// </summary>
        public bool IsHeaderMinimizedAfterScroll => DriverExtensions.IsDisplayed(BrowsePageHeaderLocator, 5);

        /// <summary>
        /// Category page scope label
        /// </summary>
        public ILabel CategoryPageScopeTitle => new Label(CategoryPageScopeContainerLocator);

        /// <summary>
        /// Returns the title text of the browse page, used to ensure the test is on the correct page
        /// </summary>
        /// <returns>the title of the browse page</returns>
        public string GetBrowsePageTitle() => this.IsBrowsePageTitleDisplayed ? DriverExtensions.GetElement(BrowsePageTitleLocator).Text : DriverExtensions.GetElement(BrowsePageTitleImgLocator).GetAttribute("alt");

        /// <summary>
        /// Returns the title text of the browse page (Out of Plan), used to ensure the test is on the correct page
        /// </summary>
        /// <returns>the title of the browse page</returns>
        public string GetOutOfPlanBrowsePageTitle() => DriverExtensions.GetElement(OutOfPlanBrowsePageTitleLocator).Text;

        /// <summary>
        /// Get Smart Breadcrumb Container
        /// </summary>     
        /// <returns>Creates a new instance of the <see cref="BreadCrumbComponent"/> class.</returns>   
        public BreadCrumbComponent GetSmartBreadCrumbContainer()
            => this.IsSmartBreadCrumbContainerPresent() ? new BreadCrumbComponent() : null;

        /// <summary>
        /// Check if smart breadcrumb container present                
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsSmartBreadCrumbContainerPresent() => DriverExtensions.IsElementPresent(SmartBreadcrumbTrailContainerLocator);

        /// <summary>
        /// Click Link Builder Button
        /// </summary>
        /// <returns>The <see cref="CopyLinkDialog"/>.</returns>
        public CopyLinkDialog ClickCopyLink()
        {
            DriverExtensions.WaitForElement(CopyLinkLocator).Click();
            return new CopyLinkDialog();
        }

        /// <summary>
        /// Verify that Copy Link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCopyLinkDisplayed() => DriverExtensions.IsDisplayed(CopyLinkLocator, 5);

        /// <summary>
        /// Wait for Sign On page is displayed
        /// </summary>
        /// <param name="time"> Time to wait (in seconds)</param>
        public void WaitForLogOut(int time)
        {
            string title = this.Title;
            DriverExtensions.WaitForCondition(condition => !BrowserPool.CurrentBrowser.Title.Equals(title), time);
        }

        /// <summary>
        /// Wait for Sign On page is displayed
        /// </summary>
        /// <param name="time"> Time to wait (in seconds)</param>
        public SessionTimeOutDialog WaitForSessionTimeOutDialog(int time)
        {
            DriverExtensions.WaitForCondition(condition => this.IsDisplayed(Dialogs.SessionTimeOut), time);
            return new SessionTimeOutDialog();
        }

        /// <summary>
        /// Wait for ExtendSessionDialog
        /// </summary>
        /// <param name="time">Time to wait (in seconds)</param>
        /// <returns>The <see cref="ExtendSessionDialog"/>.</returns>
        public ExtendSessionDialog WaitForExtendSessionDialog(int time)
        {
            DriverExtensions.WaitForCondition(condition => this.IsDisplayed(Dialogs.ExtendSessionDialog), time);
            return new ExtendSessionDialog();
        }

        /// <summary>
        /// Get text of the success message on the document page
        /// </summary>
        /// <returns> Success message text </returns>
        public string GetSuccessMessageText() => DriverExtensions.GetText(SuccessMessageContainerLocator);

        /// <summary>
        /// Get text of the failure message on the page
        /// </summary>
        /// <returns> Failure message text </returns>
        public List<string> GetFailureMessages()
            => DriverExtensions.GetElements(FailureMessageContainerLocator).Select(elem => elem.GetText()).ToList();

        /// <summary>
        /// Verify that success message is displayed on the page
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSuccessMessageDisplayed() => DriverExtensions.IsDisplayed(SuccessMessageContainerLocator, 5);

        /// <summary>
        /// Verify failure message display
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsFailureMessageDisplayed() => DriverExtensions.IsDisplayed(FailureMessageContainerLocator, 5);

        /// <summary>
        /// IsFailureMessageExists
        /// </summary>
        /// <returns></returns>
        public bool IsFailureMessageInView() =>
            DriverExtensions.GetElement(FailureMessageContainerLocator).IsElementInView();

        /// <summary>
        /// Returns the text of the browse page, used to ensure the test is on the correct page
        /// </summary>
        /// <returns>the title of the browse page</returns>
        public string GetSystemErrorMessage() => DriverExtensions.GetElement(SystemErrorMessageLocator).Text;
    }
}