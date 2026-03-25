using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using Framework.Common.UI.Interfaces;
using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Dialogs;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Elements.Links;
using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;
using Framework.Common.UI.Utils.Browser;
using Framework.Common.UI.Utils.Core;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
using java.util;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs
{
    /// <summary>
    /// Advantage Notifications Dialog
    /// </summary>
    public class AdvantageBrowseDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");
        private const string BrowseCategoryLabelLocatorMask = ("//button[contains(@class,'__panelListButton')]/span[contains(text(), {0})]");
        private static readonly By ContentTypePanelHeaderLabelLocator = By.XPath("//div[contains(@class, 'panelHeader')]/h2[contains(text(), 'Content types')]");
        private static readonly By FederalMaterialsPanelHeaderLabelLocator = By.XPath("//div[contains(@class, 'panelHeader')]/h2[contains(text(), 'Federal materials')]");
        private static readonly By StateMaterialsPanelHeaderLabelLocator = By.XPath("//div[contains(@class, 'panelHeader')]/h2[contains(text(), 'State materials')]");
        private static readonly By PracticeAreasPanelHeaderLabelLocator = By.XPath("//div[contains(@class, 'panelHeader')]/h2[contains(text(), 'Practice areas')]");
        private const string BrowseCategoryContentTypeLinkLctMask = ("//div[contains(@class,'__panelContent')]//*[starts-with(text(), {0})]");
        private static readonly By BrowsePageOpenedLabelLocator = By.XPath("//h1[@id='co_browsePageLabel']");
        private static readonly string BrowseCategoryContentTypeLinkLocatorString = "a.control span.content";
       
        /// <summary>
        /// Clicks one of the browse category
        /// </summary>
        /// <typeparam name="T">The type of page that will be navigated to</typeparam>
        /// <param name="category">Category to click</param>
        /// <returns>A new instance of an object of type T</returns>
        public T SelectBrowseCategory<T>(string category) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(SafeXpath.BySafeXpath(BrowseCategoryLabelLocatorMask, category));
            DriverExtensions.ClickUsingJavaScript(SafeXpath.BySafeXpath(BrowseCategoryLabelLocatorMask, category));
            return DriverExtensions.CreatePageInstance<T>();
        }


        /// <summary>
        /// Content type panel header label
        /// </summary>
        public ILink ContentTypePanelHeaderLabel => new Link(ContentTypePanelHeaderLabelLocator);

        /// <summary>
        /// Federal materials panel header label
        /// </summary>
        public ILink FederalMaterialsPanelHeaderLabel => new Link(FederalMaterialsPanelHeaderLabelLocator);

        /// <summary>
        /// State materials panel header label
        /// </summary>
        public ILink StateMaterialsPanelHeaderLabel => new Link(StateMaterialsPanelHeaderLabelLocator);

        /// <summary>
        /// Practice areas panel header label
        /// </summary>
        public ILink PracticeAreasPanelHeaderLabel => new Link(PracticeAreasPanelHeaderLabelLocator);

        /// <summary>
        /// Opened page header Label
        /// </summary>
        public ILink BrowseOpenedPageLabel => new Link(BrowsePageOpenedLabelLocator);


        /// <summary>
        /// Click Browse category link
        /// </summary>
        /// <typeparam name="T"> Page object</typeparam>
        /// <param name="category"> Product title</param>
        /// <returns>ICreatablePageObject</returns>
        public T ClickBrowseCategoryContentTypeLink<T>(string category) where T : ICreatablePageObject
        {
            var hostElement = DriverExtensions.GetElement(SafeXpath.BySafeXpath(BrowseCategoryContentTypeLinkLctMask, category));
            var browserCategoryLink = (IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{BrowseCategoryContentTypeLinkLocatorString}'));",
            hostElement);
            browserCategoryLink.JavascriptClick();
            return DriverExtensions.CreatePageInstance<T>();
        }  
    }
}
