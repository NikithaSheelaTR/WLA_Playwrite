namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Advantage Product Selector dialog
    /// </summary>
    public class AdvantageProductSelectorDialog : AdvantageBaseDialog
    {
        private const string ProductLinkMsk = "//saf-anchor[contains(@class, '__productLink') and text()={0}]";
        private const string ProductLinckLocatorString = "a";

        /// <summary>
        /// Click product category
        /// </summary>
        /// <typeparam name="T"> Page object</typeparam>
        /// <param name="category"> Product title</param>
        /// <returns>ICreatablePageObject</returns>
        public T ClickProductCategory<T>(string category) where T : ICreatablePageObject
        {
            var hostElement = DriverExtensions.GetElement(SafeXpath.BySafeXpath(ProductLinkMsk, category));
            var productLink = (IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{ProductLinckLocatorString}'));",
            hostElement);
            productLink.JavascriptClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
