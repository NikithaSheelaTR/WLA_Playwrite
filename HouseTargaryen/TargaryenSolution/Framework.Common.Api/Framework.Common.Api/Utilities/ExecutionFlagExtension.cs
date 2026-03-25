namespace Framework.Common.Api.Utilities
{
    using Framework.Common.Api.DataModel;

    /// <summary>
    /// The execution flags extension (API)
    /// </summary>
    public static class ExecutionFlagExtension
    {
        /// <summary>
        /// Check that the execution flags variable has a flag from the dependency map (collection of related flags)
        /// </summary>
        /// <param name="flags">
        /// The original execution flags variable
        /// </param>
        /// <param name="flagDependencyMapToCheck">
        /// The flag from the dependency map (collection of related flags) for checking.
        /// </param>
        /// <returns>
        /// True if original enumeration has all flag associated with the flag from dependency map <see cref="bool"/>.
        /// </returns>
        internal static bool IsFlagDependencyMet(
            this ApiExecutionFlags flags,
            ApiFlagDependencyMap flagDependencyMapToCheck) => flags.HasFlag((ApiExecutionFlags)flagDependencyMapToCheck);
    }
}