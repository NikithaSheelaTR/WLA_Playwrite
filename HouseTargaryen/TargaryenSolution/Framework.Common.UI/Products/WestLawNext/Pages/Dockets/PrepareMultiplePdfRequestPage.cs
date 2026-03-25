namespace Framework.Common.UI.Products.WestLawNext.Pages.Dockets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawNext.Components.Dockets.PrepareMultipleRequestComponents;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Prepare Multiple Pdf Request Page
    /// </summary>
    public class PrepareMultiplePdfRequestPage : CommonDocketsPage
    {
        private static readonly By CreateRequestButtonLocator = By.XPath("//a[contains(text(),'Create request')] ");

        private static readonly By GoToDocketRequestPageLinkLocator = By.XPath("//a[contains(text(),'Go to Dockets requests page')]");

        private static readonly By YouRequestIsFinishedLabelLocator = By.XPath("//li[contains(text(),'Your request is finished')] ");

        private static readonly By ReturnToDocketButtonLocator = By.XPath("//a[@id='co_returnToDocket'] ");

        private static readonly By SelectedItemsLocator = By.XPath("//li[@id='co_docToolbarSelectedItemsCounterWidget']");

        private static readonly By RequestStatusLabelLocator = By.XPath("//li[@id='co_docToolbarRequestStatusNotificationWidget']");

        private static readonly By ClearAllLinkLocator = By.XPath("//li[@id='co_docToolbarClearAll']");

        private static readonly By StatusMessageLocator = By.XPath("//li[@id='co_docToolbarRequestStatusNotificationWidget']");

        private static readonly By PageTitleLocator = By.XPath("//div[@id='co_subHeaderTitle']/h2");

        /// <summary>
        /// Proceeding entries component.
        /// </summary>
        public EntriesTableComponent EntriesTable { get; } = new EntriesTableComponent();

        /// <summary>
        /// Delivery widget
        /// </summary>
        public DeliveryDropdown DeliveryWidget { get; } = new DeliveryDropdown();

        /// <summary>
        /// FixedHeader component
        /// </summary>
        public DocumentFixedHeaderComponent FixedHeader { get; } = new DocumentFixedHeaderComponent();

        /// <summary>
        /// Click Create Request Button
        /// </summary>
        /// <typeparam name="T">The type of the page</typeparam>
        /// <returns>The instance of the page</returns>
        public T ClickCreateRequestButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CreateRequestButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Return to Docket button
        /// </summary>
        /// <returns>The instance of the page</returns>
        public T ClickReturnToDocketButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ReturnToDocketButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        ///  Click Go to Docket request page link
        /// </summary>
        /// <returns>Prepare multiple PDF request</returns>
        public DocketsRequestsPage ClickGoToDocketRequestPageLink()
        {
            DriverExtensions.WaitForElement(GoToDocketRequestPageLinkLocator).Click();
            return new DocketsRequestsPage();
        }

        /// <summary>
        /// Is Go To Docket Requests Page Link Displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsGoToDocketRequestsPageLinkDisplayed() =>
            DriverExtensions.IsDisplayed(GoToDocketRequestPageLinkLocator);

        /// <summary>
        /// Is Return To Docket Button Displayed
        /// </summary>
        /// <returns>true if displayed</returns>
        public bool IsReturnToDocketButtonDisplayed() =>
            DriverExtensions.IsDisplayed(ReturnToDocketButtonLocator);

        /// <summary>
        /// Wait For Request Complete
        /// </summary>
        /// <param name="timeOut"> Time Out </param>
        /// <returns> The <see cref="PrepareMultiplePdfRequestPage"/>. </returns>
        public PrepareMultiplePdfRequestPage WaitForRequestComplete(int timeOut = 40000)
        {
            DriverExtensions.WaitForElementDisplayed(YouRequestIsFinishedLabelLocator, timeOut);
            return this;
        }

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsDisplayedReturnToDocketButton() => DriverExtensions.IsDisplayed(ReturnToDocketButtonLocator);

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsDisplayedClearAllLink() => DriverExtensions.IsDisplayed(ClearAllLinkLocator);

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsDisplayedCreateRequestButton() => DriverExtensions.IsDisplayed(CreateRequestButtonLocator);

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>True if enabled</returns>
        public bool IsEnabledCreateRequestButton() => this.IsDisplayedCreateRequestButton() && !DriverExtensions.GetAttribute("class", CreateRequestButtonLocator).Contains("co_disabled");

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>Text of the IWebElement</returns>
        public string GetSelectedItemsText() => DriverExtensions.GetText(SelectedItemsLocator);

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>Text of the status label</returns>
        public string GetTexStatusMessage() => DriverExtensions.WaitForElementDisplayed(StatusMessageLocator).Text;

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>Text of request status</returns>
        public string GetRequestStatusMessage() => DriverExtensions.GetText(RequestStatusLabelLocator);

        /// <summary>
        /// Wrapper of DE method for IWebElement
        /// </summary>
        /// <returns>The class attribute value for Icon span</returns>
        public string GetRequestStatusIconClassAttribute() => DriverExtensions.GetAttribute("class", RequestStatusLabelLocator, By.XPath("./span"));

        /// <summary>
        /// Prepare multiple request page title
        /// </summary>
        /// <returns>text of the title</returns>
        public new string GetBrowsePageTitle() => DriverExtensions.GetText(PageTitleLocator);
    }
}