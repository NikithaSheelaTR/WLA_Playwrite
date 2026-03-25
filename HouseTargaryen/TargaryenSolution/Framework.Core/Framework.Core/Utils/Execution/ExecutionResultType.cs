namespace Framework.Core.Utils.Execution
{
    /// <summary>
    /// An enumeration to represent different types of execution results.
    /// </summary>
    public enum ExecutionResultType
    {
        /// <summary>
        /// Execution failed.
        /// </summary>
        Failure = -1,

        /// <summary>
        /// Execution succeeded.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Execution has not been performed yet. Default value.
        /// </summary>
        Inconclusive = 1
    }
}