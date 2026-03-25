namespace Framework.Common.UI.Utils
{
    using Framework.Common.UI.DataModel;

    /// <summary>
    /// The execution flags extension.
    /// </summary>
    public static class ExecutionFlagsExtension
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
            this UiExecutionFlags flags,
            UiFlagDependencyMap flagDependencyMapToCheck)
        {
            return flags.HasFlag((UiExecutionFlags)flagDependencyMapToCheck);
        }
    }
}