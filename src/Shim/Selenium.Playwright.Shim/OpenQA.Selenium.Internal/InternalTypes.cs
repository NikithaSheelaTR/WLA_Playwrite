namespace OpenQA.Selenium.Internal
{
    /// <summary>
    /// Stub for OpenQA.Selenium.Internal namespace.
    /// Provides IWrapsDriver interface used in some framework files.
    /// </summary>
    public interface IWrapsDriver
    {
        IWebDriver WrappedDriver { get; }
    }

    public interface IWrapsElement
    {
        IWebElement WrappedElement { get; }
    }
}
