using System;

namespace OpenQA.Selenium
{
    public interface ITimeouts
    {
        TimeSpan ImplicitWait { get; set; }
        TimeSpan PageLoad { get; set; }
        TimeSpan AsynchronousJavaScript { get; set; }
    }
}
