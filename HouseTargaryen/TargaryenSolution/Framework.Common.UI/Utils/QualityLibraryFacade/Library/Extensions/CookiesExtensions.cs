namespace Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions
{
    using System.Collections.Generic;
    using System.Net;

    using Cookie = OpenQA.Selenium.Cookie;

    /// <summary>
    /// <see cref="IEnumerable{T}"/> Extensions.
    /// </summary>
    public static class CookiesExtensions
    {
        /// <summary>
        /// Gets the <see cref="CookieContainer"/> object from <see cref="IEnumerable{Cookie}"/> source.
        /// </summary>
        /// <param name="sourseCookies"> The cookies source. </param>
        /// <returns> Returns the <see cref="CookieContainer"/> object. </returns>
        public static CookieContainer GetCookieContainerFromCookies(this IEnumerable<Cookie> sourseCookies)
        {
            var sessionCookies = new CookieContainer();
            foreach (Cookie cookie in sourseCookies)
            {
                sessionCookies.Add(new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
            }

            return sessionCookies;
        }
    }
}