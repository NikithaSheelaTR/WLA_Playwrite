using System;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium
{
    public interface ISearchContext
    {
        IWebElement FindElement(By by);
        ReadOnlyCollection<IWebElement> FindElements(By by);
    }
}
