namespace OpenQA.Selenium
{
    public interface IAlert
    {
        string Text { get; }
        void Accept();
        void Dismiss();
        void SendKeys(string keysToSend);
    }
}
