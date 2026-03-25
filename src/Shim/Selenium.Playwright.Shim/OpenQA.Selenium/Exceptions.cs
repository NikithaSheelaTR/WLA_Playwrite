using System;

namespace OpenQA.Selenium
{
    public class WebDriverException : Exception
    {
        public WebDriverException() : base() { }
        public WebDriverException(string message) : base(message) { }
        public WebDriverException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NoSuchElementException : WebDriverException
    {
        public NoSuchElementException() : base() { }
        public NoSuchElementException(string message) : base(message) { }
        public NoSuchElementException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NoSuchFrameException : WebDriverException
    {
        public NoSuchFrameException() : base() { }
        public NoSuchFrameException(string message) : base(message) { }
        public NoSuchFrameException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NoSuchWindowException : WebDriverException
    {
        public NoSuchWindowException() : base() { }
        public NoSuchWindowException(string message) : base(message) { }
        public NoSuchWindowException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class StaleElementReferenceException : WebDriverException
    {
        public StaleElementReferenceException() : base() { }
        public StaleElementReferenceException(string message) : base(message) { }
        public StaleElementReferenceException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ElementNotVisibleException : WebDriverException
    {
        public ElementNotVisibleException() : base() { }
        public ElementNotVisibleException(string message) : base(message) { }
        public ElementNotVisibleException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class WebDriverTimeoutException : WebDriverException
    {
        public WebDriverTimeoutException() : base() { }
        public WebDriverTimeoutException(string message) : base(message) { }
        public WebDriverTimeoutException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidSelectorException : WebDriverException
    {
        public InvalidSelectorException() : base() { }
        public InvalidSelectorException(string message) : base(message) { }
        public InvalidSelectorException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NoAlertPresentException : WebDriverException
    {
        public NoAlertPresentException() : base() { }
        public NoAlertPresentException(string message) : base(message) { }
        public NoAlertPresentException(string message, Exception innerException) : base(message, innerException) { }
    }
}
