namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// Pages which are involved into environment stability check: CommonRoutingPage, CommonSignOnPage, CommonClientIdPage
    /// </summary>
    public interface IEnvironmentCheckPage
    {
        /// <summary>
        /// Checks if there is any error on the page
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsEnvironmentErrorsOnPage();
    }
}