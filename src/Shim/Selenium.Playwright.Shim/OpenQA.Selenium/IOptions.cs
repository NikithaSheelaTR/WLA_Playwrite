namespace OpenQA.Selenium
{
    public interface IOptions
    {
        ICookieJar Cookies { get; }
        ITimeouts Timeouts();
        IWindow Window { get; }
    }
}
