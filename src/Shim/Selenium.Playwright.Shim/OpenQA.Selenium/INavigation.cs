using System;

namespace OpenQA.Selenium
{
    public interface INavigation
    {
        void Back();
        void Forward();
        void GoToUrl(string url);
        void GoToUrl(Uri url);
        void Refresh();
    }
}
