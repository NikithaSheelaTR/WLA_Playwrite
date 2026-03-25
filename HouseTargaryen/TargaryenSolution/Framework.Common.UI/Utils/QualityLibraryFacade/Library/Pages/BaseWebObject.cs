namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Pages
{
    using System.Linq;    
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;    

    /// <summary>
    /// Base class for all PO types: Pages, Dialogs, Components and Dropdown
    /// </summary>
    public abstract class BaseWebObject
    {
        private const string ElementToClickLctMask = "//*[text()='{0}'] | //*[@value='{0}']";
        private const string ImageToClickLctMask = "//img[@alt='{0}']";
        private static readonly By NewsLetterLocator = By.XPath("//button[@title='Newsletters' and text()='Newsletters']");

        /// <summary>
        /// Constructor for Base Page
        /// Should be removed after global refactoring - remove driver from pages
        /// </summary>
        public BaseWebObject()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on element, open and activate new browser tab 
        /// </summary>
        /// <param name="linkText"> The link Text. </param>
        /// <param name="newTabName"> name of new tab </param>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> New Page object </returns>
        public T ClickLinkAndOpenNewBrowserTab<T>(string linkText, string newTabName) where T : ICreatablePageObject
            => this.ClickAndOpenNewBrowserTab<T>(By.LinkText(linkText), newTabName);

        /// <summary>
        /// Click on element, open and activate new browser tab using JavaScript 
        /// </summary>
        /// <param name="linkText"> The link Text. </param>
        /// <param name="newTabName"> name of new tab </param>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> New Page object </returns>
        public T ClickLinkAndOpenNewBrowserTabUsingJavaScript<T>(string linkText, string newTabName) where T : ICreatablePageObject
            => this.OpenNewTabUsingJavascript<T>(newTabName, linkText);

        /// <summary>
        /// Click on element, open and activate new browser tab 
        /// </summary>
        /// <param name="elementText"> The element Text. </param>
        /// <param name="newTabName"> name of new tab </param>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> New Page object </returns>
        public T ClickElementAndOpenNewBrowserTab<T>(string elementText, string newTabName) where T : ICreatablePageObject
            => this.ClickAndOpenNewBrowserTab<T>(By.XPath(string.Format(ElementToClickLctMask, elementText)), newTabName);

        /// <summary>
        /// Click on element, open and activate new browser tab 
        /// </summary>
        /// <param name="elementText"> The element Text. </param>
        /// <param name="newTabName"> name of new tab </param>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> New Page object </returns>
        public T ClickImageAndOpenNewBrowserTab<T>(string elementText, string newTabName) where T : ICreatablePageObject
            => this.ClickAndOpenNewBrowserTab<T>(By.XPath(string.Format(ImageToClickLctMask, elementText)), newTabName);

        /// <summary>
        /// Click link by text
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <param name="linkText"> The link text. </param>
        /// <returns> New page object </returns>
        public virtual T ClickLinkByText<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.ScrollTo(By.LinkText(linkText));
            DriverExtensions.WaitForElement(By.LinkText(linkText)).Click();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// NewsLettter Button 
        /// </summary>
        public IButton NewsLettterButton  => new Button(NewsLetterLocator);

        /// <summary>
        /// Verify that link by text is displayed
        /// </summary>
        /// <param name="linkText"> Link text </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTextLinkDisplayed(string linkText) => DriverExtensions.IsDisplayed(By.LinkText(linkText));

        /// <summary>
        /// Close Current Tab and switch to default tab
        /// </summary> 
        /// <param name="tabName"> The tab Name. </param>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> New Page object </returns>
        public T CloseCurrentAndSwitchToDefaultTab<T>(string tabName) where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            BrowserPool.CurrentBrowser.CloseTab(tabName);
            //DriverExtensions.WaitForTabClosed(browserTabsCount);
            BrowserPool.CurrentBrowser.ActivateTab(BrowserPool.CurrentBrowser.TabNames.First());
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Method which checks if text present on Page
        /// </summary>
        /// <param name="text"> text to be found </param>
        /// <returns> The result </returns>
        public bool IsTextPresented(string text) => DriverExtensions.IsTextOnPage(text);

        /// <summary>
        /// The click and open new browser tab.
        /// </summary>
        /// <param name="elementLocator">
        /// The element locator.
        /// </param>
        /// <param name="newTabName">
        /// The new tab name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        internal T ClickAndOpenNewBrowserTab<T>(By elementLocator, string newTabName) where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            DriverExtensions.WaitForElement(elementLocator).Click();
            DriverExtensions.WaitForCondition(a => BrowserPool.CurrentBrowser.TabHandles.Count > browserTabsCount);
            return this.CreateNewTabPageObject<T>(browserTabsCount, newTabName);
        }

        /// <summary>
        /// The click and open new browser tab.
        /// </summary>
        /// <param name="element"> The element locator. </param>
        /// <param name="newTabName"> The new tab name. </param>
        /// <typeparam name="T"> Type </typeparam>
        /// <returns> New instance </returns>
        internal T ClickAndOpenNewBrowserTab<T>(IWebElement element, string newTabName) where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            DriverExtensions.Click(element);
            return this.CreateNewTabPageObject<T>(browserTabsCount, newTabName);
        }

        /// <summary>
        /// The click and open new browser tab
        /// </summary>
        /// <param name="button"> The element locator. </param>
        /// <param name="newTabName"> The new tab name. </param>
        /// <typeparam name="T"> Type </typeparam>
        /// <returns> New instance </returns>
        public T ClickAndOpenNewBrowserTab<T>(IButton button, string newTabName)
            where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            button.Click();
            return this.CreateNewTabPageObject<T>(browserTabsCount, newTabName);
        }

        /// <summary>
        /// Opens a new browser tab with specified url and notifies the driver.
        /// </summary>
        /// <param name="newTabName"> The new tab name. </param>
        /// <param name="url">The url to be navigated</param>
        /// <typeparam name="T"> Type </typeparam>
        /// <returns> New instance </returns>
        public T OpenNewTabUsingJavascript<T>(string newTabName, string url) where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            DriverExtensions.ExecuteScript($"window.open('{url}','{newTabName}');");
            return this.CreateNewTabPageObject<T>(browserTabsCount, newTabName);
        }

        /// <summary>
        /// Creating new browser tab page instance.
        /// </summary>
        /// <param name="browserTabsCount"> The tabs count </param>
        /// <param name="newTabName"> The new tab name. </param>
        /// <typeparam name="T"> Type </typeparam>
        /// <returns> New instance </returns>
        private T CreateNewTabPageObject<T>(int browserTabsCount, string newTabName) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            BrowserPool.CurrentBrowser.CreateTab(newTabName);
            BrowserPool.CurrentBrowser.ActivateTab(newTabName);
            DriverExtensions.WaitForNewTabLoaded(browserTabsCount);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}