using System;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium.Playwright.Shim.Impl
{
    internal static class SyncHelper
    {
        public static TResult RunSync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            // Use a dedicated thread with no SynchronizationContext to avoid
            // deadlocks when called from vstest/MSTest adapter threads.
            TResult result = default;
            Exception caught = null;
            var thread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(null);
                try
                {
                    result = asyncFunc().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    caught = ex;
                }
            });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            if (caught != null)
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(caught).Throw();
            return result;
        }

        public static void RunSync(Func<Task> asyncFunc)
        {
            Exception caught = null;
            var thread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(null);
                try
                {
                    asyncFunc().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    caught = ex;
                }
            });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            if (caught != null)
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(caught).Throw();
        }
    }
}
