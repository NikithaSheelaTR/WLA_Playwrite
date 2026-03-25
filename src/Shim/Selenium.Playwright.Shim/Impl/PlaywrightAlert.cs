using System;
using System.Threading;
using Microsoft.Playwright;
using OpenQA.Selenium;

namespace Selenium.Playwright.Shim.Impl
{
    internal class PlaywrightAlert : IAlert
    {
        private readonly PlaywrightWebDriver _driver;
        private IDialog _dialog;
        private readonly ManualResetEventSlim _dialogReceived = new ManualResetEventSlim(false);

        public PlaywrightAlert(PlaywrightWebDriver driver)
        {
            _driver = driver;

            // Register dialog handler
            _driver.Page.Dialog += OnDialog;

            // Wait briefly for dialog (may already be present)
            if (!_dialogReceived.Wait(TimeSpan.FromSeconds(5)))
            {
                _driver.Page.Dialog -= OnDialog;
                throw new NoAlertPresentException("No alert is present");
            }
        }

        private void OnDialog(object sender, IDialog dialog)
        {
            _dialog = dialog;
            _dialogReceived.Set();
        }

        public string Text => _dialog?.Message ?? string.Empty;

        public void Accept()
        {
            if (_dialog != null)
            {
                SyncHelper.RunSync(() => _dialog.AcceptAsync());
                Cleanup();
            }
        }

        public void Dismiss()
        {
            if (_dialog != null)
            {
                SyncHelper.RunSync(() => _dialog.DismissAsync());
                Cleanup();
            }
        }

        public void SendKeys(string keysToSend)
        {
            if (_dialog != null)
            {
                SyncHelper.RunSync(() => _dialog.AcceptAsync(keysToSend));
                Cleanup();
            }
        }

        private void Cleanup()
        {
            _driver.Page.Dialog -= OnDialog;
            _dialog = null;
        }
    }
}
