namespace Framework.Core.Utils.Execution
{
    using System;
    using System.Threading;
    using System.Diagnostics;

    /// <summary>
    /// Provides for safe execution of blocks of managed code.
    /// </summary>
    public static class SafeMethodExecutor
    {
        /// <summary>
        /// Executes a method and returns the result of operation.
        /// </summary>
        /// <param name="method">A void-return delegate.</param>
        /// <returns>The result of a method invocation.</returns>
        public static ExecutionResult Execute(Action method)
        {
            var result = new ExecutionResult();

            try
            {
                method();
                result.ResultType = ExecutionResultType.Success;
            }
            catch (Exception e)
            {
                result.ResultType = ExecutionResultType.Failure;
                result.Details = e;
            }

            return result;
        }

        /// <summary>
        /// Executes a method that returns a value and returns the result of operation.
        /// </summary>
        /// <param name="method">A value-return delegate.</param>
        /// <param name="returnedValue">A value returned by the method.</param>
        /// <typeparam name="T">The type of the value returned by the method.</typeparam>
        /// <returns>The result of a method invocation.</returns>
        public static ExecutionResult Execute<T>(Func<T> method, out T returnedValue)
        {
            T value = default(T);
            ExecutionResult result = SafeMethodExecutor.Execute(() => { value = method(); });

            returnedValue = value;
            return result;
        }

        /// <summary>
        ///  Execute with wait
        ///  </summary>
        ///  <param name="method">Execution method</param>
        /// <param name="numOfAttempts">The number of attempts.</param>
        /// <param name="timeoutFromSec">The timeout between condition checks</param>
        /// <returns></returns>
        public static bool ExecuteUntil(Func<bool> method, int numOfAttempts = 15, int timeoutFromSec = 1)
        {
            bool isConditionReached = false;
            for (int i = 0; i < numOfAttempts; i++)
            {
                SafeMethodExecutor.Execute<bool>(method, out isConditionReached);
                if (!isConditionReached)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(timeoutFromSec));
                }
            }
            return isConditionReached;
        }

        /// <summary>
        /// Execute with wait
        /// </summary>
        /// <param name="method">Execution method</param>
        /// <param name="timeoutFromSec">The timeout between condition checks</param>
        /// <param name="pollingIntervalInMilliseconds">Polling interval</param>
        public static void WaitUntil(Func<bool> method, int timeoutFromSec = 2000, int pollingIntervalInMilliseconds = 400)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool isConditionMet = false;

            while (stopwatch.Elapsed.TotalSeconds < timeoutFromSec && !isConditionMet)
            {
                isConditionMet = method();

                if (!isConditionMet)
                {
                    Thread.Sleep(pollingIntervalInMilliseconds);
                }
            }

            stopwatch.Stop();
        }
    }
}