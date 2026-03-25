namespace Framework.Common.UI.Products.ANZ.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;


    /// <summary>
    /// ANZ Footer Components
    /// </summary>
    public class ANZFooterComponents : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_indigo_footerContainer");
        private static readonly By QuickStartGuide = By.XPath("//*[contains(text(),'Quick Start Guide')]");
        private static readonly By ImproveWestlawReportanerror = By.XPath("//*[@id=\"co_footer_improve\"]//a[@id=\"coid_websiteFooter_pageSurvey\"]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Current Url for page
        /// </summary>
        /// <returns>Current Url</returns>
        public class QuickStartGuideLink : ICreatablePageObject
        {
            /// <summary>
            /// Get Current Url for page
            /// </summary>
            /// <returns>Current Url</returns>
            public string Url => BrowserPool.CurrentBrowser.Url;
        }

        /// <summary>
        /// Get Current Url for page
        /// </summary>
        /// <returns>Current Url</returns>
        public class SupplierTerms : ICreatablePageObject
        {
            /// <summary>
            /// Get Current Url for page
            /// </summary>
            /// <returns>Current Url</returns>
            public string Url => BrowserPool.CurrentBrowser.Url;
        }

        /// <summary>
        /// Get Current Url for page
        /// </summary>
        /// <returns>Current Url</returns>
        public class ThomsonReuters : ICreatablePageObject
        {
            /// <summary>
            /// Get Current Url for page
            /// </summary>
            /// <returns>Current Url</returns>
            public string Url => BrowserPool.CurrentBrowser.Url;
        }

        /// <summary>
        /// Get Current Url for page
        /// </summary>
        /// <returns>Current Url</returns>
        public class PricingGuide : ICreatablePageObject
        {
            /// <summary>
            /// Get Current Url for page
            /// </summary>
            /// <returns>Current Url</returns>
            public string Url => BrowserPool.CurrentBrowser.Url;
        }

        /// <summary>
        /// Clicks Improve Westlaw link
        /// </summary>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <returns>
        /// A new instance of type T
        /// </returns>
        public T ClickImproveWestLawLinkButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.IsElementPresent(ImproveWestlawReportanerror);
            DriverExtensions.ClickUsingJavaScript(ImproveWestlawReportanerror);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
