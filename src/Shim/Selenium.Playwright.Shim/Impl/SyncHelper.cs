using System;
using System.Threading.Tasks;

namespace Selenium.Playwright.Shim.Impl
{
    internal static class SyncHelper
    {
        public static TResult RunSync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            return Task.Run(asyncFunc).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static void RunSync(Func<Task> asyncFunc)
        {
            Task.Run(asyncFunc).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
