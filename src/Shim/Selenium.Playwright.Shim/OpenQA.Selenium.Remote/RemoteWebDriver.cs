using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Remote
{
    /// <summary>
    /// Stub for RemoteWebDriver. Allows compilation of code referencing remote driver creation.
    /// </summary>
    public class RemoteWebDriver : IWebDriver, IJavaScriptExecutor
    {
        protected RemoteWebDriver() { }

        public RemoteWebDriver(Uri remoteAddress, DriverOptions options)
        {
            throw new NotSupportedException("RemoteWebDriver is not supported in the Playwright shim. Use PlaywrightWebDriver instead.");
        }

        public virtual string Url { get; set; }
        public virtual string Title => string.Empty;
        public virtual string PageSource => string.Empty;
        public virtual string CurrentWindowHandle => "0";
        public virtual ReadOnlyCollection<string> WindowHandles => new ReadOnlyCollection<string>(new List<string>());

        public virtual IWebElement FindElement(By by) => throw new NotSupportedException();
        public virtual ReadOnlyCollection<IWebElement> FindElements(By by) => new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
        public virtual void Close() { }
        public virtual void Quit() { }
        public virtual IOptions Manage() => throw new NotSupportedException();
        public virtual INavigation Navigate() => throw new NotSupportedException();
        public virtual ITargetLocator SwitchTo() => throw new NotSupportedException();
        public virtual object ExecuteScript(string script, params object[] args) => throw new NotSupportedException();
        public virtual object ExecuteAsyncScript(string script, params object[] args) => throw new NotSupportedException();

        public virtual void Dispose() { }
    }

    /// <summary>
    /// Base class for driver options (capabilities).
    /// </summary>
    public abstract class DriverOptions
    {
        public virtual void AddAdditionalCapability(string capabilityName, object capabilityValue) { }
    }
}
