using System;
using System.Threading;

namespace OpenQA.Selenium.Support.UI
{
    public class WebDriverWait
    {
        private readonly IWebDriver _driver;
        private TimeSpan _timeout;
        private TimeSpan _pollingInterval = TimeSpan.FromMilliseconds(500);
        private string _message = string.Empty;

        public WebDriverWait(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _timeout = timeout;
        }

        public TimeSpan Timeout
        {
            get => _timeout;
            set => _timeout = value;
        }

        public TimeSpan PollingInterval
        {
            get => _pollingInterval;
            set => _pollingInterval = value;
        }

        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public TResult Until<TResult>(Func<IWebDriver, TResult> condition)
        {
            var endTime = DateTime.Now.Add(_timeout);
            Exception lastException = null;

            while (DateTime.Now < endTime)
            {
                try
                {
                    var result = condition(_driver);

                    if (result is bool boolResult)
                    {
                        if (boolResult) return result;
                    }
                    else if (result != null)
                    {
                        return result;
                    }
                }
                catch (NoSuchElementException ex)
                {
                    lastException = ex;
                }
                catch (StaleElementReferenceException ex)
                {
                    lastException = ex;
                }
                catch (WebDriverException ex)
                {
                    lastException = ex;
                }

                Thread.Sleep(_pollingInterval);
            }

            var timeoutMessage = string.IsNullOrEmpty(_message)
                ? $"Timed out after {_timeout.TotalSeconds} seconds"
                : _message;

            throw new WebDriverTimeoutException(timeoutMessage, lastException);
        }
    }
}
