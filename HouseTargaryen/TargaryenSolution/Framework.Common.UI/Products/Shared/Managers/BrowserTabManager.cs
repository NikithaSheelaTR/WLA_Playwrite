namespace Framework.Common.UI.Products.Shared.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Utility methods to manage browser tabs.
    /// </summary>
    public class BrowserTabManager
    {
        private static BrowserTabManager instance;
        
        private readonly Dictionary<string, string> tabsByTags = new Dictionary<string, string>();

        private BrowserTabManager()
        {
        }

        /// <summary>
        /// Return BrowserTabManager's instance
        /// </summary>
        /// <returns> The <see cref="BrowserTabManager"/>. </returns>
        public static BrowserTabManager Instance => instance ?? (instance = new BrowserTabManager());

        /// <summary>
        /// Set Current Tab Name
        /// </summary>
        /// <param name="tabName">The tab Name.</param>
        public void SetCurrentTabName(string tabName)
        {
            if (string.IsNullOrWhiteSpace(tabName))
            {
                throw new ArgumentException("Tag is not valid");
            }

            if (this.tabsByTags.ContainsKey(tabName))
            {
                throw new ArgumentException("Tag is not unique");
            }

            this.tabsByTags.Add(tabName, BrowserPool.CurrentBrowser.Driver.CurrentWindowHandle);
        }

        /// <summary>
        /// Set Tab Active by tabName
        /// </summary>
        /// <param name="tabName">The tab Name.</param>
        public void SetTabActive(string tabName)
        {
            if (string.IsNullOrWhiteSpace(tabName))
            {
                throw new ArgumentException("Tag is not valid");
            }

            if (!this.tabsByTags.ContainsKey(tabName))
            {
                throw new InvalidOperationException($"Tag '{tabName}' has not been registered.");
            }

            this.SwitchTab(this.tabsByTags[tabName]);
        }

        /// <summary>
        /// Opens a new browser tab and notifies the driver.
        /// </summary>
        /// <param name="tabName">The tab Name.</param>
        public void OpenUrlInNewTab(string tabName) => this.OpenUrlInNewTab(tabName, BrowserPool.CurrentBrowser.Url);

        /// <summary>
        /// Opens a new browser tab and notifies the driver.
        /// </summary>
        /// <param name="tabName">The tab Name.</param>
        /// <param name="urlToOpen">The url To Open.</param>
        public void OpenUrlInNewTab(string tabName, string urlToOpen)
        {
            ReadOnlyCollection<string> tabHandlesSnapshot = BrowserPool.CurrentBrowser.TabHandles;

            // Send a command to a browser to create a new tab
            DriverExtensions.ExecuteScript(@"window.open()");
            DriverExtensions.WaitForNewTabLoaded(tabHandlesSnapshot.Count);

            // Keep driver in sync with the browser:
            // Get the new tab handle 
            string newTabHandle = BrowserPool.CurrentBrowser
                                             .TabHandles
                                             .Except(tabHandlesSnapshot)
                                             .Single();

            // Switch to  a new tab -> updates <see cref="IWebDriver.CurrentWindowHandle"/>
            this.SwitchTab(newTabHandle);

            // Set a friendly name for a new tab
            this.SetCurrentTabName(tabName);

            // Optionally - open an URL
            if (!string.IsNullOrWhiteSpace(urlToOpen))
            {
                BrowserPool.CurrentBrowser.GoToUrl(urlToOpen);
            }
        }

        /// <summary>
        /// Switches the browser tabs to a one with the given <paramref name="tabHandle"/>.
        /// </summary>
        private void SwitchTab(string tabHandle)
        {
            if (!BrowserPool.CurrentBrowser.TabHandles.Contains(tabHandle))
            {
                throw new ArgumentException("tabHandle is not valid");
            }

            BrowserPool.CurrentBrowser.Driver
                       .SwitchTo()
                       .Window(tabHandle);
        }
        /// <summary>
        /// CloseCurrentAndSwitchToDefaultTab
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="tabName">Tab name</param>
        /// <returns>T</returns>
        public T CloseCurrentAndSwitchToDefaultTab<T>(string tabName)
            where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            BrowserPool.CurrentBrowser.CloseTab(tabName);
            DriverExtensions.WaitForTabClosed(browserTabsCount);
            DriverExtensions.WaitForJavaScript();
            BrowserPool.CurrentBrowser.ActivateTab(BrowserPool.CurrentBrowser.TabNames.First());
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }
        /// <summary>
        /// Click and open new browser tab
        /// </summary>
        /// <param name="button">
        /// Element 
        /// </param>
        /// <param name="newTabName">
        /// New tab name
        /// </param>
        /// <typeparam name="T">
        /// Type
        /// </typeparam>
        
        public T ClickAndOpenNewBrowserTab<T>(IButton button, string newTabName)
            where T : ICreatablePageObject
        {
            int browserTabsCount = BrowserPool.CurrentBrowser.TabHandles.Count;
            button.Click();
            return this.OpenNewBrowserTab<T>(newTabName, browserTabsCount);
        }

        /// <summary>
        /// Opens a new browser tab and notifies the driver.
        /// </summary>
        private T OpenNewBrowserTab<T>(string newTabName, int browserTabsCount)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForJavaScript();
            BrowserPool.CurrentBrowser.CreateTab(newTabName);
            BrowserPool.CurrentBrowser.ActivateTab(newTabName);
            DriverExtensions.WaitForNewTabLoaded(browserTabsCount);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
