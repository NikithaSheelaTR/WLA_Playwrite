using System.Drawing;

namespace OpenQA.Selenium
{
    public interface IWebElement : ISearchContext
    {
        string TagName { get; }
        string Text { get; }
        bool Enabled { get; }
        bool Selected { get; }
        Point Location { get; }
        Size Size { get; }
        bool Displayed { get; }

        void Clear();
        void Click();
        string GetAttribute(string attributeName);
        string GetProperty(string propertyName);
        string GetCssValue(string propertyName);
        void SendKeys(string text);
        void Submit();
    }
}
