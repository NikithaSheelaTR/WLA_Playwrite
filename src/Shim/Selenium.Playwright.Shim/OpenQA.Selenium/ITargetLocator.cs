namespace OpenQA.Selenium
{
    public interface ITargetLocator
    {
        IWebDriver Frame(int frameIndex);
        IWebDriver Frame(string frameName);
        IWebDriver Frame(IWebElement frameElement);
        IWebDriver ParentFrame();
        IWebDriver DefaultContent();
        IWebDriver Window(string windowName);
        IAlert Alert();
        IWebElement ActiveElement();
    }
}
