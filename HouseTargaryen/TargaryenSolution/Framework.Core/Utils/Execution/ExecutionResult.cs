namespace Framework.Core.Utils.Execution
{
    using System;

    /// <summary>
    /// Represents the result of execution of a block of code.
    /// </summary>
    public sealed class ExecutionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionResult"/> class. 
        /// </summary>
        public ExecutionResult()
        {
            this.ResultType = ExecutionResultType.Inconclusive;
            this.Details = null;
        }

        /// <summary>
        /// Contains details of an exception that led to errors in execution of code.
        /// </summary>
        public Exception Details { get; set; }

        /// <summary>
        /// The type of execution result.
        /// </summary>
        public ExecutionResultType ResultType { get; set; }

        /// <summary>
        /// Logs the exception details, if the result was unsuccessful.
        /// </summary>
        /// <param name="logMethod">The handler to use in order to log the message. If omitted or NULL, Logger.LogError will be used.
        /// </param>
        public void LogDetails(Action<string> logMethod = null)
        {
            if (this.Details != null)
            {
                string message = this.Details.ToString();

                if (logMethod == null)
                {
                    Logger.LogError(message);
                }
                else
                {
                    logMethod(message);
                }
            }
        }
    }
}