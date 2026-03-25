namespace Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.PortalManagerSearch;
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Form Summary
    /// </summary>
    public class FormSummaryPage : BaseModuleRegressionPage
    {
        private static readonly By DescriptionLabelLocator = By.XPath("id('mainWell')/ul/li[2]");

        private static readonly By GetHtmlButtonLocator = By.Id("getHTMLBtn");

        private static readonly By GoToPortalManagerButtonLocator = By.LinkText("Go to Portal Manager");

        private static readonly By NameLabelLocator = By.XPath("id('mainWell')/ul/li[1]");

        /// <summary> 
        /// Gets Common Westlaw Next Header Section 
        /// </summary>
        public WestlawNextHeaderComponent Header { get; } = new WestlawNextHeaderComponent();

        /// <summary>
        /// Clicks button to download HTML
        /// </summary>
        /// <typeparam name="TPage">Page object</typeparam>
        /// <returns>The Page object instance.</returns>
        public TPage ClickDownloadAndOpen<TPage>() where TPage : BaseModuleRegressionPage
        {
            this.DownloadHtml();
            return this.OpenDownloadedHtml<TPage>();
        }

        /// <summary>
        /// clicks download HTML button
        /// </summary>
        public void DownloadHtml()
        {
            GetHtmlDialog getHtmlDialog = this.ClickGetHtml();
            getHtmlDialog.ClickDownload();
            getHtmlDialog.ClickClose();

           // string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";
            string downloadsPath ="C:\\Users\\Administrator\\Downloads";

            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => { Thread.Sleep(500); },
                () => Directory.GetFiles(downloadsPath).Any(s => s.EndsWith(".html")),
                20);
        }

        /// <summary>
        /// Get Html
        /// </summary>
        /// <returns>The <see cref="GetHtmlDialog"/>.</returns>
        public GetHtmlDialog ClickGetHtml()
        {
            DriverExtensions.WaitForElementDisplayed(GetHtmlButtonLocator).Click();
            return new GetHtmlDialog();
        }

        /// <summary>
        /// Click Go Back
        /// </summary>
        /// <returns>The <see cref="PortalManagerPage"/>.</returns>
        public PortalManagerPage ClickGoToPortalManagerButton()
        {
            DriverExtensions.WaitForElementDisplayed(GoToPortalManagerButtonLocator).Click();
            return new PortalManagerPage();
        }

        /// <summary>
        /// Returns the Description text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDescription() => DriverExtensions.WaitForElement(DescriptionLabelLocator).Text.Split(':')[1].Trim();

        /// <summary>
        /// Returns the Name text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName() => DriverExtensions.WaitForElement(NameLabelLocator).Text.Split(':')[1].Trim();

        /// <summary>
        /// Navigates to chrome downloads and opens the html.
        /// </summary>
        /// <typeparam name="TPage">Page object</typeparam>
        /// <returns>The Page object instance.</returns>
        public TPage OpenDownloadedHtml<TPage>() where TPage : BaseModuleRegressionPage
            => BrowserPool.CurrentBrowser
                .GoToUrl<TPage>(Path.GetFullPath(Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\")
                .First(s => s.EndsWith(".html"))));
    }
}