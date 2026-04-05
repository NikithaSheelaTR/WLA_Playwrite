namespace Framework.Common.UI.Utils.Browser
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Represents a pool of available Browser objects and provides a unified means of working with a Browser abstraction.
    /// </summary>
    public sealed class BrowserPool
    {
        /// <summary>
        /// The default browser reference name.
        /// </summary>
        public const string DefaultBrowserName = "default";

        private static readonly object Locker = new object();

        /// <summary>
        /// Log diagnostic messages for browser pool operations.
        /// </summary>
        private static void LogDebug(string message)
        {
            try
            {
                var line = $"[BrowserPool {DateTime.Now:HH:mm:ss.fff}] Thread {Thread.CurrentThread.ManagedThreadId}: {message}";
                Console.WriteLine(line);
                System.Diagnostics.Debug.WriteLine(line);
            }
            catch { /* best effort */ }
        }

        /// <summary>
        /// Save browserName and corresponding Browser object as thread safe dictionary(browserName_threadId, Browser)
        /// </summary>
        private static readonly ConcurrentDictionary<string, Browser> BrowsersDictionary = new ConcurrentDictionary<string, Browser>();

        /// <summary>
        /// Save Thread Id and active browserName for this thread
        /// </summary>
        private static readonly ConcurrentDictionary<int, string> ThreadActiveBrowser = new ConcurrentDictionary<int, string>();

        /// <summary>
        /// Get the reference names of all active browser instances.
        /// </summary>
        public static List<string> BrowserNames
        {
            get
            {
                BrowserPool.RemoveDisposedInstances();
                return BrowsersDictionary.Keys.ToList();
            }
        }

        /// <summary>
        /// Get the current browser instance for the specific thread.
        /// If no browser is registered for the current thread (e.g. MSTest switches threads
        /// between [TestInitialize] and [TestMethod]), auto-adopts an active browser from
        /// another thread and registers it for the current thread.
        /// </summary>
        public static Browser CurrentBrowser
        {
            get
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                LogDebug($"CurrentBrowser requested. ThreadActiveBrowser count={ThreadActiveBrowser.Count}, BrowsersDictionary count={BrowsersDictionary.Count}");
                LogDebug($"ThreadActiveBrowser keys: [{string.Join(", ", ThreadActiveBrowser.Keys)}]");
                LogDebug($"BrowsersDictionary keys: [{string.Join(", ", BrowsersDictionary.Keys)}]");

                if (!ThreadActiveBrowser.TryGetValue(currentThreadId, out string browserName))
                {
                    LogDebug($"No browser registered for current thread {currentThreadId}. Attempting to adopt from another thread...");

                    // MSTest can run [TestInitialize] and [TestMethod] on different threads.
                    // Search for a non-disposed browser registered on another thread and adopt it.
                    lock (Locker)
                    {
                        // Re-check inside the lock in case another thread just registered.
                        if (!ThreadActiveBrowser.TryGetValue(currentThreadId, out browserName))
                        {
                            foreach (var kvp in ThreadActiveBrowser)
                            {
                                string candidateKey = $"{kvp.Value}_{kvp.Key}";
                                LogDebug($"Checking candidate: key='{candidateKey}', threadId={kvp.Key}, browserName='{kvp.Value}'");

                                if (BrowsersDictionary.TryGetValue(candidateKey, out Browser candidate))
                                {
                                    LogDebug($"Found candidate browser. IsDisposed={candidate.IsDisposed}");
                                    if (!candidate.IsDisposed)
                                    {
                                        browserName = kvp.Value;
                                        ThreadActiveBrowser[currentThreadId] = browserName;
                                        BrowsersDictionary.TryAdd($"{browserName}_{currentThreadId}", candidate);
                                        LogDebug($"Successfully adopted browser '{browserName}' from thread {kvp.Key} to thread {currentThreadId}");
                                        break;
                                    }
                                }
                                else
                                {
                                    LogDebug($"Candidate key '{candidateKey}' not found in BrowsersDictionary");
                                }
                            }
                        }
                    }

                    if (browserName == null)
                    {
                        // Log detailed state before throwing
                        LogDebug($"FAILURE: No browser could be adopted. Dumping full state:");
                        LogDebug($"  ThreadActiveBrowser entries:");
                        foreach (var kvp in ThreadActiveBrowser)
                        {
                            LogDebug($"    ThreadId={kvp.Key}, BrowserName='{kvp.Value}'");
                        }
                        LogDebug($"  BrowsersDictionary entries:");
                        foreach (var kvp in BrowsersDictionary)
                        {
                            LogDebug($"    Key='{kvp.Key}', IsDisposed={kvp.Value?.IsDisposed}");
                        }

                        throw new KeyNotFoundException(
                            $"No browser registered for thread {currentThreadId} and no active browser found on any other thread. " +
                            $"ThreadActiveBrowser has {ThreadActiveBrowser.Count} entries, BrowsersDictionary has {BrowsersDictionary.Count} entries. " +
                            $"This typically means InitializeTestClient() failed or was not called before accessing CurrentBrowser.");
                    }
                }

                var resultKey = BrowserPool.GetBrowserNameWithThreadId(browserName);
                if (!BrowsersDictionary.TryGetValue(resultKey, out Browser result))
                {
                    LogDebug($"FAILURE: Browser key '{resultKey}' not found in BrowsersDictionary after adoption");
                    throw new KeyNotFoundException($"Browser with key '{resultKey}' not found in pool. Browser may have been disposed.");
                }

                LogDebug($"Returning browser for key '{resultKey}'");
                return result;
            }
        }

        /// <summary>
        /// Get the browser instance by the thread id
        /// </summary>
        /// <param name="threadId">ThreadId</param>
        /// <returns>The <see cref="Browser"/>.</returns>
        public static Browser GetBrowserByThreadId(int threadId) 
            => BrowsersDictionary[$"{ThreadActiveBrowser[threadId]}_{threadId}"];

        /// <summary>
        /// Dispose of a browser instance
        /// Remove corresponding key value pair from the BrowsersDictionary and ThreadActiveBrowser
        /// </summary>
        /// <param name="browserName">The reference name of a browser.</param>
        public static void DisposeOfBrowser(string browserName)
        {
            Browser browser;

            BrowsersDictionary.TryGetValue(BrowserPool.GetBrowserNameWithThreadId(browserName), out browser);
            browser?.Dispose();

            BrowserPool.RemoveDisposedInstances();

            // remove the active browser name from the dictionary(threadId, browserName)
            ThreadActiveBrowser.TryRemove(Thread.CurrentThread.ManagedThreadId, out browserName);
        }

        /// <summary>
        /// Dispose all browsers for the current thread in the pool.
        /// </summary>
        public static void DisposeOfBrowsers()
        {
            string browserName;

            lock (Locker)
            {
                BrowsersDictionary.Where(pair => pair.Key.Contains(Thread.CurrentThread.ManagedThreadId.ToString()))
                                  .Select(pair => pair.Value).ToList().ForEach(value => value.Dispose());
            }

            ThreadActiveBrowser.TryRemove(Thread.CurrentThread.ManagedThreadId, out browserName);

            BrowserPool.RemoveDisposedInstances();
        }

        /// <summary>
        /// Change the context by switching the current browser.
        /// </summary>
        /// <param name="browserName">The reference name of a browser.</param>
        public static void MakeCurrentBrowser(string browserName)
        {
            LogDebug($"MakeCurrentBrowser called: browserName='{browserName}'");
            BrowserPool.RemoveDisposedInstances();

            lock (Locker)
            {
                var fullKey = BrowserPool.GetBrowserNameWithThreadId(browserName);
                if (BrowsersDictionary.ContainsKey(fullKey))
                {
                    ThreadActiveBrowser.AddOrUpdate(Thread.CurrentThread.ManagedThreadId, browserName, (key, oldValue) => browserName);
                    LogDebug($"MakeCurrentBrowser: Thread {Thread.CurrentThread.ManagedThreadId} now uses browser '{browserName}'");
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(browserName), $"No browser with reference name '{browserName}' is available");
                }
            }
        }

        /// <summary>
        /// Only register a new instance of Browser with a valid reference name, if no browser with such a name exists in the pool, and makes it current.
        /// </summary>
        /// <param name="browserName">The reference name of a browser.</param>
        /// <param name="newBrowser">The Browser object to register in the pool.</param>
        public static void RegisterAndMakeCurrentBrowser(string browserName, Browser newBrowser)
        {
            LogDebug($"RegisterAndMakeCurrentBrowser called: browserName='{browserName}', newBrowser is null={newBrowser == null}");
            BrowserPool.RegisterBrowser(browserName, newBrowser);
            BrowserPool.MakeCurrentBrowser(browserName);
            LogDebug($"RegisterAndMakeCurrentBrowser completed successfully for '{browserName}'");
        }

        /// <summary>
        /// Only registers a new instance of Browser with the default reference name, if no browser with such a name exists in the pool, and makes it current.
        /// </summary>
        /// <param name="newBrowser">The Browser object to register in the pool.</param>
        public static void RegisterAndMakeCurrentBrowser(Browser newBrowser)
            => BrowserPool.RegisterAndMakeCurrentBrowser(DefaultBrowserName, newBrowser);

        /// <summary>
        /// Only register a new instance of Browser with a valid reference name, if no browser with such a name exists in the pool.
        /// </summary>
        /// <param name="browserName">The reference name of a browser.</param>
        /// <param name="newBrowser">The Browser object to register in the pool.</param>
        public static void RegisterBrowser(string browserName, Browser newBrowser)
        {
            LogDebug($"RegisterBrowser called: browserName='{browserName}'");

            if (string.IsNullOrEmpty(browserName))
            {
                throw new ArgumentException("A browser reference name must not be NULL or empty string.", nameof(browserName));
            }

            BrowserPool.RemoveDisposedInstances();

            var fullKey = BrowserPool.GetBrowserNameWithThreadId(browserName);
            LogDebug($"Attempting to add browser with key '{fullKey}'");

            if (!BrowsersDictionary.TryAdd(fullKey, newBrowser))
            {
                LogDebug($"FAILURE: Browser with key '{fullKey}' already exists");
                throw new InvalidOperationException("The browser with the same name already exists.");
            }

            LogDebug($"RegisterBrowser succeeded. BrowsersDictionary now has {BrowsersDictionary.Count} entries");
        }

        /// <summary>
        /// Return full browser name in the format of BrowserName_ThreadId to store it in the BrowserList dictionary
        /// </summary>
        /// <param name="browserName">browser name</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetBrowserNameWithThreadId(string browserName)
            => browserName.EndsWith($"_{Thread.CurrentThread.ManagedThreadId}")
                   ? browserName
                   : $"{browserName}_{Thread.CurrentThread.ManagedThreadId}";

        /// <summary>
        /// Remove all unusable Browser instances from the pool.
        /// </summary>
        private static void RemoveDisposedInstances()
        {
            lock (Locker)
            {
                IEnumerable<string> keys = BrowsersDictionary.Where(pair => pair.Value.IsDisposed).Select(pair => pair.Key);

                foreach (string key in keys)
                {
                    Browser value;
                    BrowsersDictionary.TryRemove(key, out value);
                }
            }
        }
    }
}