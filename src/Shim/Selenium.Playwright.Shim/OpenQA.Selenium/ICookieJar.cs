using System.Collections.ObjectModel;

namespace OpenQA.Selenium
{
    public interface ICookieJar
    {
        ReadOnlyCollection<Cookie> AllCookies { get; }
        void AddCookie(Cookie cookie);
        Cookie GetCookieNamed(string name);
        void DeleteCookie(Cookie cookie);
        void DeleteCookieNamed(string name);
        void DeleteAllCookies();
    }
}
