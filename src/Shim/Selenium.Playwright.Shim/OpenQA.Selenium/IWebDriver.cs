using System;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium
{
    public interface IWebDriver : ISearchContext, IDisposable
    {
        string Url { get; set; }
        string Title { get; }
        string PageSource { get; }
        string CurrentWindowHandle { get; }
        ReadOnlyCollection<string> WindowHandles { get; }

        void Close();
        void Quit();
        IOptions Manage();
        INavigation Navigate();
        ITargetLocator SwitchTo();
    }
}
